﻿// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


var url = "http://localhost:53483/api/Planeacion/Asignaturas";
var targetDropdown = $("#Asi");
targetDropdown.attr('disabled', false);
targetDropdown.empty();
$.get(url, function (json) {
    for (var i = 0; i <= json.length - 1; i++) {
        targetDropdown.append($("<option value=" + json[i].IdAsignatura + ">" + json[i].ClaveAsignatura + "</option>"));
    }
});

var url = "http://localhost:53483/api/Planeacion/Periodos";
var targetDropdownP = $("#Per");
targetDropdownP.attr('disabled', false);
targetDropdownP.empty();
$.get(url, function (json) {
    for (var i = 0; i <= json.length - 1; i++) {
        targetDropdownP.append($("<option value=" + json[i].IdPeriodo + ">" + json[i].NombreCorto + "</option>"));
    }
});

var targetDropdown1 = $("#Us");
targetDropdown1.attr('disabled', false);
targetDropdown1.empty();
targetDropdown1.append($("<option value=1> Bryan </option>"));
targetDropdown1.append($("<option value=2> Ernesto </option>"));
targetDropdown1.append($("<option value=3> Gil </option>"));
targetDropdown1.append($("<option value=4> Guillermo </option>"));
targetDropdown1.append($("<option value=5> Ana </option>"));
        

var url = "http://localhost:53483/api/Planeacion/NombresFuentes";
var targetDropdow = $("#Fuentes");
targetDropdow.attr('disabled', false);
targetDropdow.empty();
$.get(url, function (json) {
    for (var i = 0; i <= json.length - 1; i++) {
        targetDropdow.append($("<option value=" + json[i].IdFuente + ">" + json[i].DesFuenteCompleta + "</option>"));
    }
});