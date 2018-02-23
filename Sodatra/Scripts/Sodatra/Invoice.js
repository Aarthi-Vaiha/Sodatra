
function TextFileUpload()
{
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
            url: url_GetTextFilesData,
            type: "POST",
            contentType: false, // Not to set any content header  
            processData: false, // Not to process data  
            data: fileData,
            success: function (data) {
                if (data != null) {
                    SaveHistory("file", data.split('$')[0], data.split('$')[1]);
                }
            },
            error: function (err) {
                alert(err.statusText);
            }
        });
    } else {
        alert("FormData is not supported.");
    }
}

