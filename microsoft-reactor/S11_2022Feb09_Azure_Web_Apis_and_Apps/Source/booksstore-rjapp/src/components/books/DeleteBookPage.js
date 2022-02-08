import React, { useState, useEffect } from "react";
import { Link, useNavigate } from "react-router-dom";
import { toast } from "react-toastify";

import { deleteBookById, getBookById } from "../../services/booksService";

function DeleteBookPage({ match }) {

    let history = useNavigate();

    const [book, setBook] = useState({
        title: "",
        author: "No Name",
        dateOfPublish: (new Date()).toISOString().slice(0, 10).replace(/-/g, "-").replace("T", " "),
        language: "C#"
    });

    useEffect(() => {
        getBookById(match.params.id)
            .then(_book => setBook(_book));
    }, [match.params.id]);

    function handleDeleteBookSubmit(event) {
        event.preventDefault();
        deleteBookById(match.params.id)
            .then(_ => {
                history.push('/list-books');
                toast.success("Book Deleted.");
            })
            .catch(_ => {
                toast.error("Error deleting book");
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
                <h2 className="PageTitle">Delete Book</h2>
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
                                onChange={handleFormChange} value={new Date(book.dateOfPublish).toISOString().slice(0, 10).replace(/-/g, "-").replace("T", " ")} />
                        </div>

                        <div className="form-group divflex labelAndTextbox">
                            <label className="element col-md-2">Language: </label>
                            <input type="text" name="language" className="form-control element ml-4" maxLength="100"
                                onChange={handleFormChange} value={book.language} />
                        </div>
                    </form>
                    <Link to="" onClick={handleDeleteBookSubmit} type="submit" className="btn btn-danger btn-sm ml-2 shadow mr-2">
                        <i className="fa fa-trash" aria-hidden="true"></i> Delete</Link>
                    <Link to="/list-books" className="btn btn-maincolor btn-sm ml-2 shadow">
                        <i className="fa fa-list" aria-hidden="true"></i> Books List</Link>

                </div>
            </div>
        </div>
    );
}

export default DeleteBookPage;