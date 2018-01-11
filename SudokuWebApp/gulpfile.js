/// <binding BeforeBuild='build:sources' Clean='clean' ProjectOpened='watch:sources' />
/*
This file in the main entry point for defining Gulp tasks and using Gulp plugins.
Click here to learn more. http://go.microsoft.com/fwlink/?LinkId=518007
*/
"use strict";

const gulp = require('gulp');
const bower = require('gulp-bower');
const series = require('stream-series');
const inject = require('gulp-inject');
const wiredep = require('wiredep').stream;
const del = require('del');
const uglifyjs = require('gulp-uglifyjs');
const concat = require('gulp-concat');
const sequence = require('gulp-sequence');
const imagemin = require('gulp-imagemin');
const pngquant = require('imagemin-pngquant');
const cache = require('gulp-cache');
const autoprefixer = require('gulp-autoprefixer');
const browserSync = require('browser-sync').create();



var webroot = './wwwroot/',
    frontend = './Application/';

var paths = {
    appSrc: frontend + "app/sudokuApp.js",
    ngModule: frontend + "app/modules/**/*.js",
    ngDirective: frontend + "app/directives/**/*.js",
    ngController: frontend + "app/controllers/**/*.js",
    ngFactory: frontend + "app/factories/**/*.js",
    script: frontend + "scripts/**/*.js",
    views: frontend + 'views/**/*.html',
    indexView: frontend + 'views/index.html',
    styles: frontend + "css/**/*.css",
    images: frontend + 'img/**/*',
    api: frontend + "api/**/*.json"
};

gulp.task('bower', function(){
    return bower();   // By default gulp-bower runs install command for Bower
});

gulp.task('styles', function () {
    return gulp.src(paths.styles, { base: frontend })
        .pipe(autoprefixer({
            browsers: ['last 15 versions'],
            cascade: false
        }))
        .pipe(concat('css/site.css'))
        .pipe(gulp.dest(webroot));

});

gulp.task('inject:index', function () {
    var commonjsSrc = gulp.src(webroot + 'js/common.min.js',{read:false});
    var applicationSrc = gulp.src(webroot + 'js/application.min.js', { read: false });
    var styleSrc = gulp.src(webroot + 'css/**/*.css');

    gulp.src(paths.indexView)
        .pipe(wiredep({
            optional: 'configuration',
            goes: 'here',
            ignorePath: '../../wwwroot'
        }))
        .pipe(inject(series(commonjsSrc, applicationSrc), { ignorePath: '/wwwroot' }))
        .pipe(inject(series(styleSrc), { ignorePath: '/wwwroot' }))
        .pipe(gulp.dest(webroot));
});

gulp.task('scripts:minapp',function() {
    return gulp.src([paths.ngModule, paths.appSrc, paths.ngDirective, paths.ngController, paths.ngFactory])
        .pipe(uglifyjs('application.min.js'))
        .pipe(gulp.dest(webroot + 'js'));
});

gulp.task('scripts:min', function () {
    return gulp.src(paths.script, { base: frontend})
        .pipe(uglifyjs('common.min.js'))
        .pipe(gulp.dest(webroot + 'js'));
});

gulp.task('clean',function() {
    return del(['wwwroot/**', '!wwwroot', '!wwwroot/libraries/**', '!wwwroot/content/**']);
});

gulp.task('views', ['inject:index'],function() {
    return gulp.src([paths.views, '!' + paths.indexView], { base: frontend })
        .pipe(gulp.dest(webroot));
});

gulp.task('img', function () {
    return gulp.src(paths.images, { base: frontend })
        //.pipe(cache(imagemin({
        //    interlaced: true,
        //    progressive: true,
        //    svgoPlugins: [{ removeViewBox: false }],
        //    use: [pngquant()]
        //})))
        .pipe(gulp.dest(webroot));
});

gulp.task('api', function () {
    return gulp.src(paths.api, { base: frontend })
        .pipe(gulp.dest(webroot));
});

gulp.task('build', sequence('clean', ['scripts:min', 'scripts:minapp', 'styles', 'img', 'api'], 'views'));

gulp.task('watch', ['build'],function() {
    gulp.watch(paths.script, ['scripts:min']);
    gulp.watch(frontend + 'app/**/*.js', ['scripts:minapp']);
    gulp.watch(paths.styles, ['styles']);
    gulp.watch(paths.views, ['views']);
    gulp.watch(paths.images, ['img']);
});

gulp.task('default', ['watch']);

gulp.task('serve', function() {
    browserSync.init({
        server: {
            baseDir : webroot
        },
        notify: false
    });

    browserSync.watch(webroot+'**/*').on('change', browserSync.reload);
});

gulp.task('frontend', sequence('watch:sources', 'serve'));

//Sources for js develop
//=====================================================================================================================
gulp.task('inject:indexSrc', function () {
    var commonjsSrc = gulp.src(webroot + 'js/scripts/**/*.js', { read: false });
    var applicationSrc = gulp.src(webroot + 'js/app/**/*.js', { read: false });
    var styleSrc = gulp.src(webroot + 'css/**/*.css');

    gulp.src(paths.indexView)
        .pipe(wiredep({
            optional: 'configuration',
            goes: 'here',
            ignorePath: '../../wwwroot'
        }))
        .pipe(inject(series(commonjsSrc, applicationSrc), { ignorePath: '/wwwroot' }))
        .pipe(inject(series(styleSrc), { ignorePath: '/wwwroot' }))
        .pipe(gulp.dest(webroot));
});

gulp.task('scripts:app', function () {
    return gulp.src([paths.ngModule, paths.appSrc, paths.ngController, paths.ngFactory, paths.ngDirective], { base: frontend })
        .pipe(gulp.dest(webroot + 'js'));
});

gulp.task('scripts', function () {
    return gulp.src(paths.script, { base: frontend })
        .pipe(gulp.dest(webroot + 'js'));
});

gulp.task('views:sources', ['inject:indexSrc'], function () {
    return gulp.src([paths.views, '!' + paths.indexView], { base: frontend })
        .pipe(gulp.dest(webroot));
});

gulp.task('build:sources', sequence('clean', ['scripts', 'scripts:app', 'styles', 'img', 'api'], 'views:sources'));

gulp.task('watch:sources', ['build:sources'], function () {
    gulp.watch(paths.script, ['scripts']);
    gulp.watch(frontend + 'app/**/*.js', ['scripts:app']);
    gulp.watch(paths.styles, ['styles']);
    gulp.watch(paths.views, ['views:sources']);
    gulp.watch(paths.images, ['img']);
    gulp.watch(paths.api, ['api']);
});