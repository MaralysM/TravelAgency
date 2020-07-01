//import { debug } from "util";

// Función para formatear el datatable de las listas con una clase determinada
// Parametros: 
//  *claseDataTable: Índica el nombre de la clase que contiene la tabla
//  *titulo: Es el título que saldrá en el documento si es exportado a excel, pdf o impresión
//  *columnasAImprimir: Es un arreglo que indica las posiciones de las columnas que se imprimirán,
//  *columnasConCheckBox: Es un arreglo que indica cuáles columnas son checkbox para que en el columnDefs serán formateados
function formatearDataTable(claseDataTable, url, columnas, urlUpdate, identificador, sourceList = "", addcolumn = false, ID_PriceList = 0) {
    debugger
    $.fn.dataTable.pipeline = function (opts) {
        // Configuration options
        var conf = $.extend({
            pages: 5,     // number of pages to cache. That means action(url) will be called in 1st, 6th, 11th ... pages
            url: url,  // url to controller action
            data: ID_PriceList,   // function or object with parameters to send to the server
            method: 'GET' // Ajax HTTP method
        }, opts);

        // Private variables for storing the cache
        var cacheLower = -1;
        var cacheUpper = null;
        var cacheLastRequest = null;
        var cacheLastJson = null;

        return function (request, drawCallback, settings) {
            var ajax = false;
            var requestStart = request.start;
            var drawStart = request.start;
            var requestLength = request.length;
            var requestEnd = requestStart + requestLength;

            if (settings.clearCache) {
                // API requested that the cache be cleared
                ajax = true;
                settings.clearCache = false;
            }
            else if (cacheLower < 0 || requestStart < cacheLower || requestEnd > cacheUpper) {
                // outside cached data - need to make a request
                ajax = true;
            }
            else if (JSON.stringify(request.order) !== JSON.stringify(cacheLastRequest.order) ||
                JSON.stringify(request.columns) !== JSON.stringify(cacheLastRequest.columns) ||
                JSON.stringify(request.search) !== JSON.stringify(cacheLastRequest.search)
            ) {
                // properties changed (ordering, columns, searching)
                ajax = true;
            }

            // Store the request for checking next time around
            cacheLastRequest = $.extend(true, {}, request);

            if (ajax) {
                // Need data from the server
                if (requestStart < cacheLower) {
                    requestStart = requestStart - (requestLength * (conf.pages - 1));

                    if (requestStart < 0) {
                        requestStart = 0;
                    }
                }

                cacheLower = requestStart;
                cacheUpper = requestStart + (requestLength * conf.pages);

                request.start = requestStart;
                request.length = requestLength * conf.pages;
                request.data = ID_PriceList;
                // Provide the same `data` options as DataTables.
                if (typeof conf.data === 'function') {
                    // As a function it is executed with the data object as an arg
                    // for manipulation. If an object is returned, it is used as the
                    // data object to submit
                    var d = conf.data(request);
                    if (d) {
                        $.extend(request, d);
                    }
                }
                else if ($.isPlainObject(conf.data)) {
                    // As an object, the data given extends the default
                    $.extend(request, conf.data);
                }

                settings.jqXHR = $.ajax({
                    "type": conf.method,
                    "url": conf.url,
                    "data": request,
                    "dataType": "json",
                    "cache": false,
                    "success": function (json) {
                        cacheLastJson = $.extend(true, {}, json);

                        if (cacheLower != drawStart) {
                            json.data.splice(0, drawStart - cacheLower);
                        }
                        if (requestLength >= -1) {
                            json.data.splice(requestLength, json.data.length);
                        }

                        drawCallback(json);
                    }
                });
            }
            else {
                json = $.extend(true, {}, cacheLastJson);
                json.draw = request.draw; // Update the echo for each response
                json.data.splice(0, requestStart - cacheLower);
                json.data.splice(requestLength, json.data.length);

                drawCallback(json);
            }
        }
    };


    $.fn.dataTable.Api.register('clearPipeline()', function () {
        return this.iterator('table', function (settings) {
            settings.clearCache = true;
        });
    });
    if (addcolumn) {      
        columnas.push({
            'data': null,
            'render': function (data, type, row) {                
                var list = '<select disabled class="form-control moneda_' + row[identificador] + ' ' + row[identificador] + ' " placeholder="Moneda" ><option value = "0" > Seleccione </option ></select >';
                FillMonedaList(row[identificador], sourceList, row.priceListItem.moneda);
                return list;

            }
        });

        columnas.push({
            'data': null,
            'render': function (data, type, row) {
                let input = ' <input disabled type="text" placeholder="Precio"  class="form-control price_' + row[identificador] + ' ' + row[identificador] + '  Price" value ="' + row.priceListItem.precioPrincipal + '"/ >';
                ApplyMaskInputPrice();
                return input;
            }
        });

        columnas.push({
            'data': null,
            'render': function (data, type, row) {
                return '<textarea  disabled asp-for="Notes" data-id="' + row[identificador] + '" placeholder="Notas" class="form-control note_' + row[identificador] + ' ' + row[identificador] + '" maxlength="100">' + row.priceListItem.notas + '</textarea>';
            }
        });
        columnas.push({
            'data': null,
            'render': function (data, type, row) {
                return '<div class="btn-group"><a class="btn btn-primary edit" data-id="' + row[identificador] + '" id="' + row[identificador] + '"><i class="fa fa-edit"></i> </a>'
                    + '<a data-id="' + row.priceListItem.iD_PriceListItem + '" data-row ="' + row[identificador] + '" class="btn btn-danger delete"><i class="fa fa-remove"></i> </a></div>';
            }
        });



    } else {

        columnas.push({
            'data': null,
            'render': function (data, type, row) {
                return '<div class="btn-group"><a class="btn btn-primary" href=' + urlUpdate + row[identificador] + '><i class="fa fa-edit"></i> Editar</a>'
                    + '<a data-id="' + row[identificador] + '" class="btn btn-danger delete"><i class="fa fa-remove"></i> Eliminar</a></div>';
            }
        });
    }

    $(claseDataTable).DataTable({
        "processing": true,
        "serverSide": true,
        "searching": true,
        "paging": true,
        "iDisplayLength": 25,
        "ajax": $.fn.dataTable.pipeline({
            url: url,
            pages: 5 //number of pages to cache
        }),
        "columns": columnas,
        "language": {
            "url": "https://cdn.datatables.net/plug-ins/1.10.20/i18n/Spanish.json"
        }
    });
}

