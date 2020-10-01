(function ($) {
    "use strict";
    var editor;
    $("#example").DataTable({
        dom: "Bfrtip",
        buttons: ["copy", "csv", "excel", "pdf", "print"],
        responsive: true,
    });

    $("#firmSearch").DataTable({
        responsive: true,
        searching: false,
        lengthChange: false,
    });

    $("#searchresult").DataTable({
        responsive: true,
        searching: false,
        lengthChange: false,
        colReorder: {
            order: [4, 3, 2, 1, 0, 5],
        },
    });

    $("#requestserviceresult").DataTable({
        responsive: true,
        searching: false,
        lengthChange: false,
    });

    $("#companytable").DataTable({
        responsive: true,
        searching: false,
        lengthChange: false,
        dom: "Bfrtip",
        buttons: ["csv", "pdf"],
        colReorder: true
    });

    $("#manageInvoice").DataTable({
        responsive: true,
        searching: false,
        lengthChange: false,
    });

    //$("#manageMyUsers").DataTable({
    //    responsive: true,
    //    searching: false,
    //    lengthChange: false,
    //});

    $("#paymentreport").DataTable({
        responsive: true,
        searching: false,
        lengthChange: false,
    });
   
})(jQuery);
