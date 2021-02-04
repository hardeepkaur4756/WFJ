function GetPaymentsDataTable() {

    if ($.fn.DataTable.isDataTable("#paymentsTable")) {
        oTable.draw();
    }
    else {
        oTable =
            $('#paymentsTable').DataTable({
                "aaSorting": [],
                "bServerSide": true,
                "sAjaxSource": "/Payment/GetPaymentsList",
                "fnServerData": function (sSource, aoData, fnCallback, oSettings) {
                    var requestId = $("#Request_ID").val();
                    aoData.push({ "name": "requestId", "value": requestId });
                    $.ajax({
                        type: "get",
                        data: aoData,
                        url: sSource,
                        success: function (resp) {
                            return fnCallback(resp);
                        }
                    });

                },
                "columns": [
                    {
                        data: "",
                        "render": function (row, type, full) {
                            return '<a class="note-delete" data-id="' + full.Id + '" href="javascript: addPayments(' + full.Id + ');" class="anchor-design" title="Edit"><u>Edit</u></a> <a class="note-edit" data-id="' + full.Id + '" href="javascript: deletePayment(' + full.Id + ');" class="anchor-design" title="Edit"><u>Delete</u></a>';
                        }
                    },
                    { "data": "PaymentDate", "title": "Payment Date" },
                    { "data": "PaymentType", "title": "Payment Type" },
                    { "data": "PaymentAmount", "title": "Payment Amount" },
                    { "data": "WFJFees", "title": "WFJ Fees" },
                    { "data": "WFJReferenceNumber", "title": "WFJ Ref No" },
                    { "data": "WFJReferenceDate", "title": "WFJ Ref Date" }
                ],
                "columnDefs": [
                    { "orderable": false, "targets": "_all" }
                ],
                bProcessing: true,
                pageLength: 10,
                "bFilter": false,
                "paging": true,
                //bSearching: false,
                bLengthChange: false,
                lengthMenu: [10, 25, 50],
                "bInfo": true,
                "language": {
                    //"zeroRecords": "no search results",
                    //"info": "Page _PAGE_ of _PAGES_",
                    "emptyTable": "no records found",
                    "processing": "Processing... Please wait",
                },

            });

    }

}

function addPayments(paymentId) {
    $(".se-pre-con").fadeIn();
    var clientId = parseInt($("#ClientId").val());
    if (isNaN(clientId)) { clientId = 0; }
    $.ajax({
        type: "Get",
        url: "/Payment/AddEditPayment",
        data: { paymentId: paymentId, clientId: clientId },
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (response) {
            $(".se-pre-con").fadeOut("slow");
            if (response.Success) {
                if (paymentId > 0) {
                    $('#addPaymentsModallLabel').html('Edit Payment');
                }
                else {
                    $('#addPaymentsModallLabel').html('Add Payment');
                }

                $('#addPaymentsDiv').html(response.Html);
                $('#addPaymentModal').modal('show');


                $('#paymentDate').daterangepicker({
                    autoUpdateInput: true,
                    singleDatePicker: true,
                    showDropdowns: true,
                    locale: { format: 'MM/DD/YYYY' },
                    minYear: 2000,
                    maxYear: parseInt(moment().format('YYYY'), 10)
                });
                $('#remitDate').daterangepicker({
                    autoUpdateInput: false,
                    singleDatePicker: true,
                    showDropdowns: true,
                    locale: { format: 'MM/DD/YYYY' },
                    minYear: 2000,
                    maxYear: parseInt(moment().format('YYYY'), 10)
                });
                $('#remitDate').on('apply.daterangepicker', function (ev, picker) {
                    $(this).val(picker.startDate.format('MM/DD/YYYY'));
                });
                $('#WFJReferenceDate').daterangepicker({
                    autoUpdateInput: false,
                    singleDatePicker: true,
                    showDropdowns: true,
                    locale: { format: 'MM/DD/YYYY' },
                    minYear: 2000,
                    maxYear: parseInt(moment().format('YYYY'), 10)
                });
                $('#WFJReferenceDate').on('apply.daterangepicker', function (ev, picker) {
                    $(this).val(picker.startDate.format('MM/DD/YYYY'));
                });
                $('#WFJInvoiceDatePaid').daterangepicker({
                    autoUpdateInput: false,
                    singleDatePicker: true,
                    showDropdowns: true,
                    locale: { format: 'MM/DD/YYYY' },
                    minYear: 2000,
                    maxYear: parseInt(moment().format('YYYY'), 10)
                });
                $('#WFJInvoiceDatePaid').on('apply.daterangepicker', function (ev, picker) {
                    $(this).val(picker.startDate.format('MM/DD/YYYY'));
                });
            }
            else {
                //notificationhelper.showerror('sorry an error occured.')
            }
        },
        error: function (result) {
            //notificationHelper.ShowError(result.Message);
        }
    });

}

function addPaymentSubmit() {

    var paymentDate = $("#paymentDate").val();
    var remitDate = $("#remitDate").val();
    var enteredBy = $("#enteredBy").val();
    var paymentType = $("#paymentType").val();
    var checkNumber = $("#checkNumber").val();
    var paymentAmount = $("#paymentAmount").val();
    var currency = $("#currency").val();
    var WFJFees = $("#WFJFees").val();
    var WFJReferenceNumber = $("#WFJReferenceNumber").val();
    var WFJReferenceDate = $("#WFJReferenceDate").val();
    var WFJInvoiceDatePaid = $("#WFJInvoiceDatePaid").val();

    var requestId = $("#Request_ID").val();
    var paymentId = $("#paymentId").val();

    $.ajax({
        url: '/Payment/AddPayment',
        data: {
            Id: paymentId,
            RequestId: requestId,
            PaymentDate: paymentDate,
            RemitDate: remitDate,
            EnteredBy: enteredBy,
            PaymentTypeId: paymentType,
            CheckNumber: checkNumber,
            PaymentAmount: paymentAmount,
            Currency: currency,
            WFJFees: WFJFees,
            WFJReferenceNumber: WFJReferenceNumber,
            WFJReferenceDate: WFJReferenceDate,
            WFJInvoiceDatePaid: WFJInvoiceDatePaid
        },
        type: 'post',
        dataType: 'json',
        success: function (resp) {
            if (resp.success === true) {

                $("#addPaymentModal").modal("hide");
                GetPaymentsDataTable();
                $.notify("Success.", "success");

            }
            else {
                if (resp.errorMessage.length === 0)
                    $.notify("There was an error", "danger");
                else
                    $.notify(resp.errorMessage, "danger");
            }
        }
    });

}

function deletePayment(paymentId) {

    eModal.confirm({
        message: "Are you sure you want to delete this Payment?",
        //title: 'Confirm!',
        size: eModal.size.sm,
        subtitle: 'smaller text header',
        label: 'Yes' | 'True' | 'OK'
    })
        .then(function myfunction() {

            $.ajax({
                data: { paymentId: paymentId },
                url: '/Payment/DeletePayment',
                type: 'post',
                dataType: 'json',
                success: function (resp) {
                    if (resp.success === true) {

                        GetPaymentsDataTable();
                    }
                    else {
                        $.notify("There was an error", "danger");
                    }
                }
            });

        }, function myfunction() { });
}