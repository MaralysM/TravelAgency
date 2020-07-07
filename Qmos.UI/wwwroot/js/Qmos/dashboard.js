

function GetMTDMissedHeats() {
    //$.ajax({
    //    url: "http://sapwebbeap03:8002/api/Q727",
    //    method: "GET",
    //    success: function (retorno) {
    let canvasMTDMissedHeats = document.getElementById("chartMTDMissedHeats").getContext("2d");
    let data = {
        labels: [],
        datasets: [{ data: [], backgroundColor: [] }]
    };
    //let index = 1;
    //$.each(retorno, function () {
    //    data.labels.push(this.CREW);
    //    data.datasets[0].data.push(this.AMOUNT);
    //    data.datasets[0].backgroundColor.push(colorList[index]);
    //    index++;
    //});
    data = {
        labels: ["A", "B", "C", "D"],
        datasets: [{ data: [2, 2, 4, 4], backgroundColor: ["rgba(247,165,74,0.5)", "rgba(181,184,207,0.5)", "rgba(156,195,218,1)", "rgba(26,123,185,0.5)"] }]
    };

    let options = {
        animation: { animateScale: true },
        legend: { display: true, position: 'right' },
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
    //});
   // }


function GetMTDDelays() {
    //$.ajax({
    //    url: "http://localhost:8002/api/Q727",
    //    method: "GET",
    //    success: function (retorno) {
            let canvaschartMTDDelays = document.getElementById("chartMTDDelays").getContext("2d");

            let data = {
                labels: [],
                datasets: [
                    { barPercentage: 0.5, data: [], label: 'Electrode Change', backgroundColor: colorList[1] },
                    { barPercentage: 0.5, data: [], label: 'Inspect Furnace', backgroundColor: colorList[2] },
                    { barPercentage: 0.5, data: [], label: 'Stnd P. Off', backgroundColor: colorList[3] },
                    { barPercentage: 0.5, data: [], label: 'Tapping Prep', backgroundColor: colorList[4] }]
            };

            //for (var i = 0; i < retorno; i++) {
            //    $.each(retorno, function () {
            //        data.labels.push(this.CREW);
            //        data.datasets[i].data.push(this.AMOUNT);
            //    });
            //}

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
        //}
        //});
    }

function GetMTDTapTempand02PPM() {    
    $.ajax({
        url: "http://sapwebbeap03:8002/api/MTDTapTempAndO2PPM",
        method: "GET",
        success: function (retorno) {
            var myobject = JSON.parse(retorno);
            let canvaschartMTDTapTempand02PPM = document.getElementById("chartMTDTapTempand02PPM").getContext("2d");

            let data = {
                labels: [],
                datasets: [
                    { barPercentage: 1, data: [], label: 'Average of TempInSpec', backgroundColor: "#138cfc" },
                    { barPercentage: 1, data: [], label: 'Average of O2InSpec', backgroundColor: "#13239c" }]
            };

            $.each(myobject, function () {
                    data.labels.push(this.Crew);
                    data.datasets[0].data.push(this.TempInSpec);
                    data.datasets[1].data.push(this.O2InSpec);
                });

            let options = {
                responsive: true,
                legend: { position: 'bottom' },
                title: { display: true, text: "" },
                plugins: { labels: [{ render: 'value', position: 'outside' }] },
                scales: { yAxes: [{ ticks: { beginAtZero: true } }] }
            };

            barChartMTDTapTempand02PPM = new Chart(canvaschartMTDTapTempand02PPM,
                { type: 'bar', data: data, options: options });
        }
    });
 }


function GetKWhPerScrapTon() {
    $.ajax({
        url: "http://sapwebbeap03:8002/api/KwhPerScrapTon",
        method: "GET",
        success: function (retorno) {
            var myobject = JSON.parse(retorno);
            $('#Average').html(myobject.AvgKwhTon);
            let canvaschartKWhPerScrapTon = document.getElementById("chartKWhPerScrapTon").getContext("2d");

            var data = {
                labels: myobject.AxeX,
                datasets: [{
                    label: "Average of KwhTon",
                    data: myobject.AxeY,
                    lineTension: 0,
                    borderColor: "#2791ee",
                    backgroundColor: 'transparent',
                    pointBackgroundColor: "#2791ee",
                    pointBorderWidth: 1
                }]
            };

            let options = {
                responsive: true,
                legend: {
                    display: false
                },
                title: { display: true, text: "Average of KwhTon", position: "left", fontSize: 10 }

            };

            barChartKWhPerScrapTon = new Chart(canvaschartKWhPerScrapTon,
                { type: 'line', data: data, options: options });
        }
    });
}

function GetScrapTonPerHour() {
    $.ajax({
        url: "http://sapwebbeap03:8002/api/ScrapTonPerHour",
        method: "GET",
        success: function (retorno) {
            var myobject = JSON.parse(retorno);
            $('#Average').html(myobject.AvgTonHourPon);
            let canvaschartScrapTonPerHour = document.getElementById("chartScrapTonPerHour").getContext("2d");

            var data = {
                labels: myobject.AxeX,
                datasets: [{
                    label: "TonHour(Pon)",
                    data: myobject.AxeY,
                    lineTension: 0,
                    borderColor: "#2791ee",
                    backgroundColor: 'transparent',
                    pointBackgroundColor: "#2791ee",
                    pointBorderWidth: 1
                }]
            };

            let options = {
                responsive: true,
                legend: {
                    display: false
                },
                title: { display: true, text: "TonHour(Pon)", position: "left", fontSize: 10 }
            };

            barChartKWhPerScrapTon = new Chart(canvaschartScrapTonPerHour,
                { type: 'line', data: data, options: options });
        }
    });
}

function GetIronYield() {    
    $.ajax({
        url: "http://sapwebbeap03:8002/api/IronYield",
        method: "GET",
        success: function (retorno) {
            var myobject = JSON.parse(retorno);
            $('#Average').html(myobject.AvYield);
            let canvaschartIronYield = document.getElementById("chartIronYield").getContext("2d");

            var data = {
                labels: myobject.AxeX,
                datasets: [{
                    label: "Yield",
                    data: myobject.AxeY,
                    lineTension: 0,
                    borderColor: "#2791ee",
                    backgroundColor: 'transparent',
                    pointBackgroundColor: "#2791ee",
                    pointBorderWidth: 1
                }]
            };

            let options = {
                responsive: true,
                legend: {
                    display: false
                },
                title: { display: true, text: "Yield", position: "left", fontSize: 10 }
            };

            barChartIronYield = new Chart(canvaschartIronYield,
                { type: 'line', data: data, options: options });
        }
    });
}


function GetTargetPPM() {    
    $.ajax({
        url: "http://sapwebbeap03:8002/api/TapPPMTargetPPM",
        method: "GET",
        success: function (retorno) {   
            var myobject = JSON.parse(retorno);
            $('#Average').html(myobject.AvgO2InSpec);
            let canvaschartTargetPPM = document.getElementById("chartTargetPPM").getContext("2d");

            var data = {
                labels: myobject.AxeX,
                datasets: [{
                    label: "02AimDiff",
                    steppedLine: true,
                    data: myobject.AxeY,
                    borderColor: "#2791ee",
                    backgroundColor: 'transparent',
                    pointBackgroundColor: "#2791ee",
                    pointBorderWidth: 1
                }]
            };

            let options = {
                responsive: true,
                legend: {
                    display: false
                },
                title: { display: true, text: "02AimDiff", position: "left", fontSize: 10 }
            };

            barChartTargetPPM = new Chart(canvaschartTargetPPM,
                { type: 'line', data: data, options: options });
        }
    });

}

function GetTargetTemp() {
    $.ajax({
        url: "http://sapwebbeap03:8002/api/TapTempTargetTemp",
        method: "GET",
        success: function (retorno) {
            var myobject = JSON.parse(retorno);
            $('#Average').html(myobject.AvgTempInSpec);
            let canvaschartTargetTemp = document.getElementById("chartTargetTemp").getContext("2d");

            var data = {
                labels: myobject.AxeX,
                datasets: [{
                    label: "TempAimDiff",
                    steppedLine: true,
                    data: myobject.AxeY,
                    borderColor: "#2791ee",
                    backgroundColor: 'transparent',
                    pointBackgroundColor: "#2791ee",
                    pointBorderWidth: 1
                }]
            };
            let options = {
                responsive: true,
                legend: {
                    display: false
                },
                title: { display: true, text: "TempAimDiff", position: "left", fontSize: 10 }
            };

            barChartTargetTemp = new Chart(canvaschartTargetTemp,
                { type: 'line', data: data, options: options });
        }
    });
}
function GetTapWtTarget() {
    $.ajax({
        url: "http://sapwebbeap03:8002/api/TapWtTapWtTarget",
        method: "GET",
        success: function (retorno) {
            var myobject = JSON.parse(retorno);
            $('#Average').html(myobject.AvgTapWtDiff);
            let canvaschartTapWtTarget = document.getElementById("chartTapWtTarget").getContext("2d");

            var data = {
                labels: myobject.AxeX,
                datasets: [{
                    label: "Average of TapWtDiff",
                    steppedLine: true,
                    data: myobject.AxeY,
                    borderColor: "#2791ee",
                    backgroundColor: 'transparent',
                    pointBackgroundColor: "#2791ee",
                    pointBorderWidth: 1
                }]
            };
            let options = {
                responsive: true,
                legend: {
                    display: false
                },
                title: { display: true, text: "Average of TapWtDiff", position: "left", fontSize: 10 }

            };

            barChartTargetTemp = new Chart(canvaschartTapWtTarget,
                { type: 'line', data: data, options: options });
        }
    });
}


function GetMTDProduction() {

    let canvaschartMTDProduction = document.getElementById("chartMTDProduction").getContext("2d");

    var barChartData = {
        labels: [1, 2, 3, 4, 5, 6, 'Total'],
        datasets: [{
            label: 'Increase',
            backgroundColor: "rgba(131,208,192,0.5)",
            borderColor: "rgba(131,208,192,0.5)",
            borderWidth: 1,
            data: [
                [1, 3],
                [2, 4],
                [2, 4],
                [4, 6],
                [5, 7],
                [3, 5],
                [0, 0]
            ]
        },
        {
            label: 'Total',
            backgroundColor: "rgba(156,195,218,1)",
            borderColor: "rgba(156,195,218,1)",
            borderWidth: 1,

            data: [
                [0, 0],
                [0, 0],
                [0, 0],
                [0, 0],
                [0, 0],
                [0, 0],
                [0, 7]
            ]
        }
        ]

    };

    let options = {
        responsive: true,
        legend: { position: 'top' },
        title: { display: true, text: "Billet Tons", position: "left", fontSize: 10 },
        plugins: { labels: [{ render: 'value', position: 'outside' }] },
        scales: { yAxes: [{ ticks: { beginAtZero: true } }] }
    };


    barChartMTDProduction = new Chart(canvaschartMTDProduction,
        { type: 'bar', data: barChartData, options: options });
}




function GetMTDAverage() {
    $.ajax({
        url: "http://sapwebbeap03:8002/api/MTDAverage",
        method: "GET",
        success: function (retorno) {
            var myobject = JSON.parse(retorno);
            EafKWht(myobject);
            EafLrfKwht(myobject);
            TonPerHour(myobject);
            IronYield(myobject);
            FoamyCarbon(myobject);
            NG(myobject);
            Aluminum(myobject);
            ChargeCarbon(myobject);
        }
    });
}

function EafKWht(retorno) {
    var EafKWht = c3.generate({
        bindto: '#EafKWht',
        data: {
            columns: [
                ['EAF KWht', retorno.EAFkWhValue]
            ],
            type: 'gauge',
            onclick: function (d, i) { console.log("onclick", d, i); },
            onmouseover: function (d, i) { console.log("onmouseover", d, i); },
            onmouseout: function (d, i) { console.log("onmouseout", d, i); }
        },
        gauge: {
            //        label: {
            //            format: function(value, ratio) {
            //                return value;
            //            },
            //            show: false // to turn off the min/max labels.
            //        },
                min: retorno.EAFkWhMin, // 0 is default, //can handle negative min e.g. vacuum / voltage / current flow / rate of change
                max: retorno.EAFkWhMax // 100 is default
            //    units: ' %',
            //    width: 39 // for adjusting arc thickness
        },
        color: {
            pattern: ['#178af5', '#178af5', '#178af5', '#178af5'], // the three color levels for the percentage values.
            threshold: {
                //            unit: 'value', // percentage is default
                //            max: 200, // 100 is default
                values: [30, 60, 90, 100]
            }
        },
        size: {
            height: 180
        }
    });



}

function EafLrfKwht(retorno) {
    var EafKWht = c3.generate({
        bindto: '#EafLrfKwht',
        data: {
            columns: [
                ['EAF + LRF KWht', 91.4]
            ],
            type: 'gauge',
            onclick: function (d, i) { console.log("onclick", d, i); },
            onmouseover: function (d, i) { console.log("onmouseover", d, i); },
            onmouseout: function (d, i) { console.log("onmouseout", d, i); }
        },
        gauge: {
            //        label: {
            //            format: function(value, ratio) {
            //                return value;
            //            },
            //            show: false // to turn off the min/max labels.
            //        },
            //    min: 0, // 0 is default, //can handle negative min e.g. vacuum / voltage / current flow / rate of change
            //    max: 100, // 100 is default
            //    units: ' %',
            //    width: 39 // for adjusting arc thickness
        },
        color: {
            pattern: ['rgb(121, 149, 233)', 'rgb(121, 149, 233)', 'rgb(121, 149, 233)', 'rgb(121, 149, 233)'], // the three color levels for the percentage values.
            threshold: {
                //            unit: 'value', // percentage is default
                //            max: 200, // 100 is default
                values: [30, 60, 90, 100]
            }
        },
        size: {
            height: 180
        }
    });



}

function TonPerHour(retorno) {

    var TonPerHour = c3.generate({
        bindto: '#TonPerHour',
        data: {
            columns: [
                ['Ton Per Hour (POn)', retorno.TonHourPonValue]
            ],
            type: 'gauge',
            onclick: function (d, i) { console.log("onclick", d, i); },
            onmouseover: function (d, i) { console.log("onmouseover", d, i); },
            onmouseout: function (d, i) { console.log("onmouseout", d, i); }
        },
        gauge: {
            //        label: {
            //            format: function(value, ratio) {
            //                return value;
            //            },
            //            show: false // to turn off the min/max labels.
            //        },
                min: retorno.TonHourPonMin, // 0 is default, //can handle negative min e.g. vacuum / voltage / current flow / rate of change
                max: retorno.TonHourPonMax // 100 is default
            //    units: ' %',
            //    width: 39 // for adjusting arc thickness
        },
        color: {
            pattern: ['#178af5', '#178af5', '#178af5', '#178af5'], // the three color levels for the percentage values.
            threshold: {
                //            unit: 'value', // percentage is default
                //            max: 200, // 100 is default
                values: [30, 60, 90, 100]
            }
        },
        size: {
            height: 180
        }
    });
}

function IronYield(retorno) {
    var IronYield = c3.generate({
    bindto: '#IronYield',
    data: {
        columns: [
            ['Iron Yield', retorno.IronYieldValue]
        ],
        type: 'gauge',
        onclick: function (d, i) { console.log("onclick", d, i); },
        onmouseover: function (d, i) { console.log("onmouseover", d, i); },
        onmouseout: function (d, i) { console.log("onmouseout", d, i); }
    },
    gauge: {
        //        label: {
        //            format: function(value, ratio) {
        //                return value;
        //            },
        //            show: false // to turn off the min/max labels.
            //    },
        min: retorno.IronYieldMin, // 0 is default, //can handle negative min e.g. vacuum / voltage / current flow / rate of change
        max: retorno.IronYieldMax // 100 is default
        //    units: ' %',
        //    width: 39 // for adjusting arc thickness
    },
        color: {
        pattern: ['#178af5', '#178af5', '#178af5', '#178af5'], // the three color levels for the percentage values.
        threshold: {
            //            unit: 'value', // percentage is default
            //            max: 200, // 100 is default
            values: [30, 60, 90, 100]
        }
    },
    size: {
        height: 180
    }
});
}

function FoamyCarbon(retorno) {
    var FoamyCarbon = c3.generate({
        bindto: '#FoamyCarbon',
        data: {
            columns: [
                ['Foamy Carbon', 91.4]
            ],
            type: 'gauge',
            onclick: function (d, i) { console.log("onclick", d, i); },
            onmouseover: function (d, i) { console.log("onmouseover", d, i); },
            onmouseout: function (d, i) { console.log("onmouseout", d, i); }
        },
        gauge: {
            //        label: {
            //            format: function(value, ratio) {
            //                return value;
            //            },
            //            show: false // to turn off the min/max labels.
            //        },
            //    min: 0, // 0 is default, //can handle negative min e.g. vacuum / voltage / current flow / rate of change
            //    max: 100, // 100 is default
            //    units: ' %',
            //    width: 39 // for adjusting arc thickness
        },
        color: {
            pattern: ['rgb(121, 149, 233)', 'rgb(121, 149, 233)', 'rgb(121, 149, 233)', 'rgb(121, 149, 233)'], // the three color levels for the percentage values.
            threshold: {
                //            unit: 'value', // percentage is default
                //            max: 200, // 100 is default
                values: [30, 60, 90, 100]
            }
        },
        size: {
            height: 180
        }
    });
}

function NG(retorno) {
    var NG = c3.generate({
        bindto: '#NG',
        data: {
            columns: [
                ['NG', retorno.NGTonValue]
            ],
            type: 'gauge',
            onclick: function (d, i) { console.log("onclick", d, i); },
            onmouseover: function (d, i) { console.log("onmouseover", d, i); },
            onmouseout: function (d, i) { console.log("onmouseout", d, i); }
        },
        gauge: {
            //        label: {
            //            format: function(value, ratio) {
            //                return value;
            //            },
            //            show: false // to turn off the min/max labels.
            //        },
            min: retorno.NGTonMin, // 0 is default, //can handle negative min e.g. vacuum / voltage / current flow / rate of change
            max: retorno.NGTonMax // 100 is default
            //    units: ' %',
            //    width: 39 // for adjusting arc thickness
        },
        color: {
            pattern: ['#178af5', '#178af5', '#178af5', '#178af5'], // the three color levels for the percentage values.
            threshold: {
                //            unit: 'value', // percentage is default
                //            max: 200, // 100 is default
                values: [30, 60, 90, 100]
            }
        },
        size: {
            height: 180
        }
    });
}

function Aluminum() {
    var Aluminum = c3.generate({
        bindto: '#Aluminum',
        data: {
            columns: [
                ['Aluminum', 91.4]
            ],
            type: 'gauge',
            onclick: function (d, i) { console.log("onclick", d, i); },
            onmouseover: function (d, i) { console.log("onmouseover", d, i); },
            onmouseout: function (d, i) { console.log("onmouseout", d, i); }
        },
        gauge: {
            //        label: {
            //            format: function(value, ratio) {
            //                return value;
            //            },
            //            show: false // to turn off the min/max labels.
            //        },
            //    min: 0, // 0 is default, //can handle negative min e.g. vacuum / voltage / current flow / rate of change
            //    max: 100, // 100 is default
            //    units: ' %',
            //    width: 39 // for adjusting arc thickness
        },
        color: {
            pattern: ['rgb(121, 149, 233)', 'rgb(121, 149, 233)', 'rgb(121, 149, 233)', 'rgb(121, 149, 233)'], // the three color levels for the percentage values.
            threshold: {
                //            unit: 'value', // percentage is default
                //            max: 200, // 100 is default
                values: [30, 60, 90, 100]
            }
        },
        size: {
            height: 180
        }
    });
}

function ChargeCarbon() {
    var ChargeCarbon = c3.generate({
        bindto: '#ChargeCarbon',
        data: {
            columns: [
                ['Charge Carbon', 91.4]
            ],
            type: 'gauge',
            onclick: function (d, i) { console.log("onclick", d, i); },
            onmouseover: function (d, i) { console.log("onmouseover", d, i); },
            onmouseout: function (d, i) { console.log("onmouseout", d, i); }
        },
        gauge: {
            //        label: {
            //            format: function(value, ratio) {
            //                return value;
            //            },
            //            show: false // to turn off the min/max labels.
            //        },
            //    min: 0, // 0 is default, //can handle negative min e.g. vacuum / voltage / current flow / rate of change
            //    max: 100, // 100 is default
            //    units: ' %',
            //    width: 39 // for adjusting arc thickness
        },
        color: {
            pattern: ['rgb(121, 149, 233)', 'rgb(121, 149, 233)', 'rgb(121, 149, 233)', 'rgb(121, 149, 233)'], // the three color levels for the percentage values.
            threshold: {
                //            unit: 'value', // percentage is default
                //            max: 200, // 100 is default
                values: [30, 60, 90, 100]
            }
        },
        size: {
            height: 180
        }
    });}



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