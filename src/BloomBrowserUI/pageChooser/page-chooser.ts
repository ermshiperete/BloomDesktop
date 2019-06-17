﻿/// <reference path="../lib/localizationManager/localizationManager.ts" />
import * as $ from "jquery";
import * as React from "react";
import * as ReactDOM from "react-dom";
import theOneLocalizationManager from "../lib/localizationManager/localizationManager";
import "jquery-ui/jquery-ui-1.10.3.custom.min.js";
import axios from "axios";
import { BloomApi } from "../utils/bloomApi";
import "errorHandler";
import TemplatePagePreview from "./templatePagePreview";
import { getEditViewFrameExports } from "../bookEdit/js/bloomFrames";
import { ThemeProvider } from "@material-ui/styles";
import theme from "../bloomMaterialUITheme";

$(window).ready(() => {
    BloomApi.get("pageTemplates", result => {
        const templatesJSON = result.data;
        const pageChooser = new PageChooser(JSON.stringify(templatesJSON));
        pageChooser.loadPageGroups();
    });
});

// latest version of the expected JSON initialization string (from PageTemplatesApi.HandleTemplatesRequest)
// "{\"defaultPageToSelect\":\"(guid of template page)\",
//   \"orientation\":\"landscape\",
//   \"groups\":[{\"templateBookFolderUrl\":\"/bloom/localhost/C$/BloomDesktop/DistFiles/factoryGroups/Templates/Basic Book\",
//                     \"templateBookUrl\":\"/bloom/localhost/C$/BloomDesktop/DistFiles/factoryGroups/Templates/Basic Book/Basic Book.htm\"}]}"

export class PageChooser {
    private _templateBookUrls: string;
    private _defaultPageToSelect: string;
    private _orientation: string;
    private _selectedGridItem: JQuery;
    private _indexOfPageToSelect: number;
    private _scrollingDiv: JQuery;
    private _scrollTopOfTheScrollingDiv: number;
    private _forChooseLayout: boolean;

    constructor(initializationJsonString: string) {
        let initializationObject: object;
        if (initializationJsonString) {
            try {
                initializationObject = $.parseJSON(initializationJsonString);
            } catch (e) {
                alert("Received bad JSON string: " + e);
                return;
            }
            this._templateBookUrls = initializationObject["groups"];
            this._defaultPageToSelect =
                initializationObject["defaultPageToSelect"];
            this._orientation = initializationObject["orientation"];
            this._forChooseLayout = initializationObject["forChooseLayout"];
        } else {
            alert("Expected url in PageChooser ctor!");
        }

        this._selectedGridItem = $();
        this._indexOfPageToSelect = 0;
        this._scrollTopOfTheScrollingDiv = 0;
    }

