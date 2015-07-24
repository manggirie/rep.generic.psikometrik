/// <reference path="/Scripts/jquery-2.1.1.intellisense.js" />
/// <reference path="/Scripts/knockout-3.2.0.debug.js" />
/// <reference path="/Scripts/knockout.mapping-latest.debug.js" />
/// <reference path="/Scripts/require.js" />
/// <reference path="/Scripts/underscore.js" />
/// <reference path="/Scripts/moment.js" />
/// <reference path="../services/datacontext.js" />
/// <reference path="../schemas/trigger.workflow.g.js" />
/// <reference path="../../Scripts/bootstrap.js" />


define(['services/datacontext', 'services/logger', 'plugins/router', "services/config"],
  function(context, logger, router, config) {

    var isBusy = ko.observable(false),
      id = ko.observable([]),
      tools = ko.observableArray([]),
      reports = ko.observableArray([]),
      recentItems = ko.observableArray([]),
      charts = ko.observableArray([]),
      views = ko.observableArray([]),
      entity = ko.observable(new bespoke.sph.domain.EntityDefinition()),
      today = moment().format("YYYY-MM-DDTHH:mm:ss.SSS"),
      query = {
              "query": {
                "filtered": {
                  "filter": {
                    "bool": {
                      "must": [
                        {
                          "term": {
                            "StatusPermohonan": "LULUS"
                          }
                        },
                        {
                          "term": {
                            "Penyelaras": config.userName
                          }
                        },
                        {
                            "range": {
                                "TarikhTamat": {
                                   "from": today
                                }
                             }
                        },
                        {
                             "range": {
                                "TarikhMula": {
                                   "to": today
                                }
                             }
                        }
                      ],
                      "must_not": []
                    }
                  }
                }
              },
              "sort": []
            },
            permohonanLulusList = ko.observableArray(),
      activate = function() {
        var query = String.format("Name eq '{0}'", 'Pengguna'),
          chartsQuery = String.format("Entity eq 'Pengguna' and IsDashboardItem eq 1 and CreatedBy eq ''{0}''", config.userName),
          formsQuery = String.format("EntityDefinitionId eq 'pengguna' and IsPublished eq 1 and IsAllowedNewItem eq 1"),
          edTask = context.loadOneAsync("EntityDefinition", query),
          viewsTask = $.get("/Sph/EntityView/Dashboard/pengguna"),
          permohonanViewsTask = $.get("/Sph/EntityView/Dashboard/permohonan");


          context.getScalarAsync("Pengguna", "MyKad eq '" + config.userName + "'", "NamaJabatan")
            .done(function(jabatan){
              config.namaJabatan = jabatan;
            });

          context.getScalarAsync("Pengguna", "MyKad eq '" + config.userName + "'", "NamaKementerian")
            .done(function(ministry){
              config.namaKementerian = ministry;
            });


        return $.when(edTask, viewsTask, permohonanViewsTask)
          .done(function(b, viewsLo, permohonanViewsLo) {
            entity(b);
            var vj = _(JSON.parse(viewsLo[0])).map(function(v) {
              return context.toObservable(v);
            });
            views(vj);

            _(JSON.parse(permohonanViewsLo[0])).each(function(v) {
              var pvw =  context.toObservable(v);
              views.push(pvw);
            });

            // get counts
            _(views()).each(function(v) {
              v.CountMessage("....");
              var tm = setInterval(function() {
                v.CountMessage(v.CountMessage() == "...." ? "..." : "....");
              }, 250);
              $.get("/Sph/EntityView/Count/" + v.Id())
                .done(function(c) {
                  clearInterval(tm);
                  v.CountMessage(c.hits.total);
                });
            });
          });
      },
      attached = function(view) {
        $(view).on('click', 'a.hover-drop', function(e) {
          e.preventDefault();
          var chart = ko.dataFor(this),
            link = $(this);
          if (!chart) {
            return;
          }
          if (typeof chart.unpin === "function") {
            link.prop('disabled', true);
            chart.unpin().done(function() {
              charts.remove(chart);
            });
          }
        });
      },
      addForm = function() {

      },
      addView = function() {

      },
      recentItemsQuery = {
        "sort": [{
          "ChangedDate": {
            "order": "desc"
          }
        }]
      },

      getTileClass = function(color){
        switch (ko.unwrap(color)) {
          case "bred": return "red-intense";
          case "bgreen": return "green-haze";
          case "bviolet": return "purple-plum";
          case "borange": return "yellow-gold";
          default:return "blue-madison";

        }
      };

    var vm = {
      query: query,
      getTileClass : getTileClass,
      permohonanLulusList: permohonanLulusList,
      isBusy: isBusy,
      views: views,
      charts: charts,
      entity: entity,
      activate: activate,
      attached: attached,
      reports: reports,
      tools: tools,
      recentItems: recentItems,
      addForm: addForm,
      addView: addView,
      recentItemsQuery: recentItemsQuery,
      toolbar: {
        commands: ko.observableArray([{
          command : function(){
            return router.navigate("permohonan-penyelaras/0");
          },
          caption : "Mohon Program Baru",
          icon : "fa fa-file-text-o"
        },
        {
          command : function(){
            return router.navigate("tambah-pengguna/0");
          },
          caption : "Tambah Responden",
          icon : "fa fa-user-plus"
        }
        ])
      }
    };

    return vm;

  });
