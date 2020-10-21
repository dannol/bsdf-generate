'use strict'

const isProduction = process.env.NODE_ENV === 'production'
const sourceMapEnabled = true

module.exports = {
    cashBusting: true,
    transformAssetUrls: {
        video: ['src', 'poster'],
        source: 'src',
        img: 'src',
        image: 'xlink:hrefs'
    }
}