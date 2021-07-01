var myLineChart;
var bodycolor = getComputedStyle(document.body).getPropertyValue('--bodycolor');
function bindLineChart() {
    if (myLineChart) {
        myLineChart.destroy();
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
                    label: "Number of Sales",
                    fill: false,
                },
                {
                    data: dataPrevious,
                    borderColor: "#3e95cd",
                    label: "Number of Sales",
                    fill: false,
                }
                ],
                labels: label
            };

            var context = document.getElementById("placement-data-chart").getContext("2d");
            myLineChart = new Chart(context, {
                type: 'line',
                data: lineData,
                options: {
                    title: {
                        display: true,
                        text: 'Sales Over Time '// + selectionLabel(response.label)
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
                    label: "Number of Sales",
                    fill: false,
                },
                {
                    data: dataPrevious,
                    borderColor: "#3e95cd",
                    label: "Number of Sales",
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
                        text: 'Sales Over Time '// + selectionLabel(response.label)
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
                    label: "Number of Sales",
                    fill: false,
                },
                {
                    data: dataPrevious,
                    borderColor: "#3e95cd",
                    label: "Number of Sales",
                    fill: false,
                }
                ],
                labels: label
            };

            var context = document.getElementById("placement-collected-chart").getContext("2d");
            myLineChart = new Chart(context, {
                type: 'line',
                data: lineData,
                options: {
                    title: {
                        display: true,
                        text: 'Sales Over Time '// + selectionLabel(response.label)
                    }
                }
            });
            //  HideLoader();
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
                    label: "Number of Sales",
                    fill: false,
                },
                {
                    data: dataPrevious,
                    borderColor: "#3e95cd",
                    label: "Number of Sales",
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
                        text: 'Sales Over Time '// + selectionLabel(response.label)
                    }
                }
            });
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
    $.ajax({
        type: "POST",
        url: "/Dashboard/GetActiveAccounts",
        contentType: 'application/json; charset=utf-8',
        //data: { "formId": formId },
        dataType: "json",
        success: function (data) {

            var datasetLabel = [];
            var datasetValue = [];
            var datasetColor = [];

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
                }
            };

            var chartjs_other_pie = document.getElementById("chartjs-other-pie");
            if (chartjs_other_pie) {
                var ctx = document.getElementById('chartjs-other-pie').getContext('2d');
                window.myDoughnut = new Chart(ctx, config);
            }
        },
        "error": function (data) {
        }
    });
}

