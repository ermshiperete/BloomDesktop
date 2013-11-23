﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using BloomTemp;

namespace Bloom.WebLibraryIntegration
{
	/// <summary>
	/// Handles Bloom file/folder operations on Amazon Web Services S3 service.
	/// </summary>
	public class BloomS3Client:IDisposable
	{
		private IAmazonS3 _amazonS3;
		private TransferUtility _transferUtility;
		private readonly string _bucketName;
		public const string kDirectoryDelimeterForS3 = "/";
		public const string UnitTestBucketName = "BloomLibraryBooks-UnitTests";
		public const string SandboxBucketName = "BloomLibraryBooks-Sandbox";
		public const string ProductionBucketName = "BloomLibraryBooks-Production";

		public BloomS3Client(string bucketName)
		{
			_bucketName = bucketName;
			_amazonS3 = AWSClientFactory.CreateAmazonS3Client("AKIAJEKSYRFYYQQFJ6VQ",
				"8sMcTTUkA2GlqeJDDD9QZWRmYMmjVxrAckocnB5r", new AmazonS3Config { ServiceURL = "https://s3.amazonaws.com" });
			_transferUtility = new TransferUtility(_amazonS3);
		}

		public bool GetBookExists(string key)
		{
			var matchingFilesResponse = _amazonS3.ListObjects(new ListObjectsRequest()
			{
				BucketName = _bucketName,
				Prefix = key
			});
			return matchingFilesResponse.S3Objects.Count>0;
		}

		public int GetCountOfAllFilesInBucket()
		{
			var matchingFilesResponse = _amazonS3.ListObjects(new ListObjectsRequest()
			{
				BucketName = _bucketName
			});
			return matchingFilesResponse.S3Objects.Count;
		}


		public IEnumerable<string> GetFilePaths()
		{
			var matchingFilesResponse = _amazonS3.ListObjects(new ListObjectsRequest()
			{
				BucketName = _bucketName
			});
			return from x in matchingFilesResponse.S3Objects select x.Key;
		}


		public bool FileExists(params string[] parts)
		{
			var request = new ListObjectsRequest()
			{
				BucketName = _bucketName,
				Prefix = String.Join(kDirectoryDelimeterForS3,parts)
			};
			return _amazonS3.ListObjects(request).S3Objects.Count>0;
		}

		public void EmptyUnitTestBucket()
		{
			var matchingFilesResponse = _amazonS3.ListObjects(new ListObjectsRequest()
			{
				//NB: this one intentionally hard-codes the folder it can delete, to protect from accidents
				BucketName = UnitTestBucketName,
			});
			if (matchingFilesResponse.S3Objects.Count == 0)
				return;

			var deleteObjectsRequest = new DeleteObjectsRequest()
			{
				BucketName = UnitTestBucketName,
				Objects = matchingFilesResponse.S3Objects.Select(s3Object => new KeyVersion() {Key = s3Object.Key}).ToList()
			};

			var response = _amazonS3.DeleteObjects(deleteObjectsRequest);
			Debug.Assert(response.DeleteErrors.Count == 0);
		}

		/// <summary>
		/// The thing here is that we need to guarantee unique names at the top level, so we wrap the books inside a folder
		/// with some unique name
		/// </summary>
		/// <param name="storageKeyOfBookFolder"></param>
		/// <param name="pathToBloomBookDirectory"></param>
		public void UploadBook(string storageKeyOfBookFolder, string pathToBloomBookDirectory)
		{
			//first, let's copy to temp so that we don't have to worry about changes to the original while we're uploading,
			//and at the same time introduce a wrapper with the unique key for this person+book

			var wrapperPath = Path.Combine(Path.GetTempPath(), storageKeyOfBookFolder);
			Directory.CreateDirectory(wrapperPath);

			CopyDirectory(pathToBloomBookDirectory, Path.Combine(wrapperPath, Path.GetFileName(pathToBloomBookDirectory)));
			UploadDirectory(wrapperPath);

			Directory.Delete(wrapperPath, true);
		}


		/// <summary>
		/// THe weird thing here is that S3 doesn't really have folders, but you can give it a key like "collection/book2/file3.htm"
		/// and it will name it that, and gui client apps then treat that like a folder structure, so you feel like there are folders.
		/// </summary>
		private void UploadDirectory(string directoryPath)
		{
			UploadDirectory("", directoryPath);
		}

