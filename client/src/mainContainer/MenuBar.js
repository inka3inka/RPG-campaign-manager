import React from 'react';
import { HashRouter as Router , Route } from'react-router-dom';
import ContainerElement from './ContainerElement.js';
import HomePage from './HomePage';
import SideNav, { NavItem, NavIcon, NavText } from '@trendmicro/react-sidenav';
import '@trendmicro/react-sidenav/dist/react-sidenav.css';

export default function MenuBar() {
  return (
    <Router>
      <Route render={({ location, history }) => (
        <>
          <SideNav
            onSelect={(selected) => {
              const to = '/' + selected;
              if (location.pathname !== to) {
                history.push(to);
              }
            }}
          >
            <SideNav.Toggle />
            <SideNav.Nav defaultSelected="home">
              <NavItem eventKey="home">
                <NavIcon>
                  <i className="fa fa-fw fa-home" style={{ fontSize: '1.75em' }} />
                </NavIcon>
                <NavText>
                  Home
                </NavText>
              </NavItem>
              <NavItem eventKey="devices">
                <NavIcon>
                  <i className="fa fa-fw fa-device" style={{ fontSize: '1.75em' }} />
                </NavIcon>
                <NavText>
                  Devices
                </NavText>
              </NavItem>
            </SideNav.Nav>
          </SideNav>
          <main>
            <Route path="/" exact component={props => <HomePage />} />
            <Route path="/home" component={props => <ContainerElement />} />
            <Route path="/devices" component={props => <HomePage />} />
          </main>
        </>
      )}
      />
    </Router>
    )
}