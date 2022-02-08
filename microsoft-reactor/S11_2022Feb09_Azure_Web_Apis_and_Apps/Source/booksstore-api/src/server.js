'use strict';

import path from 'path';
import dotenv from 'dotenv';

import webApi from './app.js';
import connectToMongoDb from './persistence/mongoDbHelper.js';

// Load the Configuration from the given Path
const _ = dotenv.config({ path: path.resolve(process.cwd(), 'src/config/.env') });

const port = process.env.PORT || 3000;

await connectToMongoDb()
    .then(() => {

        // Listen to the server
        webApi.listen(port, () => {
            console.log(`Env Port: ${process.env.PORT}`);
            console.log(`Server Listening at port ${port}. http://localhost:${port}`);
        });

    })
    .catch((error) => {

        console.log(`Error:: Unable to connect to Mongo Database. Message:: ${error}`);

    });
