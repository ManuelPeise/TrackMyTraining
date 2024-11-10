import React from "react";
import { Outlet } from "react-router-dom";
import { IApp } from "src/_lib/_interfaces/IApp";
import css from "./layout.module.css";
import Sidebar from "./_components/Sidebar";
import Navbar from "./_components/Navbar";
import { useJwtToken } from "src/_hooks/useJwtToken";

const AppLayout: React.FC<IApp> = (props) => {
  const [isExpanded, setIsExpanded] = React.useState<boolean>(false);

  const jwt = useJwtToken();

  const toggleExpanded = React.useCallback(() => {
    setIsExpanded(isExpanded === true ? false : true);
  }, [isExpanded]);

  return (
    <div className={`container-fluid m-0 p-0 ${css.appPageContainer}`}>
      <Navbar toggleExpanded={toggleExpanded} />
      <div className={css.contentContainer}>
        <Sidebar isExpanded={isExpanded} token={jwt.userData} />
        <Outlet context={props} />
      </div>
    </div>
  );
};

export default AppLayout;
