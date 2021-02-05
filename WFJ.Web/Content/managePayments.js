$("#PaymentSendTo").multiselect({
    includeSelectAllOption: true,
    enableFiltering: true,
});

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
                    {
                        data: "",
                        "render": function (row, type, full) {
                            
                            return '<div class="td-with-inline-icons">' +
                                '<div class="custom-control custom-checkbox custom-control-inline"><input type="checkbox" class="custom-control-input" id="noteCb' + full.Id + '" value="' + full.Id + '"><label class="custom-control-label" for="noteCb' + full.Id + '"></label></div></div>';
                        }
                    },
                    { "data": "PaymentDate", "title": "Payment Date" },
                    { "data": "PaymentType", "title": "Payment Type" },
                    { "data": "PaymentAmountStr", "title": "Payment Amount" },
                    { "data": "WFJFeesStr", "title": "WFJ Fees" },
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
    var formId = parseInt($("#Request_FormID").val());

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
            WFJInvoiceDatePaid: WFJInvoiceDatePaid,
            FormId: formId
        },
        type: 'post',
        dataType: 'json',
        success: function (resp) {
            if (resp.success === true) {
                $(".paymentBalanceDue").text(resp.balanceDue);
                $(".paymentTotalPayment").text(resp.totalPayment);
                $(".paymentRemainingAmount").text(resp.remainingAmount);
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

function getSelectedPayments() {
    var selectedPayments = [];
    $("#paymentsTable").find("input:checked").each(function () {
        selectedPayments.push($(this).val());
    });
    return selectedPayments;
}
function sendPayments() {
    var selectedPayments = getSelectedPayments();
    var sendToUsers = $("#PaymentSendTo").val();

    if (selectedPayments.length === 0) {
        $.notify("Please select some payments", "danger");
    }
    else if (sendToUsers.length === 0) {
        $.notify("Please select some users", "danger");
    }
    else {
        var requestId = $("#Request_ID").val();
        $.ajax({
            data: { payments: selectedPayments, requestId: requestId, users: sendToUsers },
            traditional: true,
            url: '/Payment/SendPayments',
            type: 'post',
            dataType: 'json',
            success: function (resp) {
                if (resp.success === true) {

                    $.notify("Payments sent to selected users.", "success");
                }
                else {
                    $.notify("There was an error", "danger");
                }
            }
        });
    }
}