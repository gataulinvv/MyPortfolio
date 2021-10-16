"use strict";

var path = require('path')
const { CleanWebpackPlugin } = require('clean-webpack-plugin');
const MiniCssExtractPlugin = require('mini-css-extract-plugin');

module.exports = {

    mode: "development",
    devtool: 'source-map',

    entry: {

        layout: path.resolve(__dirname, './src/Home/index.js'),
        login: path.resolve(__dirname, './src/Account/index.js'),

    },

    output: {

        path: path.resolve(__dirname, './wwwroot/bundles'),
        filename: '[name].js'
    },
    module: {
        rules: [

            {
                test: /\.css$/,
                use: [MiniCssExtractPlugin.loader, 'css-loader']
            },
            {
                test: /\.jsx?$/,
                exclude: /(node_modules)/,
                loader: "babel-loader"

            }
        ]
    },
    optimization: {
        usedExports: true,
    },
    plugins: [
        new MiniCssExtractPlugin(),
        new CleanWebpackPlugin(),
    ],
    resolve: {
        extensions: ['.js', '.jsx']
    },
}