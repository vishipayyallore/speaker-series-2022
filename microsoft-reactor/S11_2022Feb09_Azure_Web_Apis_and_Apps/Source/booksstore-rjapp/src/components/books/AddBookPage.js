import React, { useState } from "react";
import { Link, useNavigate } from "react-router-dom";
import { toast } from "react-toastify";

import { saveBook } from "../../services/booksService";

function AddBookPage() {

    let history = useNavigate();

    const [book, setBook] = useState({
        title: "",
        author: "No Name",
        dateOfPublish: (new Date()).toISOString().slice(0, 10).replace(/-/g, "-").replace("T", " "),
        language: "C#"
    });

    function handleAddBookSubmit(event) {
        event.preventDefault();
        saveBook(book)
            .then(_ => {
                history.push('/list-books');
                toast.success("Book saved.");
            })
            .catch(_ => {
                toast.error("Error saving book");
            });
    }

    function handleFormChange({ target }) {
        setBook({
            ...book,
            [target.name]: target.value
        });
    }

    return (
        <div className="card shadow mt-2 mb-2">
            <div className="card-header">
                <h2 className="PageTitle">Add Book</h2>
            </div>
            <div className="card-body">
                <div className="col-md-8 mb-4">

                    <form>

                        <div className="form-group divflex labelAndTextbox">
                            <label className="element col-md-2">Title : </label>
                            <input type="text" name="title" className="form-control element ml-4" maxLength="100"
                                onChange={handleFormChange} value={book.title} />
                        </div>

                        <div className="form-group divflex labelAndTextbox">
                            <label className="element col-md-2">Author: </label>
                            <input type="text" name="author" className="form-control element ml-4" maxLength="100"
                                onChange={handleFormChange} value={book.author} />
                        </div>

                        <div className="form-group divflex labelAndTextbox">
                            <label className="element col-md-2">Published: </label>
                            <input type="date" name="dateOfPublish" className="form-control element ml-4"
                                onChange={handleFormChange} pattern="\d{4}-\d{2}-\d{2}" value={book.dateOfPublish} />
                        </div>

                        <div className="form-group divflex labelAndTextbox">
                            <label className="element col-md-2">Language: </label>
                            <input type="text" name="language" className="form-control element ml-4" maxLength="100"
                                onChange={handleFormChange} value={book.language} />
                        </div>
                    </form>
                    <Link to="" onClick={handleAddBookSubmit} type="submit" className="btn btn-primary btn-sm ml-2 shadow mr-2">
                        <i className="fa fa-save fa-fw" aria-hidden="true"></i> Save</Link>
                    <Link to="/list-books" className="btn btn-maincolor btn-sm ml-2 shadow">
                        <i className="fa fa-list" aria-hidden="true"></i> Books List</Link>

                </div>
            </div>
        </div>
    );
}

export default AddBookPage;