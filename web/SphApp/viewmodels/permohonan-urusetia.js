
    define([objectbuilders.datacontext, objectbuilders.logger, objectbuilders.router,
        objectbuilders.system, objectbuilders.validation, objectbuilders.eximp,
        objectbuilders.dialog, objectbuilders.watcher, objectbuilders.config,
        objectbuilders.app ],
        function (context, logger, router, system, validation, eximp, dialog, watcher,config,app
            ) {

            var entity = ko.observable(new bespoke.epsikologi_permohonan.domain.Permohonan({WebId:system.guid()})),
                errors = ko.observableArray(),
                form = ko.observable(new bespoke.sph.domain.EntityForm()),
                watching = ko.observable(false),
                id = ko.observable(),
                i18n = null,
                activate = function (entityId) {
                    id(entityId);

                    var query = String.format("Id eq '{0}'", entityId),
                        tcs = new $.Deferred(),
                        itemTask = context.loadOneAsync("Permohonan", query),
                        formTask = context.loadOneAsync("EntityForm", "Route eq 'permohonan-urusetia'"),
                        watcherTask = watcher.getIsWatchingAsync("Permohonan", entityId),
                        i18nTask = $.getJSON("i18n/" + config.lang + "/permohonan-urusetia");

                    $.when(itemTask, formTask, watcherTask, i18nTask).done(function(b,f,w,n) {
                        if (b) {
                            var item = context.toObservable(b);
                            entity(item);
                        }
                        else {
                            entity(new bespoke.epsikologi_permohonan.domain.Permohonan({WebId:system.guid()}));
                        }
                        form(f);
                        watching(w);
                        i18n = n[0];
                            tcs.resolve(true);
                        
                    });

                    return tcs.promise();
                },
                permohonanDariPenyelaras = function(){

                     if (!validation.valid()) {
                         return Task.fromResult(false);
                     }

                     var data = ko.mapping.toJSON(entity);

                    return  context.post(data, "/Permohonan/PermohonanDariPenyelaras" )
                         .then(function (result) {
                             if (result.success) {
                                 logger.info(result.message);
                                 entity().Id(result.id);
                                 errors.removeAll();

                                  
                                    app.showMessage("Permohonan sudah berjaya dihantar", "JPA Sistem Ujian e-Psikometrik", ["OK"])
	                                    .done(function () {
                                            window.location='#penyelaras-home'
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
                urusetiaProcessPermohonanDariPenyelaras = function(){

                     if (!validation.valid()) {
                         return Task.fromResult(false);
                     }

                     var data = ko.mapping.toJSON(entity);

                    return  context.post(data, "/Permohonan/UrusetiaProcessPermohonanDariPenyelaras" )
                         .then(function (result) {
                             if (result.success) {
                                 logger.info(result.message);
                                 entity().Id(result.id);
                                 errors.removeAll();

                                  
                                    app.showMessage("Permohonan program sudah dikemaskini", "JPA Sistem Ujian e-Psikometrik", ["OK"])
	                                    .done(function () {
                                            
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
                    validation.init($('#permohonan-urusetia-form'), form());



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

                        

                    return context.post(data, "/Permohonan/Save")
                        .then(function(result) {
                            entity().Id(result.id);
                            app.showMessage("Your Permohonan has been successfully saved", "JPA Sistem Ujian e-Psikometrik", ["ok"]);

                        });
                    

                },
                remove = function() {
                    return $.ajax({
                        type: "DELETE",
                        url: "/Permohonan/Remove/" + entity().Id(),
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function() {
                            app.showMessage("Your item has been successfully removed", "Removed", ["OK"])
                              .done(function () {
                                  window.location = "#permohonan";
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
                    permohonanDariPenyelaras : permohonanDariPenyelaras,
                    urusetiaProcessPermohonanDariPenyelaras : urusetiaProcessPermohonanDariPenyelaras,
                //


                toolbar : {
                        emailCommand : {
                        entity : "Permohonan",
                        id :id
                    },
                                            printCommand :{
                        entity : 'Permohonan',
                        id : id
                    },
                                    removeCommand :remove,
                    canExecuteRemoveCommand : ko.computed(function(){
                        return entity().Id();
                    }),
                                            
                    watchCommand: function() {
                        return watcher.watch("Permohonan", entity().Id())
                            .done(function(){
                                watching(true);
                            });
                    },
                    unwatchCommand: function() {
                        return watcher.unwatch("Permohonan", entity().Id())
                            .done(function(){
                                watching(false);
                            });
                    },
                    watching: watching,
                                            
                    saveCommand : urusetiaProcessPermohonanDariPenyelaras,
                    
                    commands : ko.observableArray([])
                }
            };

            return vm;
        });
