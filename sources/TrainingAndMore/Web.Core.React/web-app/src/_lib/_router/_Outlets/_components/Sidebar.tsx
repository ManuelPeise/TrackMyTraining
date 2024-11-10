import React from "react";
import css from "./sidebar.module.css";
import NavList from "./NavList";
import { Token } from "src/_hooks/useJwtToken";
import SidebarHeader from "./SidebarHeader";

interface IProps {
  isExpanded: boolean;
  token?: Token;
}

const Sidebar: React.FC<IProps> = (props) => {
  const { isExpanded, token } = props;

  return (
    <div className={`${isExpanded ? css.sidebarExpanded : css.sidebar}`}>
      <SidebarHeader token={token} isExpanded={isExpanded} />
      <NavList isExpanded={isExpanded} className={css.navNavigation} />
    </div>
  );
};

export default Sidebar;
