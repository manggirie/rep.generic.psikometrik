define([], function(){
    var activate = function(list){

            var tcs = new $.Deferred();
            setTimeout(function(){
                tcs.resolve(true);
            }, 500);

            return tcs.promise();


        },
        attached  = function(view){
          $(view).on("click", "a", function(e){
            var data = ko.dataFor(this);
            if(typeof data.NamaUjian !== "string")
            {
              return;
            }

            e.preventDefault();
            window.open("/cetak-laporan/trait/" + data.NamaUjian +"/" + data.Id);
          });
        };

    return {
        activate : activate,
        attached : attached
    };

});
