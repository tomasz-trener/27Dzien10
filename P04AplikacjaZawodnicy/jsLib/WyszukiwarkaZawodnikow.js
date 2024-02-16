
$(document).ready(function () {

    $("#btnSzukaj").on("click", function () {
        wyszukaj();
    });

    function wyszukaj() {
        var wartoscZInputa = $('#txtSzukaj').val();


        $.ajax({
            method: "POST",
            url: "./services/Wyszukiwarka.aspx",
            data: { fraza: wartoscZInputa }
        })
            .done(function (msg) {
                alert(msg);
            });
    }


});