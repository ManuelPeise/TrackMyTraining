import React from "react";
import { NavLink } from "react-router-dom";
import css from "./link.module.css";

interface IProps {
  label: string;
  to: string;
}

const Link: React.FC<IProps> = (props) => {
  const { label, to } = props;
  return (
    <div className={`${css.linkContainer}`}>
      <NavLink className={`${css.link}`} to={to}>
        <span>{label}</span>
      </NavLink>
    </div>
  );
};

export default Link;
