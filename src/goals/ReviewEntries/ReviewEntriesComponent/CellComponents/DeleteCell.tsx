import React from "react";
import { Chip } from "@material-ui/core";
import { Delete } from "@material-ui/icons";

import AlignedList, { SPACER } from "./AlignedList";
import { ReviewEntriesWord } from "../ReviewEntriesTypes";

interface DeleteCellProps {
  rowData: ReviewEntriesWord;
  delete: (deleteIndex: string) => void;
}

class DeleteCell extends React.Component<DeleteCellProps> {
  render() {
    return (
      <AlignedList
        key={`delete:${this.props.rowData.id}`}
        listId={`delete${this.props.rowData.id}`}
        contents={this.props.rowData.senses.map((value) => (
          <React.Fragment>
            <Chip
              color={value.deleted ? "secondary" : "default"}
              label={<Delete />}
              onClick={() => {
                this.props.delete(value.senseId);
              }}
            />
          </React.Fragment>
        ))}
        bottomCell={SPACER}
      />
    );
  }
}

export default DeleteCell;