		private void UploadDirectory(string prefix, string directoryPath)
		{
			if (!Directory.Exists(directoryPath))
			{
				throw new DirectoryNotFoundException(
					"Source directory does not exist or could not be found: "
					+ directoryPath);
			}
			prefix = prefix + Path.GetFileName(directoryPath)+kDirectoryDelimeterForS3;

			foreach (string file in Directory.GetFiles(directoryPath))
			{
				var request = new UploadPartRequest()
				{
					BucketName = _bucketName,
					FilePath = file,
					IsLastPart = true,
					Key = prefix+ Path.GetFileName(file)
				};

				_amazonS3.UploadPart(request);
			}

			foreach (string subdir in Directory.GetDirectories(directoryPath))
			{
				UploadDirectory(prefix, subdir);
			}
		}

		/// <summary>
		/// copy directory and all subdirectories
		/// </summary>
		/// <param name="sourceDirName"></param>
		/// <param name="destDirName">Note, this is not the *parent*; this is the actual name you want, e.g. CopyDirectory("c:/foo", "c:/temp/foo") </param>
		private static void CopyDirectory(string sourceDirName, string destDirName)
		{
			var sourceDirectory = new DirectoryInfo(sourceDirName);

			if (!sourceDirectory.Exists)
			{
				throw new DirectoryNotFoundException(
					"Source directory does not exist or could not be found: "
					+ sourceDirName);
			}

			if (!Directory.Exists(destDirName))
			{
				Directory.CreateDirectory(destDirName);
			}

			foreach (FileInfo file in sourceDirectory.GetFiles())
			{
				file.CopyTo(Path.Combine(destDirName, file.Name), true);
			}

			foreach (DirectoryInfo subdir in sourceDirectory.GetDirectories())
			{
				CopyDirectory(subdir.FullName, Path.Combine(destDirName, subdir.Name));
			}
		}

		/// <summary>
		/// Warning, if the book already exists in the location, this is going to delete it an over-write it. So it's up to the caller to check the sanity of that.
		/// </summary>
		/// <param name="storageKeyOfBookFolder"></param>
		public void DownloadBook(string storageKeyOfBookFolder, string pathToDestinationParentDirectory)
		{
			//TODO tell it not to download pdfs. Those are just in there for previewing purposes, we don't need to get them now that we're getting the real thing

			var matchingFilesResponse = _amazonS3.ListObjects(new ListObjectsRequest()
			{
				BucketName = _bucketName,
				Delimiter = kDirectoryDelimeterForS3,
				Prefix = storageKeyOfBookFolder
			});

			foreach (var s3Object in matchingFilesResponse.S3Objects)
			{
				_amazonS3.BeginGetObject(new GetObjectRequest() {BucketName = _bucketName, Key = s3Object.Key},
					OnDownloadCallback, s3Object);
			}

			//review: should we instead save to a newly created folder so that we don't have to worry about the
			//other folder existing already? Todo: add a test for that first.

			if (!GetBookExists(storageKeyOfBookFolder))
				throw new DirectoryNotFoundException("The book we tried to download is no longer in the BloomLibrary");

			using (var tempDestination =
					new TemporaryFolder("BloomDownloadStaging " + storageKeyOfBookFolder + " " + Guid.NewGuid()))
			{
				var token = _transferUtility.BeginDownloadDirectory(_bucketName, storageKeyOfBookFolder,
					tempDestination.FolderPath, OnDownloadProgress, storageKeyOfBookFolder);

				_transferUtility.EndDownloadDirectory(token);

				//look inside the wrapper that we got

				var children = Directory.GetDirectories(tempDestination.FolderPath);
				if (children.Length != 1)
				{
					throw new ApplicationException(
						string.Format("Bloom expected to find a single directory in {0}, but instead there were {1}",
							tempDestination.FolderPath, children.Length));
				}
				var destinationPath = Path.Combine(pathToDestinationParentDirectory, Path.GetFileName(children[0]));

				//clear out anything exisitng on our target
				if (Directory.Exists(destinationPath))
				{
					Directory.Delete(destinationPath, true);
				}

				//if we're on the same volume, we can just move it. Else copy it.
				if (Directory.GetDirectoryRoot(pathToDestinationParentDirectory) == Directory.GetDirectoryRoot(tempDestination.FolderPath))
				{
					Directory.Move(children[0], destinationPath);
				}
				else
				{
					CopyDirectory(children[0], destinationPath);
				}
			}
		}

		private void OnDownloadCallback(IAsyncResult ar)
		{

		}


		private void OnDownloadProgress(IAsyncResult ar)
		{

		}

		public void Dispose()
		{
			if (_transferUtility != null)
			{
				_transferUtility.Dispose();
				_transferUtility = null;
			}
			if (_amazonS3 != null)
			{
				_amazonS3.Dispose();
				_amazonS3 = null;
			}
		}

	}
}
