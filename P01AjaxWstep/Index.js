
$(document).ready(function () {


    $("#btnPolicz").on("click", function () {

        var liczba1 = $('#txtLiczba1').val();

        var liczba2 = $('#txtLiczba2').val();

        var wynik = parseInt(liczba1) + parseInt(liczba2);

        $('#txtWynik').val(wynik);

    });


});