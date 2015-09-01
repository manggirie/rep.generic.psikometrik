define([objectbuilders.datacontext, objectbuilders.config],
  function(context, config) {
    var isBusy = ko.observable(false),
      userName = ko.observable(config.userName),
      pengguna = ko.observable(),
      userProfile = ko.observable(new bespoke.sph.domain.UserProfile()),
      startModule = ko.observable("urusetia-dashboard"),
      startModuleOptions = ko.observableArray(["af perempuan", "all ibkkodkerjayas", "all iprecommendations", "all percubaan sesi ujian", "all skor hlp", "hlprecomendation", "ibkkodkerjaya", "ibkrecommendation", "iprecommendation", "ipurecommendation", "isorecommendation", "jabatan", "kementerian", "laporan-sesi-ujian", "pendaftaranprogram", "pengguna", "penyelaras", "percubaansesi", "permohonan", "permohonan baru", "programlookup", "semua permohonan", "senarai pengguna", "senarai program", "senarai program semasa", "senarai responden", "sesi ujian belum ambil", "sesi ujian belum diambil mengikut program", "sesi ujian diambil mengikut program", "sesi ujian responden", "sesiujian", "skor af lelaki", "skorhlp", "soalan", "ujian", "urusetia", "urusetia-dashboard"]),
      languageOptions = ko.observableArray(),
      activate = function() {
        var query = String.format("UserName eq '{0}'", userName()),
          tcs = new $.Deferred(),
          loadTask = context.loadOneAsync("UserProfile", query),
          languageOptionsTask = $.getJSON("/i18n/options"),
          penggunaTask = context.loadOneAsync("Pengguna", "MyKad eq '" + userName() + "'");
        $.when(loadTask, languageOptionsTask, penggunaTask).done(function(b, langs, penggunaLo) {

          if(penggunaLo){
            if(typeof penggunaLo.Emel2 !== "function"){
              penggunaLo.Emel2 = ko.observable();
            }
            pengguna(penggunaLo);
          }

          if (b)
            userProfile(b);
          else
            userProfile(new bespoke.sph.domain.UserProfile());
          var lang = langs[0],
            options = [];
          for (var code in lang) {
            if (lang.hasOwnProperty(code)) {
              options.push({
                code: code,
                display: lang[code]
              });
            }
          }
          languageOptions(options);
          tcs.resolve(true);
        });

        return tcs.promise();
      },
      saveAsync = function() {
        var json = ko.toJSON(userProfile);
        return context.post(ko.toJSON(pengguna), "/Pengguna/Save")
          .then(function(){

            return context.post(json, "/App/UserProfile/UpdateUser");
          })
          .then(function(){
            console.log("saved");
          });
      };

    var vm = {
      activate: activate,
      userProfile: userProfile,
      pengguna : pengguna,
      startModule: startModule,
      startModuleOptions: startModuleOptions,
      languageOptions: languageOptions,
      toolbar: {
        saveCommand: saveAsync
      },
      isBusy: isBusy,
      title: "User profile Details"
    };

    return vm;
  });