//Sin Boton Eliminar
function formatearDataTableSinBotonEliminar(claseDataTable, url, columnas, urlUpdate, identificador, sourceList = "", addcolumn = false, ID_PriceList = 0) {
    //debugger
    $.fn.dataTable.pipeline = function (opts) {
        // Configuration options
        var conf = $.extend({
            pages: 5,     // number of pages to cache. That means action(url) will be called in 1st, 6th, 11th ... pages
            url: url,  // url to controller action
            data: ID_PriceList,   // function or object with parameters to send to the server
            method: 'GET' // Ajax HTTP method
        }, opts);

        // Private variables for storing the cache
        var cacheLower = -1;
        var cacheUpper = null;
        var cacheLastRequest = null;
        var cacheLastJson = null;

        return function (request, drawCallback, settings) {
            var ajax = false;
            var requestStart = request.start;
            var drawStart = request.start;
            var requestLength = request.length;
            var requestEnd = requestStart + requestLength;

            if (settings.clearCache) {
                // API requested that the cache be cleared
                ajax = true;
                settings.clearCache = false;
            }
            else if (cacheLower < 0 || requestStart < cacheLower || requestEnd > cacheUpper) {
                // outside cached data - need to make a request
                ajax = true;
            }
            else if (JSON.stringify(request.order) !== JSON.stringify(cacheLastRequest.order) ||
                JSON.stringify(request.columns) !== JSON.stringify(cacheLastRequest.columns) ||
                JSON.stringify(request.search) !== JSON.stringify(cacheLastRequest.search)
            ) {
                // properties changed (ordering, columns, searching)
                ajax = true;
            }

            // Store the request for checking next time around
            cacheLastRequest = $.extend(true, {}, request);

            if (ajax) {
                // Need data from the server
                if (requestStart < cacheLower) {
                    requestStart = requestStart - (requestLength * (conf.pages - 1));

                    if (requestStart < 0) {
                        requestStart = 0;
                    }
                }

                cacheLower = requestStart;
                cacheUpper = requestStart + (requestLength * conf.pages);

                request.start = requestStart;
                request.length = requestLength * conf.pages;
                request.data = ID_PriceList;
                // Provide the same `data` options as DataTables.
                if (typeof conf.data === 'function') {
                    // As a function it is executed with the data object as an arg
                    // for manipulation. If an object is returned, it is used as the
                    // data object to submit
                    var d = conf.data(request);
                    if (d) {
                        $.extend(request, d);
                    }
                }
                else if ($.isPlainObject(conf.data)) {
                    // As an object, the data given extends the default
                    $.extend(request, conf.data);
                }

                settings.jqXHR = $.ajax({
                    "type": conf.method,
                    "url": conf.url,
                    "data": request,
                    "dataType": "json",
                    "cache": false,
                    "success": function (json) {
                        cacheLastJson = $.extend(true, {}, json);

                        if (cacheLower != drawStart) {
                            json.data.splice(0, drawStart - cacheLower);
                        }
                        if (requestLength >= -1) {
                            json.data.splice(requestLength, json.data.length);
                        }

                        drawCallback(json);
                    }
                });
            }
            else {
                json = $.extend(true, {}, cacheLastJson);
                json.draw = request.draw; // Update the echo for each response
                json.data.splice(0, requestStart - cacheLower);
                json.data.splice(requestLength, json.data.length);

                drawCallback(json);
            }
        }
    };


    $.fn.dataTable.Api.register('clearPipeline()', function () {
        return this.iterator('table', function (settings) {
            settings.clearCache = true;
        });
    });
    if (addcolumn) {
        columnas.push({
            'data': null,
            'render': function (data, type, row) {
                var list = '<select disabled class="form-control moneda_' + row[identificador] + ' ' + row[identificador] + ' " placeholder="Moneda" ><option value = "0" > Seleccione </option ></select >';
                FillMonedaList(row[identificador], sourceList, row.priceListItem.moneda);
                return list;

            }
        });

        columnas.push({
            'data': null,
            'render': function (data, type, row) {
                let input = ' <input disabled type="text" placeholder="Precio"  class="form-control price_' + row[identificador] + ' ' + row[identificador] + '  Price" value ="' + row.priceListItem.precioPrincipal + '"/ >';
                ApplyMaskInputPrice();
                return input;
            }
        });

        columnas.push({
            'data': null,
            'render': function (data, type, row) {
                return '<textarea  disabled asp-for="Notes" data-id="' + row[identificador] + '" placeholder="Notas" class="form-control note_' + row[identificador] + ' ' + row[identificador] + '" maxlength="100">' + row.priceListItem.notas + '</textarea>';
            }
        });
        columnas.push({
            'data': null,
            'render': function (data, type, row) {
                return '<div class="btn-group"><a class="btn btn-primary edit" data-id="' + row[identificador] + '" id="' + row[identificador] + '"><i class="fa fa-edit"></i> </a>'
                    + '<a data-id="' + row.priceListItem.iD_PriceListItem + '" data-row ="' + row[identificador] + '" class="btn btn-danger delete"><i class="fa fa-remove"></i> </a></div>';
            }
        });



    } else {

        columnas.push({
            'data': null,
            'render': function (data, type, row) {
                return '<div class="btn-group"><a class="btn btn-primary" href=' + urlUpdate + row[identificador] + '><i class="fa fa-edit"></i> Editar</a>';
                    //+ '<a data-id="' + row[identificador] + '" class="btn btn-danger delete"><i class="fa fa-remove"></i> Eliminar</a></div>';
            }
        });
    }

    $(claseDataTable).DataTable({
        "processing": true,
        "serverSide": true,
        "searching": true,
        "paging": true,
        "iDisplayLength": 25,
        "ajax": $.fn.dataTable.pipeline({
            url: url,
            pages: 5 //number of pages to cache
        }),
        "columns": columnas,
        "language": {
            "url": "https://cdn.datatables.net/plug-ins/1.10.20/i18n/Spanish.json"
        }
    });
}

