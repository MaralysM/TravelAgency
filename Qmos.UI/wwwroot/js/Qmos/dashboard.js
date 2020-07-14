function GetMTDMissedHeats() {
    $.ajax({
        url: "http://sapwebbeap03:8002/api/MTDMissedHeats",
        method: "GET",
        success: function (retorno) {
            var myobject = JSON.parse(retorno);
            let canvasMTDMissedHeats = document.getElementById("chartMTDMissedHeats").getContext("2d");

            //let data = {
            //    labels: ["A","B","C","D"],
            //    datasets: [{ data: [45, 83, 32, 66], backgroundColor: ["#e36b33", "#7c1c84", "#138cfc", "#2c54a9"], labels: ["A", "B", "C", "D"]}]
            //};
            data = {
                labels: myobject.Crew,
                datasets: [{ data: myobject.Value, backgroundColor: ["#e36b33", "#7c1c84", "#138cfc", "#2c54a9"], labels: myobject.Crew }]
            };
            let options = {
                animation: { animateScale: true },
                legend: { display: false, position: 'right' },
                plugins: {
                    datalabels: {
                        position: 'outside',
                        color: 'white',
                        textAlign: 'center',
                        formatter: function (value, context) {
                            let porcentaje = value * 100 / context.dataset._meta[0].total;
                            return context.chart.data.labels[context.dataIndex] + '\n'+ value +' (' + Math.round(porcentaje * 10) / 10 + '%)';
                        }
                    }
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
    });
}

function GetMTDDelays() {
    $.ajax({
        url: "http://sapwebbeap03:8002/api/MTDDelays",
        method: "GET",
        success: function (retorno) {
            var myobject = JSON.parse(retorno);
            let canvaschartMTDDelays = document.getElementById("chartMTDDelays").getContext("2d");

            let data = {
                labels: [],
                datasets: [
                    { barPercentage: 1, data: [], label: 'Electrode Change', backgroundColor: "#138cfc" },
                    { barPercentage: 1, data: [], label: 'Inspect Furnace', backgroundColor: "#2c54a9" },
                    { barPercentage: 1, data: [], label: 'Stnd P. Off', backgroundColor: "#e36b33" },
                    { barPercentage: 1, data: [], label: 'Tapping Prep', backgroundColor: "#7c1c84" }]
            };

            $.each(myobject, function () {
                data.labels.push(this.Crew);
                data.datasets[0].data.push(this.MTDDelays.ElectrodeChange);
                data.datasets[1].data.push(this.MTDDelays.InspectFurnace);
                data.datasets[2].data.push(this.MTDDelays.StndPOff);
                data.datasets[3].data.push(this.MTDDelays.TappingPrep);
            });
            let options = {
                responsive: true,
                legend: { position: 'bottom' },
                title: { display: true, text: "" },
                plugins: { labels: [{ render: 'value', position: 'outside' }] },
                scales: {
                    yAxes: [{
                        ticks: { beginAtZero: true }, scaleLabel: {
                            display: true,
                            labelString: 'NormDuration'
                        }
                    }],
                    xAxes: [{
                        scaleLabel: {
                            display: true,
                            labelString: 'Crew'
                        }
                    }]
                }
            };

            barChartMTDDelays = new Chart(canvaschartMTDDelays,
                { type: 'bar', data: data, options: options });
        }
    });
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
                scales: {
                    yAxes: [{
                        ticks: {
                            beginAtZero: true,
                            fontSize: 14
                        },
                        scaleLabel: {
                            display: true,
                            labelString: '%'
                        }
                    }],
                    xAxes: [{
                        ticks: {
                            beginAtZero: true,
                            fontSize: 14
                        },
                        scaleLabel: {
                            display: true,
                            labelString: 'Crew'
                        }
                    }]
                }
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
                    pointRadius: 0,
                    datalabels: {
                        color: 'yellow'
                    }
                }, {
                    datalabels: {
                        labels: {
                            title: null
                        }
                    },
                    fill: false,
                    backgroundColor: "#676a6c",
                    borderColor: "#676a6c",
                    borderDash: [5, 5],
                    pointRadius: 0,
                    data: myobject.Avg
                }]
            };


            let options = {
                legend: {
                    display: false
                },
                title: { display: true, text: "" },
                responsive: true,
                plugins: {
                    datalabels: {
                        color: 'pink',
                        labels: {
                            title: {
                                color: 'red'
                            }
                        },
                        anchor: 'end',
                        align: 'top'
                    }
                },
                scales: {
                    yAxes: [{
                        ticks: {
                            fontSize: 14
                        },
                        scaleLabel: {
                            display: true,
                            labelString: 'Average of Kwh Ton'
                        }
                    }],
                    xAxes: [{
                        ticks: {
                            fontSize: 14,
                            callback: function (dataLabel, index) {
                                // Hide the label of every 2nd dataset. return null to hide the grid line too
                                return index % 2 === 0 ? dataLabel : '';
                            }
                        },
                        scaleLabel: {
                            display: true,
                            labelString: 'Heat'
                        }
                    }]
                }

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
                    pointRadius: 0,
                    datalabels: {
                        color: 'yellow'
                    }
                }, {
                    datalabels: {
                        labels: {
                            title: null
                        }
                    },
                    fill: false,
                    backgroundColor: "#676a6c",
                    borderColor: "#676a6c",
                    borderDash: [5, 5],
                    pointRadius: 0,
                    data: myobject.Avg
                }]
            };

            let options = {
                legend: {
                    display: false
                },
                title: { display: true, text: "" },
                responsive: true,
                plugins: {
                    datalabels: {
                        color: 'pink',
                        labels: {
                            title: {
                                color: 'red'
                            }
                        },
                        anchor: 'end',
                        align: 'top'
                    }
                },
                scales: {
                    yAxes: [{
                        ticks: {
                            fontSize: 14
                        },
                        scaleLabel: {
                            display: true,
                            labelString: 'TonHour(POn)'
                        }
                    }],
                    xAxes: [{
                        ticks: {
                            fontSize: 14,
                            callback: function (dataLabel, index) {
                                // Hide the label of every 2nd dataset. return null to hide the grid line too
                                return index % 2 === 0 ? dataLabel : '';
                            }
                        },
                        scaleLabel: {
                            display: true,
                            labelString: 'Heat'
                        }
                    }]
                }

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
                    pointRadius: 0
                }, {
                    datalabels: {
                        labels: {
                            title: null
                        }
                    },
                    fill: false,
                    backgroundColor: "#676a6c",
                    borderColor: "#676a6c",
                    borderDash: [5, 5],
                    pointRadius: 0,
                    data: myobject.Avg
                }]
            };

            let options = {
                legend: {
                    display: false
                },
                title: { display: true, text: "" },
                responsive: true,
                plugins: {
                    datalabels: {
                        color: 'pink',
                        labels: {
                            title: {
                                color: 'red'
                            }
                        },
                        anchor: 'end',
                        align: 'top'
                    }
                },
                scales: {
                    yAxes: [{
                        ticks: {
                            fontSize: 14
                        },
                        scaleLabel: {
                            display: true,
                            labelString: 'Yield'
                        }
                    }],
                    xAxes: [{
                        ticks: {
                            fontSize: 14,
                            callback: function (dataLabel, index) {
                                // Hide the label of every 2nd dataset. return null to hide the grid line too
                                return index % 2 === 0 ? dataLabel : '';
                            }
                        },
                        scaleLabel: {
                            display: true,
                            labelString: 'Heat'
                        }
                    }]
                }

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
                    pointRadius: 0,
                    pointBorderWidth: 1
                }, {
                    fill: false,
                    backgroundColor: "#676a6c",
                    borderColor: "#676a6c",
                    borderDash: [5, 5],
                    pointRadius: 0,
                    data: myobject.Min
                }, {
                    fill: false,
                    backgroundColor: "#676a6c",
                    borderColor: "#676a6c",
                    borderDash: [5, 5],
                    pointRadius: 0,
                    data: myobject.Max
                }
                ]
            };

            let options = {
                responsive: true,
                legend: {
                    display: false
                },
                //   title: { display: true, text: "02AimDiff", position: "left", fontSize: 10 },
                scales: {
                    yAxes: [{
                        ticks: {
                            fontSize: 14
                        },
                        scaleLabel: {
                            display: true,
                            labelString: '02AimDiff'
                        }
                    }],
                    xAxes: [{
                        xAxes: [{
                            ticks: {
                                fontSize: 14,
                                callback: function (dataLabel, index) {
                                    // Hide the label of every 2nd dataset. return null to hide the grid line too
                                    return index % 2 === 0 ? dataLabel : '';
                                }
                            },
                        scaleLabel: {
                            display: true,
                            labelString: 'Heat'
                        }
                    }]
                }
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
                    pointRadius: 0,
                    pointBorderWidth: 1
                }, {
                    fill: false,
                    backgroundColor: "#676a6c",
                    borderColor: "#676a6c",
                    borderDash: [5, 5],
                    pointRadius: 0,
                    data: myobject.Min
                }, {
                    fill: false,
                    backgroundColor: "#676a6c",
                    borderColor: "#676a6c",
                    borderDash: [5, 5],
                    pointRadius: 0,
                    data: myobject.Max
                }
                ]
            };
            let options = {
                responsive: true,
                legend: {
                    display: false
                },
                //title: { display: true, text: "TempAimDiff", position: "left", fontSize: 10 },
                scales: {
                    yAxes: [{
                        ticks: {
                            beginAtZero: true ,
                            fontSize: 14
                        }, scaleLabel: {
                            display: true,
                            labelString: 'TempAimDiff'
                        }
                    }],
                    xAxes: [{
                        ticks: {
                            beginAtZero: true,
                            fontSize: 14,
                            callback: function (dataLabel, index) {
                                // Hide the label of every 2nd dataset. return null to hide the grid line too
                                return index % 2 === 0 ? dataLabel : '';
                            }
                        },scaleLabel: {
                            display: true,
                            labelString: 'Heat'
                        }
                    }]
                }
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
                    pointRadius: 0,
                    pointBorderWidth: 1
                }, {
                    fill: false,
                    backgroundColor: "#676a6c",
                    borderColor: "#676a6c",
                    borderDash: [5, 5],
                    pointRadius: 0,
                    data: myobject.Min
                }, {
                    fill: false,
                    backgroundColor: "#676a6c",
                    borderColor: "#676a6c",
                    borderDash: [5, 5],
                    pointRadius: 0,
                    data: myobject.Max
                }]
            };
            let options = {
                responsive: true,
                legend: {
                    display: false
                },
                // title: { display: true, text: "Average of TapWtDiff", position: "left", fontSize: 10 },
                scales: {
                    yAxes: [{
                        ticks: { beginAtZero: true, fontSize: 14 }, scaleLabel: {
                            display: true,
                            labelString: 'Average of TapWtDiff'
                        }
                    }],
                    xAxes: [{
                        ticks: {
                            beginAtZero: true, fontSize: 14,
                            callback: function (dataLabel, index) {
                                // Hide the label of every 2nd dataset. return null to hide the grid line too
                                return index % 2 === 0 ? dataLabel : '';
                            } },
                        scaleLabel: {
                            display: true,
                            labelString: 'Heat'
                        }
                    }]
                }

            };

            barChartTargetTemp = new Chart(canvaschartTapWtTarget,
                { type: 'line', data: data, options: options });
        }
    });
}

