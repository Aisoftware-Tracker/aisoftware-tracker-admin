function generateFileName(reportName) {
    const now = new Date(); 
    const day = addZero(now.getDate());
    const month = addZero(now.getMonth() + 1);
    const hour = addZero(now.getHours());
    const min = addZero(now.getMinutes());
    return `${reportName}_${now.getFullYear()}${month}${day}_${hour}${min}`;
}

function addZero(date) {
    date.toLocaleString().length < 2 ? `0${date}` : `${date}`
}

function downloadFile(fileName, csv) {
    const href = 'data:text/csv;charset=UTF-8,' + encodeURIComponent(csv);
    download(fileName, href)
}

function downloadFileHref(fileName, href) {
    download(fileName, href)
}

function download(fileName, href) {
    const aLink = document.createElement('a');
    const evt = document.createEvent("MouseEvents");
    evt.initMouseEvent("click", true, true, window, 0, 0, 0, 0, 0, false, false, false, false, 0, null);
    aLink.download = fileName;
    aLink.href = href;
    aLink.dispatchEvent(evt);
}