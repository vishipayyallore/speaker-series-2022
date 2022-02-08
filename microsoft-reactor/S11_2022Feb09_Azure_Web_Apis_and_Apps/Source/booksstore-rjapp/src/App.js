import React from 'react';
import { Routes, Route } from 'react-router-dom';
import { ToastContainer } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";

import TopNavbar from './components/layout/TopNavbar';
import Footer from './components/layout/Footer';
import SideNavbar from "./components/layout/SideNavBar";

import HomePage from "./components/pages/HomePage";
import AboutPage from "./components/pages/AboutPage";
import PageNotFound from "./components/shared/PageNotFound";
import ListBooksPage from "./components/books/ListBooksPage";
import AddBookPage from "./components/books/AddBookPage";
import EditBookPage from "./components/books/EditBookPage";
import DeleteBookPage from "./components/books/DeleteBookPage";

function App() {
    return (
        <>
            <TopNavbar />

            <div className="container-fluid">
                <ToastContainer autoClose={3000} hideProgressBar />
                <div className=" row">
                    <div className="col-md-2 d-none d-md-block bg-sidebar sidebar">
                        <div className="sidebar-sticky">
                            <SideNavbar />
                        </div>
                    </div>
                    <div className="col-md-10 ml-sm-auto col-lg-10 px-4">
                        <Routes>
                            <Route path="/" exact element={< HomePage />} />
                            <Route path="/about" element={<AboutPage />} />
                            <Route path="/list-books" element={<ListBooksPage />} />
                            <Route path="/add-book" element={<AddBookPage />} />
                            <Route path="/edit-book/:id" element={<EditBookPage />} />
                            <Route path="/delete-book/:id" element={<DeleteBookPage />} />
                            <Route element={<PageNotFound />} />
                        </Routes>
                    </div>
                </div>
            </div>

            <Footer />
        </>
    );
}

export default App;
