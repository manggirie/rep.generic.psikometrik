define(["plugins/dialog", "services/datacontext", "services/config"],
    function(dialog, context, config)
        {
            var item = ko.observable(),
                searchText = ko.observable(),
                results = ko.observableArray(),
                maxCount = ko.observable(),
                selectedRespondens = ko.observableArray(),
                senaraiPendaftaran = ko.observableArray(),
                searchAsync = function(){


                    var query = {
                              "query": {
                                "filtered": {
                                  "filter": {
                                    "bool": {
                                      "must": [
                                        {
                                          "term": {
                                            "NamaJabatan": config.namaJabatan
                                          }
                                        },
                                        {
                                          "term": {
                                            "NamaKementerian": config.namaKementerian
                                          }
                                        }
                                      ]
                                    }
                                  }
                                }
                              },
                              "from": 0,
                              "size": 20
                            };

                    if (ko.unwrap(searchText)) {
                        query.query = {
                            "query_string": {
                                "default_field": "_all",
                                "query": ko.unwrap(searchText)
                            }
                        };
                    }



                    return context.searchAsync({ entity : "Pengguna" }, query)
                        .done(function (lo) {
                            var list = _(lo.itemCollection).filter(function(v){
                              var exist = _(senaraiPendaftaran()).find(function(k){
                                return ko.unwrap(k.MyKad) === v.MyKad;
                              });
                              if(exist){
                                return false;
                              }
                              return v.IsResponden === true;
                            });
                            results(list);
                        });


                },
                activate = function(){

                    return context.getScalarAsync("Pengguna", "MyKad eq '" + config.userName + "'", "NamaKementerian")
                        .then(function(ministry){
                          config.namaKementerian = ministry;
                          return  context.getScalarAsync("Pengguna", "MyKad eq '" + config.userName + "'", "NamaJabatan");
                        })
                        .then(function(jabatan){
                          config.namaJabatan = jabatan;
                          return searchAsync();
                        });
                },
                attached = function(view){
                    $(view).on("click", "input[type=checkbox]", function(e){
                        var pengguna = ko.dataFor(this);
                        if($(this).is(":checked")){
                            selectedRespondens.push(pengguna);
                        }else{
                            selectedRespondens.remove(pengguna);
                        }
                    });
                },
                okClick = function(data, ev) {
                    if (bespoke.utils.form.checkValidity(ev.target))
                    {
                        dialog.close(this, "OK");
                    }
                },
                cancelClick = function() {
                    dialog.close(this, "Cancel");
                };

            var vm = {
                selectedRespondens : selectedRespondens,
                senaraiPendaftaran : senaraiPendaftaran,
                maxCount : maxCount,
                canExecuteSave : ko.computed(function(){
                    return selectedRespondens().length > 0 && selectedRespondens().length <= maxCount();
                }),
                searchText : searchText,
                searchAsync : searchAsync,
                results : results,
                activate :activate,
                attached : attached,
                permohonan : item,
                okClick : okClick,
                cancelClick : cancelClick
        };


        return vm;

    });
