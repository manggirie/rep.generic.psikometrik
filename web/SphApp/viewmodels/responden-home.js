define(["services/datacontext", objectbuilders.config ], function(context, config){
    var testList =  ko.observableArray()
        ujianList = ko.observableArray(),
        activate = function(){

            return context.loadAsync("SesiUjian", String.format("MyKad eq '{0}' and Status ne 'Diambil'", config.userName))
                .then(function(lo){
                    testList(lo.itemCollection);
                    return context.loadAsync("Ujian", "Id ne '0'");
                })
                .then(function(lo1){
                    ujianList(lo1.itemCollection);
                });
        },
        attached  = function(view){

        },
        getNamaUjian = function(kod){
            var ujian = _(ujianList()).find(function(v){
                return ko.unwrap(v.UjianNo) === ko.unwrap(kod);
            });

            return ko.unwrap(ujian.NamaUjian);
        };

    return {
        testList : testList,
        activate : activate,
        attached : attached,
        getNamaUjian : getNamaUjian
    };

});
