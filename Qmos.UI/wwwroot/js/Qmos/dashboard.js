let canvasDistributionHoursArea = document.getElementById("chartDistributionHoursArea").getContext("2d");
var doughnutChartDistributionHoursArea = new Chart(canvasDistributionHoursArea,
    { type: 'doughnut', data: {}, options: {} });

function GetDistributionHours(source, rangeDate, typeLevel) {

            debugger
            let data = {
                labels: ["A", "B", "C", "D"],
                datasets: [{ data: [2, 2, 4, 4], backgroundColor: ["rgba(163,225,211,1)", "rgba(222,222,222,1)", "rgba(156,195,218,1)", "rgba(181,184,207,0.5)"] }]
            };
            //let index = 9;
            //$.each(retorno, function () {
            //    data.labels.push(this.description);
            //    data.datasets[0].data.push(this.total);
            //    data.datasets[0].backgroundColor.push(colorList[index]);
            //    index++;
            //});
            let options = {
                animation: { animateScale: true },
                legend: { display: true, position: 'right'},
                plugins: {
                    labels: [{ render: 'label', position: 'outside' },
                        { render: 'percentage', fontSize: 9, precision: 2 }]
                },
                tooltips: {
                    callbacks: {
                        label: function (t, d) {
                            let xLabel = d.labels[t.index];
                            let yLabel = d.datasets[0].data[t.index];
                            return xLabel + ': ' + yLabel;
                        }
                    }
                }
            };
                doughnutChartDistributionHoursArea = new Chart(canvasDistributionHoursArea,
                    { type: 'doughnut', data: data, options: options });
}

var colorList = [];
for (var i = 0; i < 5; i++) {
    colorList.push("rgba(237,85,101,0.5)");  // Red
    colorList.push("rgba(26,123,185,0.5)");  // Blue
    colorList.push("rgba(247,165,74,0.5)");  // Yellow
    colorList.push("rgba(33,185,187,0.5)");  // Turquesa
    colorList.push("rgba(181,184,207,0.5)"); // Purple
    colorList.push("rgba(131,208,192,0.5)"); // Light Green
    colorList.push("rgba(241,116,34,0.5)");  // Orange
    colorList.push("rgba(35,198,200,0.5)");  // Dark Green
    colorList.push("rgba(237,237,237,0.5)"); // Gray
    colorList.push("rgba(163,225,211,1)"); // Light Green
    colorList.push("rgba(222,222,222,1)"); // Light Gray
    colorList.push("rgba(156,195,218,1)"); // Light Blue
}