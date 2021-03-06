import { goalSelectReducer, defaultState } from "../GoalSelectorReducer";
import {
  SELECT_ACTION,
  GoalScrollAction,
  MOUSE_ACTION,
} from "../GoalSelectorAction";
import { GoalSelectorState } from "../../../../../types/goals";
import { StoreActions, StoreAction } from "../../../../../rootActions";

const VAL = 5;
const scrollAct: GoalScrollAction = {
  type: SELECT_ACTION,
  payload: VAL,
};
const scrollResultStore: GoalSelectorState = {
  ...defaultState,
  selectedIndex: VAL,
};
const mouseAct: GoalScrollAction = {
  type: MOUSE_ACTION,
  payload: VAL,
};
const mouseResultStore: GoalSelectorState = {
  ...defaultState,
  mouseX: VAL,
};

describe("Testing goal select reducer", () => {
  it("Should return defaultState", () => {
    expect(goalSelectReducer(undefined, scrollAct)).toEqual(defaultState);
  });

  it("Should return a state with an index of " + VAL, () => {
    expect(goalSelectReducer(defaultState, scrollAct)).toEqual(
      scrollResultStore
    );
  });

  it("Should return a state with an iX of " + VAL, () => {
    expect(goalSelectReducer(defaultState, mouseAct)).toEqual(mouseResultStore);
  });

  it("Should return the passed-in store", () => {
    expect(
      goalSelectReducer(scrollResultStore, ({
        type: "",
        payload: 0,
      } as unknown) as GoalScrollAction)
    ).toEqual(scrollResultStore);
  });

  it("Should return the default state", () => {
    const action: StoreAction = {
      type: StoreActions.RESET,
    };
    expect(goalSelectReducer(scrollResultStore, action)).toEqual(defaultState);
  });
});
