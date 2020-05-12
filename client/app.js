import React, { useState } from "react";
import ReactDOM from "react-dom";
import './scss/index.scss';
import NavTab from './src/navTab/index'
import MainContainer from "./src/mainContainer";

import { Provider } from "react-redux";
import { Route, Switch } from "react-router-dom";
import Login from "./src/containers/Login";
import Logout from "./src/containers/Logout";
import Store from './src/store/Store';
import registerServiceWorker from './src/registerServiceWorker';
import { BrowserRouter as Router } from 'react-router-dom';
import TopNavigation from "./src/components/TopNavigation";
import Home from "./src/components/Home";


//App

const About = () => (
  <div><h1>About wwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwww</h1></div>
)

const NotFound = () => (
  <div><h1>NotFound</h1></div>
)

function App() {

  const [counter, setcounter] = useState(10);

    return (
      <>
        <div className="app-container" >
          <NavTab/>
          <MainContainer/>
          <div style={{paddingLeft: "7vw"}}>
            <TopNavigation />
            <main role="main" className="container-log" >
              <Switch >
                <Route exact path='/' component={Home} />
                <Route path='/about' component={About} />
                <Route path='/login' component={Login} />
                <Route path='/logout' component={Logout} />
                <Route component={NotFound} />
              </Switch>
            </main>
          </div>
        </div>

        </>
  )
}

ReactDOM.render(
  <Provider store={Store}>
    <Router>
      <App />
    </Router>
  </Provider>,
  document.getElementById('root')
);
registerServiceWorker();