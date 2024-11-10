export interface IAuthProps {
  inProgress: boolean;
  authenticated: boolean;
  emailConfrimationParams: EmailConfirmationParams;
  onLogin: (data: ILogin) => Promise<boolean>;
  onLogout: () => Promise<void>;
  onRegister: (data: IRegistration) => Promise<IRegistrationResult | null>;
  onConfirmEmail: (
    model: EmailConfirmation
  ) => Promise<AccountActivationResponse | null>;
}

export type JwtData = {
  token: string;
};
export interface ILogin {
  email: string;
  password: string;
  remember: boolean;
}

export interface IAuthData {
  token: string;
  authenticated: boolean;
}

export interface IRegistration {
  name: string;
  firstName: string;
  email: string;
  dateOfBirth: string;
  password: string;
  passwordReplication: string;
}

export interface IRegistrationResult {
  success: boolean;
  userId: string;
  firstName: string;
}

export type EmailConfirmation = {
  userId: number;
  email: string;
};

export type EmailConfirmationParams = {
  userId: string;
  firstName: string;
};

export type AccountActivationResponse = {
  email: string;
};
