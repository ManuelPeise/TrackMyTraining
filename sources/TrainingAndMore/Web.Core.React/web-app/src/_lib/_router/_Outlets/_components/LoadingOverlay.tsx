import React from "react";
import css from "./loadingOverlay.module.css";

const LoadingOverlay: React.FC = () => {
  return (
    <div className={css.loaderWrapper}>
      <div className={css.loader}></div>
      <div className={css.label}>Loading</div>
    </div>
  );
};

export default LoadingOverlay;