    private thumbnailClickHandler(clickedDiv, evt): void {
        // 'div' is an .invisibleThumbCover
        // Select new thumbnail
        const newsel = this.findProperElement(clickedDiv, evt);
        if (newsel == null) return;
        // Mark any previously selected thumbnail as no longer selected
        if (this._selectedGridItem != undefined) {
            $(this._selectedGridItem).removeClass("ui-selected");
        }
        this._selectedGridItem = newsel;
        $(this._selectedGridItem).addClass("ui-selected");

        // Scroll to show it (useful for original selection). So far this only scrolls DOWN
        // to make sure we can see the BOTTOM of the clicked item; that's good enough for when
        // we open the dialog and a far-down item is selected, and marginally helpful when we click
        // an item partly scrolled off the bottom. There's no way currently to select an item
        // that's entirely scrolled off the top, and it doesn't seem worth the complication
        // to force a partly-visible one at the top to become wholly visible.
        const container = $(".gridItemDisplay");
        const positionOfTopOfSelected =
            $(this._selectedGridItem).offset().top + container.scrollTop();
        const positionOfBottomOfSelected =
            $(this._selectedGridItem).height() + positionOfTopOfSelected;
        if (
            container.height() + container.scrollTop() <
            positionOfBottomOfSelected
        ) {
            container.scrollTop(
                positionOfBottomOfSelected - container.height()
            );
        }

        // Display large preview
        // This is now done with React via the TemplatePagePreview component.
        // Localization will happen there, so we just send english strings from the page templates.
        const englishCaptionText = $(
            ".gridItemCaption",
            this._selectedGridItem
        ).text();
        const englishPageDescription = $(
            ".pageDescription",
            this._selectedGridItem
        ).text();
        const isEnterprise = $(this._selectedGridItem).hasClass(
            "enterprise-only-flag"
        );
        const imgSrc = $(this._selectedGridItem)
            .find("img")
            .first()
            .attr("src");
        const previewElement = document.getElementById(
            "singlePagePreview"
        ) as HTMLDivElement;
        ReactDOM.render(
            React.createElement(
                ThemeProvider,
                { theme: theme },
                React.createElement(TemplatePagePreview, {
                    caption: englishCaptionText,
                    imageSource: imgSrc,
                    pageDescription: englishPageDescription,
                    pageIsDigitalOnly: this.isDigitalOnly(
                        this._selectedGridItem[0]
                    ),
                    pageIsEnterpriseOnly: isEnterprise,
                    templateBookPath: this._selectedGridItem
                        .closest(".group")
                        .attr("data-template-book-path"),
                    pageId: this._selectedGridItem.attr("data-pageId"),
                    forChangeLayout: this._forChooseLayout,
                    willLoseData: this._forChooseLayout
                        ? this.willLoseData()
                        : false
                })
            ),
            previewElement
        );
    } // thumbnailClickHandler

    // Return true if choosing the current layout will cause loss of data
    private willLoseData(): boolean {
        const selected = $(this._selectedGridItem);
        const selectedTemplateTranslationGroupCount = parseInt(
            selected.attr("data-textDivCount"),
            10
        );
        const selectedTemplatePictureCount = parseInt(
            selected.attr("data-pictureCount"),
            10
        );
        const selectedTemplateVideoCount = parseInt(
            selected.attr("data-videoCount"),
            10
        );

        const page = <HTMLIFrameElement>(
            window.parent.document.getElementById("page")
        );
        const current =
            page && page.contentWindow ? $(page.contentWindow.document) : $();
        const currentTranslationGroupCount = this.countTranslationGroupsForChangeLayout(
            current
        );
        const currentPictureCount = current.find(".bloom-imageContainer")
            .length;
        // ".bloom-videoContainer:not(.bloom-noVideoSelected)" is not working reliably as a selector.
        // It's also insufficient if we allow the user to change multiple pages at once to look at
        // only the current page for content.  Not checking for actual video content matches what is
        // done for text and pictures, and means that the check is equally valid for any number of
        // pages with the same layout.  See https://issues.bloomlibrary.org/youtrack/issue/BL-6921.
        const currentVideoCount = current.find(".bloom-videoContainer").length;

        return (
            selectedTemplateTranslationGroupCount <
                currentTranslationGroupCount ||
            selectedTemplatePictureCount < currentPictureCount ||
            selectedTemplateVideoCount < currentVideoCount
        );
    }

    private isDigitalOnly(element: HTMLElement): boolean {
        const classList = element.classList;
        return (
            classList.contains("bloom-nonprinting") &&
            !classList.contains("bloom-noreader")
        );
    }

