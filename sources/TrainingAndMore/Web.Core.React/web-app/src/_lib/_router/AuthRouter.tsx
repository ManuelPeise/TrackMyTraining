import React from "react";
import { IApp } from "../_interfaces/IApp";
import { BrowserRouter, Routes, Route } from "react-router-dom";
import LoginForm from "src/_views/Auth/LoginForm";
import RegisterForm from "src/_views/Auth/RegisterForm";
import AuthLayout from "./_Outlets/AuthLayout";
import ConfirmEmailForm from "src/_views/Auth/ConfirmEmailForm";
import AppLayout from "./_Outlets/AppOutlet";
import Sandbox from "src/_views/Sandbox/SandBox";

const AuthRouter: React.FC<IApp> = (props) => {
  return (
    <BrowserRouter>
      <Routes>
        {!props.authenticated ? (
          <Route path="/" caseSensitive element={<AuthLayout {...props} />}>
            <Route path="/" caseSensitive element={<LoginForm {...props} />} />
            <Route
              path="/register"
              caseSensitive
              element={<RegisterForm {...props} />}
            />
            <Route
              path="/confirm-email"
              caseSensitive
              element={<ConfirmEmailForm {...props} />}
            />
          </Route>
        ) : (
          <Route path="/" caseSensitive element={<AppLayout {...props} />}>
            <Route path="/" caseSensitive element={<Sandbox {...props} />} />
          </Route>
        )}
      </Routes>
    </BrowserRouter>
  );
};

export default AuthRouter;
