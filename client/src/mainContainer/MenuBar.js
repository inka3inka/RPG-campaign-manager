import React from 'react';
import { HashRouter as Router , Route } from'react-router-dom';
import ContainerElement from './ContainerElement.js';
import HomePage from './HomePage';
import SideNav, { NavItem, NavIcon, NavText, Toggle, Nav } from '@trendmicro/react-sidenav';
import '@trendmicro/react-sidenav/dist/react-sidenav.css';
import '../../scss/_variables.scss';

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
            style={{backgroundColor: "#7d7d7d", paddingTop: "2vw", boxShadow: "0 0 4px #4b4b4b", position: "fixed"}}
          >
            <SideNav.Toggle />
            <SideNav.Nav defaultSelected="home">
              <NavItem eventKey="home">
                <NavIcon>
                  <i className="fa fa-fw fa-home" style={{ fontSize: '1.5vw' }} />
                </NavIcon>
                <NavText>
                  Home
                </NavText>
              </NavItem>
              <NavItem eventKey="local-vault">
                <NavIcon>
                  <i className="far fa-play-circle" style={{ fontSize: '1.5vw' }} />
                </NavIcon>
                <NavText>
                  Local Vault
                </NavText>
              </NavItem>
              <NavItem eventKey="plugins">
                <NavIcon>
                  <i className="fas fa-plug" style={{ fontSize: '1.5vw' }} />
                </NavIcon>
                <NavText>
                  My Plugins
                </NavText>
              </NavItem>
            </SideNav.Nav>
          </SideNav>
          <main>
            <Route path="/" exact component={props => <HomePage />} />
            <Route path="/home" component={props => <ContainerElement />} />
            <Route path="/local-vault" component={props => <HomePage />} />
            <Route path="/plugins" component={props => <ContainerElement />} />
          </main>
        </>
      )}
      />
    </Router>
    )
}