    // There's a bug deep in javascript that doesn't take into account the scrolling
    // of a div element before something inside it is clicked on.  The following code
    // detects whether the scrolling has changed since the last mouse click, and if so,
    // searches for the item which should have matched.  For the initial bug report,
    // see https://silbloom.myjetbrains.com/youtrack/issue/BL-2623.
    // Note that the offset().top values returned by jquery properly take into account
    // the scrollTop of the scrolling parent div.  Which makes me think the bug may be
    // below the jquery level!?
    private findProperElement(clickedDiv, evt): JQuery {
        const gridItem = $(clickedDiv).parent();
        if (evt) {
            const currentScrollTop = this._scrollingDiv.scrollTop();
            if (currentScrollTop !== this._scrollTopOfTheScrollingDiv) {
                // The scrolling position has changed, so we need to explicitly search
                // for the proper object.
                const y = evt["clientY"]; // retrieve the original click position
                const x = evt["clientX"];
                const container = $(clickedDiv)
                    .parent()
                    .parent();
                const childs = $(container).children();
                for (let i = 0; i < childs.length; ++i) {
                    const child = childs.eq(i);
                    const top = child.offset().top;
                    const bottom = top + child.height();
                    const left = child.offset().left;
                    const right = left + child.width();
                    if (top <= y && y <= bottom && left <= x && x <= right) {
                        // Remember the new scroll position and return the proper object.
                        this._scrollTopOfTheScrollingDiv = currentScrollTop;
                        return child;
                    }
                }
                // We couldn't find the proper object, so don't do anything.  The user
                // apparently clicked on a visually empty spot that got misidentified.
                return $();
            }
        }
        return gridItem;
    }

    // Set the text of the given element to the appropriate localization of defaultText
    // (or to defaultText, if no localization is available).
    // If defaultText is empty, set the element text to empty.
    // The localization ID to look up is made by concatenating the supplied prefix and the id
    // parameter, which defaults to the defaultText since we often use the English text of a
    // label as the last part of its ID.
    private setLocalizedText(
        elt: JQuery,
        idPrefix: string,
        defaultText: string,
        id: string = defaultText
    ) {
        defaultText = defaultText.trim();
        if (defaultText) {
            theOneLocalizationManager
                .asyncGetText(
                    idPrefix + id,
                    defaultText,
                    elt.attr("l10nComment")
                )
                .done(translation => {
                    elt.text(translation);
                });
        } else {
            elt.text("");
        }
    }

    public static addPageClickHandler(
        forChangeLayout: boolean,
        pageId: string,
        templateBookPath: string,
        convertAnywayChecked: boolean,
        willLoseData: boolean,
        convertWholeBookChecked: boolean
    ): void {
        if (forChangeLayout) {
            if (willLoseData && !convertAnywayChecked) {
                return;
            }
            BloomApi.postData(
                "changeLayout",
                {
                    pageId: pageId,
                    templateBookPath: templateBookPath,
                    convertWholeBook: convertWholeBookChecked
                },
                PageChooser.closeup
            );
        } else {
            BloomApi.postData(
                "addPage",
                {
                    templateBookPath: templateBookPath,
                    pageId: pageId,
                    convertWholeBook: false
                },
                PageChooser.closeup
            );
        }
    }

    private static closeup(): void {
        // End the disabling of other panes for the modal dialog. The final argument is because in this
        // method the current window is the dialog, and it's the parent window's document that is being
        // monitored for this event.
        fireCSharpEvent("setModalStateEvent", "false", parent.window);
        // this fails with a message saying the dialog isn't initialized. Apparently a dialog must be closed
        // by code loaded into the window that opened it.
        //$(parent.document.getElementById('addPageConfig')).dialog('close');
        getEditViewFrameExports().closeDialog("addPageConfig");
    }

    // This is the starting-point method that is invoked to initialize the dialog.
    // At the point where it is called, the json parameters that control what will be displayed
    public loadPageGroups(): void {
        // Save a reference to the scrolling div that contains the various page items.
        this._scrollingDiv = $(".gridItemDisplay", document);

        // Originally (now maybe YAGNI) the dialog handled more than one group of template pages.
        // Right now it only handles one, so the cloning of stub html is perhaps unnecessary,
        // but I've left it in case we need it later.

        // Save html sections that will get cloned later
        // there should only be one 'group' at this point; a stub with one default template page
        const groupHtml = $(".group", document)
            .first()
            .clone();
        // there should only be the one default 'gridItem' at this point
        const gridItemHtml = $(".gridItem", groupHtml)
            .first()
            .clone();
        if ($(this._templateBookUrls).length > 0) {
            // Remove original stub section
            $(".outerGroupContainer", document).empty();
            this.loadNextPageGroup(
                this._templateBookUrls,
                groupHtml,
                gridItemHtml,
                this._defaultPageToSelect,
                0
            );
        }

        if (this._orientation === "landscape") {
            $("#mainContainer").addClass("landscape");
        }
    } // loadPageGroups

