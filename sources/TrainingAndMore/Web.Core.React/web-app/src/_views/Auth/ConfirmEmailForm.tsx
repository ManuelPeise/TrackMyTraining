import React from "react";
import { IApp } from "src/_lib/_interfaces/IApp";
import Paper from "src/_components/_containers/Paper";
import FormHeader from "./_components/FormHeader";
import { appColors } from "src/styles/colors";
import FormTextField from "src/_components/_inputs/FormTextField";
import { useForm } from "src/_hooks/useForm";
import { validationRules } from "src/_lib/validation";
import css from "./auth.module.css";
import AuthFormButton from "./_components/AuthFormButton";
import { useNavigate } from "react-router-dom";
import { EmailConfirmation } from "src/_lib/_interfaces/IAuthProps";

const ConfirmEmailForm: React.FC<IApp> = (props) => {
  const { getResource, onConfirmEmail } = props;
  const { emailConfrimationParams } = props;

  const navigate = useNavigate();

  const emailConfirmForm = useForm<EmailConfirmation>([
    {
      key: "email",
      value: "",
      validationPattern: validationRules.email,
    },
    {
      key: "userId",
      value: emailConfrimationParams?.userId,
    },
  ]);

  const handleConfirm = React.useCallback(async () => {
    const response = await onConfirmEmail(emailConfirmForm.getUpdatedModel());

    if (response?.email) {
      navigate("/");
    }
  }, [emailConfirmForm, onConfirmEmail, navigate]);

  return (
    <div className={css.wrapper}>
      <Paper cssClasses={`${css.formContainer} p-3`}>
        <div className="m-3 px-5 w-100">
          <FormHeader
            color={appColors.iconLightBlue}
            icon="bi-envelope-at"
            title={getResource("common.captionAccountActivation")}
            subTitle={getResource("common.labelConfirmEmail").replace(
              "{UserName}",
              emailConfrimationParams.firstName
            )}
          />
        </div>
        <div className="m-3 w-75">
          <FormTextField
            propertyKey={emailConfirmForm.formState["email"].key}
            type="text"
            value={emailConfirmForm.formState["email"].value}
            label={getResource("common.labelEmail")}
            required
            onChange={emailConfirmForm.updateField}
          />
        </div>
        <div className={`${css.buttonContainer}`}>
          <AuthFormButton
            label={getResource("labelConfirm")}
            onClick={handleConfirm}
            disabled={!emailConfirmForm.isValid}
          />
        </div>
      </Paper>
    </div>
  );
};

export default ConfirmEmailForm;
