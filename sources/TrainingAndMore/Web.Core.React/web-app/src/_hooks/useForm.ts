import React from "react";
import { isEqual } from "lodash";

type FormProperty<T> = {
  key: keyof T;
  originalValue: unknown;
  value: unknown;
  validationPattern: string;
  matchTo?: keyof T;
};

type FormFieldInitProps<T> = {
  key: keyof T;
  value: unknown;
  validationPattern?: string;
  matchTo?: keyof T;
};

export const formReducer = <TState>(
  prevState: FormProperty<TState>[],
  stateUpdate: FormProperty<TState>[]
): FormProperty<TState>[] => {
  if (stateUpdate == null) {
    return null;
  }
  let stateChanged = false;

  if (prevState == null) {
    stateChanged = stateUpdate != null;
  } else {
    stateChanged = isEqual(prevState, stateUpdate);

    if (stateChanged) {
      return stateUpdate;
    }
  }
  return prevState;
};

const loadReducerState = <TModel>(
  fields: FormFieldInitProps<TModel>[]
): FormProperty<TModel>[] => {
  const items: FormProperty<TModel>[] = [];

  fields.forEach((field) => {
    items.push({
      key: field.key,
      originalValue: field.value,
      value: field.value,
      validationPattern: field.validationPattern,
      matchTo: field.matchTo,
    });
  });

  return items;
};

export const useForm = <TModel>(initialState: FormFieldInitProps<TModel>[]) => {
  const [state, dispatch] = React.useReducer<
    React.Reducer<FormProperty<TModel>[], Partial<FormProperty<TModel>[]>>
  >(
    (prevState: FormProperty<TModel>[], update: FormProperty<TModel>[]) =>
      formReducer(prevState, update),
    loadReducerState(initialState)
  );

  const resetForm = React.useCallback(() => {
    const reducerState = { ...state };

    reducerState.forEach((item) => {
      item.value = item.originalValue;
    });

    dispatch(reducerState);
  }, [state, dispatch]);

  const updateField = React.useCallback(
    (key: keyof TModel, value: unknown) => {
      const reducerState = state.slice();
      const fieldIndex = reducerState.findIndex((x) => x.key === key) ?? null;

      if (fieldIndex != null) {
        reducerState[fieldIndex].value = value;
      }

      dispatch(reducerState);
    },
    [state, dispatch]
  );

  const getUpdatedModel = React.useCallback((): TModel => {
    const reducerState = state.slice();

    const keys = reducerState.map((item) => item.key) as Array<keyof TModel>;

    const object = Object.create({});

    keys.forEach((key) => {
      const index = reducerState.findIndex((x) => x.key === key);

      if (index !== -1) {
        object[key] = reducerState[index].value;
      }
    });

    return object as TModel;
  }, [state]);

  const isValidFieldValue = React.useCallback(
    (key: keyof TModel, value: unknown): boolean | undefined => {
      const fieldIndex = state.findIndex((x) => x.key === key) ?? null;

      if (fieldIndex != null) {
        const item: FormProperty<TModel> = state[fieldIndex];

        if (item.validationPattern === undefined) {
          return undefined;
        }

        const regex = new RegExp(item?.validationPattern as string);
        const isValidValue = regex.test(value as string);

        if (item.matchTo === undefined) {
          return isValidValue;
        }

        const compareItemIndex =
          state.findIndex((x) => x.key === item.matchTo) ?? null;

        if (compareItemIndex !== undefined) {
          const compareItem: FormProperty<TModel> = state[compareItemIndex];

          return compareItem.value === item.value;
        }
      }

      return undefined;
    },
    [state]
  );

  const isModified = React.useMemo((): boolean => {
    if (!state?.length) {
      return false;
    }

    return state?.some((item) => item.originalValue !== item.value);
  }, [state]);

  const isValid = React.useMemo(() => {
    const validationResults: boolean[] = [];
    const stateUpdate = state.slice();

    if (stateUpdate !== undefined && stateUpdate?.length) {
      stateUpdate
        .filter(
          (item) =>
            item.validationPattern !== undefined &&
            item.validationPattern !== ""
        )
        .forEach((item) => {
          const regEx = new RegExp(item.validationPattern);

          validationResults.push(regEx.test(item.value as string));

          if (item.matchTo !== undefined) {
            const index =
              state.findIndex((x) => x.key === item.matchTo) ?? null;

            if (index != null) {
              validationResults.push(item?.value === state[index]?.value);
            }
          }

          // if (
          //   item.key !== undefined &&
          //   stateUpdate[item?.key as string] !== undefined
          // ) {
          //   stateUpdate[item?.key as string].isValid = isValidField;
          // }
        });

      const invalidItems = validationResults.filter(
        (result) => result === false
      );

      return invalidItems?.length === 0;
    }
    return true;
  }, [state]);

  const formState = React.useMemo(() => {
    const reducerState = state.slice();
    const formStateModel: { [key: string]: FormProperty<TModel> } = {};

    if (reducerState?.length === 0) {
      return formStateModel;
    }

    reducerState?.forEach((item) => {
      const itemKey = item.key as string;
      if (!formStateModel[itemKey]) {
        formStateModel[itemKey] = item;
      }
    });

    return formStateModel;
  }, [state]);

  return {
    formState: formState,
    isModified,
    isValid,
    resetForm,
    updateField,
    getUpdatedModel,
    isValidFieldValue,
  };
};
