import { combineReducers, Reducer } from "redux";

import { StoreState } from "./types";
import { tempReducer } from "./components/Temp/TempReducer";
import { goalsReducer } from "./components/GoalTimeline/GoalTimelineReducers";
import { navReducer } from "./components/Navigation/NavigationReducer";
import { localizeReducer } from "react-localize-redux";
import { dragWordReducer } from "./goals/DraggableWord/reducer";
import { mergeDupStepReducer } from "./goals/MergeDupGoal/MergeDupStep/reducer";
import { loginReducer } from "./components/Login/LoginReducer";
import { createProjectReducer } from "./components/CreateProject/CreateProjectReducer";

export const rootReducer: Reducer<StoreState> = combineReducers<StoreState>({
  //handles localization through react-localize-redux utilities
  localize: localizeReducer,

  //intro windows
  loginState: loginReducer,
  createProjectState: createProjectReducer,

  //general cleanup tools
  goalsState: goalsReducer,
  navState: navReducer,

  //merge duplicates goal
  draggedWordState: dragWordReducer,
  mergeDupStepProps: mergeDupStepReducer,

  //temporary
  tempState: tempReducer
});
