import { IAuthProps } from "./IAuthProps";

export interface IApp extends IAuthProps {
  getResource: (key: string) => string;
  changeLanguage: (lng: "en" | "de") => void;
}
