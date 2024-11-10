import React from "react";
import { Link } from "react-router-dom";
import css from "./navListItem.module.css";

interface IProps {
  icon: string;
  label: string;
  to: string;
  isExpanded: boolean;
}

const NavListItem: React.FC<IProps> = (props) => {
  const { isExpanded } = props;
  return (
    <li className={`${isExpanded ? css.navListItemExpanded : css.navListItem}`}>
      <i className={`bi ${props.icon}`}></i>
      <span>
        <Link to={props.to}>{props.label}</Link>
      </span>
    </li>
  );
};

export default NavListItem;
