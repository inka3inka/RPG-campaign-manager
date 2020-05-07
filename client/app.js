import React, { useState } from "react";
import ReactDOM from "react-dom";
import './scss/index.scss';
import NavTab from './src/navTab/index'
import MainContainer from "./src/mainContainer";


//App

function App() {

  const [counter, setcounter] = useState(10);

    return (
      <div className="app-container">
        <NavTab/>
        <MainContainer/>
      </div>
  )
}

ReactDOM.render(
<App />,
  document.getElementById('app')
);