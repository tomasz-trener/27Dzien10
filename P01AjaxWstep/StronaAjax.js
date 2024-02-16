

$(document).ready(function () {


    $("#btnPolicz").on("click", function () {

        var liczba1 = $('#txtLiczba1').val();

        var liczba2 = $('#txtLiczba2').val();


        $.ajax({
            method: "POST",
            url: "./serv/Obliczenia.aspx",
            data: { liczba1: liczba1, liczba2:liczba2 }
        })
            .done(function (msg) {
                //alert("Data Saved: " + msg);
               $('#txtWynik').val(msg);
            });





        //$('#txtWynik').val(wynik);

    });


});