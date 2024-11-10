import { useTranslation } from "react-i18next";
import React from "react";

export const useI18N = () => {
  const { t, i18n } = useTranslation(["common"]);

  const changeLanguage = React.useCallback(
    (lng: "en" | "de") => {
      i18n.changeLanguage(lng);
    },
    [i18n]
  );

  const getResource = React.useCallback(
    (key: string): string => {
      return t(key.replace(".", ":"));
    },
    [t]
  );

  return {
    getResource: getResource,
    changeLanguage: changeLanguage,
  };
};
