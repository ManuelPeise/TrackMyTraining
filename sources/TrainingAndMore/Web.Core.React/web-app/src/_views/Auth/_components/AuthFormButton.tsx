import React from "react";
import css from "./authFormButton.module.css";

interface IProps {
  label: string;
  disabled?: boolean;
  onClick: () => void | Promise<void>;
}

const AuthFormButton: React.FC<IProps> = (props) => {
  const { label, disabled, onClick } = props;

  return (
    <button
      className={`${css.authFormButton}`}
      type="button"
      disabled={disabled}
      onClick={onClick}
    >
      <span>{label}</span>
    </button>
  );
};

export default AuthFormButton;
