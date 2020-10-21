'use strict'

const path = require('path')
const VueLoaderPlugin = require('vue-loader/lib/plugin')
const vueLoaderConfig = require('./vue-loader.conf')
const MiniCssExtractPlugin = require("mini-css-extract-plugin")

// const createLodashAliases = require('lodash-loader').createLodashAliases

module.exports = {
  mode: 'production',
  entry: {
    booktools: path.join(__dirname, 'src', 'booktools'),
  },
  output: {
    filename: 'js/book-tools--bundle.js',
    path: path.resolve(__dirname, 'dist')
  },
  resolve: {
    extensions: ['.js', '.vue', '.json', '.scss', '.css'],
    alias: {
      'vue$': 'vue/dist/vue.esm.js'
    },
  },
  module: {
    rules: [
      {
        test: /\.vue$/,
        loader: 'vue-loader',
        options: vueLoaderConfig
      },
      // this will apply to both plain `.js` files
      // AND `<script>` blocks in `.vue` files
      {
        test: /\.js$/,
        include: [
          path.resolve(__dirname, 'src')
        ],
        exclude: [
          path.resolve(__dirname, 'node_modules')
        ],
        loader: 'babel-loader',
        options: {
          presets: ['@babel/preset-env'],
          ignore: ['node_modules']
        },
      },
      // this will apply to both plain `.css` files
      // AND `<style>` blocks in `.vue` files
      {
        test:  /\.s[a|c]ss$/,
        use: [
          process.env.NODE_ENV !== 'production'
            ? { loader: 'vue-style-loader' }
            : MiniCssExtractPlugin.loader,
          { loader: 'css-loader', options: { sourceMap: true } },
          { loader: 'sass-loader', options: { sourceMap: true } },
          { loader: 'sass-resources-loader',
            options: {
              sourceMap: true,
              resources: [
                './src/scss/variables.scss',
                './src/scss/colors.scss',
                './src/scss/components.scss'
              ]
            }
          }
        ]
      },
      {
        test: /\.(png|jpg|gif)$/,
        use: [
          {
            loader: 'file-loader',
            options: {}
          }
        ]
      }
    ]
  },
  devtool: 'source-map',
  devServer: {
    publicPath: path.join('/dist/')
  },
  plugins: [
    new VueLoaderPlugin(),
    new MiniCssExtractPlugin({
      // Options similar to the same options in webpackOptions.output
      // both options are optional
      //filename: "[name].css",
      filename: "css/[name].css",
      chunkFilename: "[id].css",
      path: path.resolve(__dirname, 'dist')
    })
  ]
};