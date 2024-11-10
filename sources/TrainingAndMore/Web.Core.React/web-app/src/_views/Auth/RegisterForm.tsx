import React from "react";
import Paper from "src/_components/_containers/Paper";
import { IApp } from "src/_lib/_interfaces/IApp";
import css from "./auth.module.css";
import FormTextField from "src/_components/_inputs/FormTextField";
import FormHeader from "./_components/FormHeader";
import { appColors } from "src/styles/colors";
import AuthFormButton from "./_components/AuthFormButton";
import { useForm } from "src/_hooks/useForm";
import { IRegistration } from "src/_lib/_interfaces/IAuthProps";
import { validationRules } from "src/_lib/validation";
import { useNavigate } from "react-router-dom";

const RegisterForm: React.FC<IApp> = (props) => {
  const { getResource, onRegister } = props;
  const navigate = useNavigate();

  const registerForm = useForm<IRegistration>([
    {
      key: "name",
      value: "",
      validationPattern: validationRules.valueLength.replace(
        "replaceValue",
        "3"
      ),
    },
    {
      key: "firstName",
      value: "",
      validationPattern: validationRules.valueLength.replace(
        "replaceValue",
        "3"
      ),
    },
    {
      key: "email",
      value: "",
      validationPattern: validationRules.email,
    },
    {
      key: "dateOfBirth",
      value: "",
      validationPattern: validationRules.date,
    },
    {
      key: "password",
      value: "",
      validationPattern: validationRules.password,
      matchTo: "passwordReplication",
    },
    {
      key: "passwordReplication",
      value: "",
      validationPattern: validationRules.password,
      matchTo: "password",
    },
  ]);

  const handleRegister = React.useCallback(async () => {
    const model = registerForm.getUpdatedModel();
    const result = await onRegister(model);

    if (result != null) {
      navigate("/confirm-email", { replace: true });
    }
  }, [registerForm, navigate, onRegister]);

  return (
    <div className={css.wrapper}>
      <Paper cssClasses={`${css.formContainer} p-3`}>
        <div className="m-3 w-100">
          <FormHeader
            color={appColors.iconLightBlue}
            icon="bi-person"
            title={getResource("common.labelRegister")}
            subTitle={getResource("common.labelCreateAccount")}
          />
        </div>
        <div className="m-3 w-75">
          <FormTextField
            propertyKey={registerForm.formState["name"].key}
            type="text"
            value={registerForm.formState["name"].value}
            label={getResource("common.labelName")}
            required
            onChange={registerForm.updateField}
          />
        </div>
        <div className="m-3 w-75">
          <FormTextField
            propertyKey={registerForm.formState["firstName"].key}
            type="text"
            value={registerForm.formState["firstName"].value}
            label={getResource("common.labelFirstName")}
            required
            onChange={registerForm.updateField}
          />
        </div>
        <div className="m-3 w-75">
          <FormTextField
            propertyKey={registerForm.formState["dateOfBirth"].key}
            type="text"
            required
            label={getResource("common.labelDateOfBirth")}
            value={registerForm.formState["dateOfBirth"].value}
            onChange={registerForm.updateField}
          />
        </div>
        <div className="m-3 w-75">
          <FormTextField
            propertyKey={registerForm.formState["email"].key}
            type="text"
            required
            label={getResource("common.labelEmail")}
            value={registerForm.formState["email"].value}
            onChange={registerForm.updateField}
          />
        </div>
        <div className="m-3 w-75">
          <FormTextField
            propertyKey={registerForm.formState["password"].key}
            type="password"
            value={registerForm.formState["password"].value}
            label={getResource("common.labelPassword")}
            required
            onChange={registerForm.updateField}
            validationCallback={registerForm.isValidFieldValue}
          />
        </div>
        <div className="m-3 w-75">
          <FormTextField
            propertyKey={registerForm.formState["passwordReplication"].key}
            type="password"
            value={registerForm.formState["passwordReplication"].value}
            label={getResource("common.labelPasswordReplication")}
            required
            onChange={registerForm.updateField}
            validationCallback={registerForm.isValidFieldValue}
          />
        </div>
        <div className={`${css.registerButtonContainer}`}>
          <AuthFormButton
            label={getResource("labelLogin")}
            onClick={() => navigate("/", { replace: true })}
          />
          <AuthFormButton
            label={getResource("labelRegister")}
            disabled={!registerForm.isModified || !registerForm.isValid}
            onClick={handleRegister}
          />
        </div>
      </Paper>
    </div>
  );
};

export default RegisterForm;