function GetMTDProduction() {
    $.ajax({
        url: "http://sapwebbeap03:8002/api/MTDProduction",
        method: "GET",
        success: function (retorno) {
            var myobject = JSON.parse(retorno);
            let canvaschartMTDProduction = document.getElementById("chartMTDProduction").getContext("2d");
            var barChartData = {
                labels: [/*1,2,3,4,5,6,'Total'*/],
                datasets: [{
                    label: 'Increase',
                    datalabels: {
                        color: 'red'
                    },
                    data: [
                        //[1,382.28],
                        //[2, 1566.66666],
                        //[2, 43334.876],
                        //[4, 6324.6754],
                        //[5, 743535.8866],
                        //[3, 535.7564],
                        //[0, 734534.56457]
                    ],
                    backgroundColor: [
                        //'#269643',
                        //'#269643',
                        //'#269643',
                        //'#269643',
                        //'#269643',
                        //'#269643',
                        //'#148dfb'

                    ],
                    borderColor: [
                        //'#269643',
                        //'#269643',
                        //'#269643',
                        //'#269643',
                        //'#269643',
                        //'#269643',
                        //'#148dfb'
                    ],
                    borderWidth: 1
                }, {
                    type: 'line',
                    label: 'Dataset 3',
                    backgroundColor: 'transparent',
                        data: myobject.target, //[8000000, 8000000, 8000000, 8000000, 8000000, 8000000, 8000000],
                    fill: false,
                    borderColor: "#676a6c",
                    borderDash: [5, 5],
                    pointRadius: 0,
                    datalabels: {
                        labels: {
                            title: null
                        }
                    }
                }
                ]

            };
            var a = myobject.data.length;
            var i = 0;
            $.each(myobject.data, function () {
                if (i != (a - 1)) {
                    barChartData.labels.push(this.ShiftDay);
                    barChartData.datasets[0].data.push(this.BilletTons);
                    console.log(this);
                    barChartData.datasets[0].backgroundColor.push('#269643');
                    barChartData.datasets[0].borderColor.push('#269643');
                } else {
                    barChartData.labels.push(this.ShiftDay);
                    console.log(this);
                    barChartData.datasets[0].data.push(this.BilletTons);
                    barChartData.datasets[0].backgroundColor.push('#148dfb');
                    barChartData.datasets[0].borderColor.push('#148dfb');
                }
                i++;
            });

            let options = {
                responsive: true,
                legend: false,
                // title: { display: true, text: "Billet Tons", position: "left", fontSize: 10 },
                plugins: {
                    labels: [],
                    datalabels: {
                        anchor: 'end',
                        align: 'top',
                        rotation: -90,
                        formatter: function (value, context) {
                            return Math.round(value[1] - value[0]);
                        }
                    }
                },
                scales: {
                    yAxes: [{
                        ticks: {
                            beginAtZero: true,
                            fontSize: 14
                        }, scaleLabel: {
                            display: true,
                            labelString: 'Billet Tons'
                        }
                    }],
                    xAxes: [{
                        ticks: {
                            beginAtZero: true,
                            fontSize: 14
                        },
                        scaleLabel: {
                            display: true,
                            labelString: 'Shift Day'
                        }
                    }]
                }
            };
            barChartMTDProduction = new Chart(canvaschartMTDProduction,
                { type: 'bar', data: barChartData, options: options });
        }


    });
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

    var thresholdOpts = {
        boxSize: 8,
        boxFill: false,
        strokeWidth: 2
    };
    var opts = {
        bindto: '#Chart1',
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
            label: {
                format: function (value, ratio) {
                    return value;
                },
                show: true // to turn off the min/max labels.                       
            },
            min: retorno.EAFkWhMin, // 0 is default, //can handle negative min e.g. vacuum / voltage / current flow / rate of change
            max: retorno.EAFkWhMax // 100 is default
            //    units: ' %',
            //    width: 39 // for adjusting arc thickness
        },
        color: {
            pattern: ['#1887ef'], // the three color levels for the percentage values.
            threshold: {
                //            unit: 'value', // percentage is default
                //            max: 200, // 100 is default
                values: []
            }
        },
        size: {
            height: 180
        },
        onrendered: function () {
            drawThresholds(this, thresholdOpts, opts, retorno.EAFkWhMax, ".DrawChart1", retorno.EAFkWhTarget);
        }
    };

    chart = c3.generate(opts);

}

