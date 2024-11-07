$(document).ready(function () {
    loadDataM();
});

function loadDataM() {    
    $.ajax({
        url: "https://localhost:7002/api/medicamentos",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            var html = '';
            $.each(result, function (key, item) {
                html += '<tr>';
                html += '<td>' + item.idmedicamento + '</td>';
                html += '<td>' + item.nombre + '</td>';
                html += '<td>' + item.concentracion + '</td>';
                html += '<td>' + item.formasfarmaceuticas.nombre + '</td>';
                html += '<td>' + item.precio + '</td>';
                html += '<td>' + item.presentacion + '</td>';
                html += '<td>' + (item.bhabilitado ==='1'?"activo":"inactivo") + '</td>';
                html += '<td><a href="#" onclick="return getbyIDM(' + item.idmedicamento + ')">Editar</a> | <a href="#" onclick="DeleleM(' + item.idmedicamento + ')">Eliminar</a></td>';
                html += '</tr>';
            });
            $('.tbodyM').html(html);
            $('.data_tableM').DataTable();
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });

 
}
function AddM() {
    var medObj = {
        idmedicamento: 0,
        nombre: $('#nombre').val(),
        concentracion: $('#concentracion').val(),
        idformafarmaceutica: $('#idformafarmaceutica').val(),
        precio: $('#precio').val(),
        stock: $('#stock').val(),
        presentacion: $('#presentacion').val(),
        bhabilitado: '1'
    };
    $.ajax({
        url: "https://localhost:7002/api/medicamentos",
        data: JSON.stringify(medObj),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            loadDataM();
            $('#myModalM').modal('hide');
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}
function getbyIDM(idmedicamento) {
    $('#Name').css('border-color', 'lightgrey');
    $('#Age').css('border-color', 'lightgrey');
    $('#State').css('border-color', 'lightgrey');
    $('#Country').css('border-color', 'lightgrey');
    $.ajax({
        url: "https://localhost:7002/api/medicamentos/" + idmedicamento,
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            $('#idmedicamento').val(result.idmedicamento);
            $('#nombre').val(result.nombre);
            $('#concentracion').val(result.concentracion);
            $('#idformafarmaceutica').val(result.formasfarmaceuticas.nombre); 
            $('#precio').val(result.precio);
            $('#stock').val(result.stock);
            $('#presentacion').val(result.presentacion);
            $('#bhabilitado').val((result.estatus === '1' ? "activo" : "inactivo"));

            $('#myModalM').modal('show');
            $('#btnUpdate').show();
            $('#btnAdd').hide();
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
    return false;
}
function UpdateM() {
    var res = validate();
    if (res == false) {
        return false;
    }
    var medObj = {
        idmedicamento: $('#idmedicamento').val(),
        nombre: $('#nombre').val(),
        concentracion: $('#concentracion').val(),
        idformafarmaceutica: $('#idformafarmaceutica').val(),
        precio: $('#precio').val(),
        stock: $('#stock').val(),
        presentacion: $('#presentacion').val(),
        bhabilitado: '1'
    };
    $.ajax({
        url: "https://localhost:7002/api/medicamentos/",
        data: JSON.stringify(medObj),
        type: "PUT",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            loadDataM();
            $('#myModalM').modal('hide');
            $('#idusuario').val("");
            $('#nombre').val("");
            $('#usuario').val("");
            $('#password').val("");
            $('#fechacreacion').val("");
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}
function DeleleM(ID) {
    var ans = confirm("¿Estás seguro de eliminar este registro?");
    if (ans) {
        $.ajax({
            url: "https://localhost:7002/api/medicamentos/" + ID,
            type: "DELETE",
            contentType: "application/json;charset=UTF-8",
            dataType: "json",
            success: function (result) {
                loadDataM();
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });
    }
}
function clearTextBoxM() {
    $('#idmedicamento').val("");
    $('#nombre').val("");
    $('#concentracion').val("");
    $('#idformafarmaceutica').val("");
    $('#precio').val("");
    $('#stock').val("");
    $('#presentacion').val("");
    
    $('#idmedicamento').css('border-color', 'lightgrey');
    $('#nombre').css('border-color', 'lightgrey');
    $('#concentracion').css('border-color', 'lightgrey');
    $('#idformafarmaceutica').css('border-color', 'lightgrey');
    $('#precio').css('border-color', 'lightgrey');
    $('#stock').css('border-color', 'lightgrey');
    $('#presentacion').css('border-color', 'lightgrey');

    $('#btnUpdate').hide();
    $('#btnAdd').show();
    $('#myModalM').modal('show');
}
function cerrarModalM() {
    $('#myModalM').modal('hide');
}