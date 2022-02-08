import React, { useState, useEffect } from "react";
import { Link } from "react-router-dom";
import ClockLoader from "react-spinners/ClockLoader";

import { getAllBooks } from "../../services/booksService";

function ListBooksPage() {

    const [books, setBooks] = useState([]);
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        if (books.length === 0) {
            getAllBooks()
                .then(_books => {
                    setBooks(_books);
                    setLoading(false);
                });
        }
    }, [books.length]);

    return (
        <div>
            <div className="card shadow mt-2 mb-2">
                <div className="card-header">
                    <h2 className="PageTitle">Books List</h2>
                </div>
                <div className="card-body">
                    <div>
                        <a href="/add-book" className="float">
                            <i className="fa fa-plus float-margintop"></i>
                        </a>
                    </div>
                    <div className="sweet-loading d-flex justify-content-center">
                        <ClockLoader
                            size={150}
                            color={"#123abc"}
                            loading={loading}
                        />
                    </div>
                    <br></br>
                    <table className='table table-striped table-bordered' hidden={loading} aria-labelledby="tableLabel">
                        <thead className="thead-dark">
                            <tr>
                                <th>Book ID</th>
                                <th>Published On</th>
                                <th>Language</th>
                                <th>Author</th>
                                <th>Title</th>
                                <th scope="col">Actions</th>
                            </tr>
                        </thead>
                        <tbody>
                            {
                                books.map(book => {
                                    return (
                                        <tr key={book._id}>
                                            <td>{book._id}</td>
                                            <td>{book.dateOfPublish}</td>
                                            <td>{book.language}</td>
                                            <td>{book.author}</td>
                                            <td>{book.title}</td>
                                            <th scope="col">
                                                <Link to={"/edit-book/" + book._id} className="btn btn-warning btn-sm ml-2 shadow mr-2">
                                                    <i className="fa fa-edit fa-fw" aria-hidden="true"></i> Edit</Link>
                                                <Link to={"/delete-book/" + book._id} className="btn btn-danger btn-sm ml-2 shadow">
                                                    <i className="fa fa-trash" aria-hidden="true"></i> Delete</Link>
                                            </th>
                                        </tr>
                                    );
                                })
                            }
                        </tbody>
                    </table>
                </div>
            </div>
        </div >
    );
}

export default ListBooksPage;
