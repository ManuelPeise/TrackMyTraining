import React from "react";
import { Token } from "src/_hooks/useJwtToken";
import css from "./sidebarHeader.module.css";

interface IProps {
  token: Token;
  isExpanded: boolean;
}

const SidebarHeader: React.FC<IProps> = (props) => {
  const { token, isExpanded } = props;

  return (
    <div className={isExpanded ? css.containerExpanded : css.container}>
      <div className={isExpanded ? css.innerExpanded : css.inner}>
        <div
          className={isExpanded ? css.iconContainerExpanded : css.iconContainer}
        >
          <i className="bi bi-person-circle"></i>
        </div>
        <div
          className={
            isExpanded ? css.labelContainerExpanded : css.labelContainer
          }
        >
          <label>{token?.userName}</label>
          <hr />
        </div>
      </div>
    </div>
  );
};

export default SidebarHeader;
