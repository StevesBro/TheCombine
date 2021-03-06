import React from "react";
import {
  Translate,
  LocalizeContextProps,
  withLocalize,
} from "react-localize-redux";
import {
  Typography,
  CardContent,
  Card,
  List,
  ListItem,
} from "@material-ui/core";
import { Project } from "../../../types/project";
import { getAllProjectsByUser } from "../../../backend";
import history from "../../../history";
import { User } from "../../../types/user";

export interface ChooseProjectProps {
  setCurrentProject: (project: Project) => void;
}

interface ChooseProjectState {
  projectList: Project[];
}

class ChooseProject extends React.Component<
  ChooseProjectProps & LocalizeContextProps,
  ChooseProjectState
> {
  constructor(props: ChooseProjectProps & LocalizeContextProps) {
    super(props);
    this.state = { projectList: [] };
    let user: string | null = localStorage.getItem("user");
    if (user) {
      let userObject: User = JSON.parse(user);
      getAllProjectsByUser(userObject).then((projects) => {
        this.setState({ ...this.state, projectList: projects });
      });
    }
  }

  selectProject(project: Project) {
    this.props.setCurrentProject(project);
    history.push("/goals");
  }

  render() {
    return (
      <Card style={{ width: "100%", maxWidth: 450 }}>
        <CardContent>
          {/* Title */}
          <Typography variant="h5" align="center" gutterBottom>
            <Translate id="selectProject.title" />
          </Typography>

          {/* List of projects */}
          <List>
            {this.state.projectList.map((project) => (
              <ListItem
                key={project.id}
                button
                onClick={() => this.selectProject(project)}
              >
                <Typography variant="h6">{project.name}</Typography>
              </ListItem>
            ))}
          </List>
        </CardContent>
      </Card>
    );
  }
}

export default withLocalize(ChooseProject);
