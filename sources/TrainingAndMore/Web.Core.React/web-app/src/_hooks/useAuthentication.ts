import React from "react";
import { AuthContext } from "src/_lib/AuthContext";

export const useAuthentication = () => {
  return React.useContext(AuthContext);
};
