'use strict';

import express from 'express';

import morganLogger from './middleware/loggerMiddleware.js';
import bookRouter from './routes/bookRouter.js';

// Initialize the application
const webApi = express();

// Logger Middleware
webApi.use(morganLogger);

// Allowing CORS
webApi.use(function (_, response, next) {

    response.header("Access-Control-Allow-Origin", "*");
    response.header("Access-Control-Allow-Headers", "Origin, X-Requested-With, Content-Type, Accept");
    response.header("Access-Control-Allow-Methods", "GET, POST, OPTIONS, PUT, DELETE");

    next();
});

// express middleware to handle the json body request
webApi.use(express.json());

// Default Route
webApi.get('/api', (request, response) => {

    response.status(200).json(
        {
            success: true,
            data: {
                message: "Welcome to Books Web API."
            }
        }
    );

});

// Middleware (To Import Additional Routes)
webApi.use('/api/books', bookRouter);

// webApi.use('/api/authors', authorRouter);

export default webApi;
