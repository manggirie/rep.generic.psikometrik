define(["services/datacontext", objectbuilders.config ], function(context, config){
    var testList =  ko.observableArray()
        ujianList = ko.observableArray(),
        activate = function(){
           var date = moment().format('YYYY-MM-DD HH:mm:ss'),
           query = String.format("MyKad eq '{0}' and Status eq 'Belum Ambil' and TarikhMulaProgram le DateTime'{1}' and TarikhTamatProgram ge DateTime'{1}' ", config.userName, date);
           console.log(query);
            return context.loadAsync("SesiUjian", query)
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
