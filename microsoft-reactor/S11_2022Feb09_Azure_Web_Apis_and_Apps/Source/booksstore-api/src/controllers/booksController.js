'use strict';

import asyncHandler from 'express-async-handler'

import Book from '../models/bookModel.js';
import bookSchemaValidator from '../models/bookSchemaValidator.js';

const getAllBooks = asyncHandler(async (request, response) => {

	response.status(200)
		.json(await Book.find({}));

});

const addBook = asyncHandler(async (request, response) => {

	console.log(`Input Received: ${JSON.stringify(request.body)}`);

	// We need to verify both Author Name and Title
	const isBookValid = bookSchemaValidator.validate(request.body);

	if (isBookValid.error) {

		console.log("validation result", isBookValid);
		return response.status(400).json(isBookValid.error);
	}

	// Verify if book's title with same author already exists.
	const similarBookExist = await Book.findOne({ author: request.body.author, title: request.body.title, language: request.body.language });

	if (similarBookExist) {

		console.log(`Does Similar Book Exists: ${similarBookExist}`);
		return response.status(400).json(`Book with "${request.body.title}" title exists from "${request.body.author}" author.`);
	}

	try {

		const book = await Book.create(request.body);

		console.log(`Sending Output: ${JSON.stringify(book)}`);
		return response.status(201).json(book);

	} catch (error) {
		return response.status(500).json(error);
	}

});

const doesBookExists = asyncHandler(async (request, response, next) => {
	console.log(`Using Middleware for finding Book. ${request.params.bookId}`);

	Book.findById(request.params.bookId, (error, book) => {

		if (error) {
			return response.status(500).json(`Error from Middleware: ${error}`);
		}

		if (book) {
			console.log(`Book Found: ${book}`);
			request.book = book;
			return next();
		}

		return response.status(404).json();
	});

});

const getBookById = asyncHandler(async (request, response) => {

	return response.status(200)
		.json(request.book);
});

const updateBookById = asyncHandler(async (request, response) => {

	console.log(`Book Id: ${JSON.parse(JSON.stringify(request.book))._id} | Complete Book: ${JSON.stringify(request.book)}`);

	Book.findByIdAndUpdate(request.params.bookId, request.body, {
		new: true,
		useFindAndModify: false,
		runValidators: true
	},
		function (error, book) {
			if (error) {
				return response.status(500).json(error);
			} else {
				return response.status(200).json({ 'success': true, 'Message': 'Book updated Successfully', data: book });
			}
		});

});

const deleteBookById = asyncHandler(async (request, response) => {

	console.log(`Book Id: ${JSON.parse(JSON.stringify(request.book))._id} | Completed Book: ${JSON.stringify(request.book)}`);

	Book.findByIdAndDelete(request.book._id, function (error, book) {
		if (error) {
			return response.status(500).json(error);
		}
		else {
			return response.status(204).json({ 'success': true, 'Message': 'Book Deleted Successfully' });
		}
	});

});

export {
	getAllBooks, doesBookExists, getBookById,
	addBook, updateBookById, deleteBookById
};