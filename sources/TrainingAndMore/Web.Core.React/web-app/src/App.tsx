import React from "react";
import "./_lib/_translations/i18n";
import AppContextProvider, { AppContext, IAppContext } from "./_lib/AppContext";
import AuthContextProvider, { AuthContext } from "./_lib/AuthContext";
import AppStart from "./AppStart";
import { IAuthProps } from "./_lib/_interfaces/IAuthProps";
import "./styles/styles.css";
import "./styles/bootstrap-icons.css";

const App: React.FC = () => {
  return (
    <div className="container-fluid vh-100 vw-100 p-0 m-0">
      <AuthContextProvider>
        <AppContextProvider>
          <AuthContext.Consumer>
            {(authProps: IAuthProps) => (
              <AppContext.Consumer>
                {(props: IAppContext) => (
                  <AppStart
                    authenticated={authProps.authenticated}
                    emailConfrimationParams={authProps.emailConfrimationParams}
                    onConfirmEmail={authProps.onConfirmEmail}
                    onLogin={authProps.onLogin}
                    onLogout={authProps.onLogout}
                    onRegister={authProps.onRegister}
                    getResource={props.getResource}
                    changeLanguage={props.changeLanguage}
                  />
                )}
              </AppContext.Consumer>
            )}
          </AuthContext.Consumer>
        </AppContextProvider>
      </AuthContextProvider>
    </div>
  );
};

export default App;
