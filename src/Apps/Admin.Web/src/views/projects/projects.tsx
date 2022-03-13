import React, { useCallback, useState } from "react";
import {
  Project,
  useProjectActivateToggleMutation,
  useProjectsQuery,
} from "@entities/projects";
import { FullPageContainer } from "@shared/components/full-page-container";
import { Spin, List, Avatar, Switch, Tag, Button, Drawer } from "antd";
import css from "./projects.module.scss";
import cn from "classnames";
import { useBoolean } from "ahooks";
import { ProjectForm } from "@views/projects/project-form";
import { boxCss, mouseCss, spacingCss } from "@shared/styles";

const SwitchProject: React.FC<{ project: Project }> = ({ project }) => {
  const toggleProjectActivation = useProjectActivateToggleMutation(project.id);
  const onChange = useCallback(
    () => toggleProjectActivation.mutate(project.active),
    [toggleProjectActivation, project.active]
  );

  return (
    <Switch
      checked={project.active}
      size="small"
      onChange={onChange}
      loading={toggleProjectActivation.isLoading}
    />
  );
};

export const Projects: React.FC = () => {
  const projectsQuery = useProjectsQuery();

  const [formIsShowed, { setTrue: showForm, setFalse: hideForm }] =
    useBoolean(false);
  const [editing, setEditing] = useState<Project | undefined>();
  const toEdit = useCallback(
    (item?: Project) => {
      showForm();
      setEditing(item);
    },
    [showForm, setEditing]
  );
  const add = useCallback(() => toEdit(), [toEdit]);

  if (projectsQuery.isLoading) {
    return (
      <FullPageContainer
        className={cn(
          boxCss.flex,
          boxCss.alignItemsCenter,
          boxCss.justifyContentCenter,
          boxCss.overflowAuto
        )}
      >
        <Spin />
      </FullPageContainer>
    );
  }

  return (
    <FullPageContainer
      className={cn(boxCss.flex, boxCss.overflowAuto, boxCss.flexColumn)}
    >
      <div
        className={cn(
          boxCss.flex,
          boxCss.justifyContentSpaceBetween,
          spacingCss.marginBottomSm
        )}
      >
        <Button type="primary" onClick={add}>
          Add
        </Button>
      </div>
      <List
        itemLayout="horizontal"
        className={cn(css.list, boxCss.fullWidth)}
        dataSource={projectsQuery.data!}
        renderItem={(item) => (
          <List.Item
            className={spacingCss.paddingHorizSm}
            actions={[<SwitchProject project={item} />]}
          >
            <div
              className={cn(
                boxCss.flex,
                boxCss.alignItemsCenter,
                mouseCss.pointer,
                spacingCss.marginLeftSm
              )}
              onClick={() => toEdit(item)}
            >
              <Avatar src={item.info.image} />
              <span className={spacingCss.marginLeftSm}>{item.info.name}</span>
            </div>

            <div className={spacingCss.marginLeftAuto}>
              {item.languages.map((x) => (
                <Tag key={x} color="blue">
                  {x.toUpperCase()}
                </Tag>
              ))}
            </div>
          </List.Item>
        )}
      />
      <Drawer visible={formIsShowed} onClose={hideForm} width="500px">
        {formIsShowed && <ProjectForm project={editing} onClose={hideForm} />}
      </Drawer>
    </FullPageContainer>
  );
};