function EafLrfKwht(retorno) {
    var thresholdOpts = {
        boxSize: 8,
        boxFill: false,
        strokeWidth: 2
    };
    var chart;
    var opts = {
        bindto: '#Chart2',
        data: {
            columns: [
                ['EAF KWht', retorno.EAFLRFKWhValue]
            ],
            type: 'gauge',
            onclick: function (d, i) { console.log("onclick", d, i); },
            onmouseover: function (d, i) { console.log("onmouseover", d, i); },
            onmouseout: function (d, i) { console.log("onmouseout", d, i); }
        },
        gauge: {
            label: {
                format: function (value, ratio) {
                    return value;
                },
                show: true // to turn off the min/max labels.                       
            },
            min: retorno.EAFLRFKWhMin, // 0 is default, //can handle negative min e.g. vacuum / voltage / current flow / rate of change
            max: retorno.EAFLRFKWhnMax // 100 is default
            //    units: ' %',
            //    width: 39 // for adjusting arc thickness
        },
        color: {
            pattern: ['#1887ef'], // the three color levels for the percentage values.
            threshold: {
                //            unit: 'value', // percentage is default
                //            max: 200, // 100 is default
                values: []
            }
        },
        size: {
            height: 180
        },
        onrendered: function () {
            drawThresholds(this, thresholdOpts, opts, retorno.EAFLRFKWhnMax, ".DrawChart2", retorno.EAFLRFKWhTarget);
        }
    };
    chart = c3.generate(opts);
}

