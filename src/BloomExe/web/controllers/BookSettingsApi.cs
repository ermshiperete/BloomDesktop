﻿using System;
using System.Dynamic;
using Bloom.Book;

namespace Bloom.Api
{
	/// <summary>
	/// Exposes some settings of the current Book via API
	/// </summary>
	public class BookSettingsApi
	{
		private readonly BookSelection _bookSelection;
		private readonly PageRefreshEvent _pageRefreshEvent;

		public BookSettingsApi(BookSelection bookSelection, PageRefreshEvent pageRefreshEvent)
		{
			_bookSelection = bookSelection;
			_pageRefreshEvent = pageRefreshEvent;
		}

		public void RegisterWithServer(EnhancedImageServer server)
		{
			server.RegisterEndpointHandler("book/settings", HandleBookSettings);
		}

		/// <summary>
		/// Get a json of the book's settings.
		/// </summary>
		private  void HandleBookSettings(ApiRequest request)
		{
			switch (request.HttpMethod)
			{
				case HttpMethods.Get:
					dynamic settings = new ExpandoObject();
					settings.isRecordedAsLockedDown = _bookSelection.CurrentSelection.RecordedAsLockedDown;
					settings.unlockShellBook = _bookSelection.CurrentSelection.TemporarilyUnlocked;
					settings.currentToolBoxTool = _bookSelection.CurrentSelection.BookInfo.CurrentTool;
					settings.isTemplateBook = GetIsBookATemplate();
					request.ReplyWithJson((object)settings);
					break;
				case HttpMethods.Post:
					//note: since we only have this one value, it's not clear yet whether the panel involved here will be more of a
					//an "edit settings", or a "book settings", or a combination of them.
					settings = DynamicJson.Parse(request.RequiredPostJson());
					_bookSelection.CurrentSelection.TemporarilyUnlocked = settings["unlockShellBook"];
					_pageRefreshEvent.Raise(PageRefreshEvent.SaveBehavior.SaveBeforeRefresh);
					UpdateBookTemplateMode(settings.isTemplateBook);
					request.Succeeded();
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		private bool GetIsBookATemplate()
		{
			return _bookSelection.CurrentSelection.BookInfo.Type == Book.Book.BookType.Template;
		}

		private void UpdateBookTemplateMode(bool isTemplateBook)
		{
			_bookSelection.CurrentSelection.BookInfo.Type = isTemplateBook ?
			Book.Book.BookType.Template : Book.Book.BookType.Publication;

			/* TODO (non-exhaustive)
			 * Actually make changes to the pages of the book.
			 * Don't lose setting after reopening book.
			 * Add visual feedback that this is a template
			 * Add UI pointer to more help on this topic.
			 * 
			 * Other things to think about/test
				User modified styles
				filename endcoding tests
				src vs vern collections
				same name in src collections already
				multiple copies (different versions) in src collections
				"template" in name?
				new pages as extra?
			 */
		}
	}
}
