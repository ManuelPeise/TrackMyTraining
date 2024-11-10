import React from "react";
import Paper from "src/_components/_containers/Paper";
import { IApp } from "src/_lib/_interfaces/IApp";
import css from "./auth.module.css";
import FormTextField from "src/_components/_inputs/FormTextField";
import Checkbox from "src/_components/_inputs/FormCheckbox";
import FormHeader from "./_components/FormHeader";
import { appColors } from "src/styles/colors";
import AuthFormButton from "./_components/AuthFormButton";
import { useForm } from "src/_hooks/useForm";
import { ILogin } from "src/_lib/_interfaces/IAuthProps";
import { validationRules } from "src/_lib/validation";
import { useLocalStorage } from "src/_hooks/useLocalStorage";
import { LocalStorageKeyEnum } from "src/_lib/_enums/LocalStorageKeyEnum";
import { useNavigate } from "react-router-dom";

type AuthStorageData = Pick<ILogin, "email" | "remember">;

const LoginForm: React.FC<IApp> = (props) => {
  const { getResource, onLogin } = props;

  const navigate = useNavigate();
  const loginStorage = useLocalStorage<ILogin | null>(LocalStorageKeyEnum.auth);

  const loginForm = useForm<ILogin>([
    {
      key: "email",
      value: loginStorage?.value?.email ?? "",
      validationPattern: validationRules.email,
    },
    {
      key: "password",
      value: loginStorage?.value?.password ?? "",
      validationPattern: validationRules.password,
    },
    { key: "remember", value: loginStorage?.value?.remember ?? "" },
  ]);

  const handleLogin = React.useCallback(async () => {
    const model = loginForm.getUpdatedModel();

    const result = await onLogin(model);

    if (result && model.remember) {
      const storageModel: AuthStorageData = model;

      loginStorage.setItem(
        LocalStorageKeyEnum.auth,
        JSON.stringify(storageModel)
      );
    }

    if (!model.remember) {
      loginStorage.deleteItem(LocalStorageKeyEnum.auth);
    }
  }, [loginForm, loginStorage, onLogin]);

  return (
    <div className={css.wrapper}>
      <Paper cssClasses={`${css.formContainer}`}>
        <div className="m-3 w-100">
          <FormHeader
            color={appColors.iconLightBlue}
            icon="bi-key"
            title={getResource("common.labelLogin")}
            subTitle={getResource("common.labelIntoAccount")}
          />
        </div>
        <div className="m-3 w-75">
          <FormTextField
            propertyKey={loginForm.formState["email"].key}
            type="text"
            value={loginForm.formState["email"].value}
            label={getResource("common.labelEmail")}
            required
            onChange={loginForm.updateField}
          />
        </div>
        <div className="m-3 w-75">
          <FormTextField
            propertyKey={loginForm.formState["password"].key}
            type="password"
            required
            label={getResource("common.labelPassword")}
            value={loginForm.formState["password"].value}
            onChange={loginForm.updateField}
          />
        </div>
        <div className="m-3 w-75">
          <Checkbox
            propertyKey="remember"
            checked={loginForm.formState["remember"].value}
            label={getResource("common.labelRemember")}
            onChange={loginForm.updateField}
          />
        </div>
        <div className={`${css.buttonContainer}`}>
          <AuthFormButton
            label={getResource("labelRegister")}
            onClick={() => navigate("/register", { replace: true })}
          />
          <AuthFormButton
            label={getResource("labelLogin")}
            disabled={!loginForm.isModified || !loginForm.isValid}
            onClick={handleLogin}
          />
        </div>
      </Paper>
    </div>
  );
};

export default LoginForm;
