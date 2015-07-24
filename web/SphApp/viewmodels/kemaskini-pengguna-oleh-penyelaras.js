
    define([objectbuilders.datacontext, objectbuilders.logger, objectbuilders.router,
        objectbuilders.system, objectbuilders.validation, objectbuilders.eximp,
        objectbuilders.dialog, objectbuilders.watcher, objectbuilders.config,
        objectbuilders.app ],
        function (context, logger, router, system, validation, eximp, dialog, watcher,config,app
            ) {

            var entity = ko.observable(new bespoke.epsikologi_pengguna.domain.Pengguna({WebId:system.guid()})),
                errors = ko.observableArray(),
                form = ko.observable(new bespoke.sph.domain.EntityForm()),
                watching = ko.observable(false),
                id = ko.observable(),
                i18n = null,
                activate = function (entityId) {
                    id(entityId);

                    var query = String.format("Id eq '{0}'", entityId),
                        tcs = new $.Deferred(),
                        itemTask = context.loadOneAsync("Pengguna", query),
                        formTask = context.loadOneAsync("EntityForm", "Route eq 'kemaskini-pengguna-oleh-penyelaras'"),
                        watcherTask = watcher.getIsWatchingAsync("Pengguna", entityId),
                        i18nTask = $.getJSON("i18n/" + config.lang + "/kemaskini-pengguna-oleh-penyelaras");

                    $.when(itemTask, formTask, watcherTask, i18nTask).done(function(b,f,w,n) {
                        if (b) {
                            var item = context.toObservable(b);
                            entity(item);
                        }
                        else {
                            entity(new bespoke.epsikologi_pengguna.domain.Pengguna({WebId:system.guid()}));
                        }
                        form(f);
                        watching(w);
                        i18n = n[0];
                            tcs.resolve(true);
                        
                    });

                    return tcs.promise();
                },
                dateNow = function(){

                     if (!validation.valid()) {
                         return Task.fromResult(false);
                     }

                     var data = ko.mapping.toJSON(entity);

                    return  context.post(data, "/Pengguna/DateNow" )
                         .then(function (result) {
                             if (result.success) {
                                 logger.info(result.message);
                                 entity().Id(result.id);
                                 errors.removeAll();

                                 
                             } else {
                                 errors.removeAll();
                                 _(result.rules).each(function(v){
                                     errors(v.ValidationErrors);
                                 });
                                 logger.error("There are errors in your entity, !!!");
                             }
                         });
                 },
                tarikhKemaskiniNow = function(){

                     if (!validation.valid()) {
                         return Task.fromResult(false);
                     }

                     var data = ko.mapping.toJSON(entity);

                    return  context.post(data, "/Pengguna/TarikhKemaskiniNow" )
                         .then(function (result) {
                             if (result.success) {
                                 logger.info(result.message);
                                 entity().Id(result.id);
                                 errors.removeAll();

                                  
                                    app.showMessage("Rekod berjaya didaftarkan", "JPA Sistem Ujian e-Psikometrik", ["OK"])
	                                    .done(function () {
                                            window.location='#pengguna-all'
	                                    });
                                 
                             } else {
                                 errors.removeAll();
                                 _(result.rules).each(function(v){
                                     errors(v.ValidationErrors);
                                 });
                                 logger.error("There are errors in your entity, !!!");
                             }
                         });
                 },
                kemaskiniOlehUrusetia = function(){

                     if (!validation.valid()) {
                         return Task.fromResult(false);
                     }

                     var data = ko.mapping.toJSON(entity);

                    return  context.post(data, "/Pengguna/KemaskiniOlehUrusetia" )
                         .then(function (result) {
                             if (result.success) {
                                 logger.info(result.message);
                                 entity().Id(result.id);
                                 errors.removeAll();

                                  
                                    app.showMessage("Rekod berjaya disimpan", "JPA Sistem Ujian e-Psikometrik", ["OK"])
	                                    .done(function () {
                                            window.location='#responden'
	                                    });
                                 
                             } else {
                                 errors.removeAll();
                                 _(result.rules).each(function(v){
                                     errors(v.ValidationErrors);
                                 });
                                 logger.error("There are errors in your entity, !!!");
                             }
                         });
                 },
                kemaskiniOlehPenyelaras = function(){

                     if (!validation.valid()) {
                         return Task.fromResult(false);
                     }

                     var data = ko.mapping.toJSON(entity);

                    return  context.post(data, "/Pengguna/KemaskiniOlehPenyelaras" )
                         .then(function (result) {
                             if (result.success) {
                                 logger.info(result.message);
                                 entity().Id(result.id);
                                 errors.removeAll();

                                  
                                    app.showMessage("Rekod berjaya disimpan", "JPA Sistem Ujian e-Psikometrik", ["OK"])
	                                    .done(function () {
                                            window.location='#responden-dari-jabatan'
	                                    });
                                 
                             } else {
                                 errors.removeAll();
                                 _(result.rules).each(function(v){
                                     errors(v.ValidationErrors);
                                 });
                                 logger.error("There are errors in your entity, !!!");
                             }
                         });
                 },
                attached = function (view) {
                    // validation
                    validation.init($('#kemaskini-pengguna-oleh-penyelaras-form'), form());



                },
                compositionComplete = function() {
                    $("[data-i18n]").each(function (i, v) {
                        var $label = $(v),
                            text = $label.data("i18n");
                        if (typeof i18n[text] === "string") {
                            $label.text(i18n[text]);
                        }
                    });
                },

                                save = function() {
                    if (!validation.valid()) {
                        return Task.fromResult(false);
                    }

                    var data = ko.mapping.toJSON(entity);

                        

                    return context.post(data, "/Pengguna/Save")
                        .then(function(result) {
                            entity().Id(result.id);
                            app.showMessage("Your Pengguna has been successfully saved", "JPA Sistem Ujian e-Psikometrik", ["OK"]);

                        });
                    

                },
                remove = function() {
                    return $.ajax({
                        type: "DELETE",
                        url: "/Pengguna/Remove/" + entity().Id(),
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function() {
                            app.showMessage("Your item has been successfully removed", "Removed", ["OK"])
                              .done(function () {
                                  window.location = "#pengguna";
                              });
                        }
                    });


                };

            var vm = {
                                    activate: activate,
                config: config,
                attached: attached,
                compositionComplete:compositionComplete,
                entity: entity,
                errors: errors,
                save : save,
                    dateNow : dateNow,
                    tarikhKemaskiniNow : tarikhKemaskiniNow,
                    kemaskiniOlehUrusetia : kemaskiniOlehUrusetia,
                    kemaskiniOlehPenyelaras : kemaskiniOlehPenyelaras,
                //


                toolbar : {
                                                                                                        
                    saveCommand : kemaskiniOlehPenyelaras,
                    
                    commands : ko.observableArray([])
                }
            };

            return vm;
        });
