import React from "react";
import { Goal, GoalData, Tools, GoalOption } from "../types/goals";
import { User } from "./user";
import BaseGoalScreen from "../goals/DefaultGoal/BaseGoalScreen/BaseGoalScreen";
import BaseGoalSelect from "../goals/DefaultGoal/BaseGoalSelect/BaseGoalSelect";

export class BaseGoal implements Goal {
  id: number;
  name: string;
  user: User;

  display: JSX.Element;
  goalWidget: JSX.Element;

  steps: JSX.Element[];
  curNdx: number;
  data: GoalData; // The data required to load/reload this exact goal

  tool: Tools;
  completed: boolean;
  result: GoalOption;

  constructor() {
    this.id = -1;
    this.name = "";
    this.user = {
      name: "",
      username: "",
      id: -1
    };
    this.display = <BaseGoalScreen goal={this} />;
    this.goalWidget = <BaseGoalSelect goal={this} />;
    this.steps = [];
    this.curNdx = 0;
    this.data = {};
    this.tool = Tools.TempTool;
    this.completed = false;
    this.result = GoalOption.Current;
  }
}
