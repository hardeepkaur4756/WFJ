var myLineChartUser;
function bindLineChartUser() {
    if (myLineChartUser) {
        myLineChartUser.destroy();
    }

    var gData = {};
    var formId = $("#ddlLocalCounselState").val();
    gData.formId = parseInt(formId);
    var jsonData = JSON.stringify(gData);

    $.ajax({
        type: "POST",
        url: "/Dashboard/GetDollarsCollectedData",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: jsonData,
        success: function (response) {
            var aData = response.ChartBaseModelPreviousYear;
            var data = [];
            var label = [];
            var dataPrevious = [];
            $.each(aData, function (inx, val) {
                var obj = {};
                if (val.Value == null) {
                    data.push(0);
                } else {
                    data.push(val.Value);
                }
                label.push(val.Name);
            });

            $.each(response.ChartBaseModelCurrentYear, function (inx, val) {
                dataPrevious.push(val.Value);
            });

            var lineData = {
                datasets: [{
                    data: data,
                    borderColor: "#3e95cd",
                    label: "Dollar Collection",
                    fill: false,
                },
                {
                    data: dataPrevious,
                    borderColor: "#A30000",
                    label: "Dollar Collection",
                    fill: false,
                }
                ],
                labels: label
            };

            var context = document.getElementById("dollars-collected-chart-user").getContext("2d");
            myLineChartUser = new Chart(context, {
                type: 'line',
                data: lineData,
                options: {
                    title: {
                        display: true,
                        text: 'Collected Dollars'// + selectionLabel(response.label)
                    }
                }
            });
        }
    });

    $.ajax({
        type: "POST",
        url: "/Dashboard/GetDollarsPlacedData",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: jsonData,
        success: function (response) {
            var aData = response.ChartBaseModelPreviousYear;
            var data = [];
            var label = [];
            var dataPrevious = [];
            $.each(aData, function (inx, val) {
                var obj = {};
                if (val.Value == null) {
                    data.push(0);
                } else {
                    data.push(val.Value);
                }
                label.push(val.Name);
            });

            $.each(response.ChartBaseModelCurrentYear, function (inx, val) {
                dataPrevious.push(val.Value);
            });

            var lineData = {
                datasets: [{
                    data: data,
                    borderColor: "#3e95cd",
                    label: "1st Payment Collection",
                    fill: false,
                },
                {
                    data: dataPrevious,
                    borderColor: "#A30000",
                    label: "1st Payment Collection",
                    fill: false,
                }
                ],
                labels: label
            };

            var context = document.getElementById("dollars-placed-chart-user").getContext("2d");
            myLineChartUser = new Chart(context, {
                type: 'line',
                data: lineData,
                options: {
                    title: {
                        display: true,
                        text: 'Collected'
                    }
                }
            });
        }
    });


    $.ajax({
        type: "POST",
        url: "/Dashboard/GetPlacementAndCollectedData",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: jsonData,
        success: function (response) {
            new Chart(document.getElementById("placement-collected-data-chart-user").getContext("2d"), {
                type: 'bar',
                data: {
                    labels: ["Jan", "Feb", 'Mar', 'April', 'May', 'June', 'July', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'],
                    datasets: response
                },
                options: {
                    legend: {
                        display: true,
                        position: 'bottom'
                    },
                    responsive: true,
                    interaction: {
                        intersect: false,
                    },
                    scales: {
                        xAxes: [{
                            stacked: true
                        }],
                        yAxes: [{
                            stacked: true
                        }],
                    },
                    title: {
                        display: true,
                        text: 'Placements and Collections by Month'
                    }
                }
            });
        }
    });
}

function approvePayment(paymentId, obj) {
    eModal.confirm({
        message: "Are you sure you want approve this payment?",
        size: eModal.size.sm,
        subtitle: 'smaller text header',
        label: 'Yes' | 'True' | 'OK'
    })
        .then(function myfunction() {
            $.ajax({
                type: "Post",
                url: "/Dashboard/UpdatePaymentStatus",
                data: { 'paymentId': paymentId },
                dataType: "json",
                success: function (resp) {
                    if (resp.success === true) {
                        $.notify("Payment approved successfully.", "success");
                        $(obj).parent().parent().remove();
                    }
                    else {
                        $.notify("There was an error", "danger");
                    }
                },
                error: function (result) {
                }
            });

        }, function myfunction() {
            $("#" + paymentId).prop("checked", false);
        });
}

function RequestNoteAlreadySeen(requestId, obj) {
    eModal.confirm({
        message: "Are you sure you have already seen this Request Note?",
        size: eModal.size.sm,
        subtitle: 'smaller text header',
        label: 'Yes' | 'True' | 'OK'
    })
        .then(function myfunction() {
            $.ajax({
                type: "Post",
                url: "/Dashboard/UpdateAlreadySeenStatus",
                data: { 'requestId': requestId },
                dataType: "json",
                success: function (resp) {
                    if (resp.success === true) {
                        $.notify("Request notes already seen updated successfully.", "success");
                        $(obj).parent().parent().remove();
                    }
                    else {
                        $.notify("There was an error", "danger");
                    }
                },
                error: function (result) {
                }
            });

        }, function myfunction() {
            $("#" + requestId).prop("checked", false);
        });
}