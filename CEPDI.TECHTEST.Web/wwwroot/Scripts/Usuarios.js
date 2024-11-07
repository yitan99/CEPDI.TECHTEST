$(document).ready(function () {
    loadData();

});

function loadData() {    
    $.ajax({
        url: "https://localhost:7002/api/usuarios",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            var html = '';
            $.each(result, function (key, item) {
                html += '<tr>';
                html += '<td>' + item.idUsuario + '</td>';
                html += '<td>' + item.nombre + '</td>';
                html += '<td>' + item.fechacreacion + '</td>';
                html += '<td>' + item.usuario + '</td>';
                html += '<td>' + item.password + '</td>';
                html += '<td>' + (item.estatus==='1'?"activo":"inactivo") + '</td>';
                html += '<td><a href="#" onclick="return getbyID(' + item.idUsuario + ')">Editar</a> | <a href="#" onclick="Delele(' + item.idUsuario + ')">Eliminar</a></td>';
                html += '</tr>';
            });
            $('.tbody').html(html);
            $('.data_table').DataTable();
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });

 
}
function Add() {
    var usrObj = {
        idusuario: 0,
        nombre: $('#nombre').val(),
        usuario: $('#usuario').val(),
        fechacreacion: $('#fechacreacion').val(),
        password: $('#password').val(),
        estatus: '1'
    };
    $.ajax({
        url: "https://localhost:7002/api/usuarios",
        data: JSON.stringify(usrObj),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            loadData();
            $('#myModal').modal('hide');
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}
function getbyID(idusuario) {
    $('#Name').css('border-color', 'lightgrey');
    $('#Age').css('border-color', 'lightgrey');
    $('#State').css('border-color', 'lightgrey');
    $('#Country').css('border-color', 'lightgrey');
    $.ajax({
        url: "https://localhost:7002/api/usuarios/" + idusuario,
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            $('#idusuario').val(result.idusuario);
            $('#nombre').val(result.nombre);
            $('#usuario').val(result.usuario);
            $('#fechacreacion').val(result.fechacreacion); 
            $('#password').val(result.password);
            $('#estatus').val((result.estatus === '1' ? "activo" : "inactivo"));

            $('#myModal').modal('show');
            $('#btnUpdate').show();
            $('#btnAdd').hide();
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
    return false;
}
function Update() {
    var res = validate();
    if (res == false) {
        return false;
    }
    var usrObj = {
        idusuario: $('#idusuario').val(),
        nombre: $('#nombre').val(),
        usuario: $('#usuario').val(),
        fechacreacion: $('#fechacreacion').val(),
        password: $('#password').val(),
        estatus: '1'
    };
    $.ajax({
        url: "https://localhost:7002/api/usuarios/",
        data: JSON.stringify(usrObj),
        type: "PUT",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            loadData();
            $('#myModal').modal('hide');
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
function Delele(ID) {
    var ans = confirm("¿Estás seguro de eliminar este registro?");
    if (ans) {
        $.ajax({
            url: "https://localhost:7002/api/usuarios/" + ID,
            type: "DELETE",
            contentType: "application/json;charset=UTF-8",
            dataType: "json",
            success: function (result) {
                loadData();
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });
    }
}
function clearTextBox() {
    $('#idusuario').val("");
    $('#nombre').val("");
    $('#usuario').val("");
    $('#password').val("");
    $('#fechacreacion').val("");
    $('#btnUpdate').hide();
    $('#btnAdd').show();
    $('#idusuario').css('border-color', 'lightgrey');
    $('#nombre').css('border-color', 'lightgrey');
    $('#usuario').css('border-color', 'lightgrey');
    $('#password').css('border-color', 'lightgrey');
    $('#fechacreacion').css('border-color', 'lightgrey');
    $('#myModal').modal('show');
}
function cerrarModal() {
    $('#myModal').modal('hide');
}