import React from "react";
import { IApp } from "src/_lib/_interfaces/IApp";
import css from "./layout.module.css";
import { Outlet } from "react-router-dom";
import LoadingOverlay from "./_components/LoadingOverlay";

const AuthLayout: React.FC<IApp> = (props) => {
  const { inProgress } = props;
  return (
    <div className={css.authPageContainer}>
      <Outlet context={props} />
      {inProgress && <LoadingOverlay />}
    </div>
  );
};

export default AuthLayout;
