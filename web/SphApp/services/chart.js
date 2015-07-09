define(["services/datacontext", objectbuilders.system], function (context, system) {

    // ReSharper disable InconsistentNaming
    var _field = ko.observable(),
        _format = function () {
            switch (_dateInterval()) {
                case "year": return "yyyy";
                case "month": return "MMM yyyy";
                case "week": return "w yyyy";
                case "day": return "yyyy-MM-dd";
                case "hour": return "yyyy-MM-dd HH";
                case "minute": return "yyyy-MM-dd HH:mm";
                case "second": return "yyyy-MM-dd HH:mm:ss";

                default: return "yyyy-MM-dd";
            }
        },
        _aggregate = ko.observable(),
        _fieldOptions = ko.observableArray(),
        _dateInterval = ko.observable(),
        _histogramInterval = ko.observable(),
        _entity = ko.observable(),
        _entityDefinitionId = ko.observable(),
        _query = null,
        _type = ko.observable("pie"),
        _click = null,
        _charts = ko.observableArray(),
        _viewId = ko.observable(),
        _selectedChartId = ko.observable(),
        _selectedChart = ko.observable(),
// ReSharper restore InconsistentNaming
        init = function (entity, query, click, viewId) {
            var tcs = new $.Deferred(),
                query1 = String.format("Entity eq '{0}'", entity),
                query2 = String.format("Name eq '{0}'", entity);

            if (viewId) {
                _viewId(viewId);
            }

            _entity(entity);
            if (typeof query === "function") {
                _query = query();
            } else {
                _query = query;
            }
            _click = click;
            context.loadAsync("EntityChart", query1)
                .done(function (lo) {
                    _charts(lo.itemCollection);
                });
            context.getScalarAsync("EntityDefinition", query2, "Id")
                .done(function (id) {
                    _entityDefinitionId(id);
                    // get the fields
                    $.get("/entity-definition/variable-path/" + id)
                        .done(function (ps) {
                            _fieldOptions(ps);
                            tcs.resolve(true);
                        });
                });
            return tcs.promise();
        },
        getMissingBucket = function (fd) {

            var missingQuery = JSON.parse(JSON.stringify(_query));
            if (_aggregate() === "term") {
                missingQuery.aggs = {
                    "category": {
                        "missing": {
                            "field": fd
                        }
                    }
                };
            }

            if (_aggregate() === "histogram") {
                missingQuery.aggs = {
                    "category": {
                        "histogram": {
                            "field": fd,
                            "interval": parseInt(_histogramInterval()),
                            "min_doc_count": 0
                        }
                    }
                };
            }


            if (_aggregate() === "date_histogram") {
                missingQuery.aggs = {
                    "category": {
                        "date_histogram": {
                            "field": fd,
                            "interval": _dateInterval(),
                            "format": _format()
                        }
                    }
                };
            }

            var tcs = new $.Deferred();
            context.searchAsync(_entity(), missingQuery)
                .fail(tcs.reject)
                .done(function (result) {
                    tcs.resolve(result.aggregations.category.doc_count);

                });
            return tcs.promise();


        },
        draw = function (fd) {
            if (!_field()) {
                _field(fd);
            }
            if (!fd) {
                return Task.fromResult(false);
            }

            if (_aggregate() === "term") {
                _query.aggs = {
                    "category": {
                        "terms": {
                            "field": fd,
                            "size": 0
                        }
                    }
                };
            }

            if (_aggregate() === "histogram") {
                _query.aggs = {
                    "category": {
                        "histogram": {
                            "field": fd,
                            "interval": parseInt(_histogramInterval()),
                            "min_doc_count": 0
                        }
                    }
                };
            }


            if (_aggregate() === "date_histogram") {
                _query.aggs = {
                    "category": {
                        "date_histogram": {
                            "field": fd,
                            "interval": _dateInterval(),
                            "format": _format()
                        }
                    }
                };
            }

            var missing = 0;
            return getMissingBucket(fd)
                .then(function(result){
                  missing = result;
                  return context.searchAsync(_entity(), _query);
                })
                .then(function (result) {

                    var buckets = result.aggregations.category.buckets || result.aggregations.category,
                        data = _(buckets).map(function (v) {
                            return {
                                category: v.key_as_string || v.key.toString(),
                                value: v.doc_count
                            };
                        }),
                        categories = _(buckets).map(function (v) {
                            return v.key_as_string || v.key.toString();
                        }),
                        container = $("div#chart-container").length ? "div#chart-container" : "div#chart-" + _entity();

                        if(missing > 0){

                          data.push({category : "<Empty>", value : missing});
                        }
                        var chart = $(container).empty().kendoChart({
                            theme: "metro",
                            title: {
                                text: _entity() + " count by " + _field()
                            },
                            legend: {
                                position: "bottom"
                            },
                            seriesDefaults: {
                                labels: {
                                    visible: true,
                                    format: "{0}"
                                }
                            },
                            series: [
                                {
                                    type: _type(),
                                    data: data
                                }
                            ],
                            categoryAxis: {
                                categories: categories,
                                majorGridLines: {
                                    visible: false
                                }
                            },
                            seriesClick: function (e) {
                                if (typeof _click === "function") {
                                    _click({
                                        query: _query,
                                        category: e.category,
                                        value: e.value,
                                        field: _field(),
                                        aggregate: _aggregate(),
                                        histogramInterval: _histogramInterval(),
                                        dateInterval: _dateInterval()

                                    });

                                }
                            }, tooltip: {
                                visible: true,
                                format: "{0}",
                                template: "#= category #: #= value #"
                            }
                        }).data("kendoChart");

                });


        },
        execute = function () {
            return draw(_field());
        },
        saveAs = function () {
            var tcs = new $.Deferred(),
                editedChart = new bespoke.sph.domain.EntityChart({
                    WebId: system.guid(),
                    Name: "",
                    Query: ko.mapping.toJSON(_query),
                    Entity: _entity(),
                    EntityDefinitionId: _entityDefinitionId(),
                    Type: _type(),
                    EntityViewId: _viewId(),
                    Aggregate: _aggregate(),
                    Field: _field(),
                    DateInterval: _dateInterval(),
                    HistogramInterval: _histogramInterval()

                });

            require(["viewmodels/entity.chart.dialog", "durandal/app"], function (dialog, app2) {
                dialog.chart(editedChart);

                app2.showDialog(dialog)
                    .done(function (result) {
                        if (!result) return;
                        if (result === "OK") {
                            context.post(ko.mapping.toJSON(editedChart), "/sph/entitychart/save")
                                .done(function (r1) {
                                    tcs.resolve(true);
                                    editedChart.Id(r1.id);
                                    _charts.push(editedChart);
                                });
                        } else {
                            tcs.resolve(false);
                        }
                    });

            });

            return tcs.promise();
        },
        save = function () {
            var tcs = new $.Deferred();
            _selectedChart().Type(_type());
            _selectedChart().Aggregate(_aggregate());
            _selectedChart().Field(_field());
            _selectedChart().DateInterval(_dateInterval());
            _selectedChart().HistogramInterval(_histogramInterval());

            require(["viewmodels/entity.chart.dialog", "durandal/app"], function (dialog, app2) {
                dialog.chart(_selectedChart());

                app2.showDialog(dialog)
                    .done(function (result) {
                        if (!result) return;
                        if (result === "OK") {
                            context.post(ko.mapping.toJSON(_selectedChart), "/sph/entitychart/save")
                                .done(tcs.resolve);
                        } else {
                            tcs.resolve(false);
                        }
                    });

            });

            return tcs.promise();
        },
        remove = function () {
            var tcs = new $.Deferred();

            context.post(ko.mapping.toJSON(_selectedChart), "/sph/entitychart/remove")
                .done(function () {
                    tcs.resolve(true);
                    var rt = _(_charts()).find(function (v) { return v.Id() === _selectedChartId(); });
                    _charts.remove(rt);
                });


            return tcs.promise();
        },
        pin = function () {
            if (typeof _selectedChart().IsDashboardItem === "function") {
                _selectedChart().IsDashboardItem(true);
            } else {
                _selectedChart().IsDashboardItem = ko.observable(true);
            }

            var tcs = new $.Deferred();

            context.post(ko.mapping.toJSON(_selectedChart), "/sph/entitychart/save")
                .done(tcs.resolve);

            return tcs.promise();
        },
        unpin = function () {
            if (typeof _selectedChart().IsDashboardItem === "function") {
                _selectedChart().IsDashboardItem(false);
            } else {
                _selectedChart().IsDashboardItem = ko.observable(false);
            }

            var tcs = new $.Deferred();

            context.post(ko.mapping.toJSON(_selectedChart), "/sph/entitychart/save")
                .done(tcs.resolve);

            return tcs.promise();
        };


    _field.subscribe(function (f) {
        if (_aggregate() === "term") {
            draw(f);
        }
        if (_aggregate() === "date_histogram" && _dateInterval()) {
            draw(f);
        }
        if (_aggregate() === "histogram" && _histogramInterval()) {
            draw(f);
        }
    });
    _histogramInterval.subscribe(function (hi) {
        if (_aggregate() === "histogram" && hi) {
            draw(_field());
        }
    });
    _dateInterval.subscribe(function (di) {
        if (_aggregate() === "date_histogram" && di) {
            draw(_field());
        }
    });
    _selectedChartId.subscribe(function (di) {
        if (!di) {
            return Task.fromResult(0);
        }
        var tcs = new $.Deferred();
        context.loadOneAsync("EntityChart", "Id eq '" + di + "'")
            .done(function (ec) {
                if (!ec) {
                    return;
                }
                _selectedChart(ec);
                _field("");// stop draw
                _type(ec.Type());
                _aggregate(ec.Aggregate());
                _dateInterval(ec.DateInterval());
                _histogramInterval(ec.HistogramInterval());
                _field(ec.Field());

            });
        return tcs.promise();
    });
    _type.subscribe(function (type) {
        if (!_field()) {
            return;
        }
        if (!type) {
            return;
        }
        if (_aggregate() === "term") {
            draw(_field());
        }
        if (_aggregate() === "date_histogram" && _dateInterval()) {
            draw(_field());
        }
        if (_aggregate() === "histogram" && _histogramInterval()) {
            draw(_field());
        }
    });

    return {
        selectedChartId: _selectedChartId,
        save: save,
        saveAs: saveAs,
        remove: remove,
        pin: pin,
        unpin: unpin,
        type: _type,
        charts: _charts,
        execute: execute,
        format: _format,
        aggregate: _aggregate,
        dateInterval: _dateInterval,
        histogramInterval: _histogramInterval,
        draw: draw,
        init: init,
        fieldOptions: _fieldOptions,
        field: _field
    };
});
