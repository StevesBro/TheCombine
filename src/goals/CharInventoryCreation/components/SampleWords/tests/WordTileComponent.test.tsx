import React from "react";
import WordTileComponent from "../WordTileComponent";
import ReactDOM from "react-dom";

describe("Testing Word Tile Component", () => {
  it("Renders without crashing", () => {
    const div = document.createElement("div");
    ReactDOM.render(
      <WordTileComponent
        word={""}
        allCharacters={[]}
        addToCharSet={jest.fn()}
        addWordToIgnoreList={jest.fn()}
        hover={false}
        setHover={jest.fn()}
        unsetHover={jest.fn()}
      />,
      div
    );
    ReactDOM.unmountComponentAtNode(div);
  });
});
