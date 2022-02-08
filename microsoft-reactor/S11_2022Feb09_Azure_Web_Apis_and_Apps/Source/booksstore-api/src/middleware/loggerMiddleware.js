'use strict';

import morgan from 'morgan';
import chalk from 'chalk';

// Logger Middleware
const morganLogger = morgan(function (tokens, req, res) {
    return chalk.blue.bold([
        'Method:', tokens.method(req, res),
        '\tEnd Point:', tokens.url(req, res),
        '\tStatus:', tokens.status(req, res),
        '\tContent Length:', tokens.res(req, res, 'content-length'),
        '\tResponse Time', tokens['response-time'](req, res), 'ms'
    ].join(' '));
});

export default morganLogger;
