// gulp
var gulp = require('gulp');

var usemin = require('gulp-usemin');
var uglify = require('gulp-uglify');
var htmlmin = require('gulp-htmlmin');
var cleanCss = require('gulp-clean-css');
var rev = require('gulp-rev');
var del = require('del');
var gulpSequence = require('gulp-sequence');


gulp.task('minified', function () {
  return gulp.src('Views/Shared/_Layout.cshtml')
    .pipe(usemin({
      css: [rev],
      html: [htmlmin({
        collapseWhitespace: true
      })],
      js: [uglify, rev]
    }))
    .pipe(gulp.dest('build/'));
});

gulp.task('clean', function () {
  return del('build', {
    force: true
  });
});

gulp.task('copy-html-files', function () {
  gulp.src(['./app/**/*.html'])
    .pipe(gulp.dest('build/'));
});

gulp.task('copy-image-files', function () {
  gulp.src(['./assets/images/**'])
    .pipe(gulp.dest('build/images'));
});


gulp.task('build', function (cb) {
  gulpSequence( 'clean', 'copy-html-files', 'copy-image-files', 'minified')(cb)
})