import React from 'react';
import NavTile from './navTile';
import './styles.scss';
import {HashRouter as Router, Route, Switch} from 'react-router-dom';
import Login from "../authentication/containers/Login";
import Logout from "../authentication/containers/Logout";

export default function NavTab() {

  return (
    <Router>
      <div className="navtab">
        <div className="nav-bar">
          <NavTile name="Logo" />
          <NavTile name="Projects" />
          <NavTile name="Groups" />
          <NavTile name="More" />
        </div>
        <div className="nav-bar" >
          <Login />
          <Logout />
        </div>
      </div>
    </Router>
  )
}