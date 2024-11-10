import React from "react";
import { appColors } from "src/styles/colors";
import css from "./formHeader.module.css";

interface IProps {
  icon: string;
  title: string;
  subTitle?: string;
  color: string;
  iconSizeRem?: number;
}

const FormHeader: React.FC<IProps> = (props) => {
  const { icon, title, subTitle, color, iconSizeRem } = props;

  return (
    <div className={`${css.formHeader}`}>
      <div>
        <i
          className={`bi ${icon} ${css.headerIcon}`}
          style={{ color: color, fontSize: `${iconSizeRem ?? 3}rem` }}
        ></i>
      </div>
      <div className={`${css.labelContainer}`}>
        <label
          className={`${css.headerTitel}`}
          style={{
            color: appColors.lightblue,
          }}
        >
          {title}
        </label>
        <label
          className={`${css.headerSubTitel}`}
          style={{
            color: appColors.labelgray,
          }}
        >
          {subTitle}
        </label>
      </div>
    </div>
  );
};

export default FormHeader;
