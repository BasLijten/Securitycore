var gulp = require("gulp");
var dest = require('gulp-dest');
var debug = require('gulp-debug');

gulp.task("default", function (callback) {
  return runSequence(callback);
});

gulp.task("copy-Views-to-www", function () {
  console.log("copying views");
  return;
});

gulp.task("watch", function () {
  gulp.watch("*.cshtml", ["copy-Views-to-www"]);
});

gulp.task('hello', function() {
   console.log("hello"); 
});

gulp.task('copy-css'), function() {    
    gulp.src('.\node_modules\bootstrap\dist\css\bootstrap.css').pipe(debug()).pipe(gulp.dest('.\src\Project\Prototype\css'));
};

gulp.task('webserver', function() {
   gulp.src('app').pipe(webserver) 
});