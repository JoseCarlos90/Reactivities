import React from 'react'
import { Item, Button, Label, Segment } from 'semantic-ui-react'
import { IActivity } from '../../../app/models/activity'

interface IProps {
    activities: IActivity[];
    selectActivity: (id: string) => void;
    deleteActivity: (id: string) => void;
}

export const ActivityList: React.FC<IProps> = ({ activities, selectActivity, deleteActivity }) => {
    return (
        <Segment clearing>
            <Item.Group divided>
                {activities.map(activities => (
                    <Item key={activities.id}>
                        <Item.Content>
                            <Item.Header as='a'>{activities.title}</Item.Header>
                            <Item.Meta>{activities.date}</Item.Meta>
                            <Item.Description>
                                <div> {activities.description} </div>
                                <div> {activities.city}, {activities.venue} </div>
                            </Item.Description>
                            <Item.Extra>
                                <Button onClick={() => selectActivity(activities.id)}
                                    floated='right'
                                    content='View'
                                    color='blue' />

                                <Button onClick={() => deleteActivity(activities.id)}
                                    floated='right'
                                    content='Delete'
                                    color='red' />
                                <Label basic content={activities.category} />
                            </Item.Extra>
                        </Item.Content>
                    </Item>
                ))}
            </Item.Group>
        </Segment >
    )
}
