import React, {Component} from "react";
import ReactDOM from "react-dom";
import './scss/index.scss';


//App

class App extends Component{
  render() {
    return (
      <div>
      Hello
      </div>
  )
  }
}

ReactDOM.render(
<App />,
  document.getElementById('app')
);