import React from "react";
import { IApp } from "../_interfaces/IApp";
import { BrowserRouter, Routes, Route } from "react-router-dom";
import Sandbox from "src/_views/Sandbox/SandBox";

const AppRouter: React.FC<IApp> = (props) => {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<Sandbox {...props} />} />
      </Routes>
    </BrowserRouter>
  );
};

export default AppRouter;