    // This pops one template book order from the queue, does the async get,
    // loads it in the dialog, then recursively goes back for another.
    // Doing one at a time does two things for us. First, it makes the
    // books get added in the order we want (which we couldn't control if we ask for them all
    // at once). Secondly, it ensures we get the most important template pages shown and ready
    // to use as quickly as possible.
    private loadNextPageGroup(
        queue,
        groupHTML,
        gridItemHTML,
        defaultPageToSelect: string,
        previousPagesCount: number
    ): void {
        const order = queue.shift();
        if (!order) {
            return; // no more to get
        }
        axios
            .get("/bloom/" + order.templateBookPath)
            .then(result => {
                const pageData = result.data;

                // Grab all pages in this group
                // N.B. normal selector syntax or .find() WON'T work here because pageData is not yet part of the DOM!
                // Creating a jquery object via $(pageData) causes any img elements in the html string to be dereferenced,
                // which can cause the Bloom server to complain about not finding files of the form "pageChooser/read.png".
                // So we must remove the img elements from the returned string before the conversion to a jquery object.
                // Note that none of the img elements in the template file are needed at this point for laying out the
                // Add Page dialog, or for creating thumbnails, so it's safe to delete them.  See
                // https://silbloom.myjetbrains.com/youtrack/issue/BL-3819 for details of the symptoms experienced when
                // running Bloom without this ugly hack.
                const pageNoImg = (<string>pageData).replace(
                    /<img[^>]*><\/img>/g,
                    ""
                );
                let pages = $(pageNoImg)
                    .filter(".bloom-page[id]")
                    .filter('[data-page="extra"]');

                if (pages.length == 0) {
                    console.log(
                        "Could not find any template pages in " +
                            order.templateBookPath
                    );
                    //don't add a group for books that don't have template pages; just move on.
                    // (This will always be true for a newly created template.)
                    this.loadNextPageGroup(
                        queue,
                        groupHTML,
                        gridItemHTML,
                        defaultPageToSelect,
                        previousPagesCount
                    );
                    return; // suppress adding this group.
                }

                const dataBookArray = $(
                    "div[data-book='bookTitle']",
                    pageNoImg
                );
                const groupTitle = $(dataBookArray.first()).text();
                // Add title and container to dialog
                const groupToAdd = $(groupHTML).clone();
                groupToAdd.attr(
                    "data-template-book-path",
                    order.templateBookPath
                );
                const captionDiv = $(groupToAdd).find(".groupCaption");
                this.setLocalizedText(
                    captionDiv,
                    "TemplateBooks.BookName.",
                    groupTitle
                );
                $(".outerGroupContainer", document).append(groupToAdd);

                if (this._forChooseLayout) {
                    // This filters out the (empty) custom page, which is currently never a useful layout change, since all data would be lost.
                    pages = pages.not(
                        '.bloom-page[id="5dcd48df-e9ab-4a07-afd4-6a24d0398386"]'
                    );
                }
                //console.log("loadPageFromGroup("+order.templateBookFolderUrl+")");
                this.loadPageFromGroup(
                    groupToAdd,
                    pages,
                    gridItemHTML,
                    order.templateBookFolderUrl,
                    defaultPageToSelect,
                    previousPagesCount
                );
                const pagesCountSoFar = previousPagesCount + $(pages).length;

                this.loadNextPageGroup(
                    queue,
                    groupHTML,
                    gridItemHTML,
                    defaultPageToSelect,
                    pagesCountSoFar
                );
            })
            .catch(e => {
                //we don't really want to let one bad template keep us from showing others.
                // Insert a message into the dialog
                const path = order.templateBookPath;
                const index = path.lastIndexOf("/");
                const templateName = path.substring(index + 1, path.length);
                const templateTitle = templateName.replace(".html", "");
                const groupToAdd = $(groupHTML).clone();
                this.setLocalizedText(
                    $(groupToAdd).find(".groupCaption"),
                    "TemplateBooks.BookName.",
                    templateTitle
                );
                const innerGroup = groupToAdd.find(".innerGroupContainer");
                innerGroup.remove();
                groupToAdd.append("<div id='missingMsg'/>");
                theOneLocalizationManager
                    .asyncGetText(
                        "EditTab.AddPageDialog.NoTemplate",
                        "Could not find {0}",
                        ""
                    )
                    .done(translation => {
                        groupToAdd
                            .find("#missingMsg")
                            .text(translation.replace("{0}", templateName));
                    });
                $(".outerGroupContainer", document).append(groupToAdd);

                this.loadNextPageGroup(
                    queue,
                    groupHTML,
                    gridItemHTML,
                    defaultPageToSelect,
                    previousPagesCount
                );
            });
    }

