import React from "react";
import css from "./navbar.module.css";
import NavList from "./NavList";

interface IProps {
  toggleExpanded: () => void;
}

const Navbar: React.FC<IProps> = (props) => {
  const { toggleExpanded } = props;
  return (
    <div className={`${css.navbar} navbar-expand-lg navbar-dark`}>
      <div className={css.navbarTogglerContainer}>
        <div className={`${css.brandContainer}`}>
          <i
            className={`bi bi-list ${css.toggleButton}`}
            onClick={toggleExpanded}
          ></i>
          <a className={css.branding} href="/">
            {process.env.REACT_APP_Name}
          </a>
        </div>
        <button
          className={css.navBarToggler}
          type="button"
          data-bs-toggle="collapse"
          data-bs-target="#navbarNav"
          aria-controls="navbarNav"
          aria-expanded="false"
          aria-label="toggle navigation"
        >
          <span className="navbar-toggler-icon"></span>
        </button>
      </div>
      <div className="" id="navbarNav">
        <NavList isExpanded={true} className={css.navNavigation} />
      </div>
    </div>
  );
};

export default Navbar;
