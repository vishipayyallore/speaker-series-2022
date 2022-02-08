'use strict';

import mongoose from 'mongoose';
import chalk from 'chalk';

const connectToMongoDb = async () => {

    try {

        await mongoose.connect(process.env.MongoDbConnection, {
            useNewUrlParser: true,
            useUnifiedTopology: true
        });

        // Connecting to the MongoDb Cloud Instance
        // console.log(chalk.cyan.bold(`Mongo Db Connection: ${process.env.MongoDbConnection}`));  // NEVER PRINT THIS
        console.log('Connected to MongoDb in Cloud');

    } catch (error) {

        console.log(chalk.red.bold(`Error Connecting to Cloud MongoDb ${error}`));
        // Exit Process with Failure
        process.exit(1);
    }

}

export default connectToMongoDb;

