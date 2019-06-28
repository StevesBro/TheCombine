import history from "../../history";
import React from "react";
import { Button } from "@material-ui/core";
import { Translate } from "react-localize-redux";

/**
 * A button that logs the user out by redirecting to the login page
 */
export class LogoutButton extends React.Component<{}, {}> {
  render() {
    return (
      <Button
        variant="contained"
        onClick={() => {
          history.push("/login");
        }}
      >
        <Translate id="login.logout" />
      </Button>
    );
  }
}