function drawThresholds(chart, thOpts, chOpts, Max, ClassChart, ValueChart) {
    d3.select(ClassChart).selectAll("text.mytxt").remove();
    d3.select(ClassChart).selectAll("rect.myrect").remove();
    d3.select(ClassChart).selectAll("line.myline").remove();
    var radius = chart.radius,
        iradius = chart.innerRadius;

    var v = ValueChart;
    var col = 'red';

    var angle = Math.PI * v / Max;
    var x0 = (iradius * Math.cos(angle));
    var y0 = (iradius * Math.sin(angle));
    var x1 = (radius * Math.cos(angle));
    var y1 = (radius * Math.sin(angle));
    d3.select(ClassChart).select(".c3-chart-arcs").append("line")
        .attr({
            x1: -x0,
            y1: -y0,
            x2: -x1,
            y2: -y1
        })
        .attr('class', 'myline')
        .style("stroke-width", thOpts.strokeWidth)
        .style("stroke", col);

    var txtSize = measure(v, "mytxt", chart);
    var xt = ((radius + thOpts.boxSize) * Math.cos(angle)) + txtSize.width / 2;
    var yt = ((radius + thOpts.boxSize) * Math.sin(angle)) + txtSize.height / 2;
    d3.select(ClassChart).select(".c3-chart-arcs").append("text")
        .attr({
            x: -xt,
            y: -yt
        })
        .attr('class', 'mytxt')
        .text(v);

}

