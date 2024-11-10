import React, { PropsWithChildren } from "react";
import css from "./paper.module.css";

interface IProps extends PropsWithChildren {
  cssClasses?: string;
}
const Paper: React.FC<IProps> = (props) => {
  const { children, cssClasses } = props;

  return <div className={`${css.paper} ${cssClasses}`}>{children}</div>;
};

export default Paper;
