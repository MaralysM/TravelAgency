$(document).ready(function () {
    GetPayTypeGraphData(1);
    GetActivesEmployeesByDeparmentData();
    GetCostByDeparmentData(1);
});

function GetPayTypeGraphData(range) {
    $.ajax({
        url: "/Dashboard/GetPayTypeDashboardAsync/",
        method: "GET",
        data : null,
        beforeSend: function () {
            $('.ajax-loader').css("visibility", "visible");
        },
        success: function (retorno) {
            var grafico = retorno.grafico;
            var dataGraficoPayTypePercent = {
                labels: grafico.labels,
                datasets:
                    $.map(grafico.dataSets, function (item, i) {
                        var ds = new Object();
                        ds.backgroundColor = item.listaColores;
                        //ds.borderColor = item.ListaColoresBordercolor;
                        ds.data = item.dataDouble;
                        return ds;
                    }),
            };

            var optionsGraficoPayTypePercent = {
                animation: {
                    animateScale: true
                },
                legend: {
                    //position: 'top',
                    display: true
                },
                plugins: {
                    labels: [
                        {
                            render: 'label',
                            position: 'outside',
                            //arc: true,
                        },
                        {
                            render: 'percentage',
                            fontSize: 9,
                            precision: 2                        }
                    ]
                },
                tooltips: {
                    callbacks: {
                        label: function (t, d) {
                            console.log(t);
                            var xLabel = d.labels[t.index];
                            var yLabel = d.datasets[0].data[t.index] >= 1000 ? '$ ' + d.datasets[0].data[t.index].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",") : '$ ' + t.yLabel;
                            return xLabel + ': ' + yLabel;
                        }
                    }
                }
            };

            var canvasGraficoPayTypePercent = document.getElementById("graficoPayTypePercent").getContext("2d");

            var objDoughnutCantidadEstatusSolicitudesXAnho = new Chart(canvasGraficoPayTypePercent, {
                type: 'doughnut',
                data: dataGraficoPayTypePercent,
                options: optionsGraficoPayTypePercent
            });
        },
        error: function (response) {
            bootbox.alert("Hay un problema: " + response.responseText);
        }
    });
}

function GetActivesEmployeesByDeparmentData() {
    $.ajax({
        url: "/Dashboard/GetDashboardActivesEmployeesByDeparment/",
        method: "GET",
        beforeSend: function () {
            $('.ajax-loader').css("visibility", "visible");
        },
        success: function (retorno) {
            var grafico = retorno.grafico;
            var dataActivesEmployeesByDeparment =

            {
                labels: grafico.labels,
                datasets:
                    $.map(grafico.dataSets, function (item, i) {
                        var ds = new Object();
                        ds.label = item.label;
                        ds.backgroundColor = item.backgroundColor;
                        ds.borderColor = item.borderColor;
                        ds.data = item.dataInt;
                        ds.borderWidth = 2;
                        return ds;
                    }),
            }
            var optionsActivesEmployeesByDeparment = {
                responsive: true,
                legend: {
                    display:false
                },
                
                scales: {
                    
                    yAxes: [{
                        ticks: {
                            beginAtZero: true
                            
                        }
                    }]
                },

                plugins: {
                    labels: [
                        {
                            render: 'value',
                            position: 'outside',
                            //arc: true,
                        }
                    ]
                },
            }
            var canvasActivesEmployeesByDeparment = document.getElementById("graficoActiveEmployeesByDepartment").getContext("2d");
            //------------ CONDICIONES RESPONSIVAS ---------------
            //if ($(window).width() < 321)
            //    canvasActivesEmployeesByDeparment.canvas.height = 600;
            //else if ($(window).width() < 769)
            //    canvasActivesEmployeesByDeparment.canvas.height = 400;
            //else
            //    canvasActivesEmployeesByDeparment.canvas.height = 400;
            //------------ CONDICIONES RESPONSIVAS ---------------
            var objActivesEmployeesByDeparment = new Chart(canvasActivesEmployeesByDeparment, {
                type: 'bar',
                data: dataActivesEmployeesByDeparment,
                options: optionsActivesEmployeesByDeparment
            });
        },
        error: function (response) {
            bootbox.alert("Hay un problema: " + response.responseText);
        }
    });
}

function GetCostByDeparmentData(range) {
    $.ajax({
        url: "/Dashboard/GetDashboardCostByDeparment/",
        method: "GET",
        beforeSend: function () {
            $('.ajax-loader').css("visibility", "visible");
        },
        success: function (retorno) {
            var grafico = retorno.grafico;
            var dataActivesEmployeesByDeparment =

            {
                labels: grafico.labels,
                datasets:
                    $.map(grafico.dataSets, function (item, i) {
                        var ds = new Object();
                        ds.label = item.label;
                        ds.backgroundColor = item.backgroundColor;
                        ds.borderColor = item.borderColor;
                        ds.data = item.dataDouble;
                        ds.borderWidth = 2;
                        return ds;
                    }),
            }
            var optionsActivesEmployeesByDeparment = {
                responsive: true,
                legend: {
                    display: false
                },

                scales: {

                    yAxes: [{
                        ticks: {
                            beginAtZero: true

                        }
                    }]
                },

                plugins: {
                    labels: [
                        {
                            render: 'value',
                            position: 'outside',
                            //arc: true,
                        }
                    ]
                },
            }
            var canvasActivesEmployeesByDeparment = document.getElementById("graficoCostByDepartment").getContext("2d");
            //------------ CONDICIONES RESPONSIVAS ---------------
            //if ($(window).width() < 321)
            //    canvasActivesEmployeesByDeparment.canvas.height = 600;
            //else if ($(window).width() < 769)
            //    canvasActivesEmployeesByDeparment.canvas.height = 400;
            //else
            //    canvasActivesEmployeesByDeparment.canvas.height = 400;
            //------------ CONDICIONES RESPONSIVAS ---------------
            var objActivesEmployeesByDeparment = new Chart(canvasActivesEmployeesByDeparment, {
                type: 'horizontalBar',
                data: dataActivesEmployeesByDeparment,
                options: optionsActivesEmployeesByDeparment
            });
        },
        error: function (response) {
            bootbox.alert("Hay un problema: " + response.responseText);
        }
    });
}