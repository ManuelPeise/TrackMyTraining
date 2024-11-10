import React from "react";
import { JwtData } from "src/_lib/_interfaces/IAuthProps";
import { useLocalStorage } from "./useLocalStorage";
import { LocalStorageKeyEnum } from "src/_lib/_enums/LocalStorageKeyEnum";
import { jwtDecode } from "jwt-decode";

export type Token = {
  id: number;
  name: string;
  firstName: string;
  email: string;
  userName: string;
  dateOfBirth: string;
  userRole: string;
  isActive: boolean;
};

export const useJwtToken = () => {
  const storage = useLocalStorage<JwtData>(LocalStorageKeyEnum.jwt);
  const [data, setData] = React.useState<Token | null>(null);

  React.useEffect(() => {
    if (storage.value != null) {
      const jwtData = jwtDecode<Token>(storage.value.token);

      setData(jwtData);
    }
  }, [storage.value]);

  return {
    userData: data,
  };
};
