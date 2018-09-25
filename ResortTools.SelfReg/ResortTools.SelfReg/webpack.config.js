/// <binding BeforeBuild='Run - Development' />
'use strict'

var path = require('path')
var webpack = require('webpack')
const MiniCssExtractPlugin = require('mini-css-extract-plugin')
const VueLoaderPlugin = require('vue-loader/lib/plugin')

const vueLoaderConfig = require('./vue-loader.conf')
const env = process.env.NODE_ENV

const resortToolsScripts = [
    './Scripts/pass-office-app.js',
    './Scripts/rental-office-app.js',
    './Scripts/ski-school-app.js']

process.noDeprecation = true

module.exports = {
    stats: 'minimal',
    mode: env || 'development',
    entry: {
        PassOfficeApp: './Scripts/pass-office-app.js',
        RentalOfficeApp: './Scripts/rental-office-app.js',
        SkiSchoolApp: './Scripts/ski-school-app.js',
        JacksonHole: './Themes/JacksonHole/scss/main.scss'
    },
    output: {
        filename: 'wwwroot/js/[name].js',
        path: path.resolve(__dirname, './')
    },
    devServer: {
        contentBase: '.',
        host: 'localhost',
        port: 9000
    },
    resolve: {
        extensions: ['.js', '.vue', '.json']
    },
    module: {
        rules: [
            {
                test: /\.vue$/,
                loader: 'vue-loader',
                options: vueLoaderConfig
            },
            {
                test: /\.js$/,
                loader: 'babel-loader',
                exclude: file => (
                    /node_modules/.test(file) &&
                    !/\.vue\.js/.test(file)
                ),
                options: {
                    babelrc: false,
                    plugins: [
                        'transform-object-rest-spread'
                    ],
                    presets: ['env'],
                    ignore: ['node_modules']
                }
            },
            {
                test: /\.(scss|css)$/,
                use: [
                    MiniCssExtractPlugin.loader,
                    {
                        loader: 'css-loader',
                        options: {
                            minimize: {
                                safe: true
                            }
                        }
                    },
                    {
                        loader: 'postcss-loader',
                        options: {
                            autoprefixer: {
                                browsers: ['last 2 versions']
                            },
                            plugins: () => [
                                require('autoprefixer')()
                            ]
                        }
                    },
                    {
                        loader: 'sass-loader'
                    }
                ]
            },
            {
                test: /\.(jpe?g|png|gif|svg)$/,
                use: [
                    {
                        loader: 'url-loader',
                        options: {
                            limit: 8000,
                            name: '../images/[name].[ext]'
                        }
                    }
                ]
            }
        ]
    },
    plugins: [
        new VueLoaderPlugin(),
        new webpack.EvalSourceMapDevToolPlugin({
            filename: '[name].js.map',
            exclude: /node_modules/
        }),
        new webpack.ProvidePlugin({
            $: 'jquery',
            jQuery: 'jquery'
        }),
        new webpack.ProvidePlugin({
            _: 'lodash'
        }),
        new MiniCssExtractPlugin({
            // Options similar to the same options in webpackOptions.output
            // both options are optional
            filename: 'wwwroot/css/self-registration.css',
            chunkFilename: '[id].css'
        })
    ]
}
