var gulp = require("gulp");
var path = require("path");
var sourcemaps = require("gulp-sourcemaps"),
    concat = require("gulp-concat"),
    useref = require("gulp-useref"),
    gulpif = require("gulp-if"),
    uglify = require("gulp-uglify");


/*
    <script type="text/javascript" src="/assets/global/plugins/select2/select2.min.js"></script>
"./web/assets/global/plugins/jquery-migrate.min.js",

*/
var sources = [
"./web/scripts/jquery-2.1.3.min.js",
"./web/scripts/bootstrap.min.js",
"./web/assets/global/plugins/jquery.blockui.min.js",
"./web/assets/global/plugins/uniform/jquery.uniform.min.js",
"./web/assets/global/plugins/jquery.cokie.min.js",
"./web/assets/global/plugins/jquery-validation/js/jquery.validate.min.js",
"./web/assets/global/plugins/backstretch/jquery.backstretch.min.js",
"./web/assets/global/scripts/metronic.js",
"./web/assets/admin/layout/scripts/layout.js",
"./web/assets/admin/layout/scripts/demo.js",
"./web/assets/admin/pages/scripts/login-soft.js"

];

watchList.push({name:"login.scripts.js",sources: sources, tasks : ["login.scripts.js"]});

gulp.task("login.scripts.min.js", function(){

    return gulp.src(sources)
        .pipe(sourcemaps.init())
        .pipe(uglify())
        .pipe(concat("login.scripts.min.js"))
        .pipe(sourcemaps.write("./"))
        .pipe(useref())
        .pipe(gulp.dest("./web/Scripts"));
});

gulp.task("login.scripts.js",["login.scripts.min.js"], function(){
    return gulp.src(sources)
        .pipe(concat("login.scripts.js"))
        .pipe(gulp.dest("./web/Scripts"));

});
