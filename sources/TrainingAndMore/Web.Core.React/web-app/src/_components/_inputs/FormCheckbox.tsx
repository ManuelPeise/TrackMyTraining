import React from "react";
import css from "./checkbox.module.css";

interface IProps {
  propertyKey: string;
  checked: unknown;
  label?: string;
  readonly?: boolean;
  required?: boolean;
  onChange: (key: string, value: boolean) => void;
}

const FormCheckbox: React.FC<IProps> = (props) => {
  const { checked, label, readonly, required, propertyKey, onChange } = props;

  const handleChange = React.useCallback(
    (e: React.ChangeEvent<HTMLInputElement>) => {
      console.log(e.currentTarget.checked);
      onChange(propertyKey, e.currentTarget.checked);
    },
    [propertyKey, onChange]
  );

  return (
    <div className={`form-item-container ${css.formItemContainer}`}>
      <div className={`${css.formCheckContainer}`}>
        <input
          className={`form-check ${css.formCheckControl}`}
          type="checkbox"
          required={required}
          checked={checked as boolean}
          disabled={readonly}
          onChange={handleChange}
        />
        <label>{label}</label>
      </div>
    </div>
  );
};

export default FormCheckbox;
