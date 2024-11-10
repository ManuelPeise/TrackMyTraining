import React from "react";
import css from "./textField.module.css";

interface IProps {
  propertyKey: string;
  type: "text" | "password";
  value: unknown;
  label?: string;
  readonly?: boolean;
  required?: boolean;
  onChange: (key: string, value: string) => void;
  validationCallback?: (key: string, value: unknown) => boolean | undefined;
}

const FormTextField: React.FC<IProps> = (props) => {
  const {
    type,
    value,
    label,
    readonly,
    required,
    propertyKey,
    validationCallback,
    onChange,
  } = props;

  let isValid = undefined;

  if (validationCallback !== undefined && value !== "") {
    isValid = validationCallback(propertyKey, value);
  }

  const handleChange = React.useCallback(
    (e: React.ChangeEvent<HTMLInputElement>) => {
      onChange(propertyKey, e.currentTarget.value);
    },
    [propertyKey, onChange]
  );

  return (
    <div className={`${css.formItemContainer}`}>
      <label className={`form-label ${css.formLabel}`}>
        {required ? `${label} *` : label}
      </label>

      <input
        className={`${css.formTextControl}`}
        type={type}
        required={required}
        value={value as string}
        disabled={readonly}
        onChange={handleChange}
      />
      <div className={`${css.iconContainer}`}>
        {isValid !== undefined && (
          <i
            className={`${css.textFieldIcon} bi ${
              isValid ? "bi-check" : "bi-x"
            }`}
            style={{
              color: isValid !== undefined && isValid ? "green" : "red",
            }}
          ></i>
        )}
      </div>
    </div>
  );
};

export default FormTextField;
