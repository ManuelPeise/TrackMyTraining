export const validationRules = {
  email: "^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+.[a-zA-Z]{2,}$",
  password: "^.{8,}$",
  alphaNumeric: "^[a-zA-Z0-9]{replaceValue}$",
  valueLength: "^.{replaceValue,}$",
  date: "^[0-3]?[0-9].[0-3]?[0-9].(?:[0-9]{2})?[0-9]{2}$",
};
