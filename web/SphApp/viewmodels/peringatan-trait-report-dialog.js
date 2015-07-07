define(["plugins/dialog"],
  function(dialog) {
    var item = ko.observable(),
      html = ko.observable(),
      activate = function() {

        return $.get("/peringatan/trait/" + ko.unwrap(item) + ".html")
          .done(html)
      },
      attached = function() {
        $("#peringatan-trait-report-dialog-form").html(ko.unwrap(html));
      },
      okClick = function(data, ev) {

        dialog.close(this, "OK");

      },
      cancelClick = function() {
        dialog.close(this, "Cancel");
      };

    var vm = {
      item: item,
      activate: activate,
      attached: attached,
      okClick: okClick,
      cancelClick: cancelClick
    };


    return vm;

  });
