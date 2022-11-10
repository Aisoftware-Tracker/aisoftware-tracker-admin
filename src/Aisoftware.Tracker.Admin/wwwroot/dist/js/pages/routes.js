function generateFileName(reportName) {
    const now = new Date(); 
    const day = addZero(now.getDate());
    const month = addZero(now.getMonth() + 1);
    const hour = addZero(now.getHours());
    const min = addZero(now.getMinutes());
    return `${reportName}_${now.getFullYear()}${month}${day}_${hour}${min}`;
}

function addZero(date) {
    return date.toString().length < 2 ? `0${date}` : `${date}`;
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

function exportCsvEvents(reports) {
    let csvContent = "";
    const headers = ["Id", "Placa", "Hora do Servidor", "Tipo", "Endereco"];

    let rows = reports;
    rows.unshift(headers);

    rows.forEach(function(rowArray) {
        rowArray[3] = removeAccent(rowArray[3].replace( /(<([^>]+)>)/ig, "").trim());
        let row = rowArray.join(";");
        csvContent += row + "\r\n";
    });

    return csvContent;
}

function exportCsvRoutes(reports) {
    const uncheck = "<input class=\"check-box\" disabled=\"disabled\" type=\"checkbox\">";
    const check = "<input checked=\"checked\" class=\"check-box\" disabled=\"disabled\" type=\"checkbox\">";
    
    let csvContent = "";

    const headers = ["Id", "Placa", "Protocolo", "Horario do Dispositivo", "Horario Corrigido", "Horario do Servidor", "Vencimento", "Valido", "Latitude", "Longitude", "Altitude", "Velociadade", "Endereco", "Irregularidade", "Ignicao", "Status", "Distancia", "Distancia Total /Km", "Movimentacao", "Horas"];

    let rows = reports;
    rows.unshift(headers);

    rows.forEach(function(rowArray) {

        if(rowArray[0] !== "Id") {
            rowArray[6] = rowArray[6] === uncheck ? "Desatualizado" : "Atualizado";
            rowArray[7] = rowArray[7] === check ? "Sim" : "Nao";
            rowArray[12] = removeAccent(rowArray[12]);
            rowArray[14] = rowArray[14] === check ? "Ligado" : "Desligado";
            rowArray[18] = rowArray[18] === check ? "Em Movimento" : "Parado";
        }

        let row = rowArray.join(";");
        csvContent += row + "\r\n";
    });

    return csvContent;
}

function exportCsvSummary(reports) {
    let csvContent = "";
    const headers = ["Nome do Dispositivo", "Placa", "Velocidade Maxima", "Velocidade Media", "Distancia", "Combistivel", "Tempo de Motor", "Odometro Inicial", "Odometro Final"];

    let rows = reports;
    rows.unshift(headers);

    rows.forEach(function(rowArray) {
        let row = rowArray.join(";");
        csvContent += row + "\r\n";
    });

    return csvContent;
}

function isNullOrEmpty(value) {
    return (value === null) || (value === "undefined") || (value === "" || value === " ");
}

function removeAccent(value)
{
    if (!isNullOrEmpty(value))
    {
        return value?.replace("ç", "c")
                    ?.replace("ã", "a")
                    ?.replace("õ", "o")
                    ?.replace("á", "a")
                    ?.replace("é", "e")
                    ?.replace("í", "i")
                    ?.replace("ó", "o")
                    ?.replace("ú", "u")
                    ?.replace("â", "a")
                    ?.replace("ê", "e")
                    ?.replace("ô", "o")
                    ?.replace("à", "a");
    }
    return "";
}
