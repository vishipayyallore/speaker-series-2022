'use strict';

import Joi from 'joi';

const bookSchemaValidator = Joi.object({

    pictureUrl: Joi.string(),

    title: Joi.string().required(),

    author: Joi.string().required(),

    language: Joi.string(),

    dateOfPublish: Joi.date(),

    isbn: Joi.string(),

    pages: Joi.number(),

    isActive: Joi.bool()
});

export default bookSchemaValidator;
