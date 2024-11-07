function EnviarXML()
{
    var fd = new FormData();
    fd.append('xmlFile', $('#DocumentUploadTB')[0].files[0]);

    $.ajax({
        url: "https://localhost:7002/api/Lector",
        type: "POST",
        data: fd,        
        processData: false,
        contentType: false,        
        //dataType: "json",
        success: function (result) {
            
            if (result.exitoso)
            {
                $("#filePDF").attr("src", 'data:application/pdf;base64,' + result.pdf);
                $('#myModalX').modal('show');
            } else {
                alert('ERROR:' + result.mensajeError);
            }
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}
function cerrarModalX() {
    $('#myModalX').modal('hide');
}