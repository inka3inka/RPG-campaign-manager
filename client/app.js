import React from "react";
import ReactDOM from "react-dom";
import NavTab from './src/navTab/index'
import MainContainer from "./src/mainContainer";
import { Provider } from "react-redux";
import Store from './src/store/Store';
import './scss/index.scss';
import registerServiceWorker from './src/registerServiceWorker';


function App() {

    return (
      <div className="app-container" >
        <NavTab/>
        <MainContainer/>
      </div>
  )
}

ReactDOM.render(
  <Provider store={Store}>
    <App />
  </Provider>,
  document.getElementById('root')
);
registerServiceWorker();