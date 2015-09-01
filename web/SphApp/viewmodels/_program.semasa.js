define(["services/datacontext", "services/logger", "plugins/router", "services/config"], function(context, logger, router, config){
    var today = moment().format("YYYY-MM-DDTHH:mm:ss.SSS"),
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
      activate = function(entity){
            
            return true;


        },
        attached  = function(view){
        
        };

    return {
        activate : activate,
        query: query,
        permohonanLulusList: permohonanLulusList,
        attached : attached
    };

});