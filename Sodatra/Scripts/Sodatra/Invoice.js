$('input[type=file]').change(function () {
    $("#DescriptionList").empty();  
    if (window.FormData !== undefined) {
        var fileUpload = $("#textFile").get(0);
        var files = fileUpload.files;

        // Create FormData object  
        var fileData = new FormData();

        // Looping over all files and add it to FormData object  
        for (var i = 0; i < files.length; i++) {
            fileData.append(files[i].name, files[i]);
        }
        // Adding one more key to FormData object  
        //// fileData.append('username', ‘Manas’);  
        $.ajax({
            // url: url_UploadFiles,
            url: url_UploadFiles,
            type: "POST",
            contentType: false, // Not to set any content header  
            processData: false, // Not to process data  
            data: fileData,
            //data: { FileDirectory: $("#textFile").val() },
            dataType: 'text',
            success: function (data) {
                if (data != "")
                    TextFileUpload(data);
            },
            error: function (err) {
                alert(err.statusText);
            }
        });
    } else {
        alert("FormData is not supported.");
    }
});

function TextFileUpload(val) {
    $.ajax({
        // url: url_UploadFiles,
        url: url_GetTextFilesData,
        type: "POST",
        data: { FileDirectory: val },
        dataType: 'json',
        success: function (data) {
            AssignData(data);
        },
        error: function (err) {
            alert(err.statusText);
        }
    });
}

function AssignPersonnelData(data) {
    if(data.length != 0)
    {
        $("#Vendor").val(data.Vendor);
        $("#Importer").val(data.Importer);
        $("#NumberAV").val(data.NumberAV);
        $("#Delivery").val(data.Delivery);  
        $("#VendorAddress1").val(data.VendorAddress1);  
        $("#ImporterAddress1").val(data.ImporterAddress1);  
        $("#VendorAddress2").val(data.VendorAddress2);  
        $("#ImporterAddress2").val(data.ImporterAddress2);  
        $("#VendorAddress3").val(data.VendorAddress3);  
        $("#ImporterAddress3").val(data.ImporterAddress3);  
        $("#NumberDPI").val(data.NumberDPI);
        $("#DateDPI").val(data.DateDPI);  
        $("#DateAV").val(data.DateAV);  
        $("#VendorTelephone").val(data.VendorTelephone);  
        $("#ImporterTelephone").val(data.ImporterTelephone);  
        $("#VendorFax").val(data.VendorFax);  
        $("#ImporterFax").val(data.ImporterFax);  
        $("#VendorContact").val(data.VendorContact);  
        $("#ImporterContact").val(data.ImporterContact);  
        $("#WeightGross").val(data.WeightGross);  
        $("#WeightNet").val(data.WeightNet); 
        $("#Ninea").val(data.Ninea);  
        $("#CodePPM").val(data.CodePPM); 
        $("#CountryofOrgin").val(data.CountryofOrgin);  
        $("#TaxPayerCode").val(data.TaxPayerCode);  
        $("#Containersandleads").val(data.Containersandleads); 
        $("#NatureofGoods").val(data.NatureofGoods);  
        $("#NatureofPackaging").val(data.NatureofPackaging);  
        $("#MarksandParcelNumbers").val(data.MarksandParcelNumbers);  
        $("#PlaceofEntry").val(data.PlaceofEntry);  
        $("#At").val(data.At); 
        $("#The").val(data.The);  
        $("#Bill").val(data.Bill); 
        $("#Sure").val(data.Sure);  
        $("#Incoterm").val(data.Incoterm);  
        $("#BillofLading").val(data.BillofLading);  
        $("#TotalBill").val(data.TotalBill);  
        $("#Date").val(data.Date);
        $("#Motto").val(data.Motto);

        
        
    }
}

function AssignData(data) {
    AssignPersonnelData(data);

    var cnt = 0;
    var htm = '';

    //   $("#DescriptionList").remove();
    $("#DescriptionList").empty();
    if (data != "") {

        for (var i = 0; i < data.InvoiceList.length; i++) {

            htm += '<div class="width100">';
            htm += '<input type="hidden" name="InvoiceList.Index" value="' + cnt + '" />';
            htm += '<div class="width5">';
            htm += '<input class="form-control"    id="InvoiceList_' + cnt + '__No" name="InvoiceList[' + cnt + '].No" type="text" value="' + data.InvoiceList[cnt].No + '" />';
            htm += '</div>';
            htm += '<div class="width30 ">';
            htm += '<input class="form-control"    id="InvoiceList_' + cnt + '__Description" name="InvoiceList[' + cnt + '].Description" type="text" value="' + data.InvoiceList[cnt].Description + '" />';
            htm += '</div>';
            htm += '<div class="width15 ">';
            htm += '<input class="form-control"    id="InvoiceList_' + cnt + '__CodeSH" name="InvoiceList[' + cnt + '].CodeSH" type="text" value="' + data.InvoiceList[cnt].CodeSH + '" />';
            htm += '</div>';
            htm += '<div class="width5 ">';
            htm += '<input class="form-control"    id="InvoiceList_' + cnt + '__Usagé" name="InvoiceList[' + cnt + '].Usagé" type="text" value="' + data.InvoiceList[cnt].Usagé + '" />';
            htm += '</div>';
            htm += '<div class="width9 ">';
            htm += '<input class="form-control"    id="InvoiceList_' + cnt + '__Quantité" name="InvoiceList[' + cnt + '].Quantité" type="text" value="' + data.InvoiceList[cnt].Quantité + '" />';
            htm += '</div>';
            htm += '<div class="width6 ">';
            htm += '<input class="form-control"    id="InvoiceList_' + cnt + '__Unite" name="InvoiceList[' + cnt + '].Unite" type="text" value="' + data.InvoiceList[cnt].Unite + '" />';
            htm += '</div>';
            htm += '<div class="width10 ">';
            htm += '<input class="form-control"    id="InvoiceList_' + cnt + '__FOBattestéendevise" name="InvoiceList[' + cnt + '].FOBattestéendevise" type="text" value="' + data.InvoiceList[cnt].FOBattestéendevise + '" />';
            htm += '</div>';
            htm += '<div class="width15 ">';
            htm += '<input class="form-control"    id="InvoiceList_' + cnt + '__ValeurderéférenceenFCFA" name="InvoiceList[' + cnt + '].ValeurderéférenceenFCFA" type="text" value="' + data.InvoiceList[cnt].ValeurderéférenceenFCFA + '" />';
            htm += '</div>';
            htm += '</div>';

            cnt++;

        }
        $(htm).appendTo(DescriptionList);

    }
}