function measure(text, classname, chart) {
    if (!text || text.length === 0) return {
        height: 0,
        width: 0
    };
    var container = d3.select('body').append('svg').attr('class', classname);
    container.append('text').attr({
        x: -9000,
        y: -9000
    }).text(text);
    var bbox = container.node().getBBox();
    container.remove();
    return {
        height: bbox.height,
        width: bbox.width
    };
}

function TonPerHour(retorno) {
    var thresholdOpts = {
        boxSize: 8,
        boxFill: false,
        strokeWidth: 2
    };
    var opts = {
        bindto: '#Chart3',
        data: {
            columns: [
                ['EAF KWht', retorno.TonHourPonValue]
            ],
            type: 'gauge',
            onclick: function (d, i) { console.log("onclick", d, i); },
            onmouseover: function (d, i) { console.log("onmouseover", d, i); },
            onmouseout: function (d, i) { console.log("onmouseout", d, i); }
        },
        gauge: {
            label: {
                format: function (value, ratio) {
                    return value;
                },
                show: true // to turn off the min/max labels.                       
            },
            min: retorno.TonHourPonMin, // 0 is default, //can handle negative min e.g. vacuum / voltage / current flow / rate of change
            max: retorno.TonHourPonMax // 100 is default
            //    units: ' %',
            //    width: 39 // for adjusting arc thickness
        },
        color: {
            pattern: ['#1887ef'], // the three color levels for the percentage values.
            threshold: {
                //            unit: 'value', // percentage is default
                //            max: 200, // 100 is default
                values: []
            }
        },
        size: {
            height: 180
        },
        onrendered: function () {
            drawThresholds(this, thresholdOpts, opts, retorno.TonHourPonMax, ".DrawChart3", retorno.TonHourPonTarget);
        }
    };

    chart = c3.generate(opts);
}

