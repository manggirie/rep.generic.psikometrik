
    define([objectbuilders.datacontext, objectbuilders.logger, objectbuilders.router,
        objectbuilders.system, objectbuilders.validation, objectbuilders.eximp,
        objectbuilders.dialog, objectbuilders.watcher, objectbuilders.config,
        objectbuilders.app ,'partial/tambah_reponden'],
        function (context, logger, router, system, validation, eximp, dialog, watcher,config,app
            ,partial) {

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
                        itemTask = context.loadOneAsync("permohonan", query),
                        formTask = context.loadOneAsync("EntityForm", "Route eq 'tambah_reponden'"),
                        watcherTask = watcher.getIsWatchingAsync("permohonan", entityId),
                        i18nTask = $.getJSON("i18n/" + config.lang + "/tambah_reponden");

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
                            
                            if(typeof partial.activate === "function"){
                                var pt = partial.activate(entity());
                                if(typeof pt.done === "function"){
                                    pt.done(tcs.resolve);
                                }else{
                                    tcs.resolve(true);
                                }
                            }
                            
                        
                    });



                    return tcs.promise();
                },
                attached = function (view) {
                    // validation
                    validation.init($('#tambah_reponden-form'), form());


                        
                    if(typeof partial.attached === "function"){
                        partial.attached(view);
                    }

                    

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

                        

                    context.post(data, "/permohonan/Save")
                        .then(function(result) {
                            tcs.resolve(result);
                            entity().Id(result.id);
                            app.showMessage("Your permohonan has been successfully saved", "epsikologi", ["ok"]);

                        });
                    

                    return tcs.promise();
                },
                remove = function() {
                    var tcs = new $.Deferred();
                    $.ajax({
                        type: "DELETE",
                        url: "/permohonan/Remove/" + entity().Id(),
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
                            
                            partial : partial,
                            
                                    activate: activate,
                config: config,
                attached: attached,
                compositionComplete:compositionComplete,
                entity: entity,
                errors: errors,
                save : save,
                //


                toolbar : {
                        emailCommand : {
                        entity : "permohonan",
                        id :id
                    },
                                            printCommand :{
                        entity : 'permohonan',
                        id : id
                    },
                                    removeCommand :remove,
                    canExecuteRemoveCommand : ko.computed(function(){
                        return entity().Id();
                    }),
                                                                
                    saveCommand : save,
                    
                    commands : ko.observableArray([])
                }
            };

            return vm;
        });
