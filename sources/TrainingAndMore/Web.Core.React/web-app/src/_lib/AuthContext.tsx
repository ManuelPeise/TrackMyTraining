import React, { PropsWithChildren } from "react";
import {
  AccountActivationResponse,
  EmailConfirmation,
  EmailConfirmationParams,
  IAuthData,
  IAuthProps,
  ILogin,
  IRegistration,
  IRegistrationResult,
  JwtData,
} from "./_interfaces/IAuthProps";
import { apiHelper } from "./_api/apiHelper";
import { useLocalStorage } from "src/_hooks/useLocalStorage";
import { LocalStorageKeyEnum } from "./_enums/LocalStorageKeyEnum";
import { ApiResponse } from "src/_hooks/useApi";
import { AxiosClient } from "./_api/AxiosClient";

export const AuthContext = React.createContext<IAuthProps>({} as IAuthProps);

const AuthContextProvider: React.FC<PropsWithChildren> = (props) => {
  const { children } = props;

  const { value, setItem, deleteItem } = useLocalStorage<JwtData | null>(
    LocalStorageKeyEnum.jwt
  );
  const [emailConfirmation, setEmailConfirmation] =
    React.useState<EmailConfirmationParams | null>(null);
  const [isLoading, setIsLoading] = React.useState<boolean>(false);
  React.useEffect(() => {
    if (value != null) {
      setAuthData({ token: value.token, authenticated: value?.token != null });
    }
  }, [value]);

  const [authData, setAuthData] = React.useState<IAuthData>({
    token: value?.token ?? "",
    authenticated: value?.token != null,
  });

  const handleLogin = React.useCallback(
    async (data: ILogin): Promise<boolean> => {
      data.remember = false;
      const result = false;
      let responseData: ApiResponse<string>;
      setIsLoading(true);
      await AxiosClient.post(apiHelper.endpoints.auth.login, data, {
        headers: { "Content-Type": "application/json" },
      }).then((res) => {
        if (res.status === 200) {
          console.log(res.data);
          responseData = res.data;
        }
      });

      if (responseData.success) {
        const jwt: JwtData = {
          token: responseData.data,
        };
        setItem(LocalStorageKeyEnum.jwt, JSON.stringify(jwt));

        setAuthData({ token: responseData.data, authenticated: true });
      }

      setIsLoading(false);
      return result;
    },
    [setItem]
  );

  const handleLogout = React.useCallback(async () => {
    setIsLoading(true);
    deleteItem(LocalStorageKeyEnum.jwt);
    setAuthData({ token: "", authenticated: false });
    setIsLoading(false);
  }, [deleteItem]);

  const handleRegister = React.useCallback(
    async (data: IRegistration): Promise<IRegistrationResult | null> => {
      let responseData: ApiResponse<IRegistrationResult>;
      setIsLoading(true);
      await AxiosClient.post(apiHelper.endpoints.auth.register, data, {
        headers: { "Content-Type": "application/json" },
      }).then((res) => {
        if (res.status === 200) {
          responseData = res.data;

          if (responseData.success) {
            setEmailConfirmation({
              userId: responseData.data.userId,
              firstName: responseData.data.firstName,
            });
          }
        }
      });

      setIsLoading(true);
      if (responseData.success) {
        return responseData.data;
      }

      return null;
    },
    []
  );

  const handleConfirmEmail = React.useCallback(
    async (
      model: EmailConfirmation
    ): Promise<AccountActivationResponse | null> => {
      let responseData: ApiResponse<AccountActivationResponse> = null;
      await AxiosClient.post(
        apiHelper.endpoints.auth.accountActivation,
        model,
        {
          headers: { "Content-Type": "application/json" },
        }
      ).then((res) => {
        if (res.status === 200) {
          responseData = res.data;
        }
      });

      return responseData.data;
    },
    []
  );

  const authProps: IAuthProps = React.useMemo((): IAuthProps => {
    return {
      inProgress: isLoading,
      authenticated: authData.authenticated,
      emailConfrimationParams: emailConfirmation,
      onLogin: handleLogin,
      onLogout: handleLogout,
      onRegister: handleRegister,
      onConfirmEmail: handleConfirmEmail,
    };
  }, [
    isLoading,
    authData.authenticated,
    emailConfirmation,
    handleConfirmEmail,
    handleLogin,
    handleLogout,
    handleRegister,
  ]);

  return (
    <AuthContext.Provider value={authProps}>{children}</AuthContext.Provider>
  );
};

export default AuthContextProvider;
