import React from "react";
import { IApp } from "./_lib/_interfaces/IApp";
import AuthRouter from "./_lib/_router/AuthRouter";
import { useAuthentication } from "./_hooks/useAuthentication";

const AppStart: React.FC<IApp> = (props) => {
  const { getResource, changeLanguage } = props;
  const {
    authenticated,
    emailConfrimationParams,
    inProgress,
    onLogin,
    onLogout,
    onRegister,
    onConfirmEmail,
  } = useAuthentication();
  return (
    <AuthRouter
      inProgress={inProgress}
      authenticated={authenticated}
      emailConfrimationParams={emailConfrimationParams}
      getResource={getResource}
      changeLanguage={changeLanguage}
      onLogin={onLogin}
      onLogout={onLogout}
      onRegister={onRegister}
      onConfirmEmail={onConfirmEmail}
    />
  );
};

export default AppStart;
