// Copyright (c) 2014 SIL International
// This software is licensed under the MIT License (http://opensource.org/licenses/MIT)
using System.Collections.Specialized;
using System.Text;
using SIL.IO;

namespace Bloom.Api
{
	public class PretendRequestInfo : IRequestInfo
	{
		public HttpMethods HttpMethod { get; set; }

		public string ReplyContents;
		public string ReplyImagePath;
		//public HttpListenerContext Context; //todo: could we mock a context and then all but do away with this pretend class by subclassing the real one?
		public long StatusCode;
		public string StatusDescription;

		public PretendRequestInfo(string url, HttpMethods httpMethod = HttpMethods.Get, bool forPrinting = false, bool forSrcAttr = false)
		{
			HttpMethod = httpMethod;
			if (forPrinting)
				url = url.Replace("/bloom/", "/bloom/OriginalImages/");

			// In the real request, RawUrl does not include this prefix
			RawUrl = url.Replace(ServerBase.ServerUrl, "");

			// Reducing the /// emulates a behavior of the real HttpListener
			var urlToDecode = url.Replace(ServerBase.ServerUrl, "").Replace("/bloom/OriginalImages///", "/bloom/OriginalImages/").Replace("/bloom///", "/bloom/").UnescapeCharsForHttp();
			// Use the same decoding logic as in the "real" RequestInfo.
			var pathWithoutLiteralPlusSigns = urlToDecode.Replace("+","%2B");
			LocalPathWithoutQuery = System.Web.HttpUtility.UrlDecode(pathWithoutLiteralPlusSigns);

		}

		public string LocalPathWithoutQuery { get; set; }

		public string ContentType { private get; set; }

		/// <summary>
		/// wrap so that it is easily consumed by our standard xml unit test stuff, which can't handled fragments
		/// </summary>
		public string ReplyContentsAsXml
		{
			get { return "<root>" + ReplyContents + "</root>"; }
		}

		// get is required to fulfil interface contract. Not currently used in tests.
		// set is required to satisfy (Team City version of) compiler for a valid auto-implemented property.
		public bool HaveOutput { get; set; }

		public void WriteCompleteOutput(string s)
		{
			var buffer = Encoding.UTF8.GetBytes(s);
			ReplyContents = Encoding.UTF8.GetString(buffer);
			HaveOutput = true;
		}

		public void ReplyWithFileContent(string path, string originalPath = null)
		{
			ReplyImagePath = path;
			WriteCompleteOutput(RobustFile.ReadAllText(path));
			HaveOutput = true;
		}

		public void ReplyWithImage(string path, string originalPath = null)
		{
			ReplyImagePath = path;
		}

		public void WriteError(int errorCode, string errorDescription)
		{
			StatusCode = errorCode;
			StatusDescription = errorDescription;
			HaveOutput = true;
		}

		public void WriteError(int errorCode)
		{
			StatusCode = errorCode;
		}

		public NameValueCollection GetQueryParameters()
		{
			return new NameValueCollection();
		}

		public NameValueCollection GetPostDataWhenFormEncoded()
		{
			return new NameValueCollection();
		}

		public string GetPostJson()
		{
			return "";
		}
		public string GetPostString()
		{
			return "";
		}
		public void ExternalLinkSucceeded(){}
		public string DoNotCacheFolder { get; set; }

		public string RawUrl { get; private set; }
	}
}
