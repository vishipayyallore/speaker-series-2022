import React from 'react';
import PropTypes from 'prop-types';
import { Link } from 'react-router-dom';

const TopNavbar = ({ icon, title }) => {
  return (
    <nav className="navbar navbar-expand-md navbar-dark top-navbar-color">
      <div className="container">
        <Link to="/list-books" className="navbar-brand"><i className="fa fa-book" aria-hidden="true"></i> Book Store</Link>
        <button className="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarMain">
          <span className="navbar-toggler-icon"></span>
        </button>
        <div className="collapse navbar-collapse" id="navbarMain">
          <ul className="navbar-nav mr-auto">
            <li className="nav-item">
              <Link to='/'>Home</Link>
            </li>
            <li className="nav-item">
              <Link to='/about'>About</Link>
            </li>
          </ul>
        </div>
      </div>
    </nav>
  );
};

TopNavbar.defaultProps = {
  title: 'Book Stores',
  icon: 'fa fa-book'
};

TopNavbar.propTypes = {
  title: PropTypes.string.isRequired,
  icon: PropTypes.string.isRequired
};

export default TopNavbar;
