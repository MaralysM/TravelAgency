﻿

function GetMTDMissedHeats() {            
    let canvasMTDMissedHeats = document.getElementById("chartMTDMissedHeats").getContext("2d");

            let data = {
                labels: ["A", "B", "C", "D"],
                datasets: [{ data: [2, 2, 4, 4], backgroundColor: ["rgba(247,165,74,0.5)", "rgba(181,184,207,0.5)", "rgba(156,195,218,1)","rgba(26,123,185,0.5)"] }]
            };

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
    doughnutMTDMissedHeats = new Chart(canvasMTDMissedHeats,
                    { type: 'doughnut', data: data, options: options });
}




function GetMTDDelays() {

    let canvaschartMTDDelays= document.getElementById("chartMTDDelays").getContext("2d");

           let data = {
                labels: [],
                datasets: [
                    { barPercentage: 0.5, data: [], label: 'C', backgroundColor: colorList[1] },
                    { barPercentage: 0.5, data: [], label: 'A', backgroundColor: colorList[0] },
                    { barPercentage: 0.5, data: [], label: 'B', backgroundColor: colorList[0] },
                    { barPercentage: 0.5, data: [], label: 'D', backgroundColor: colorList[0] }]
            };

            data = {
                labels: ["C", "A", "B", "D"],
                datasets: [
                    { barPercentage: 1, data: [0.13, 0.31, 0.08, 0.04], label: "Electrode Change", backgroundColor: "rgba(156,195,218,1)" },
                    { barPercentage: 1, data: [0.17, 0.02, 0.04, 0.02], label: "Inspect Furnace", backgroundColor: "rgba(26,123,185,0.5)" },
                    { barPercentage: 1, data: [2.00, 1.56, 0.71, 0.69], label: "Stnd P. Off", backgroundColor: "rgba(247,165,74,0.5)" },
                    { barPercentage: 1, data: [0.03, 0.06, 0.05, 0.06], label: "Tapping Prep", backgroundColor: "rgba(181,184,207,0.5)" }
                ]
            };

            let options = {
                responsive: true,
                legend: { position: 'bottom' },
                title: { display: true, text: "" },
                plugins: { labels: [{ render: 'value', position: 'outside' }] },
                scales: { yAxes: [{ ticks: { beginAtZero: true } }] }
            };

    barChartMTDDelays = new Chart(canvaschartMTDDelays,
                { type: 'bar', data: data, options: options });      
}


function GetMTDTapTempand02PPM() {

    let canvaschartMTDDelays = document.getElementById("chartMTDTapTempand02PPM").getContext("2d");

    let data = {
        labels: [],
        datasets: [
            { barPercentage: 0.5, data: [], label: 'C', backgroundColor: colorList[1] },
            { barPercentage: 0.5, data: [], label: 'A', backgroundColor: colorList[0] },
            { barPercentage: 0.5, data: [], label: 'B', backgroundColor: colorList[0] },
            { barPercentage: 0.5, data: [], label: 'D', backgroundColor: colorList[0] }]
    };

    data = {
        labels: ["C", "A", "B", "D"],
        datasets: [
            { barPercentage: 1, data: [0.08, 0.06, 0.06, 0.06], label: "Average of TempInSpec", backgroundColor: "rgba(156,195,218,1)" },
            { barPercentage: 1, data: [0.08, 0.04, 0.09, 0.12], label: "Average of O2InSpec", backgroundColor: "rgba(26,123,185,0.5)" }
        ]
    };

    let options = {
        responsive: true,
        legend: { position: 'bottom' },
        title: { display: true, text: "" },
        plugins: { labels: [{ render: 'value', position: 'outside' }] },
        scales: { yAxes: [{ ticks: { beginAtZero: true } }] }
    };

    barChartMTDDelays= new Chart(canvaschartMTDDelays,
        { type: 'bar', data: data, options: options });
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