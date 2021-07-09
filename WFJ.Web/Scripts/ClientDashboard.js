var myLineChart;
var bodycolor = getComputedStyle(document.body).getPropertyValue('--bodycolor');
function bindLineChart() {
    if (myLineChart) {
        myLineChart.destroy();
    }
    $(".outer-loader1").fadeIn();
    $(".outer-loader2").fadeIn();
    $(".outer-loader3").fadeIn();
    var gData = {};
    var formId = $("#ddlLocalCounselState").val();
    gData.formId = parseInt(formId);
    var jsonData = JSON.stringify(gData);

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

            var context = document.getElementById("dollars-placed-chart").getContext("2d");
            myLineChart = new Chart(context, {
                type: 'line',
                data: lineData,
                options: {
                    title: {
                        display: true,
                        text: 'Collected'
                    }
                }
            });
            $(".outer-loader1").fadeOut();
        },
        "error": function (data) {
            $(".outer-loader1").fadeOut();
        }
    });

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

            var context = document.getElementById("dollars-collected-chart").getContext("2d");
            myLineChart = new Chart(context, {
                type: 'line',
                data: lineData,
                options: {
                    title: {
                        display: true,
                        text: 'Collected Dollars'// + selectionLabel(response.label)
                    }
                }
            });
            $(".outer-loader2").fadeOut();
        },
        "error": function (data) {
            $(".outer-loader2").fadeOut();
        }
    });

    $.ajax({
        type: "POST",
        url: "/Dashboard/GetPlacementAndCollectedData",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: jsonData,
        success: function (response) {
            new Chart(document.getElementById("placement-collected-data-chart").getContext("2d"), {
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
            $(".outer-loader3").fadeOut();
        },
        "error": function (data) {
            $(".outer-loader3").fadeOut();
        }
    });
}

function getRandomColor() {
    var letters = '0123456789ABCDEF'.split('');
    var color = '#';
    for (var i = 0; i < 6; i++) {
        color += letters[Math.floor(Math.random() * 16)];
    }
    return color;
}

function bindPieChart() {
    $(".outer-loader").fadeIn();
    var gData = {};
    var formId = $("#ddlLocalCounselState").val();
    gData.formId = parseInt(formId);
    var jsonData = JSON.stringify(gData);
    $.ajax({
        type: "POST",
        url: "/Dashboard/GetActiveAccounts",
        contentType: 'application/json; charset=utf-8',
        data: jsonData,
        dataType: "json",
        success: function (data) {
            var datasetLabel = [];
            var datasetValue = [];
            var datasetColor = [];
            if (data.length > 0) {
                for (var i = 0; i < data.length; i++) {
                    if (data[i].Name != null) {
                        datasetLabel.push(data[i].Name);
                        datasetValue.push(data[i].Value);
                        datasetColor.push(getRandomColor());
                    }
                }
                var config = {
                    type: 'doughnut',
                    data: {
                        datasets: [{
                            data: datasetValue,
                            backgroundColor: datasetColor,
                            label: 'Dataset 1'
                        }],
                        labels: datasetLabel
                    },
                    options: {
                        responsive: true,
                        maintainAspectRatio: false,
                        legend: {
                            display: true,
                            position: 'bottom',
                            labels: {
                                fontColor: bodycolor
                            }
                        },
                        animation: {
                            animateScale: true,
                            animateRotate: true
                        },
                        title: {
                            display: true,
                            text: 'Active Accounts'
                        }
                    }
                };

                var chartjs_other_pie = document.getElementById("chartjs-other-pie");
                if (chartjs_other_pie) {
                    var ctx = document.getElementById('chartjs-other-pie').getContext('2d');
                    window.myDoughnut = new Chart(ctx, config);
                }
                $(".outer-loader").fadeOut("slow");
            } else {
                $("#noChartData").text("No chart data available");
                $(".outer-loader").fadeOut("slow");
            }
        },
        "error": function (data) {
            $(".outer-loader").fadeOut("slow");
        }
    });
}


