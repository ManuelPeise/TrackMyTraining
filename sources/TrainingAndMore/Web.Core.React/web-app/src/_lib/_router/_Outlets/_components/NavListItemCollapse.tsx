import React from "react";
import css from "./navListItemCollapse.module.css";
import { ISideMenuCollapseItem } from "../_interfaces/ISideMenuCollapseItem";

interface IProps {
  isExpanded: boolean;
  icon: string;
  label: string;
  items: ISideMenuCollapseItem[];
}

const NavListItemCollapse: React.FC<IProps> = (props) => {
  const { isExpanded, icon, label, items } = props;

  const [isCollapsed, setIsCollapsed] = React.useState<boolean>(false);

  const toggleCollapsed = React.useCallback(() => {
    setIsCollapsed(isCollapsed ? false : true);
  }, [isCollapsed]);

  React.useEffect(() => {
    if (isExpanded === false) {
      setIsCollapsed(false);
    }
  }, [isExpanded]);

  return (
    <li className={isExpanded ? css.navListItemExpanded : css.navListItem}>
      <div
        className={isExpanded ? css.navCollapseBtnExpanded : css.navCollapseBtn}
        onClick={toggleCollapsed}
      >
        <i className={`bi ${icon}`}></i>
        <span>{label}</span>
      </div>
      <div>
        {isExpanded && isCollapsed && (
          <ul className={`list-unstyled ${css.navCollapse}`}>
            {items?.length &&
              items.map((item) => {
                return (
                  <li
                    key={item.id}
                    id={item.id}
                    className={css.sidebarDropdownListItem}
                  >
                    <a href={item.to} className={css.navItemLink}>
                      <span>{item.lable}</span>
                    </a>
                  </li>
                );
              })}
          </ul>
        )}
      </div>
    </li>
  );
};

export default NavListItemCollapse;
