/// <reference path="/Scripts/jquery-2.1.3.js" />
/// <reference path="/Scripts/knockout-3.2.0.debug.js" />
/// <reference path="/Scripts/knockout.mapping-latest.debug.js" />
/// <reference path="/Scripts/underscore.js" />
/// <reference path="/Scripts/moment.js" />
/// <reference path="../services/datacontext.js" />
/// <reference path="../schemas/trigger.workflow.g.js" />
/// <reference path="../../Scripts/bootstrap.js" />


define(["services/datacontext", "services/logger", "plugins/router", "services/config"],
  function(context, logger, router, config) {

    var isBusy = ko.observable(false),
      tools = ko.observableArray([]),
      charts = ko.observableArray([]),
      views = ko.observableArray([]),
      entity = ko.observable(new bespoke.sph.domain.EntityDefinition()),
      activate = function() {
          var q = "Name eq 'Pengguna'",
          edTask = context.loadOneAsync("EntityDefinition", q),
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
                v.CountMessage(v.CountMessage() === "...." ? "..." : "....");
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
        $(view).on("click", "a.hover-drop", function(e) {
          e.preventDefault();
          var chart = ko.dataFor(this),
            link = $(this);
          if (!chart) {
            return;
          }
          if (typeof chart.unpin === "function") {
            link.prop("disabled", true);
            chart.unpin().done(function() {
              charts.remove(chart);
            });
          }
        });
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
      getTileClass : getTileClass,
      isBusy: isBusy,
      views: views,
      charts: charts,
      entity: entity,
      activate: activate,
      attached: attached,
      tools: tools,
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