function IronYield(retorno) {
    var thresholdOpts = {
        boxSize: 8,
        boxFill: false,
        strokeWidth: 2
    };
    var opts = {
        bindto: '#Chart4',
        data: {
            columns: [
                ['EAF KWht', retorno.IronYieldValue]
            ],
            type: 'gauge',
            onclick: function (d, i) { console.log("onclick", d, i); },
            onmouseover: function (d, i) { console.log("onmouseover", d, i); },
            onmouseout: function (d, i) { console.log("onmouseout", d, i); }
        },
        gauge: {
            label: {
                format: function (value, ratio) {
                    return value;
                },
                show: true // to turn off the min/max labels.                       
            },
            min: retorno.IronYieldMin, // 0 is default, //can handle negative min e.g. vacuum / voltage / current flow / rate of change
            max: retorno.IronYieldMax // 100 is default
            //    units: ' %',
            //    width: 39 // for adjusting arc thickness
        },
        color: {
            pattern: ['#1887ef'], // the three color levels for the percentage values.
            threshold: {
                //            unit: 'value', // percentage is default
                //            max: 200, // 100 is default
                values: []
            }
        },
        size: {
            height: 180
        },
        onrendered: function () {
            drawThresholds(this, thresholdOpts, opts, retorno.IronYieldMax, ".DrawChart4", retorno.IronYieldTarget);
        }
    };

    chart = c3.generate(opts);
}

function FoamyCarbon(retorno) {
    var thresholdOpts = {
        boxSize: 8,
        boxFill: false,
        strokeWidth: 2
    };
    var opts = {
        bindto: '#Chart5',
        data: {
            columns: [
                ['EAF KWht', retorno.CarbonTonValue]
            ],
            type: 'gauge',
            onclick: function (d, i) { console.log("onclick", d, i); },
            onmouseover: function (d, i) { console.log("onmouseover", d, i); },
            onmouseout: function (d, i) { console.log("onmouseout", d, i); }
        },
        gauge: {
            label: {
                format: function (value, ratio) {
                    return value;
                },
                show: true // to turn off the min/max labels.                       
            },
            min: retorno.CarbonTonMin, // 0 is default, //can handle negative min e.g. vacuum / voltage / current flow / rate of change
            max: retorno.CarbonTonnMax // 100 is default
            //    units: ' %',
            //    width: 39 // for adjusting arc thickness
        },
        color: {
            pattern: ['#1887ef'], // the three color levels for the percentage values.
            threshold: {
                //            unit: 'value', // percentage is default
                //            max: 200, // 100 is default
                values: []
            }
        },
        size: {
            height: 180
        },
        onrendered: function () {
            drawThresholds(this, thresholdOpts, opts, retorno.CarbonTonnMax, ".DrawChart5", retorno.CarbonTonTarget);
        }
    };

    chart = c3.generate(opts);
}

function NG(retorno) {
    var thresholdOpts = {
        boxSize: 8,
        boxFill: false,
        strokeWidth: 2
    };
    var opts = {
        bindto: '#Chart6',
        data: {
            columns: [
                ['EAF KWht', retorno.NGTonValue]
            ],
            type: 'gauge',
            onclick: function (d, i) { console.log("onclick", d, i); },
            onmouseover: function (d, i) { console.log("onmouseover", d, i); },
            onmouseout: function (d, i) { console.log("onmouseout", d, i); }
        },
        gauge: {
            label: {
                format: function (value, ratio) {
                    return value;
                },
                show: true // to turn off the min/max labels.                       
            },
            min: retorno.NGTonMin, // 0 is default, //can handle negative min e.g. vacuum / voltage / current flow / rate of change
            max: retorno.NGTonMax // 100 is default
            //    units: ' %',
            //    width: 39 // for adjusting arc thickness
        },
        color: {
            pattern: ['#1887ef'], // the three color levels for the percentage values.
            threshold: {
                //            unit: 'value', // percentage is default
                //            max: 200, // 100 is default
                values: []
            }
        },
        size: {
            height: 180
        },
        onrendered: function () {
            drawThresholds(this, thresholdOpts, opts, retorno.NGTonMax, ".DrawChart6", retorno.NGTonTarget);
        }
    };

    chart = c3.generate(opts);
}

