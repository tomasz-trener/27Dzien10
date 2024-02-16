


$(document).ready(function () {


    
    $("#btnAjaxZapisz").on("click", function (e) {
        e.preventDefault();
        debugger;
        //zbieranie danych z formularza 
        var id = $('#glownyObszar_txtId').val();
        var imie = $('#glownyObszar_txtImie').val();
        var nazwisko = $('#glownyObszar_txtNazwisko').val();
        var kraj = $('#glownyObszar_txtKraj').val();
        var dataUr = $('#glownyObszar_txtDataUr').val();
        var wzrost = $('#glownyObszar_txtWzrost').val();
        var waga = $('#glownyObszar_txtWaga').val();
        var trenerId = $('#glownyObszar_ddlTrener').val();

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