    private loadPageFromGroup(
        currentGroup,
        pageArray,
        gridItemTemplate,
        templateBookFolderUrl,
        defaultPageToSelect: string,
        previousPagesCount: number
    ): void {
        if ($(pageArray).length < 1) {
            console.log("pageArray empty for " + templateBookFolderUrl);
            return;
        }

        // Remove default template page
        $(".innerGroupContainer", currentGroup).empty();
        let gotSelectedPage = false;
        // insert a template page for each page with the correct #id on the url
        $(pageArray).each((index, div) => {
            const divQuery = $(div);
            if (divQuery.attr("data-page") === "singleton") return; // skip this one

            const currentGridItemHtml = $(gridItemTemplate).clone();

            if (divQuery.hasClass("enterprise-only")) {
                currentGridItemHtml.addClass("enterprise-only-flag");
            }
            const currentId = divQuery.attr("id");
            currentGridItemHtml.attr("data-pageId", currentId);
            currentGridItemHtml.attr(
                "data-textDivCount",
                this.countTranslationGroupsForChangeLayout(divQuery)
            );
            currentGridItemHtml.attr(
                "data-pictureCount",
                divQuery.find(".bloom-imageContainer").length
            );
            currentGridItemHtml.attr(
                "data-videoCount",
                divQuery.find(".bloom-videoContainer").length
            );

            // The check for _indexOfPageToSelect here keeps the selection on the *first* matching page. In BL-4500, we found
            // that different templates could reuse the same guid for custom page. That's a problem probably should be
            // sorted out, but it's out "in the wild" in the Story Primer, so we have to have a fix that doesn't depend
            // on what templates the user has installed.
            if (
                currentId === defaultPageToSelect &&
                this._indexOfPageToSelect == 0
            ) {
                this._indexOfPageToSelect = index + previousPagesCount;
                gotSelectedPage = true;
            }

            const pageDescription = $(".pageDescription", div)
                .first()
                .text();
            $(".pageDescription", currentGridItemHtml)
                .first()
                .text(pageDescription);

            // We can use these classes to determine how to display the preview.
            // Currently, this is used to determine if a template page is digital-only
            // (meaning not for PDF publication) which results in a message to the user.
            // See the classes referenced in isDigitalOnly().
            Array.from(div.classList).forEach(cssClass => {
                if (cssClass.startsWith("bloom-"))
                    currentGridItemHtml.addClass(cssClass);
            });

            const pageLabel = $(".pageLabel", div)
                .first()
                .text()
                .trim();
            $(".gridItemCaption", currentGridItemHtml)
                .first()
                .text(pageLabel);

            const possibleImageUrl = this.getPossibleImageUrl(
                templateBookFolderUrl,
                pageLabel
            );
            $("img", currentGridItemHtml).attr("src", possibleImageUrl);

            $(".innerGroupContainer", currentGroup).append(currentGridItemHtml);
        }); // each
        // once the template pages are installed, attach click handler to them.
        $(".invisibleThumbCover", currentGroup).each((index, div) => {
            $(div).dblclick(() => {
                if (!this._selectedGridItem || !this._templateBookUrls) {
                    return;
                }
                const convertAnywayCheckbox = document.getElementById(
                    "convertAnywayCheckbox"
                ) as HTMLInputElement;
                const convertWholeBookCheckbox = document.getElementById(
                    "convertWholeBookCheckbox"
                ) as HTMLInputElement;
                PageChooser.addPageClickHandler(
                    this._forChooseLayout,
                    this._selectedGridItem.attr("data-pageId"),
                    this._selectedGridItem
                        .closest(".group")
                        .attr("data-template-book-path"),
                    convertAnywayCheckbox
                        ? convertAnywayCheckbox.checked
                        : false,
                    this.willLoseData(),
                    convertWholeBookCheckbox
                        ? convertWholeBookCheckbox.checked
                        : false
                );
            }); // invisibleThumbCover double click

            $(div).click(evt => {
                this.thumbnailClickHandler(div, evt);
            }); // invisibleThumbCover click
        }); // each
        // If we found the specified page to select in this group, it is the one indicated by
        // this._indexOfPageToSelect; select that now.
        // In case we were not provided with a default page to select, this._indexOfPageToSelect remains 0,
        // and if this is the first group we go ahead and select its first page.
        if (
            gotSelectedPage ||
            (defaultPageToSelect === "" && previousPagesCount == 0)
        ) {
            this.thumbnailClickHandler(
                $(".invisibleThumbCover").eq(this._indexOfPageToSelect),
                null
            );
        }
    } // loadPageFromGroup

