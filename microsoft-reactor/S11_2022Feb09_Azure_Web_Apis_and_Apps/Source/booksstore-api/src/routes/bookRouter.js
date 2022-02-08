'use strict';

import express from 'express';
const bookRouter = express.Router();

import {
    getAllBooks, doesBookExists, getBookById,
    addBook, updateBookById, deleteBookById
} from '../controllers/booksController.js';

// Middleware For Retrieving the Book
bookRouter.use('/:bookId', doesBookExists);

bookRouter.route('/')
    .get(getAllBooks)
    .post(addBook);

bookRouter.route('/:bookId')
    .get(getBookById)
    .put(updateBookById)
    .delete(deleteBookById);

export default bookRouter;