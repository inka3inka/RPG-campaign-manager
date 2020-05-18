import React, { Component } from 'react';
import { GoogleLogin } from 'react-google-login';
import { connect } from "react-redux";
import { login } from "../actions/authActions";
import { loginAuthOptions } from "../actions/loginAuthOptions";
import config from '../../../config.json';
import { withRouter, Redirect } from "react-router-dom";
import './styles.scss';
import Store from '../../store/Store'

class Login extends Component {

  onFailure = (error) => {
    alert(error);
  };

  googleResponse = (response) => {
    if (!response.tokenId) {
      console.error("Unable to get tokenId from Google", response)
      return;
    }

    const tokenBlob = new Blob([JSON.stringify({ tokenId: response.tokenId }, null, 2)], { type: 'application/json' });
    const options = {
      method: 'POST',
      body: tokenBlob,
      mode: 'cors',
      cache: 'default'
    };

    fetch(config.GOOGLE_AUTH_CALLBACK_URL, options)
      .then(r => {
        r.json().then(user => {
          const token = user.token;
          console.log(token);
          this.props.login(token);
          console.log(Store.getState().auth.user)
        });
      }).

    fetch(config.GOOGLE_USER_INFO_URL, {
      method: 'GET',
      // headers: `Bearer ${Store.getState().auth.user}`
    })
      .then(response => console.log(response))
  };

  render() {

    let showButton = !!this.props.auth.isVisible ?
      (
        <GoogleLogin
          clientId={config.GOOGLE_CLIENT_ID}
          buttonText="Google Login"
          onSuccess={this.googleResponse}
          onFailure={this.googleResponse}
        />
      ) :
      null;

    let content = !!this.props.auth.isAuthenticated ?
      (
        <div>
          <Redirect to={{
            pathname: '/'
          }} />
        </div>
      ) :
      (
        <div>
          <div >

          Sign in
            <div className="log-element" >
              {showButton}
            </div>
          </div>
        </div>
      );

    const showState = () => console.log(Store.getState())

    return (
      <div className="navtile" >
        {showState()}
        {content}
      </div>
    );
  }
};

const mapStateToProps = (state) => {
  return {
    auth: state.auth
  };
};

const mapDispatchToProps = (dispatch) => {
  return {
    login: (token) => {
      dispatch(login(token));
    }
  }
};

export default withRouter(connect(mapStateToProps, mapDispatchToProps)(Login));