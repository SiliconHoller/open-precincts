
/**
*   Replaces the html for a small element (such as a table cell) with appropriate busy icon
*
**/
function smallElementBusy(tinyelem) {
    $(tinyelem).html("<img src='/images/buttonloader.gif' alt='Loading...'/>");
}