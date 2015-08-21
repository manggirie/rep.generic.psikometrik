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
    
/*

<link href="/assets/global/plugins/select2/select2.css" rel="stylesheet" type="text/css"/>


*/
var files = [
"/assets/global/plugins/font-awesome/css/font-awesome.min.css",
"/assets/global/plugins/simple-line-icons/simple-line-icons.min.css",
"/assets/global/plugins/bootstrap/css/bootstrap.min.css",
"/assets/global/plugins/uniform/css/uniform.default.css",
"/assets/admin/pages/css/login-soft.css",
"/assets/global/css/components.css",
"/assets/global/css/plugins.css",
"/assets/admin/layout/css/layout.css",
"/assets/admin/layout/css/themes/darkblue.css",
"/assets/admin/layout/css/custom.css"
],
    sources = _(files).map(function (v) {
        return "./web" + v;
    });

watchList.push({ name: "login.css", sources: sources, tasks: ["login.css"] });

gulp.task("login.css.min", function () {

    return gulp.src(sources)
        .pipe(mcss({ keepBreaks: true }))
        .pipe(concat("login.min.css"))
        .pipe(gulp.dest("./web/Content"));
});

gulp.task("login.css", ["login.css.min"], function () {
    return gulp.src(sources)
        .pipe(concat("login.css"))
        .pipe(gulp.dest("./web/Content"));

});