//Sin Botones Editar Eliminar
function formatearDataTableSinBotonesEditarEliminar(claseDataTable, url, columnas, urlUpdate, identificador) {
    $.fn.dataTable.pipeline = function (opts) {
        // Configuration options
        var conf = $.extend({
            pages: 5,     // number of pages to cache. That means action(url) will be called in 1st, 6th, 11th ... pages
            url: url,  // url to controller action
            data: null,   // function or object with parameters to send to the server
            method: 'GET' // Ajax HTTP method
        }, opts);

        // Private variables for storing the cache
        var cacheLower = -1;
        var cacheUpper = null;
        var cacheLastRequest = null;
        var cacheLastJson = null;

        return function (request, drawCallback, settings) {
            var ajax = false;
            var requestStart = request.start;
            var drawStart = request.start;
            var requestLength = request.length;
            var requestEnd = requestStart + requestLength;

            if (settings.clearCache) {
                // API requested that the cache be cleared
                ajax = true;
                settings.clearCache = false;
            }
            else if (cacheLower < 0 || requestStart < cacheLower || requestEnd > cacheUpper) {
                // outside cached data - need to make a request
                ajax = true;
            }
            else if (JSON.stringify(request.order) !== JSON.stringify(cacheLastRequest.order) ||
                JSON.stringify(request.columns) !== JSON.stringify(cacheLastRequest.columns) ||
                JSON.stringify(request.search) !== JSON.stringify(cacheLastRequest.search)
            ) {
                // properties changed (ordering, columns, searching)
                ajax = true;
            }

            // Store the request for checking next time around
            cacheLastRequest = $.extend(true, {}, request);

            if (ajax) {
                // Need data from the server
                if (requestStart < cacheLower) {
                    requestStart = requestStart - (requestLength * (conf.pages - 1));

                    if (requestStart < 0) {
                        requestStart = 0;
                    }
                }

                cacheLower = requestStart;
                cacheUpper = requestStart + (requestLength * conf.pages);

                request.start = requestStart;
                request.length = requestLength * conf.pages;

                // Provide the same `data` options as DataTables.
                if (typeof conf.data === 'function') {
                    // As a function it is executed with the data object as an arg
                    // for manipulation. If an object is returned, it is used as the
                    // data object to submit
                    var d = conf.data(request);
                    if (d) {
                        $.extend(request, d);
                    }
                }
                else if ($.isPlainObject(conf.data)) {
                    // As an object, the data given extends the default
                    $.extend(request, conf.data);
                }

                settings.jqXHR = $.ajax({
                    "type": conf.method,
                    "url": conf.url,
                    "data": request,
                    "dataType": "json",
                    "cache": false,
                    "success": function (json) {
                        cacheLastJson = $.extend(true, {}, json);

                        if (cacheLower != drawStart) {
                            json.data.splice(0, drawStart - cacheLower);
                        }
                        if (requestLength >= -1) {
                            json.data.splice(requestLength, json.data.length);
                        }

                        drawCallback(json);
                    }
                });
            }
            else {
                json = $.extend(true, {}, cacheLastJson);
                json.draw = request.draw; // Update the echo for each response
                json.data.splice(0, requestStart - cacheLower);
                json.data.splice(requestLength, json.data.length);

                drawCallback(json);
            }
        }
    };


    $.fn.dataTable.Api.register('clearPipeline()', function () {
        return this.iterator('table', function (settings) {
            settings.clearCache = true;
        });
    });

    //columnas.push({
    //    'data': null,
    //    'render': function (data, type, row) {
    //        return '<div class="btn-group"><a class="btn btn-primary" href=' + urlUpdate + row[identificador] + '><i class="fa fa-edit"></i> Editar</a>'
    //            + '<a data-id="' + row[identificador] + '" class="btn btn-danger delete"><i class="fa fa-remove"></i> Eliminar</a></div>'
    //    }
    //});

    $(claseDataTable).DataTable({
        "processing": true,
        "serverSide": true,
        "searching": true,
        "paging": true,
        "iDisplayLength": 25,
        "ajax": $.fn.dataTable.pipeline({
            url: url,
            pages: 5 //number of pages to cache
        }),
        "columns": columnas,
        "language": {
            "url": "https://cdn.datatables.net/plug-ins/1.10.20/i18n/Spanish.json"
        }
    });
}

