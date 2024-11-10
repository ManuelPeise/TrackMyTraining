import React from "react";
import { AxiosClient } from "src/_lib/_api/AxiosClient";
import { useLocalStorage } from "./useLocalStorage";
import { LocalStorageKeyEnum } from "src/_lib/_enums/LocalStorageKeyEnum";

type ApiCache<T> = {
  [key: string]: T;
};

type ApiRequestOptions = {
  serviceUrl: string;
  parameters?: { [key: string]: string } | null;
  body?: string;
};

export type ApiResponse<TModel> = {
  success: boolean;
  data: TModel;
};

export type ApiResult<T> = {
  result: T;
  isLoading: boolean;
  error: string | null;
  get: (forceRequest?: boolean, options?: ApiRequestOptions) => Promise<void>;
  post: (options: ApiRequestOptions, data?: string) => Promise<T | null>;
};

export const useApi = <T>(
  apiOptions: ApiRequestOptions,
  force?: boolean,
  isPublic: boolean = false
): ApiResult<T> => {
  const { value } = useLocalStorage<string>(LocalStorageKeyEnum.jwt);

  const defaultRequestOptionsRef = React.useRef<ApiRequestOptions>(apiOptions);
  const [cache, setCache] = React.useState<ApiCache<T>>({} as ApiCache<T>);
  const [isLoading, setIsLoading] = React.useState<boolean>(false);
  const [error, setError] = React.useState<string | null>(null);

  const get = React.useCallback(
    async (
      forceRequest?: boolean,
      options?: ApiRequestOptions
    ): Promise<void> => {
      try {
        if (options) {
          defaultRequestOptionsRef.current = options;
        }

        if (
          !forceRequest &&
          !cache[defaultRequestOptionsRef.current.serviceUrl]
        )
          return;

        if (!isPublic) {
          AxiosClient.defaults.headers.common[
            "Authorization"
          ] = `bearer ${value}`;
        }

        setError(null);
        setIsLoading(true);

        await AxiosClient.get(defaultRequestOptionsRef.current.serviceUrl, {
          method: "GET",
          headers: { "Content-Type": "application/json" },
        }).then(async (res) => {
          if (res.status === 200) {
            cache[defaultRequestOptionsRef.current.serviceUrl] =
              await JSON.parse(JSON.stringify(res.data));

            setCache(cache);
          } else {
            setError(
              `Request [${defaultRequestOptionsRef.current.serviceUrl}] failed with [${res.status}]!`
            );
          }
        });

        setIsLoading(true);
      } catch (err) {
        setError(err.message);
      } finally {
        setIsLoading(false);
      }
    },
    [cache, value, isPublic]
  );

  const post = React.useCallback(
    async (options: ApiRequestOptions, data?: string): Promise<T | null> => {
      if (!isPublic) {
        AxiosClient.defaults.headers.common[
          "Authorization"
        ] = `bearer ${value}`;
      }

      setError(null);
      setIsLoading(true);
      try {
        await fetch(
          `${process.env.REACT_APP_API_BASE_URL}${options.serviceUrl}`,
          {
            method: "POST",
            mode: "cors",
            body: data,
            headers: {
              "Content-Type": "application/json",
            },
          }
        ).then(async (res) => {
          console.log("status", res.status);
          if (res.status === 200) {
            console.log(`data received...`);
            cache[defaultRequestOptionsRef.current.serviceUrl] =
              await JSON.parse(JSON.stringify(res.json()));

            setCache(cache);
          } else {
            setError(
              `Request [${defaultRequestOptionsRef.current.serviceUrl}] failed with [${res.status}]!`
            );
          }
        });

        setIsLoading(true);

        return cache[defaultRequestOptionsRef.current.serviceUrl] ?? null;
      } catch (err) {
        setError(err.message);
      } finally {
        setIsLoading(false);
      }

      return null;
    },
    [cache, value, isPublic]
  );

  React.useEffect(() => {
    if (force) {
      const loadData = async () => {
        await get(true);
      };

      loadData();
    }
    // eslint-disable-next-line
  }, []);

  return {
    result: cache[defaultRequestOptionsRef.current.serviceUrl],
    isLoading: isLoading,
    error: error,
    get,
    post,
  };
};
