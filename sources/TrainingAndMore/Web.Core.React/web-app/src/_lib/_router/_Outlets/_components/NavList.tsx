import React from "react";
import NavListItem from "./NavListItem";
import NavListItemCollapse from "./NavListItemCollapse";

interface IProps {
  isExpanded: boolean;
  className: string;
}

const NavList: React.FC<IProps> = (props) => {
  const { className, isExpanded } = props;

  return (
    <div className={className}>
      <NavListItem isExpanded={isExpanded} icon="bi-gear" label="Test" to="/" />
      <NavListItem
        isExpanded={isExpanded}
        icon="bi-gear"
        label="Test - 2"
        to="/"
      />
      <NavListItemCollapse
        isExpanded={isExpanded}
        icon="bi-gear"
        label="Test-collapse"
        items={[
          { id: "test", to: "/", lable: "mySubItem" },
          { id: "test", to: "/", lable: "mySubItem-s" },
        ]}
      />
    </div>
  );
};

export default NavList;