function formatearDataTableOriginal(claseDataTable, titulo, columnasAImprimir, columnasConCheckBox, orientacionHojaPdf, elemento = null) {
    if (typeof orientacionHojaPdf === 'undefined')
        orientacionHojaPdf = 'vertical';
    tablaAux = $(claseDataTable).DataTable({

        //language: {
        //    "url": "https://cdn.datatables.net/plug-ins/9dcbecd42ad/i18n/Spanish.json"
        //},
        "iDisplayLength": 25,
        responsive: true,
        //dom: '<"html5buttons"B>lTfgitp',
        dom: '<"col-md-12"B><"col-md-12"lf>rtip',
        //buttons: [{ extend: "copy", className: "btn-info" }, { extend: "csv", className: "btn-info" }, { extend: "excel", className: "btn-info", title: "XLS-File" }, { extend: "pdf", className: "btn-info", title: $("title").text() }, { extend: "print", className: "btn-info" }]
        buttons: [
            {
                extend: 'excel',
                title: titulo,
                className: "btn-info",
                exportOptions: {
                    orthogonal: 'sort',
                    columns: columnasAImprimir
                }
            },
            {
                extend: 'pdf',
                title: titulo,
                className: "btn-info",
                orientation: orientacionHojaPdf,
                exportOptions: {
                    orthogonal: 'sort',
                    columns: columnasAImprimir
                }
            },
            {
                extend: 'print',
                className: "btn-info",
                exportOptions: {
                    orthogonal: 'sort',
                    columns: columnasAImprimir
                },
                customize: function (win) {
                    $(win.document.body).addClass('white-bg');
                    $(win.document.body).css('font-size', '10px');
                    $(win.document.body).find('table')
                        .addClass('compact')
                        .css('font-size', 'inherit');
                }
            },
            
        ],

        columnDefs: [{
            targets: columnasConCheckBox,
            render: function (data, type, row, meta) {
                if (type === 'sort') {
                    var $input = $(data).find('input[type="checkbox"]').addBack();
                    data = ($input.prop('checked')) ? "Yes" : "No";
                }
                return data;
            }
        }],
        language: {
            "url": "https://cdn.datatables.net/plug-ins/1.10.20/i18n/Spanish.json"
        },
        iDisplayLength: 25,
        lengthChange: false
    });
    if (elemento != null)
        elemento.tabla = tablaAux;
}
function eliminarRegistro(btnclass, valor, ruta, tabla, btn) {
    if (tabla == null)
        tabla = '#table';
    var link = $(btn);
    var data = {
        id: link.attr(valor)
    };
    swal({
        title: "Delete",
        text: "Are you sure delete?",
        type: "warning",
        showCancelButton: true,
        closeOnConfirm: true,
        confirmButtonText: "Yes",
        CancelButtonText: "No",
        confirmButtonColor: "#f05050"
    },
        function () {
            
            $.ajax({
                url: ruta,
                method: "POST",
                data: data,
                success: function (retorno) {
                    debugger
                    if (retorno.success == null) {
                        $(`${btnclass}[${valor}="${link.attr(valor)}"]`).parents("tr").fadeOut(function () {
                            if (table == null)
                                $(`${btnclass}[${valor}="${link.attr(valor)}"]`).parents("tr").remove();
                            else
                                $(tabla).DataTable().row($(`${btnclass}[${valor}="${link.attr(valor)}"]`).parents("tr")).remove().draw(false)
                        });
                        toastr.success('Register deleted');
                    }
                    else {
                        toastr.error(`An error occurred while deleting. ${retorno.error}`, "Error", { timeOut: 6000 });
                    }
                },
                error: function (response) {
                    toastr.error("Error", `An error occurred while deleting. ${response.responseText}`, "Error", { timeOut: 6000 });
                }
            });
        });
}
function sincronizarData(btnclass, valor, ruta, tabla, btn) {
    if (tabla == null)
        tabla = '#table';
    var link = $(btn);
    
    swal({
        title: "Sincronizar",
        text: "¿Está seguro que desea sincronizar?",
        type: "warning",
        showCancelButton: true,
        closeOnConfirm: true,
        confirmButtonText: "Sí",
        CancelButtonText: "No",
        confirmButtonColor: "#1BC107"
    },
        function () {

            $(btnclass).html("<i class='fa fa-refresh fa-spin'></i> Sincronizando");
            $(btnclass).attr("disabled", "disabled");
            $.ajax({
                
                url: ruta,
                method: "GET",
                success: function (retorno) {
                    debugger
                    if (retorno) {
                        //$(`${btnclass}[${valor}="${link.attr(valor)}"]`).parents("tr").fadeOut(function () {
                        //    if (table == null)
                        //        $(`${btnclass}[${valor}="${link.attr(valor)}"]`).parents("tr").remove();
                        //    else
                        //        $(tabla).DataTable().row($(`${btnclass}[${valor}="${link.attr(valor)}"]`).parents("tr")).remove().draw(false)
                        //});
                        toastr.success("La sincronización ha sido exitosa");
                        
                        var table = $(tabla).DataTable();
                        table.clearPipeline().draw();
                    }
                    else {
                        toastr.error("Un error ocurrió mientras sincronizamos", "Error", { timeOut: 6000 });
                    }
                    $(btnclass).html("<i class='fa fa-refresh'></i> Sincronizar");
                    $(btnclass).removeAttr("disabled");
                },
                error: function (response) {
                    toastr.error("Error", "Un error ocurrió mientras sincronizamos.", "Error", { timeOut: 6000 });
                    $(btnclass).html("<i class='fa fa-refresh'></i> Sincronizar");
                }
            });
        });
}

function eliminarRegistro1(btnclass, valor, ruta, tabla) {
    $(btnclass).click(function (e) {
        var link = $(this);
        var data = {
            id: link.attr(valor)
        };
        swal({
            title: "Delete",
            text: "Are you sure delete?",
            type: "warning",
            showCancelButton: true,
            closeOnConfirm: true,
            confirmButtonText: "Yes",
            CancelButtonText: "No",
            confirmButtonColor: "#f05050"
        },
            function () {
                $.ajax({

                    url: ruta,
                    method: "POST",
                    data: data,
                    success: function (retorno) {
                        debugger
                        if (retorno.success == null) {

                            link.parents("tr").fadeOut(function () {
                                if (tabla == null)
                                    $(this).remove();
                                else
                                    tabla.row($(link).parents("tr")).remove().draw(false)
                            });
                        }
                        else {
                            swal("An error occurred while deleting!", retorno.error)
                        }
                    },
                    error: function (response) {
                        alert(`An error occurred while deleting. ${response.responseText}`);
                    }
                });
            });

    });
}