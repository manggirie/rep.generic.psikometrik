define(["plugins/dialog"],
  function(dialog) {
    var item = ko.observable(),
      html = ko.observable(),
      activate = function() {
        return $.get("/arahan/" + ko.unwrap(item().NamaUjian) + ".html")
          .done(html)
      },
      attached = function(view) {
        $("#arahan-mula-sesi-ujian-dialog-form").html(ko.unwrap(html));
      },
      okClick = function(data, ev) {
        dialog.close(this, "OK");

      },
      cancelClick = function() {
        dialog.close(this, "Cancel");
      };

    var vm = {
      activate: activate,
      attached: attached,
      item: item,
      okClick: okClick,
      cancelClick: cancelClick
    };


    return vm;

  });