function Aluminum(retorno) {
    var thresholdOpts = {
        boxSize: 8,
        boxFill: false,
        strokeWidth: 2
    };
    var opts = {
        bindto: '#Chart7',
        data: {
            columns: [
                ['EAF KWht', retorno.AluminiumValue]
            ],
            type: 'gauge',
            onclick: function (d, i) { console.log("onclick", d, i); },
            onmouseover: function (d, i) { console.log("onmouseover", d, i); },
            onmouseout: function (d, i) { console.log("onmouseout", d, i); }
        },
        gauge: {
            label: {
                format: function (value, ratio) {
                    return value;
                },
                show: true // to turn off the min/max labels.                       
            },
            min: retorno.AluminiumMin, // 0 is default, //can handle negative min e.g. vacuum / voltage / current flow / rate of change
            max: retorno.AluminiumMax // 100 is default
            //    units: ' %',
            //    width: 39 // for adjusting arc thickness
        },
        color: {
            pattern: ['#1887ef'], // the three color levels for the percentage values.
            threshold: {
                //            unit: 'value', // percentage is default
                //            max: 200, // 100 is default
                values: []
            }
        },
        size: {
            height: 180
        },
        onrendered: function () {
            drawThresholds(this, thresholdOpts, opts, retorno.AluminiumMax, ".DrawChart7", retorno.AluminiumTarget);
        }
    };

    chart = c3.generate(opts);
}

function ChargeCarbon(retorno) {
    var thresholdOpts = {
        boxSize: 8,
        boxFill: false,
        strokeWidth: 2
    };
    var opts = {
        bindto: '#Chart8',
        data: {
            columns: [
                ['EAF KWht', retorno.ChargeCarbonValue]
            ],
            type: 'gauge',
            onclick: function (d, i) { console.log("onclick", d, i); },
            onmouseover: function (d, i) { console.log("onmouseover", d, i); },
            onmouseout: function (d, i) { console.log("onmouseout", d, i); }
        },
        gauge: {
            label: {
                format: function (value, ratio) {
                    return value;
                },
                show: true // to turn off the min/max labels.                       
            },
            min: retorno.ChargeCarbonMin, // 0 is default, //can handle negative min e.g. vacuum / voltage / current flow / rate of change
            max: retorno.ChargeCarbonMax // 100 is default
            //    units: ' %',
            //    width: 39 // for adjusting arc thickness
        },
        color: {
            pattern: ['#1887ef'], // the three color levels for the percentage values.
            threshold: {
                //            unit: 'value', // percentage is default
                //            max: 200, // 100 is default
                values: []
            }
        },
        size: {
            height: 180
        },
        onrendered: function () {
            drawThresholds(this, thresholdOpts, opts, retorno.ChargeCarbonMax, ".DrawChart8", retorno.ChargeCarbonTarget);
        }
    };

    chart = c3.generate(opts);
}

function refresh() {
    //Refresh the page
    location.reload();
}

var tiempo = {};
contador_s = 1;
contador_m = 0;
contador_h = 0;
tiempo.minutos = document.getElementById('minuto');
tiempo.segundos = document.getElementById('segundo');
tiempo.horas = document.getElementById('hora');
window.setInterval(mostrarHoras, 1000);

function mostrarHoras() {
    if (contador_s == 60) {
        contador_s = 0;
        contador_m++;
        if (contador_m < 10) { contador_m = '0' + contador_m; }
        tiempo.minutos.innerHTML = contador_m;
        if (contador_m == 60) {
            contador_m = 0;
            contador_h++;
            if (contador_h < 10) { contador_h = '0' + contador_h; }
            tiempo.horas.innerHTML = contador_h;
            if (contador_h == 24) { contador_h = 0; }
        }
    }
    if (contador_s < 10) { contador_s = '0' + contador_s; }
    if (contador_m == 0) { contador_m = '00'; }
    tiempo.segundos.innerHTML = contador_s;
    tiempo.minutos.innerHTML = contador_m;
    contador_s++;
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