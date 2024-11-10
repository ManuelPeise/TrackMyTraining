import React, { PropsWithChildren } from "react";
import { useI18N } from "src/_hooks/useI18N";

export interface IAppContext extends PropsWithChildren {
  getResource: (key: string) => string;
  changeLanguage: (lng: "de" | "en") => void;
}
export const AppContext = React.createContext<IAppContext>({} as IAppContext);

// TODO IApp sould not extend IAuthProps!

const AppContextProvider: React.FC<PropsWithChildren> = (props) => {
  const { children } = props;
  const { getResource, changeLanguage } = useI18N();

  return (
    <AppContext.Provider
      value={{ getResource: getResource, changeLanguage: changeLanguage }}
    >
      {children}
    </AppContext.Provider>
  );
};

export default AppContextProvider;
