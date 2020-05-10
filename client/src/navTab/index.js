import React from 'react';
import ReactDOM from 'react-dom';
import NavTile from './navTile';
import RightBar from './rightBar';
import './styles.scss';

export default function NavTab() {
  return (
    <div className="navtab">
      <div className="nav-bar">
        <NavTile name="Logo" />
        <NavTile name="Projects" />
        <NavTile name="Groups" />
        <NavTile name="More" />
      </div>
      <div className="nav-bar">
        <RightBar name="Sign in" />
        <RightBar name="Sign out" />
      </div>
    </div>
  )
}