
    define([objectbuilders.datacontext, objectbuilders.logger, objectbuilders.router,
        objectbuilders.system, objectbuilders.validation, objectbuilders.eximp,
        objectbuilders.dialog, objectbuilders.watcher, objectbuilders.config,
        objectbuilders.app ,'partial/ipurecommendation-details'],
        function (context, logger, router, system, validation, eximp, dialog, watcher,config,app
            ,partial) {

            var entity = ko.observable(new bespoke.epsikologi_ipurecommendation.domain.IpuRecommendation({WebId:system.guid()})),
                errors = ko.observableArray(),
                form = ko.observable(new bespoke.sph.domain.EntityForm()),
                watching = ko.observable(false),
                id = ko.observable(),
                i18n = null,
                activate = function (entityId) {
                    id(entityId);

                    var query = String.format("Id eq '{0}'", entityId),
                        tcs = new $.Deferred(),
                        itemTask = context.loadOneAsync("IpuRecommendation", query),
                        formTask = context.loadOneAsync("EntityForm", "Route eq 'ipurecommendation-details'"),
                        watcherTask = watcher.getIsWatchingAsync("IpuRecommendation", entityId),
                        i18nTask = $.getJSON("i18n/" + config.lang + "/ipurecommendation-details");

                    $.when(itemTask, formTask, watcherTask, i18nTask).done(function(b,f,w,n) {
                        if (b) {
                            var item = context.toObservable(b);
                            entity(item);
                        }
                        else {
                            entity(new bespoke.epsikologi_ipurecommendation.domain.IpuRecommendation({WebId:system.guid()}));
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
                    validation.init($('#ipurecommendation-details-form'), form());


                        
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

                    var data = ko.mapping.toJSON(entity);

                        

                    return context.post(data, "/IpuRecommendation/Save")
                        .then(function(result) {
                            entity().Id(result.id);
                            app.showMessage("Your IpuRecommendation has been successfully saved", "JPA Sistem Ujian e-Psikometrik", ["OK"]);

                        });
                    

                },
                remove = function() {
                    return $.ajax({
                        type: "DELETE",
                        url: "/IpuRecommendation/Remove/" + entity().Id(),
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function() {
                            app.showMessage("Your item has been successfully removed", "Removed", ["OK"])
                              .done(function () {
                                  window.location = "#ipurecommendation";
                              });
                        }
                    });


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
                                                                                                        
                    saveCommand : save,
                    
                    commands : ko.observableArray([])
                }
            };

            return vm;
        });
