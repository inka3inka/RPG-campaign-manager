import React from 'react';
import { HashRouter as Router , Route, Switch, Link} from'react-router-dom';
import ContainerElement from './ContainerElement';
import MenuBar from "./MenuBar";
import HomePage from "./HomePage";
import './styles.scss';

export default function MainContainer() {
  return (
    <div className="container">
      <MenuBar/>
      <Router>
        <Switch>
          <Route exact path='/' component={HomePage} />
          <Route path='/element' component={ContainerElement} />
        </Switch>
      </Router>
    </div>
  )
}