﻿var myLineChartUser;
function bindLineChartUser() {
    if (myLineChartUser) {
        myLineChartUser.destroy();
    }
    $.ajax({
        type: "POST",
        url: "/Dashboard/GetPlacementsData",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
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
                    label: "No. of Placements",
                    fill: false,
                },
                {
                    data: dataPrevious,
                    borderColor: "#A30000",
                    label: "No. of Placements",
                    fill: false,
                }
                ],
                labels: label
            };

            var context = document.getElementById("placement-data-chart-user").getContext("2d");
            myLineChartUser = new Chart(context, {
                type: 'line',
                data: lineData,
                options: {
                    title: {
                        display: true,
                        text: 'Placement'// + selectionLabel(response.label)
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
        url: "/Dashboard/GetPlacementCollectedData",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
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

            var context = document.getElementById("placement-collected-chart-user").getContext("2d");
            myLineChartUser = new Chart(context, {
                type: 'line',
                data: lineData,
                options: {
                    title: {
                        display: true,
                        text: 'Placement Dollars'
                    }
                }
            });
        }
    });

    $.ajax({
        type: "POST",
        url: "/Dashboard/GetDollarsCollectedData",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
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
                        $.notify("Payments approve successfully.", "success");
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
            $("#" + paymentId).prop("checked", false);
        });
}