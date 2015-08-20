///<reference path="../node_modules/gulp/bin/gulp.js"/>
var gulp = require("gulp");
var path = require("path");
var sourcemaps = require("gulp-sourcemaps"),
    concat = require("gulp-concat"),
    useref = require("gulp-useref"),
    gulpif = require("gulp-if"),
    uglify = require("gulp-uglify"),
    _ = require("underscore"),
    mcss = require("gulp-minify-css");
// -*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*
// core.js
var files = [
"/kendo/styles/kendo.common.css",
"/kendo/styles/kendo.metro.css",
"/kendo/styles/kendo.dataviz.css",
"/kendo/styles/kendo.dataviz.metro.css",

"/Content/bootstrap.css",
"/Content/durandal.css",
"/Content/font-awesome.css",
"/Content/nprogress.css",
"/Content/typeahead.css",
"/Content/toastr.min.css",
"/Content/daterangepicker-bs3.css",



"/assets/google-fonts-open-sans.400.300.600.700.subset.all.css",
"/assets/global/plugins/font-awesome/css/font-awesome.min.css",
"/assets/global/plugins/simple-line-icons/simple-line-icons.min.css",
"/assets/global/plugins/bootstrap/css/bootstrap.min.css",
"/assets/global/plugins/uniform/css/uniform.default.css",
"/assets/global/plugins/bootstrap-switch/css/bootstrap-switch.min.css",
"/assets/global/plugins/bootstrap-daterangepicker/daterangepicker-bs3.css",
"/assets/global/plugins/fullcalendar/fullcalendar.min.css",
"/assets/global/css/components-md.css",
"/assets/global/css/plugins-md.css",
"/assets/admin/layout/css/layout.css",
"/assets/admin/pages/css/todo.css",
"/assets/admin/pages/css/tasks.css",
"/assets/admin/pages/css/inbox.css",
"/assets/admin/layout/css/themes/blue.css",
"/assets/admin/layout/css/custom.css",
"/css/site.css",
"/content/durandal.css",
"/content/toastr.css"
],
    sources = _(files).map(function (v) {
        return "./web" + v;
    });

watchList.push({ name: "css", sources: sources, tasks: ["css"] });

gulp.task("css.min", function () {

    return gulp.src(sources)
        .pipe(mcss({ keepBreaks: true }))
        .pipe(concat("epsikologi.min.css"))
        .pipe(gulp.dest("./web/Content"));
});

gulp.task("css", ["css.min"], function () {
    return gulp.src(sources)
        .pipe(concat("epsikologi.css"))
        .pipe(gulp.dest("./web/Content"));

});
