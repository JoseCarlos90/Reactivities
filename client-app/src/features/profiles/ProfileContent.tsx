import React from 'react'
import {Tab} from 'semantic-ui-react';
import ProfilePhotos from './ProfilePhotos';
import { observer } from 'mobx-react-lite';
import ProfileFollowings from './ProfileFollowings';

interface IProps{
    setActiveTab: (activiveIndex : any) => void;
}

const panes = [
    {menuItem: 'About', render: () => <Tab.Pane>About content</Tab.Pane>},
    {menuItem: 'Photos', render: () => <ProfilePhotos />},
    {menuItem: 'Activities', render: () => <Tab.Pane>Activities content</Tab.Pane>},
    {menuItem: 'Followers', render: () => <ProfileFollowings />},
    {menuItem: 'Following', render: () => <ProfileFollowings />},
]

const ProfileContent: React.FC<IProps> = ({setActiveTab}) => {
    return (
        <Tab 
            menu={{fluid: true, vertical: true}}
            menuPosition='right'
            panes={panes}
            onTabChange={(e, data) => setActiveTab(data.activeIndex)}
            //activeIndex={1}
        />
    )
}

export default observer(ProfileContent);
