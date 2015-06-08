
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
                        formTask = context.loadOneAsync("EntityForm", "Route eq 'permohonan_penyelaras'"),
                        watcherTask = watcher.getIsWatchingAsync("Permohonan", entityId),
                        i18nTask = $.getJSON("i18n/" + config.lang + "/permohonan_penyelaras");

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

                     var tcs = new $.Deferred(),
                         data = ko.mapping.toJSON(entity);

                     context.post(data, "/Permohonan/PermohonanDariPenyelaras" )
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
                             tcs.resolve(result);
                         });
                     return tcs.promise();
                 },
                attached = function (view) {
                    // validation
                    validation.init($('#permohonan_penyelaras-form'), form());



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

                    var tcs = new $.Deferred(),
                        data = ko.mapping.toJSON(entity);

                        

                    context.post(data, "/Permohonan/Save")
                        .then(function(result) {
                            tcs.resolve(result);
                            entity().Id(result.id);
                            app.showMessage("Your Permohonan has been successfully saved", "epsikologi", ["ok"]);

                        });
                    

                    return tcs.promise();
                },
                remove = function() {
                    var tcs = new $.Deferred();
                    $.ajax({
                        type: "DELETE",
                        url: "/Permohonan/Remove/" + entity().Id(),
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        error: tcs.reject,
                        success: function() {
                            tcs.resolve(true);
                            app.showMessage("Your item has been successfully removed", "Removed", ["OK"])
                              .done(function () {
                                  window.location = "#permohonan";
                              });
                        }
                    });


                    return tcs.promise();
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
                //


                toolbar : {
                                                                                                        
                    saveCommand : permohonanDariPenyelaras,
                    
                    commands : ko.observableArray([])
                }
            };

            return vm;
        });
