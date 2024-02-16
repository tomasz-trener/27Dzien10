
$(document).ready(function () {


    $("#txtSzukaj").on("keypress", function (e) {

        if (e.which == 13) {
            e.preventDefault();
            wyszukaj();
        }

    });

    $("#btnSzukaj").on("click", function () {
        wyszukaj();
    });

    function wyszukaj() {
        var wartoscZInputa = $('#txtSzukaj').val();

        var obrazek = $("#dvLadowanieContainer").html();
        $("#dvKontenerTabelki").html(obrazek);

        $.ajax({
            method: "POST",
            url: "./services/Wyszukiwarka.aspx",
            data: { fraza: wartoscZInputa }
        })
            .done(function (msg) {
                //  alert(msg);

                $("#main-panel > div.content > div > div").html(msg);

            });
    }


});