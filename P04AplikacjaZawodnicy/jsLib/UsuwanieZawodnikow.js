

$(document).ready(function () {


    
    $(".btn-danger").on("click", function () {
        
        //musimy odczytac ktory zaostal klikniety 
        var idUsuwanego = $(this).data("id");

        $.ajax({
            method: "POST",
            url: "./services/UsunZawodnika.aspx",
            data: { idUsuwanego: idUsuwanego }
        })
            .done(function (msg) {
                //  alert('usuniêto!');

                $("button[data-id=" + idUsuwanego + "]").closest("tr").hide(1500, function () {
                    $(this).remove();
                });
               

            });
    });

     


});