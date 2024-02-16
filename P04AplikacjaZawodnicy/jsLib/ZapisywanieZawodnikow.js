


$(document).ready(function () {


    
    $("#btnAjaxZapisz").on("click", function () {

        //zbieranie danych z formularza 
        var id = $('#txtId').val();
        var imie = $('txtImie').val();
        var nazwisko = $('txtNazwisko').val();
        var kraj= $('txtKraj').val();
        var dataUr = $('txtDataUr').val();
        var wzrost = $('txtWzrost').val();
        var waga= $('txtWaga').val();
        var trenerId = $('#ddlTrener').val();

        $.ajax({
            method: "POST",
            url: "./services/ZapiszZawodnika.aspx",
            data: {
                id: id,
                imie: imie,
                nazwisko: nazwisko,
                kraj: kraj,
                dataUr: dataUr,
                wzrost: wzrost,
                waga: waga,
                trenerId:trenerId
            }
        })
            .done(function (msg) {
                  alert('Zapisano zmiany!');
            })
            .fail(function () {
                alert('Wyst¹pi³ b³¹d podczas zapisywania zmian.')
            })
    });

     


});