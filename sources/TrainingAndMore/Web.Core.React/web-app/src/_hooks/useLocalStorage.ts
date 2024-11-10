import React from "react";
import { LocalStorageKeyEnum } from "src/_lib/_enums/LocalStorageKeyEnum";

export const useLocalStorage = <TModel>(key: LocalStorageKeyEnum) => {
  const [value, setValue] = React.useState<TModel | null>(null);

  const getItem = React.useCallback(
    (key: LocalStorageKeyEnum, parse: boolean = true) => {
      let data: TModel = null;

      const json = window.localStorage.getItem(key) ?? null;

      if (json != null && json?.length && parse) {
        data = JSON.parse(json) ?? ({} as TModel);
      } else {
        data = json as TModel;
      }

      setValue(data);
    },
    []
  );

  const setItem = React.useCallback(
    (key: LocalStorageKeyEnum, model: string) => {
      window.localStorage.setItem(key, model);

      setValue(JSON.parse(model));
    },
    []
  );

  const deleteItem = React.useCallback((key: LocalStorageKeyEnum) => {
    window.localStorage.removeItem(key);
  }, []);

  React.useEffect(() => {
    getItem(key);
    // eslint-disable-next-line
  }, []);

  return {
    getItem,
    setItem,
    deleteItem,
    value,
  };
};
