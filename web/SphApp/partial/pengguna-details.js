define([], function () {
    var activate = function (entity) {
            return true;
        },
        attached = function (view) {
            $("select.required").attr("required", "");
        };

    return {
        activate: activate,
        attached: attached
    };

});