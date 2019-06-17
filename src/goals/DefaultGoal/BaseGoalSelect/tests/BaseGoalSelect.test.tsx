import React from "react";
import ReactDOM from "react-dom";
import { User } from "../../../../types/user";
import { Goal } from "../../../../types/goals";
import BaseGoalSelect from "../BaseGoalSelect";
import { CreateCharInv } from "../../../CreateCharInv/CreateCharInv";

it("renders without crashing", () => {
  const div = document.createElement("div");
  let user: User = { name: "TestUser", username: "TestUsername", id: 0 };
  let goal: Goal = new CreateCharInv([]);
  goal.user = user;
  ReactDOM.render(<BaseGoalSelect goal={goal} />, div);
  ReactDOM.unmountComponentAtNode(div);
});
