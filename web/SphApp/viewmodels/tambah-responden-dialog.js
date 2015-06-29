define(["plugins/dialog", "services/datacontext", "services/config"],
    function(dialog, context, config)
        {
            var item = ko.observable(),
                searchText = ko.observable(),
                results = ko.observableArray(),
                activate = function(){
                    
                    return context.getScalarAsync("Pengguna", "MyKad eq '" + config.userName + "'", "NamaKementerian")
                        .done(function(ministry){
                          config.namaKementerian = ministry;
                          return  context.getScalarAsync("Pengguna", "MyKad eq '" + config.userName + "'", "NamaJabatan");
                        })
                        .done(function(jabatan){
                          config.namaJabatan = jabatan;
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
                },
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
                            results(lo.itemCollection);
                        });
                    
                    
                };

            var vm = {
                searchText : searchText,
                searchAsync : searchAsync,
                results : results,
                activate :activate,
                permohonan : item,
                okClick : okClick,
                cancelClick : cancelClick
        };


        return vm;

    });