class CalledByCSharp {

  handleUndo(): void {
    // Stuff "in the toolbox" (not clear what that means) gets its own undo handling
    var contentWindow = this.getToolboxContent();
    if (contentWindow && contentWindow.model && contentWindow.model.shouldHandleUndo()) {
          contentWindow.model.undo();
    } // elsewhere, we try to ask ckEditor to undo, else just the document
    else{
        var ckEditorUndo = this.ckEditorUndoCommand();
        if (ckEditorUndo === null || !ckEditorUndo.exec()) {
            //sometimes ckEditor isn't active, so it wasn't paying attention, so it can't do the undo. So ask the document to do an undo:
            (<any>this.getPageContent()).document.execCommand('undo', false, null);
        }
    }
  }

 ckEditorUndoCommand(): any {
     try {
         return (<any>this.getPageContent()).CKEDITOR.instances.editor1.commands.undo;
     }
     catch (e) {
         return null;
     }
 }

  canUndo(): string {
    var contentWindow = this.getToolboxContent();
    if (contentWindow && contentWindow.model && contentWindow.model.shouldHandleUndo()) {
          return contentWindow.model.canUndo();
    }
    /* I couldn't find a way to ask ckeditor if it is ready to do an undo.
      The "canUndo()" is misleading; what it appears to mean is, can this command (undo) be undone?*/

    /*  var ckEditorUndo = this.ckEditorUndoCommand();
        if (ckEditorUndo === null) return 'fail';
        return ckEditorUndo.canUndo() ? 'yes' : 'no';
    */

      return "fail"; //go ask the browser
  }

  pageSelectionChanging() {
    var contentWindow = this.getPageContent();
    contentWindow['pageSelectionChanging']();
  }

  disconnectForGarbageCollection() {
      var contentWindow = this.getPageContent();
      contentWindow['disconnectForGarbageCollection']();
  }

  loadReaderToolSettings(settings: string, bookFontName: string) {

    var contentWindow = this.getToolboxContent();
    if (!contentWindow) return;

    if (typeof contentWindow['initializeSynphony'] === 'function')
      contentWindow['initializeSynphony'](settings, bookFontName);
  }

  setSampleTextsList(fileList: string) {
    this.invokeToolboxWithOneParameter('setTextsList', fileList);
  }

  setSampleFileContents(fileContents: string) {
    this.invokeToolboxWithOneParameter('setSampleFileContents', fileContents);
  }

  setCopyrightAndLicense(contents) {

    var contentWindow = this.getPageContent();
    if (!contentWindow) return;

    if (typeof contentWindow['SetCopyrightAndLicense'] === 'function')
      contentWindow['SetCopyrightAndLicense'](contents);
  }

    cleanupAudio() {
        var contentWindow = this.getPageContent();
        if (!contentWindow) return;
        if (typeof contentWindow['cleanupAudio'] === 'function') {
            contentWindow['cleanupAudio']();
        }
    }

    setPeakLevel(level: string) {
        var toolboxWindow = this.getToolboxContent();
        if (!toolboxWindow) return;
        if (typeof toolboxWindow['setPeakLevel'] === 'function') {
            toolboxWindow['setPeakLevel'](level);
        }
    }

  removeSynphonyMarkup() {

    var page = this.getPageContent();
    if (!page) return;

    if ((typeof page['jQuery'] !== 'undefined') && (page['jQuery'].fn.removeSynphonyMarkup))
      page['jQuery']('.bloom-content1').removeSynphonyMarkup();
  }

  invokeToolboxWithOneParameter(functionName: string, value: string) {

    var contentWindow = this.getToolboxContent();
    if (!contentWindow) return;

    if (typeof contentWindow[functionName] === 'function')
      contentWindow[functionName](value);
  }

  getPageContent(): Window {
    var page = <HTMLIFrameElement>document.getElementById('page');
    return (page) ? page.contentWindow : null;
  }

  getToolboxContent(): ReaderToolsWindow {
    var toolbox = <HTMLIFrameElement>document.getElementById('toolbox');
    return (toolbox) ? <ReaderToolsWindow>toolbox.contentWindow : null;
  }
}
