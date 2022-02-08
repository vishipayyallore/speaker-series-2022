'use strict';

import mongoose from 'mongoose';

const { Schema } = mongoose;

const bookModel = new Schema({

    pictureUrl: { type: String, required: false },

    title: { type: String, required: true },

    author: { type: String, required: true },

    language: { type: String, default: 'C#' },

    dateOfPublish: { type: Date, default: new Date().toUTCString() },

    isbn: { type: String, required: false },

    pages: { type: Number, required: false },

    isActive: { type: Boolean, default: false },
});

const Book = mongoose.model('book', bookModel);

export default Book;
