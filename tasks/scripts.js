var gulp = require("gulp");
var path = require("path");
var sourcemaps = require("gulp-sourcemaps"),
    concat = require("gulp-concat"),
    useref = require("gulp-useref"),
    gulpif = require("gulp-if"),
    uglify = require("gulp-uglify");
// -*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*
// core.js

/*
<!-- IMPORTANT! Load jquery-ui.min.js before bootstrap.min.js to fix bootstrap tooltip conflict with jquery ui tooltip -->
<script src="" type="text/javascript"></script>
<script src="" type="text/javascript"></script>
<script src="" type="text/javascript"></script>
<script src="/" type="text/javascript"></script>
<script src="" type="text/javascript"></script>
<script src="" type="text/javascript"></script>
<script src="" type="text/javascript"></script>
<script src="" type="text/javascript"></script>


<script src="/assets/global/plugins/flot/jquery.flot.min.js" type="text/javascript"></script>
<script src="/assets/global/plugins/flot/jquery.flot.resize.min.js" type="text/javascript"></script>
<script src="/assets/global/plugins/flot/jquery.flot.categories.min.js" type="text/javascript"></script>

<!-- IMPORTANT! fullcalendar depends on jquery-ui.min.js for drag & drop support -->
<script src="/assets/global/plugins/fullcalendar/fullcalendar.min.js" type="text/javascript"></script>
<!-- END PAGE LEVEL PLUGINS -->
<!-- BEGIN PAGE LEVEL SCRIPTS -->
<script type="text/javascript" src="/assets/global/plugins/select2/select2.min.js"></script>


*/
var sources = [
"./web/assets/global/plugins/jquery-ui/jquery-ui.min.js",
"./web/assets/global/plugins/bootstrap/js/bootstrap.min.js",
"./web/assets/global/plugins/bootstrap-hover-dropdown/bootstrap-hover-dropdown.min.js",
"./web/assets/global/plugins/jquery-slimscroll/jquery.slimscroll.min.js",
"./web/assets/global/plugins/jquery.blockui.min.js",
"./web/assets/global/plugins/jquery.cokie.min.js",
"./web/assets/global/plugins/uniform/jquery.uniform.min.js",
"./web/assets/global/plugins/bootstrap-switch/js/bootstrap-switch.min.js",
"./web/assets/global/plugins/jquery.pulsate.min.js",
"./web/assets/global/plugins/bootstrap-daterangepicker/daterangepicker.js",
"./web/assets/global/scripts/metronic.js",
"./web/assets/admin/layout/scripts/layout.js",
"./web/assets/admin/layout/scripts/quick-sidebar.js",
"./web/assets/admin/layout/scripts/demo.js",
"./web/assets/admin/pages/scripts/index.js",
"./web/assets/admin/pages/scripts/tasks.js",
"./web/assets/admin/pages/scripts/todo.js"

];

watchList.push({name:"scripts.js",sources: sources, tasks : ["scripts.js"]});

gulp.task("scripts.min.js", function(){

    return gulp.src(sources)
        .pipe(sourcemaps.init())
        .pipe(uglify())
        .pipe(concat("scripts.min.js"))
        .pipe(sourcemaps.write("./"))
        .pipe(useref())
        .pipe(gulp.dest("./web/Scripts"));
});

gulp.task("scripts.js",["scripts.min.js"], function(){
    return gulp.src(sources)
        .pipe(concat("scripts.js"))
        .pipe(gulp.dest("./web/Scripts"));

});
