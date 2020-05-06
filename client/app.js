import React, { useState } from "react";
import ReactDOM from "react-dom";
import './scss/index.scss';
import NavTab from './src/navTab/index'


//App

function App() {

  const [counter, setcounter] = useState(10);

    return (
      <div>
        <NavTab/>
      </div>
  )
}

ReactDOM.render(
<App />,
  document.getElementById('app')
);