    // We want to count all the translationGroups that do not occur inside of a bloom-imageContainer div.
    // The reason for this is that images can have textOverPicture divs and imageDescription divs inside of them
    // and these are completely independent of the template page. We need to count regular translationGroups and
    // also ensure that translationGroups inside of images get migrated correctly. If this algorithm changes, be
    // sure to also change 'GetTranslationGroupsInternal()' in HtmlDom.cs.
    private countTranslationGroupsForChangeLayout(pageDiv: JQuery): number {
        return pageDiv
            .find(".bloom-translationGroup:not(.box-header-off)")
            .filter(
                (index, elt) => elt.closest(".bloom-imageContainer") == null
            ).length;
    }

    private getPossibleImageUrl(
        templateBookFolderUrl: string,
        pageLabel: string
    ): string {
        const label = pageLabel.replace("&", "+"); //ampersands confuse the url system
        // The result may actually be a png file or an svg, and there may be some delay while the png is generated.

        //NB:  without the generateThumbnaiIfNecessary=true, we can run out of worker threads and get deadlocked.
        //See EnhancedImageServer.IsRecursiveRequestContext
        return (
            "/bloom/api/pageTemplateThumbnail/" +
            templateBookFolderUrl +
            "/template/" +
            label +
            (this._orientation === "landscape" ? "-landscape" : "") +
            ".svg?generateThumbnaiIfNecessary=true"
        );
    }
} // End OF PageChooserClass

export function addPageClickHandler(
    forChangeLayout: boolean,
    pageId: string,
    templateBookPath: string,
    convertAnywayChecked: boolean,
    willLoseData: boolean,
    convertWholeBookChecked: boolean
) {
    PageChooser.addPageClickHandler(
        forChangeLayout,
        pageId,
        templateBookPath,
        convertAnywayChecked,
        willLoseData,
        convertWholeBookChecked
    );
}

// Fires an event for C# to handle
// Enhance: JT notes that this method pops up from time to time; can we consolidate?
function fireCSharpEvent(
    eventName: string,
    eventData: string,
    dispatchWindow?: Window
) {
    const event = new MessageEvent(eventName, {
        /*'view' : window,*/ bubbles: true,
        cancelable: true,
        data: eventData
    });
    if (dispatchWindow) {
        dispatchWindow.document.dispatchEvent(event);
    } else {
        document.dispatchEvent(event);
    }
}
