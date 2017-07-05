// gulp
var gulp = require('gulp');

var del = require('del');
var gulpSequence = require('gulp-sequence');


gulp.task('clean', function () {
  return del('lib', {
    force: true
  });
});

gulp.task('copy-js', function () {
  gulp.src(["./bower_components/jquery/dist/jquery.js",
      "./bower_components/bootstrap/dist/js/bootstrap.js",
      "./bower_components/angular/angular.js",
      "./bower_components/angular-animate/angular-animate.js",
      "./bower_components/angular-sanitize/angular-sanitize.js",
      "./bower_components/angular-ui-router/release/angular-ui-router.js",
      "./bower_components/ngstorage/ngStorage.js",
      "./bower_components/angular-filter/dist/angular-filter.min.js",
      "./bower_components/angular-bootstrap/ui-bootstrap-tpls.min.js",
      "./bower_components/angular-toastr/dist/angular-toastr.tpls.min.js",
      "./bower_components/angular-loading-bar/build/loading-bar.min.js",
      "./bower_components/angular-messages/angular-messages.min.js",
      "./bower_components/lodash/dist/lodash.min.js"
    ])
    .pipe(gulp.dest('./lib/js'));


});

gulp.task('copy-css', function () {
  gulp.src(["./bower_components/bootstrap/dist/css/bootstrap.min.css",
      "./bower_components/components-font-awesome/css/font-awesome.min.css",
      "./bower_components/angular-toastr/dist/angular-toastr.min.css",
      "./bower_components/angular-loading-bar/build/loading-bar.min.css"
    ])
    .pipe(gulp.dest('./lib/css'));
});

gulp.task('copy-css', function () {
  gulp.src(["./bower_components/bootstrap/dist/css/bootstrap.min.css",
      "./bower_components/components-font-awesome/css/font-awesome.min.css",
      "./bower_components/angular-toastr/dist/angular-toastr.min.css",
      "./bower_components/angular-loading-bar/build/loading-bar.min.css"
    ])
    .pipe(gulp.dest('./lib/css'));
});

gulp.task('copy-fonts', function () {
  return gulp.src('./bower_components/components-font-awesome/fonts/**.*')
    .pipe(gulp.dest('./lib/fonts'));
});

gulp.task('copy-bootstrap-fonts', function () {
  return gulp.src('./bower_components/bootstrap/fonts/**.*')
    .pipe(gulp.dest('./lib/fonts'));
});



gulp.task('build', function (cb) {
  gulpSequence('copy-js', 'copy-css', 'copy-fonts', 'copy-bootstrap-fonts')(cb)
})