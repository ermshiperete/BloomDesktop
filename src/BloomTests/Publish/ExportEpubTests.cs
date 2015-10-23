﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using Bloom;
using Bloom.Book;
using Bloom.Publish;
using BloomTemp;
using BloomTests.Book;
using ICSharpCode.SharpZipLib.Zip;
using NUnit.Framework;
using Palaso.Extensions;

namespace BloomTests.Publish
{
	[TestFixture]
	public class ExportEpubTests : BookTestsBase
	{
		private readonly XNamespace _xhtml = "http://www.w3.org/1999/xhtml";

		[Test]
		public void SaveEpub()
		{
			SetDom(@"<div class='bloom-page'>
						<div id='somewrapper'>
							<div id='test' class='bloom-translationGroup bloom-requiresParagraphs' lang=''>
								<div aria-describedby='qtip-1' class='bloom-editable' lang='en'>
									This is some text
								</div>
								<div lang = '*'>more text</div>
							</div>
							<div><img src='myImage.png'></img></div>
							<div><img src='my%20image.png'></img></div>
						</div>
					</div>",
						   @"<link rel='stylesheet' href='../settingsCollectionStyles.css'/>
							<link rel='stylesheet' href='../customCollectionStyles.css'/>
							<link rel='stylesheet' href='customBookStyles.css'/>");
			var book = CreateBook();

			CreateCommonCssFiles(book);

			// These two names are especially interesting because they differ by case and also white space.
			// The case difference is not important to the Windows file system.
			// The white space must be removed to make an XML ID.
			MakeSamplePngImageWithMetadata(book.FolderPath.CombineForPath("myImage.png"));
			MakeSamplePngImageWithMetadata(book.FolderPath.CombineForPath("my image.png"));
			var epubFolder = new TemporaryFolder();
			var epubName = "output.epub";
			var epubPath = Path.Combine(epubFolder.FolderPath, epubName);
			using (var maker = CreateEpubMaker(book))
				maker.SaveEpub(epubPath);
			Assert.That(File.Exists(epubPath));
			var zip = new ZipFile(epubPath);

			// Every epub must have a mimetype at the root
			GetZipContent(zip, "mimetype");

			// Every epub must have a "META-INF/container.xml." (case matters). Most things we could check about its content
			// would be redundant with the code that produces it, but we can at least verify that it is valid
			// XML and points us at the rootfile (open package format) file.
			var containerData = GetZipContent(zip, "META-INF/container.xml");
			var doc = XDocument.Parse(containerData);
			XNamespace ns = doc.Root.Attribute("xmlns").Value;
			var packageFile = doc.Root.Element(ns + "rootfiles").Element(ns + "rootfile").Attribute("full-path").Value;

			// That gives us a path to the main package file, typically content.opf
			var packageData = StripXmlHeader(GetZipContent(zip, packageFile));
			var toCheck = AssertThatXmlIn.String(packageData);
			var mgr = new XmlNamespaceManager(toCheck.NameTable);
			mgr.AddNamespace("dc", "http://purl.org/dc/elements/1.1/");
			mgr.AddNamespace("opf", "http://www.idpf.org/2007/opf");
			toCheck.HasAtLeastOneMatchForXpath("package[@version='3.0']");
			toCheck.HasAtLeastOneMatchForXpath("package[@unique-identifier]");
			toCheck.HasAtLeastOneMatchForXpath("opf:package/opf:metadata/dc:title", mgr);
			toCheck.HasAtLeastOneMatchForXpath("opf:package/opf:metadata/dc:language", mgr);
			toCheck.HasAtLeastOneMatchForXpath("opf:package/opf:metadata/dc:identifier", mgr);
			toCheck.HasAtLeastOneMatchForXpath("package/metadata/meta[@property='dcterms:modified']");

			toCheck.HasAtLeastOneMatchForXpath("package/manifest/item[@id='f1' and @href='1.xhtml']");
			toCheck.HasAtLeastOneMatchForXpath("package/manifest/item[@id='myImage' and @href='myImage.png']");
			toCheck.HasAtLeastOneMatchForXpath("package/manifest/item[@id='my_image' and @href='my_image.png']");
			toCheck.HasAtLeastOneMatchForXpath("package/spine/itemref[@idref='f1']");
			toCheck.HasAtLeastOneMatchForXpath("package/manifest/item[@properties='nav']");
			toCheck.HasAtLeastOneMatchForXpath("package/manifest/item[@properties='cover-image']");

			toCheck.HasAtLeastOneMatchForXpath("package/manifest/item[@id='settingsCollectionStyles' and @href='settingsCollectionStyles.css']");
			toCheck.HasAtLeastOneMatchForXpath("package/manifest/item[@id='customCollectionStyles' and @href='customCollectionStyles.css']");
			toCheck.HasAtLeastOneMatchForXpath("package/manifest/item[@id='customBookStyles' and @href='customBookStyles.css']");

			toCheck.HasAtLeastOneMatchForXpath("package/manifest/item[@id='AndikaNewBasic-R' and @href='AndikaNewBasic-R.ttf']");
			toCheck.HasAtLeastOneMatchForXpath("package/manifest/item[@id='AndikaNewBasic-B' and @href='AndikaNewBasic-B.ttf']");
			// It should include italic and BI too...though eventually it may get smarter and figure they are not used...but I think this is enough to test
			toCheck.HasAtLeastOneMatchForXpath("package/manifest/item[@id='fonts' and @href='fonts.css']");

			var packageDoc = XDocument.Parse(packageData);
			XNamespace opf = "http://www.idpf.org/2007/opf";
			// Some attempt at validating that we actually included the images in the zip.
			// Enhance: This undesirably depends on the exact order of items in the manifest.
			var image1 = packageDoc.Root.Element(opf + "manifest").Elements(opf + "item").ToArray()[0].Attribute("href").Value;
			GetZipEntry(zip, Path.GetDirectoryName(packageFile) + "/" + image1);
			var image2 = packageDoc.Root.Element(opf + "manifest").Elements(opf + "item").ToArray()[1].Attribute("href").Value;
			GetZipEntry(zip, Path.GetDirectoryName(packageFile) + "/" + image2);
			// Similarly try to validate really copying the font files
			GetZipEntry(zip, Path.GetDirectoryName(packageFile) + "/" + "AndikaNewBasic-R.ttf");

			var page1 = packageDoc.Root.Element(opf + "manifest").Elements(opf + "item").ToArray()[2].Attribute("href").Value;
			// Names in package file are relative to its folder.
			var page1Data = GetZipContent(zip, Path.GetDirectoryName(packageFile) + "/" + page1);
			// This is possibly too strong; see comment where we remove them.
			AssertThatXmlIn.String(page1Data).HasNoMatchForXpath("//*[@aria-describedby]");
			// Not sure why we sometimes have these, but validator doesn't like them.
			AssertThatXmlIn.String(page1Data).HasNoMatchForXpath("//*[@lang='']");
			XNamespace xhtml = "http://www.w3.org/1999/xhtml";
			var mgr2 = new XmlNamespaceManager(new NameTable());
			mgr2.AddNamespace("xhtml", "http://www.w3.org/1999/xhtml");

			AssertThatXmlIn.String(page1Data).HasNoMatchForXpath("//xhtml:script", mgr2);
			AssertThatXmlIn.String(page1Data).HasNoMatchForXpath("//*[@lang='*']");
			AssertThatXmlIn.String(page1Data).HasAtLeastOneMatchForXpath("//img[@src='my_image.png']");
			AssertThatXmlIn.String(page1Data).HasAtLeastOneMatchForXpath("//xhtml:link[@rel='stylesheet' and @href='settingsCollectionStyles.css']", mgr2);
			AssertThatXmlIn.String(page1Data).HasAtLeastOneMatchForXpath("//xhtml:link[@rel='stylesheet' and @href='customCollectionStyles.css']", mgr2);
			AssertThatXmlIn.String(page1Data).HasAtLeastOneMatchForXpath("//xhtml:link[@rel='stylesheet' and @href='customBookStyles.css']", mgr2);
			AssertThatXmlIn.String(page1Data).HasAtLeastOneMatchForXpath("//xhtml:link[@rel='stylesheet' and @href='fonts.css']", mgr2);

			mgr2.AddNamespace("epub", "http://www.idpf.org/2007/ops");
			var navPage = packageDoc.Root.Element(opf + "manifest").Elements(opf + "item").Last().Attribute("href").Value;
			var navPageData = StripXmlHeader(GetZipContent(zip, Path.GetDirectoryName(packageFile) + "/" + navPage));
			AssertThatXmlIn.String(navPageData)
				.HasAtLeastOneMatchForXpath(
					"xhtml:html/xhtml:body/xhtml:nav[@epub:type='toc' and @id='toc']/xhtml:ol/xhtml:li/xhtml:a[@href='1.xhtml']", mgr2);

			var fontCssData = GetZipContent(zip, "content/fonts.css");
			Assert.That(fontCssData, Is.StringContaining("@font-face {font-family:'Andika New Basic'; font-weight:normal; font-style:normal; src:url(AndikaNewBasic-R.ttf) format('opentype');}"));
			Assert.That(fontCssData, Is.StringContaining("@font-face {font-family:'Andika New Basic'; font-weight:bold; font-style:normal; src:url(AndikaNewBasic-B.ttf) format('opentype');}"));
			Assert.That(fontCssData, Is.StringContaining("@font-face {font-family:'Andika New Basic'; font-weight:normal; font-style:italic; src:url(AndikaNewBasic-I.ttf) format('opentype');}"));
			Assert.That(fontCssData, Is.StringContaining("@font-face {font-family:'Andika New Basic'; font-weight:bold; font-style:italic; src:url(AndikaNewBasic-BI.ttf) format('opentype');}"));
		}

		private EpubMakerAdjusted CreateEpubMaker(Bloom.Book.Book book)
		{
			return new EpubMakerAdjusted(book, new BookThumbNailer(_thumbnailer.Object));
		}

		[Test]
		public void Missing_Audio_Ignored()
		{
			// Similar input as the basic SaveAudio, (also verifies that IDs are really adjusted), but this time we don't create one of the expected audio files.
			SetDom(@"<div class='bloom-page'>
						<div id='somewrapper'>
							<div id='test' class='bloom-translationGroup bloom-requiresParagraphs' lang=''>
								<div aria-describedby='qtip-1' class='bloom-editable' lang='en'>
									This is some text
								</div>
								<div lang = '*'>more text</div>
							</div>
							<div><img src='1my$Image.png'></img></div>
							<div><img src='my%20image.png'></img></div>
						</div>
					</div>",
						   @"<link rel='stylesheet' href='../settingsCollectionStyles.css'/>
							<link rel='stylesheet' href='../customCollectionStyles.css'/>
							<link rel='stylesheet' href='customBookStyles.css'/>");
			var book = CreateBook();

			CreateCommonCssFiles(book);

			MakeSamplePngImageWithMetadata(book.FolderPath.CombineForPath("1my$Image.png"));
			var epubFolder = new TemporaryFolder();
			var epubName = "output.epub";
			var epubPath = Path.Combine(epubFolder.FolderPath, epubName);
			using (var maker = CreateEpubMaker(book))
				maker.SaveEpub(epubPath);
			Assert.That(File.Exists(epubPath));
			var zip = new ZipFile(epubPath);

			// Every epub must have a mimetype at the root
			GetZipContent(zip, "mimetype");

			// Every epub must have a "META-INF/container.xml." (case matters). Most things we could check about its content
			// would be redundant with the code that produces it, but we can at least verify that it is valid
			// XML and points us at the rootfile (open package format) file.
			var containerData = GetZipContent(zip, "META-INF/container.xml");
			var doc = XDocument.Parse(containerData);
			XNamespace ns = doc.Root.Attribute("xmlns").Value;
			var packageFile = doc.Root.Element(ns + "rootfiles").Element(ns + "rootfile").Attribute("full-path").Value;

			// That gives us a path to the main package file, typically content.opf
			var packageData = StripXmlHeader(GetZipContent(zip, packageFile));
			var toCheck = AssertThatXmlIn.String(packageData);
			var mgr = new XmlNamespaceManager(toCheck.NameTable);
			mgr.AddNamespace("dc", "http://purl.org/dc/elements/1.1/");
			mgr.AddNamespace("opf", "http://www.idpf.org/2007/opf");
			toCheck.HasAtLeastOneMatchForXpath("package[@version='3.0']");
			toCheck.HasAtLeastOneMatchForXpath("package[@unique-identifier]");
			toCheck.HasAtLeastOneMatchForXpath("opf:package/opf:metadata/dc:title", mgr);
			toCheck.HasAtLeastOneMatchForXpath("opf:package/opf:metadata/dc:language", mgr);
			toCheck.HasAtLeastOneMatchForXpath("opf:package/opf:metadata/dc:identifier", mgr);
			toCheck.HasAtLeastOneMatchForXpath("package/metadata/meta[@property='dcterms:modified']");

			toCheck.HasAtLeastOneMatchForXpath("package/manifest/item[@id='f1' and @href='1.xhtml']");
			toCheck.HasAtLeastOneMatchForXpath("package/manifest/item[@id='f1my_Image' and @href='1my$Image.png']");
			toCheck.HasNoMatchForXpath("package/manifest/item[@id='my_image']");
			toCheck.HasAtLeastOneMatchForXpath("package/spine/itemref[@idref='f1']");
			toCheck.HasAtLeastOneMatchForXpath("package/manifest/item[@properties='nav']");
			toCheck.HasAtLeastOneMatchForXpath("package/manifest/item[@properties='cover-image']");

			toCheck.HasAtLeastOneMatchForXpath("package/manifest/item[@id='settingsCollectionStyles' and @href='settingsCollectionStyles.css']");
			toCheck.HasAtLeastOneMatchForXpath("package/manifest/item[@id='customCollectionStyles' and @href='customCollectionStyles.css']");
			toCheck.HasAtLeastOneMatchForXpath("package/manifest/item[@id='customBookStyles' and @href='customBookStyles.css']");

			toCheck.HasAtLeastOneMatchForXpath("package/manifest/item[@id='AndikaNewBasic-R' and @href='AndikaNewBasic-R.ttf']");
			toCheck.HasAtLeastOneMatchForXpath("package/manifest/item[@id='AndikaNewBasic-B' and @href='AndikaNewBasic-B.ttf']");
			// It should include italic and BI too...though eventually it may get smarter and figure they are not used...but I think this is enough to test
			toCheck.HasAtLeastOneMatchForXpath("package/manifest/item[@id='fonts' and @href='fonts.css']");

			var packageDoc = XDocument.Parse(packageData);
			XNamespace opf = "http://www.idpf.org/2007/opf";
			// Some attempt at validating that we actually included the images in the zip.
			// Enhance: This undesirably depends on the exact order of items in the manifest.
			var image1 = packageDoc.Root.Element(opf + "manifest").Elements(opf + "item").ToArray()[0].Attribute("href").Value;
			GetZipEntry(zip, Path.GetDirectoryName(packageFile) + "/" + image1);
		}

		/// <summary>
		/// Motivated by "El Nino" from bloom library, which (to defeat caching?) has such a query param in one of its src attrs.
		/// </summary>
		[Test]
		public void ImageSrcQuery_IsIgnored()
		{
			SetDom(@"<div class='bloom-page'>
						<div id='somewrapper'>
							<div id='test' class='bloom-translationGroup bloom-requiresParagraphs' lang=''>
								<div aria-describedby='qtip-1' class='bloom-editable' lang='en'>
									This is some text
								</div>
								<div lang = '*'>more text</div>
							</div>
							<div><img src='myImage.png?1023456'></img></div>
						</div>
					</div>",
						   @"<link rel='stylesheet' href='../settingsCollectionStyles.css'/>
							<link rel='stylesheet' href='../customCollectionStyles.css'/>
							<link rel='stylesheet' href='customBookStyles.css'/>");
			var book = CreateBook();

			CreateCommonCssFiles(book);

			MakeSamplePngImageWithMetadata(book.FolderPath.CombineForPath("myImage.png"));
			var epubFolder = new TemporaryFolder();
			var epubName = "output.epub";
			var epubPath = Path.Combine(epubFolder.FolderPath, epubName);
			using (var maker = CreateEpubMaker(book))
				maker.SaveEpub(epubPath);
			Assert.That(File.Exists(epubPath));
			var zip = new ZipFile(epubPath);

			// Every epub must have a mimetype at the root
			GetZipContent(zip, "mimetype");

			// Every epub must have a "META-INF/container.xml." (case matters). Most things we could check about its content
			// would be redundant with the code that produces it, but we can at least verify that it is valid
			// XML and points us at the rootfile (open package format) file.
			var containerData = GetZipContent(zip, "META-INF/container.xml");
			var doc = XDocument.Parse(containerData);
			XNamespace ns = doc.Root.Attribute("xmlns").Value;
			var packageFile = doc.Root.Element(ns + "rootfiles").Element(ns + "rootfile").Attribute("full-path").Value;

			// That gives us a path to the main package file, typically content.opf
			var packageData = StripXmlHeader(GetZipContent(zip, packageFile));
			var toCheck = AssertThatXmlIn.String(packageData);
			var mgr = new XmlNamespaceManager(toCheck.NameTable);
			mgr.AddNamespace("dc", "http://purl.org/dc/elements/1.1/");
			mgr.AddNamespace("opf", "http://www.idpf.org/2007/opf");
			toCheck.HasAtLeastOneMatchForXpath("package[@version='3.0']");
			toCheck.HasAtLeastOneMatchForXpath("package[@unique-identifier]");

			toCheck.HasAtLeastOneMatchForXpath("package/manifest/item[@id='myImage' and @href='myImage.png']");

			var packageDoc = XDocument.Parse(packageData);
			XNamespace opf = "http://www.idpf.org/2007/opf";
			// Some attempt at validating that we actually included the images in the zip.
			// Enhance: This undesirably depends on the exact order of items in the manifest.
			var image1 = packageDoc.Root.Element(opf + "manifest").Elements(opf + "item").ToArray()[0].Attribute("href").Value;
			GetZipEntry(zip, Path.GetDirectoryName(packageFile) + "/" + image1);

			var page1 = packageDoc.Root.Element(opf + "manifest").Elements(opf + "item").ToArray()[1].Attribute("href").Value;
			// Names in package file are relative to its folder.
			var page1Data = GetZipContent(zip, Path.GetDirectoryName(packageFile) + "/" + page1);
			XNamespace xhtml = "http://www.w3.org/1999/xhtml";
			var mgr2 = new XmlNamespaceManager(new NameTable());
			mgr2.AddNamespace("xhtml", "http://www.w3.org/1999/xhtml");

			AssertThatXmlIn.String(page1Data).HasAtLeastOneMatchForXpath("//img[@src='myImage.png']");
		}

		/// <summary>
		/// Motivated by "Look in the sky. What do you see?" from bloom library, if we can't find an image,
		/// remove the element.
		/// </summary>
		[Test]
		public void ImageMissing_IsRemoved()
		{
			SetDom(@"<div class='bloom-page'>
						<div id='somewrapper'>
							<div id='test' class='bloom-translationGroup bloom-requiresParagraphs' lang=''>
								<div aria-describedby='qtip-1' class='bloom-editable' lang='en'>
									This is some text
								</div>
								<div lang = '*'>more text</div>
							</div>
							<div><img src='myImage.png?1023456'></img></div>
						</div>
					</div>",
						   @"<link rel='stylesheet' href='../settingsCollectionStyles.css'/>
							<link rel='stylesheet' href='../customCollectionStyles.css'/>
							<link rel='stylesheet' href='customBookStyles.css'/>");
			var book = CreateBook();

			CreateCommonCssFiles(book);

			// In this test we deliberately do NOT create the image that the source calls for.
			//MakeSamplePngImageWithMetadata(book.FolderPath.CombineForPath("myImage.png"));
			var epubFolder = new TemporaryFolder();
			var epubName = "output.epub";
			var epubPath = Path.Combine(epubFolder.FolderPath, epubName);
			using (var maker = CreateEpubMaker(book))
				maker.SaveEpub(epubPath);
			Assert.That(File.Exists(epubPath));
			var zip = new ZipFile(epubPath);

			// Every epub must have a mimetype at the root
			GetZipContent(zip, "mimetype");

			// Every epub must have a "META-INF/container.xml." (case matters). Most things we could check about its content
			// would be redundant with the code that produces it, but we can at least verify that it is valid
			// XML and points us at the rootfile (open package format) file.
			var containerData = GetZipContent(zip, "META-INF/container.xml");
			var doc = XDocument.Parse(containerData);
			XNamespace ns = doc.Root.Attribute("xmlns").Value;
			var packageFile = doc.Root.Element(ns + "rootfiles").Element(ns + "rootfile").Attribute("full-path").Value;

			// That gives us a path to the main package file, typically content.opf
			var packageData = StripXmlHeader(GetZipContent(zip, packageFile));
			var toCheck = AssertThatXmlIn.String(packageData);
			var mgr = new XmlNamespaceManager(toCheck.NameTable);
			mgr.AddNamespace("dc", "http://purl.org/dc/elements/1.1/");
			mgr.AddNamespace("opf", "http://www.idpf.org/2007/opf");
			toCheck.HasAtLeastOneMatchForXpath("package[@version='3.0']");
			toCheck.HasAtLeastOneMatchForXpath("package[@unique-identifier]");

			toCheck.HasNoMatchForXpath("package/manifest/item[@id='fmyImage' and @href='myImage.png']");

			var packageDoc = XDocument.Parse(packageData);
			XNamespace opf = "http://www.idpf.org/2007/opf";

			var page1 = packageDoc.Root.Element(opf + "manifest").Elements(opf + "item").ToArray()[0].Attribute("href").Value;
			// Names in package file are relative to its folder.
			var page1Data = GetZipContent(zip, Path.GetDirectoryName(packageFile) + "/" + page1);
			XNamespace xhtml = "http://www.w3.org/1999/xhtml";
			var mgr2 = new XmlNamespaceManager(new NameTable());
			mgr2.AddNamespace("xhtml", "http://www.w3.org/1999/xhtml");

			AssertThatXmlIn.String(page1Data).HasNoMatchForXpath("//img[@src='myImage.png']");
		}

		/// <summary>
		/// Motivated by "Look in the sky. What do you see?" from bloom library, if elements with class bloom-ui have
		/// somehow been left in the book, don't put them in the epub.
		/// </summary>
		[Test]
		public void BloomUi_IsRemoved()
		{
			SetDom(@"<div class='bloom-page'>
						<div id='somewrapper'>
							<div id='test' class='bloom-translationGroup bloom-requiresParagraphs' lang=''>
								<div aria-describedby='qtip-1' class='bloom-editable' lang='en'>
									This is some text
								</div>
								<div lang = '*'>more text</div>
							</div>
							<div class='bloom-ui rubbish'><img src='myImage.png?1023456'></img></div>
						</div>
					</div>",
						   @"<link rel='stylesheet' href='../settingsCollectionStyles.css'/>
							<link rel='stylesheet' href='../customCollectionStyles.css'/>
							<link rel='stylesheet' href='customBookStyles.css'/>");
			var book = CreateBook();

			CreateCommonCssFiles(book);

			// Even though the image exists we should not use it.
			MakeSamplePngImageWithMetadata(book.FolderPath.CombineForPath("myImage.png"));
			var epubFolder = new TemporaryFolder();
			var epubName = "output.epub";
			var epubPath = Path.Combine(epubFolder.FolderPath, epubName);
			using (var maker = CreateEpubMaker(book))
				maker.SaveEpub(epubPath);
			Assert.That(File.Exists(epubPath));
			var zip = new ZipFile(epubPath);

			// Every epub must have a mimetype at the root
			GetZipContent(zip, "mimetype");

			// Every epub must have a "META-INF/container.xml." (case matters). Most things we could check about its content
			// would be redundant with the code that produces it, but we can at least verify that it is valid
			// XML and points us at the rootfile (open package format) file.
			var containerData = GetZipContent(zip, "META-INF/container.xml");
			var doc = XDocument.Parse(containerData);
			XNamespace ns = doc.Root.Attribute("xmlns").Value;
			var packageFile = doc.Root.Element(ns + "rootfiles").Element(ns + "rootfile").Attribute("full-path").Value;

			// That gives us a path to the main package file, typically content.opf
			var packageData = StripXmlHeader(GetZipContent(zip, packageFile));
			var toCheck = AssertThatXmlIn.String(packageData);
			var mgr = new XmlNamespaceManager(toCheck.NameTable);
			mgr.AddNamespace("dc", "http://purl.org/dc/elements/1.1/");
			mgr.AddNamespace("opf", "http://www.idpf.org/2007/opf");
			toCheck.HasAtLeastOneMatchForXpath("package[@version='3.0']");
			toCheck.HasAtLeastOneMatchForXpath("package[@unique-identifier]");

			toCheck.HasNoMatchForXpath("package/manifest/item[@id='fmyImage' and @href='myImage.png']");

			var packageDoc = XDocument.Parse(packageData);
			XNamespace opf = "http://www.idpf.org/2007/opf";

			var page1 = packageDoc.Root.Element(opf + "manifest").Elements(opf + "item").ToArray()[0].Attribute("href").Value;
			// Names in package file are relative to its folder.
			var page1Data = GetZipContent(zip, Path.GetDirectoryName(packageFile) + "/" + page1);
			XNamespace xhtml = "http://www.w3.org/1999/xhtml";
			var mgr2 = new XmlNamespaceManager(new NameTable());
			mgr2.AddNamespace("xhtml", "http://www.w3.org/1999/xhtml");

			AssertThatXmlIn.String(page1Data).HasNoMatchForXpath("//img[@src='myImage.png']");
		}

		// Set up some typical CSS files we DO want to include, even in 'unpaginated' mode
		private static void CreateCommonCssFiles(Bloom.Book.Book book)
		{
			var collectionFolder = Path.GetDirectoryName(book.FolderPath);
			var settingsCollectionPath = Path.Combine(collectionFolder, "settingsCollectionStyles.css");
			File.WriteAllText(settingsCollectionPath, "body:{font-family: 'Andika New Basic';}");
			var customCollectionPath = Path.Combine(collectionFolder, "customCollectionStyles.css");
			File.WriteAllText(customCollectionPath, "body:{font-family: 'Andika New Basic';}");
			var customBookPath = Path.Combine(book.FolderPath, "customBookStyles.css");
			File.WriteAllText(customBookPath, "body:{font-family: 'Andika New Basic';}");
		}

		protected override Bloom.Book.Book CreateBook()
		{
			var book = base.CreateBook();
			// Export requires us to have a thumbnail.
			MakeSamplePngImageWithMetadata(book.FolderPath.CombineForPath("thumbnail-256.png"));
			return book;
		}

		[Test]
		public void UnpaginatedOutout_UsesSpecialStylesheets()
		{
			SetDom(@"<div class='bloom-page'>
						<div id='somewrapper'>
							<div id='test' class='bloom-translationGroup bloom-requiresParagraphs' lang=''>
								<div aria-describedby='qtip-1' class='bloom-editable' lang='en'>
									English text should only display when that language is active.
								</div>
								<div lang = '*'>more text</div>
								<div lang='xyz'><label class='bubble'>Book title in {lang} should be removed</label>vernacular text should always display</div>
								<div lang='fr'>French text should only display if configured</div>
								<div lang='de'>German should never display in this collection</div>
							</div>
						</div>
					</div>",
						   @"<link rel='stylesheet' href='../settingsCollectionStyles.css'/>
							<link rel='stylesheet' href='../customCollectionStyles.css'/>
							<link rel='stylesheet' href='customBookStyles.css'/>");
			var book = CreateBook();
			CreateCommonCssFiles(book);
			var epubFolder = new TemporaryFolder();
			var epubName = "output.epub";
			var epubPath = Path.Combine(epubFolder.FolderPath, epubName);
			using (var maker = CreateEpubMaker(book))
			{
				maker.Unpaginated = true;
				maker.SaveEpub(epubPath);
			}
			Assert.That(File.Exists(epubPath));
			var zip = new ZipFile(epubPath);

			var containerData = GetZipContent(zip, "META-INF/container.xml");
			var doc = XDocument.Parse(containerData);
			XNamespace ns = doc.Root.Attribute("xmlns").Value;
			var packageFile = doc.Root.Element(ns + "rootfiles").Element(ns + "rootfile").Attribute("full-path").Value;

			// That gives us a path to the main package file, typically content.opf
			var packageData = StripXmlHeader(GetZipContent(zip, packageFile));

			// should not strip out these three css files.
			var toCheck = AssertThatXmlIn.String(packageData);
			toCheck.HasAtLeastOneMatchForXpath("package/manifest/item[@id='settingsCollectionStyles' and @href='settingsCollectionStyles.css']");
			toCheck.HasAtLeastOneMatchForXpath("package/manifest/item[@id='customCollectionStyles' and @href='customCollectionStyles.css']");
			toCheck.HasAtLeastOneMatchForXpath("package/manifest/item[@id='customBookStyles' and @href='customBookStyles.css']");

			var packageDoc = XDocument.Parse(packageData);
			XNamespace opf = "http://www.idpf.org/2007/opf";
			// Some attempt at validating that we actually included the images in the zip.
			// Enhance: This undesirably depends on the exact order of items in the manifest.

			var page1 = packageDoc.Root.Element(opf + "manifest").Elements(opf + "item").ToArray()[0].Attribute("href").Value;
			var page1Data = StripXmlHeader(GetZipContent(zip, Path.GetDirectoryName(packageFile) + "/" + page1));
			XNamespace xhtml = "http://www.w3.org/1999/xhtml";
			var mgr = new XmlNamespaceManager(new NameTable());
			mgr.AddNamespace("xhtml", "http://www.w3.org/1999/xhtml");
			AssertThatXmlIn.String(page1Data).HasNoMatchForXpath("//xhtml:head/xhtml:link[@href='basePage.css']", mgr);
			AssertThatXmlIn.String(page1Data).HasAtLeastOneMatchForXpath("//head/link[@href='baseEpub.css']");
			AssertThatXmlIn.String(page1Data).HasAtLeastOneMatchForXpath("//xhtml:link[@rel='stylesheet' and @href='settingsCollectionStyles.css']", mgr);
			AssertThatXmlIn.String(page1Data).HasAtLeastOneMatchForXpath("//xhtml:link[@rel='stylesheet' and @href='customCollectionStyles.css']", mgr);
			AssertThatXmlIn.String(page1Data).HasAtLeastOneMatchForXpath("//xhtml:link[@rel='stylesheet' and @href='customBookStyles.css']", mgr);
		}

		/// <summary>
		/// Content whose display properties resolves to display:None should be removed.
		/// </summary>
		[Test]
		public void DisplayNone_IsRemoved()
		{
			SetDom(@"<div class='bloom-page'>
						<div id='somewrapper'>
							<div class='pageLabel' lang = 'en'>Front Cover</div>
							<div id='test' class='bloom-translationGroup bloom-requiresParagraphs' lang=''>
								<div aria-describedby='qtip-1' class='bloom-editable' lang='en'>
									English text should only display when that language is active.
								</div>
								<div class='bloom-editable' lang = '*'>more text</div>
								<div class='bloom-editable' lang='xyz'><label class='bubble'>Book title in {lang} should be removed</label>vernacular text should always display</div>
								<div class='bloom-editable' lang='fr'>French text should only display if configured</div>
								<div class='bloom-editable' lang='de'>German should never display in this collection</div>
							</div>
						</div>
					</div>");
			var book = CreateBook();
			var epubFolder = new TemporaryFolder();
			var epubName = "output.epub";
			var epubPath = Path.Combine(epubFolder.FolderPath, epubName);
			using (var maker = CreateEpubMaker(book))
				maker.SaveEpub(epubPath);
			Assert.That(File.Exists(epubPath));
			var zip = new ZipFile(epubPath);

			// Every epub must have a "META-INF/container.xml." (case matters). Most things we could check about its content
			// would be redundant with the code that produces it, but we can at least verify that it is valid
			// XML and points us at the rootfile (open package format) file.
			var containerData = GetZipContent(zip, "META-INF/container.xml");
			var doc = XDocument.Parse(containerData);
			XNamespace ns = doc.Root.Attribute("xmlns").Value;
			var packageFile = doc.Root.Element(ns + "rootfiles").Element(ns + "rootfile").Attribute("full-path").Value;

			// That gives us a path to the main package file, typically content.opf
			var packageData = StripXmlHeader(GetZipContent(zip, packageFile));

			var packageDoc = XDocument.Parse(packageData);
			XNamespace opf = "http://www.idpf.org/2007/opf";

			var page1 = packageDoc.Root.Element(opf + "manifest").Elements(opf + "item").ToArray()[0].Attribute("href").Value;
			// Names in package file are relative to its folder.
			var page1Data = StripXmlHeader(GetZipContent(zip, Path.GetDirectoryName(packageFile) + "/" + page1));

			XNamespace xhtml = "http://www.w3.org/1999/xhtml";
			var mgr = new XmlNamespaceManager(new NameTable());
			mgr.AddNamespace("xhtml", "http://www.w3.org/1999/xhtml");
			var assertPage1 = AssertThatXmlIn.String(page1Data);
			assertPage1.HasAtLeastOneMatchForXpath("//xhtml:div[@lang='xyz']", mgr);
			assertPage1.HasNoMatchForXpath("//xhtml:div[@lang='en']", mgr); // one language by default
			assertPage1.HasNoMatchForXpath("//xhtml:div[@lang='fr']", mgr);
			assertPage1.HasNoMatchForXpath("//xhtml:div[@lang='de']", mgr);
			assertPage1.HasNoMatchForXpath("//xhtml:label", mgr); // labels are hidden
			assertPage1.HasNoMatchForXpath("//xhtml:div[@class='pageLabel']", mgr);
		}

		private void VerifyTranslationGroup(XElement xElement, string lang, string classVal)
		{
			VerifyAttribute(xElement, "class", "bloom-translationGroup bloom-requiresParagraphs");
			var editable = xElement.Element(_xhtml + "div");
			VerifyAttribute(editable, "class", classVal);
			VerifyAttribute(editable, "lang", lang);
		}

		private void VerifyAttribute(XElement xElement, string name, string val)
		{
			var attr = xElement.Attribute(name);
			Assert.That(attr, Is.Not.Null);
			Assert.That(attr.Value, Is.EqualTo(val));
		}


		/// <summary>
		/// Content whose display properties resolves to display:None should be removed.
		/// This should not include National1 in XMatter.
		/// </summary>
		[Test]
		public void National1_InXMatter_IsNotRemoved()
		{
			SetDom(@"<div class='bloom-page bloom-frontMatter'>
						<div id='somewrapper'>
							<div class='pageLabel' lang = 'en'>Front Cover</div>
							<div id='test' class='bloom-translationGroup bloom-requiresParagraphs' lang=''>
								<div aria-describedby='qtip-1' class='bloom-editable' lang='en'>
									English text should only display when that language is active.
								</div>
								<div class='bloom-editable' lang = '*'>more text</div>
								<div class='bloom-editable' lang='xyz'><label class='bubble'>Book title in {lang} should be removed</label>vernacular text should always display</div>
								<div class='bloom-editable' lang='fr'>French text should only display if configured</div>
								<div class='bloom-editable' lang='de'>German should never display in this collection</div>
							</div>
						</div>
					</div>");
			var book = CreateBook();
			var epubFolder = new TemporaryFolder();
			var epubName = "output.epub";
			var epubPath = Path.Combine(epubFolder.FolderPath, epubName);
			using (var maker = CreateEpubMaker(book))
				maker.SaveEpub(epubPath);
			Assert.That(File.Exists(epubPath));
			var zip = new ZipFile(epubPath);

			// Every epub must have a "META-INF/container.xml." (case matters). Most things we could check about its content
			// would be redundant with the code that produces it, but we can at least verify that it is valid
			// XML and points us at the rootfile (open package format) file.
			var containerData = GetZipContent(zip, "META-INF/container.xml");
			var doc = XDocument.Parse(containerData);
			XNamespace ns = doc.Root.Attribute("xmlns").Value;
			var packageFile = doc.Root.Element(ns + "rootfiles").Element(ns + "rootfile").Attribute("full-path").Value;

			// That gives us a path to the main package file, typically content.opf
			var packageData = StripXmlHeader(GetZipContent(zip, packageFile));

			var packageDoc = XDocument.Parse(packageData);
			XNamespace opf = "http://www.idpf.org/2007/opf";

			var page1 = packageDoc.Root.Element(opf + "manifest").Elements(opf + "item").ToArray()[0].Attribute("href").Value;
			// Names in package file are relative to its folder.
			var page1Data = StripXmlHeader(GetZipContent(zip, Path.GetDirectoryName(packageFile) + "/" + page1));

			XNamespace xhtml = "http://www.w3.org/1999/xhtml";
			var mgr = new XmlNamespaceManager(new NameTable());
			mgr.AddNamespace("xhtml", "http://www.w3.org/1999/xhtml");
			var assertPage1 = AssertThatXmlIn.String(page1Data);
			assertPage1.HasAtLeastOneMatchForXpath("//xhtml:div[@lang='xyz']", mgr);
			assertPage1.HasAtLeastOneMatchForXpath("//xhtml:div[@lang='en']", mgr);
			assertPage1.HasNoMatchForXpath("//xhtml:div[@lang='fr']", mgr);
			assertPage1.HasNoMatchForXpath("//xhtml:div[@lang='de']", mgr);
			assertPage1.HasNoMatchForXpath("//xhtml:label", mgr); // labels are hidden
			assertPage1.HasNoMatchForXpath("//xhtml:div[@class='pageLabel']", mgr);

		}

		[Test]
		public void FindFontsUsedInCss_FindsSimpleFontFamily()
		{
			var results = new HashSet<string>();
			HtmlDom.FindFontsUsedInCss("body {font-family:Arial}", results);
			Assert.That(results, Has.Count.EqualTo(1));
			Assert.That(results.Contains("Arial"));
		}

		[Test]
		public void FindFontsUsedInCss_FindsQuotedFontFamily()
		{
			var results = new HashSet<string>();
			HtmlDom.FindFontsUsedInCss("body {font-family:'Times New Roman'}", results);
			HtmlDom.FindFontsUsedInCss("body {font-family:\"Andika New Basic\"}", results);
			Assert.That(results, Has.Count.EqualTo(2));
			Assert.That(results.Contains("Times New Roman"));
			Assert.That(results.Contains("Andika New Basic"));
		}

		[Test]
		public void FindFontsUsedInCss_FindsMultipleFontFamilies()
		{
			var results = new HashSet<string>();
			HtmlDom.FindFontsUsedInCss("body {font-family: 'Times New Roman', Arial,\"Andika New Basic\";}", results);
			Assert.That(results, Has.Count.EqualTo(3));
			Assert.That(results.Contains("Times New Roman"));
			Assert.That(results.Contains("Andika New Basic"));
			Assert.That(results.Contains("Arial"));
		}

		[Test]
		public void ImageStyles_ConvertedToPercent()
		{
			SetDom(@"<div class='bloom-page A5Portrait'>
						<div id='somewrapper' class='marginBox'>
							<div id='test' class='bloom-translationGroup bloom-requiresParagraphs' lang=''>
								<div aria-describedby='qtip-1' class='bloom-editable' lang='en'>
									This is some text
								</div>
								<div lang = '*'>more text</div>
							</div>
							<div><img src='image1.png' width='334' height='220' style='width:334px; height:220px; margin-left: 34px; margin-top: 0px;'></img></div>
							<div><img src='image2.png' width='330' height='220' style='width:330px; height: 220px; margin-left: 33px; margin-top: 0px;'></img></div>
						</div>
					</div>");
			var book = CreateBook();
			MakeSamplePngImageWithMetadata(book.FolderPath.CombineForPath("image1.png"));
			MakeSamplePngImageWithMetadata(book.FolderPath.CombineForPath("image2.png"));
			var epubFolder = new TemporaryFolder();
			var epubName = "output.epub";
			var epubPath = Path.Combine(epubFolder.FolderPath, epubName);
			using (var maker = CreateEpubMaker(book))
				maker.SaveEpub(epubPath);
			Assert.That(File.Exists(epubPath));
			var zip = new ZipFile(epubPath);

			// Every epub must have a mimetype at the root
			GetZipContent(zip, "mimetype");

			// Every epub must have a "META-INF/container.xml." (case matters). Most things we could check about its content
			// would be redundant with the code that produces it, but we can at least verify that it is valid
			// XML and points us at the rootfile (open package format) file.
			var containerData = GetZipContent(zip, "META-INF/container.xml");
			var doc = XDocument.Parse(containerData);
			XNamespace ns = doc.Root.Attribute("xmlns").Value;
			var packageFile = doc.Root.Element(ns + "rootfiles").Element(ns + "rootfile").Attribute("full-path").Value;

			// That gives us a path to the main package file, typically content.opf
			var packageData = StripXmlHeader(GetZipContent(zip, packageFile));
			var packageDoc = XDocument.Parse(packageData);
			XNamespace opf = "http://www.idpf.org/2007/opf";

			var page1 = packageDoc.Root.Element(opf + "manifest").Elements(opf + "item").ToArray()[2].Attribute("href").Value;
			// Names in package file are relative to its folder.
			var page1Data = StripXmlHeader(GetZipContent(zip, Path.GetDirectoryName(packageFile) + "/" + page1));

			XNamespace xhtml = "http://www.w3.org/1999/xhtml";
			var mgr = new XmlNamespaceManager(new NameTable());
			mgr.AddNamespace("xhtml", "http://www.w3.org/1999/xhtml");
			// A5Portrait page is 297/2 mm wide
			// Percent size however is relative to containing block, typically the marginBox,
			// which is inset 40mm from page
			// a px in a printed book is exactly 1/96 in.
			// 25.4mm.in
			var marginboxInches = (297.0/2.0-40)/25.4;
			var picWidthInches = 334/96.0;
			var widthPercent = Math.Round(picWidthInches/marginboxInches*1000)/10;
			var picIndentInches = 34/96.0;
			var picIndentPercent = Math.Round(picIndentInches / marginboxInches * 1000) / 10;
			AssertThatXmlIn.String(page1Data).HasAtLeastOneMatchForXpath("//xhtml:img[@style='width:" + widthPercent.ToString("F1")
				+ "%; height:auto; margin-left: " + picIndentPercent.ToString("F1") + "%; margin-top: 0px;']", mgr);

			picWidthInches = 330 / 96.0;
			widthPercent = Math.Round(picWidthInches / marginboxInches * 1000) / 10;
			picIndentInches = 33 / 96.0;
			picIndentPercent = Math.Round(picIndentInches / marginboxInches * 1000) / 10;
			AssertThatXmlIn.String(page1Data).HasAtLeastOneMatchForXpath("//xhtml:img[@style='width:" + widthPercent.ToString("F1")
				+ "%; height:auto; margin-left: " + picIndentPercent.ToString("F1") + "%; margin-top: 0px;']", mgr);
		}

		[Test]
		public void ImageStyles_PercentsAdjustForContainingPercentDivs()
		{
			SetDom(@"<div class='bloom-page A5Portrait'>
						<div id='somewrapper' class='marginBox'>
									<div id='test' class='bloom-translationGroup bloom-requiresParagraphs' lang=''>
										<div aria-describedby='qtip-1' class='bloom-editable' lang='en'>
											This is some text
										</div>
										<div lang = '*'>more text</div>
							</div>
							<div id='anotherWrapper' style='width:80%'>
								<div id='innerrWrapper' style='width:50%'>
									<div><img src='image1.png' width='40' height='220' style='width:40px; height:220px; margin-left: 14px; margin-top: 0px;'></img></div>
								</div>
							</div>
						</div>
					</div>");
			var book = CreateBook();
			MakeSamplePngImageWithMetadata(book.FolderPath.CombineForPath("image1.png"));
			var epubFolder = new TemporaryFolder();
			var epubName = "output.epub";
			var epubPath = Path.Combine(epubFolder.FolderPath, epubName);
			using (var maker = CreateEpubMaker(book))
				maker.SaveEpub(epubPath);
			Assert.That(File.Exists(epubPath));
			var zip = new ZipFile(epubPath);

			// Every epub must have a mimetype at the root
			GetZipContent(zip, "mimetype");

			// Every epub must have a "META-INF/container.xml." (case matters). Most things we could check about its content
			// would be redundant with the code that produces it, but we can at least verify that it is valid
			// XML and points us at the rootfile (open package format) file.
			var containerData = GetZipContent(zip, "META-INF/container.xml");
			var doc = XDocument.Parse(containerData);
			XNamespace ns = doc.Root.Attribute("xmlns").Value;
			var packageFile = doc.Root.Element(ns + "rootfiles").Element(ns + "rootfile").Attribute("full-path").Value;

			// That gives us a path to the main package file, typically content.opf
			var packageData = StripXmlHeader(GetZipContent(zip, packageFile));
			var packageDoc = XDocument.Parse(packageData);
			XNamespace opf = "http://www.idpf.org/2007/opf";

			var page1 = packageDoc.Root.Element(opf + "manifest").Elements(opf + "item").ToArray()[1].Attribute("href").Value;
			// Names in package file are relative to its folder.
			var page1Data = StripXmlHeader(GetZipContent(zip, Path.GetDirectoryName(packageFile) + "/" + page1));

			XNamespace xhtml = "http://www.w3.org/1999/xhtml";
			var mgr = new XmlNamespaceManager(new NameTable());
			mgr.AddNamespace("xhtml", "http://www.w3.org/1999/xhtml");
			// A5Portrait page is 297/2 mm wide
			// Percent size however is relative to containing block,
			// which in this case is 50% of 80% of the marginBox,
			// which is inset 40mm from page
			// a px in a printed book is exactly 1/96 in.
			// 25.4mm.in
			var marginboxInches = (297.0 / 2.0 - 40) / 25.4;
			var picWidthInches = 40 / 96.0;
			var parentWidthInches = marginboxInches*0.8*0.5;
			var widthPercent = Math.Round(picWidthInches / parentWidthInches * 1000) / 10;
			var picIndentInches = 14 / 96.0;
			var picIndentPercent = Math.Round(picIndentInches / parentWidthInches * 1000) / 10;
			AssertThatXmlIn.String(page1Data).HasAtLeastOneMatchForXpath("//xhtml:img[@style='width:" + widthPercent.ToString("F1")
				+ "%; height:auto; margin-left: " + picIndentPercent.ToString("F1") + "%; margin-top: 0px;']", mgr);
		}

		[Test]
		public void BookWithAudio_ProducesOverlay()
		{
			SetDom(@"<div class='bloom-page A5Portrait'>
						<div id='somewrapper' class='marginBox'>
							<div id='test' class='bloom-translationGroup bloom-requiresParagraphs' lang=''>
								<div aria-describedby='qtip-1' class='bloom-editable' lang='xyz'>
									<span id='a123'>This is some text.</span><span id='a23'>Another sentence</span>
								</div>
								<div lang = '*'>more text</div>
							</div>
						</div>
					</div>");
			var book = CreateBook();
			MakeFakeAudio(book.FolderPath.CombineForPath("audio", "a123.mp4"));
			MakeFakeAudio(book.FolderPath.CombineForPath("audio", "a23.mp3"));
			var epubFolder = new TemporaryFolder();
			var epubName = "output.epub";
			var epubPath = Path.Combine(epubFolder.FolderPath, epubName);
			using (var maker = CreateEpubMaker(book))
				maker.SaveEpub(epubPath);
			Assert.That(File.Exists(epubPath));
			var zip = new ZipFile(epubPath);

			// Every epub must have a mimetype at the root
			GetZipContent(zip, "mimetype");

			var containerData = GetZipContent(zip, "META-INF/container.xml");
			var doc = XDocument.Parse(containerData);
			XNamespace ns = doc.Root.Attribute("xmlns").Value;
			var packageFile = doc.Root.Element(ns + "rootfiles").Element(ns + "rootfile").Attribute("full-path").Value;
			// xpath search for slash in attribute value fails (something to do with interpreting it as a namespace reference?)
			var packageData = StripXmlHeader(GetZipContent(zip, packageFile)).Replace("application/smil", "application^slash^smil").Replace("audio/", "audio^slash^");
			var packageDoc = XDocument.Parse(packageData);
			XNamespace opf = "http://www.idpf.org/2007/opf";

			var assertManifest = AssertThatXmlIn.String(packageData);
			assertManifest.HasAtLeastOneMatchForXpath("package/manifest/item[@id='f1' and @href='1.xhtml' and @media-overlay='f1_overlay']");
			assertManifest.HasAtLeastOneMatchForXpath("package/manifest/item[@id='f1_overlay' and @href='1_overlay.smil' and @media-type='application^slash^smil+xml']");
			assertManifest.HasAtLeastOneMatchForXpath("package/manifest/item[@id='a23' and @href='audio^slash^a23.mp3' and @media-type='audio^slash^mpeg']");
			assertManifest.HasAtLeastOneMatchForXpath("package/manifest/item[@id='a123' and @href='audio^slash^a123.mp4' and @media-type='audio^slash^mp4']");

			var smilData = StripXmlHeader(GetZipContent(zip, "content/1_overlay.smil"));
			var mgr = new XmlNamespaceManager(new NameTable());
			mgr.AddNamespace("smil", "http://www.w3.org/ns/SMIL");
			mgr.AddNamespace("epub", "http://www.idpf.org/2007/ops");
			var assertSmil = AssertThatXmlIn.String(smilData);
			assertSmil.HasAtLeastOneMatchForXpath("smil:smil/smil:body/smil:seq[@epub:textref='1.xhtml' and @epub:type='bodymatter chapter']", mgr);
			assertSmil.HasAtLeastOneMatchForXpath("smil:smil/smil:body/smil:seq/smil:par[@id='s1']/smil:text[@src='1.xhtml#a123']", mgr);
			assertSmil.HasAtLeastOneMatchForXpath("smil:smil/smil:body/smil:seq/smil:par[@id='s2']/smil:text[@src='1.xhtml#a23']", mgr);
			assertSmil.HasAtLeastOneMatchForXpath("smil:smil/smil:body/smil:seq/smil:par[@id='s1']/smil:audio[@src='audio/a123.mp4']", mgr);
			assertSmil.HasAtLeastOneMatchForXpath("smil:smil/smil:body/smil:seq/smil:par[@id='s2']/smil:audio[@src='audio/a23.mp3']", mgr);

			GetZipEntry(zip, "content/audio/a123.mp4");
			GetZipEntry(zip, "content/audio/a23.mp3");
		}

		/// <summary>
		/// There's some special-case code for Ids that start with digits that we test here.
		/// </summary>
		[Test]
		public void AudioWithParagraphsAndRealGuids_ProducesOverlay()
		{
			SetDom(@"<div class='bloom-page A5Portrait'>
						<div id='somewrapper' class='marginBox'>
							<div id='test' class='bloom-translationGroup bloom-requiresParagraphs' lang=''>
								<div aria-describedby='qtip-1' class='bloom-editable' lang='xyz'>
									<p><span id='e993d14a-0ec3-4316-840b-ac9143d59a2c'>This is some text.</span><span id='i0d8e9910-dfa3-4376-9373-a869e109b763'>Another sentence</span></p>
								</div>
								<div lang = '*'>more text</div>
							</div>
						</div>
					</div>");
			var book = CreateBook();
			MakeFakeAudio(book.FolderPath.CombineForPath("audio", "e993d14a-0ec3-4316-840b-ac9143d59a2c.mp4"));
			MakeFakeAudio(book.FolderPath.CombineForPath("audio", "i0d8e9910-dfa3-4376-9373-a869e109b763.mp3"));
			var epubFolder = new TemporaryFolder();
			var epubName = "output.epub";
			var epubPath = Path.Combine(epubFolder.FolderPath, epubName);
			using (var maker = CreateEpubMaker(book))
				maker.SaveEpub(epubPath);
			Assert.That(File.Exists(epubPath));
			var zip = new ZipFile(epubPath);

			// Every epub must have a mimetype at the root
			GetZipContent(zip, "mimetype");

			var containerData = GetZipContent(zip, "META-INF/container.xml");
			var doc = XDocument.Parse(containerData);
			XNamespace ns = doc.Root.Attribute("xmlns").Value;
			var packageFile = doc.Root.Element(ns + "rootfiles").Element(ns + "rootfile").Attribute("full-path").Value;
			// xpath search for slash in attribute value fails (something to do with interpreting it as a namespace reference?)
			var packageData = StripXmlHeader(GetZipContent(zip, packageFile)).Replace("application/smil", "application^slash^smil");
			var packageDoc = XDocument.Parse(packageData);
			XNamespace opf = "http://www.idpf.org/2007/opf";

			var assertManifest = AssertThatXmlIn.String(packageData);
			assertManifest.HasAtLeastOneMatchForXpath("package/manifest/item[@id='f1' and @href='1.xhtml' and @media-overlay='f1_overlay']");
			assertManifest.HasAtLeastOneMatchForXpath("package/manifest/item[@id='f1_overlay' and @href='1_overlay.smil' and @media-type='application^slash^smil+xml']");

			var smilData = StripXmlHeader(GetZipContent(zip, "content/1_overlay.smil"));
			var mgr = new XmlNamespaceManager(new NameTable());
			mgr.AddNamespace("smil", "http://www.w3.org/ns/SMIL");
			mgr.AddNamespace("epub", "http://www.idpf.org/2007/ops");
			var assertSmil = AssertThatXmlIn.String(smilData);
			assertSmil.HasAtLeastOneMatchForXpath("smil:smil/smil:body/smil:seq[@epub:textref='1.xhtml' and @epub:type='bodymatter chapter']", mgr);
			assertSmil.HasAtLeastOneMatchForXpath("smil:smil/smil:body/smil:seq/smil:par[@id='s1']/smil:text[@src='1.xhtml#e993d14a-0ec3-4316-840b-ac9143d59a2c']", mgr);
			assertSmil.HasAtLeastOneMatchForXpath("smil:smil/smil:body/smil:seq/smil:par[@id='s2']/smil:text[@src='1.xhtml#i0d8e9910-dfa3-4376-9373-a869e109b763']", mgr);
			assertSmil.HasAtLeastOneMatchForXpath("smil:smil/smil:body/smil:seq/smil:par[@id='s1']/smil:audio[@src='audio/e993d14a-0ec3-4316-840b-ac9143d59a2c.mp4']", mgr);
			assertSmil.HasAtLeastOneMatchForXpath("smil:smil/smil:body/smil:seq/smil:par[@id='s2']/smil:audio[@src='audio/i0d8e9910-dfa3-4376-9373-a869e109b763.mp3']", mgr);

			GetZipEntry(zip, "content/audio/e993d14a-0ec3-4316-840b-ac9143d59a2c.mp4");
			GetZipEntry(zip, "content/audio/i0d8e9910-dfa3-4376-9373-a869e109b763.mp3");
		}

		protected void MakeFakeAudio(string path)
		{
			Directory.CreateDirectory(Path.GetDirectoryName(path));
			File.WriteAllText(path, "fake audio");
		}

		private string GetZipContent(ZipFile zip, string path)
		{
			var entry = GetZipEntry(zip, path);
			var buffer = new byte[entry.Size];
			var stream = zip.GetInputStream(entry);
			stream.Read(buffer, 0, (int) entry.Size);
			return Encoding.UTF8.GetString(buffer);
		}

		private static ZipEntry GetZipEntry(ZipFile zip, string path)
		{
			var entry = zip.GetEntry(path);
			Assert.That(entry, Is.Not.Null, "Should have found entry at " + path);
			Assert.That(entry.Name, Is.EqualTo(path), "Expected entry has wrong case");
			return entry;
		}

		private string StripXmlHeader(string data)
		{
			var index = data.IndexOf("?>");
			if (index > 0)
				return data.Substring(index + 2);
			return data;
		}

		[TestCase("abc", ExpectedResult = "abc")]
		[TestCase("123", ExpectedResult = "f123")]
		[TestCase("123 abc", ExpectedResult = "f123abc")]
		[TestCase("x*y", ExpectedResult = "x_y")]
		[TestCase("x:y", ExpectedResult = "x:y")]
		[TestCase("*edf", ExpectedResult = "_edf")]
		[TestCase("a\u0300z", ExpectedResult = "a\u0300z")] // valid mid character
		[TestCase("\u0300z", ExpectedResult = "f\u0300z")] // invalid start character
		[TestCase("a\u037Ez", ExpectedResult = "a_z")] // high-range invalid
		[TestCase("-abc", ExpectedResult = "f-abc")]
		[Test]
		public string ToValidXmlId(string input)
		{
			return EpubMaker.ToValidXmlId(input);
		}

	}

	class EpubMakerAdjusted : EpubMaker
	{
		public EpubMakerAdjusted(Bloom.Book.Book book, BookThumbNailer thumbNailer) : base(thumbNailer)
		{
			this.Book = book;
		}

		internal override void CopyFile(string srcPath, string dstPath)
		{
			if (srcPath.Contains("notareallocation"))
			{
				File.WriteAllText("This is a test fake", dstPath);
				return;
			}
			base.CopyFile(srcPath, dstPath);
		}
	}
}