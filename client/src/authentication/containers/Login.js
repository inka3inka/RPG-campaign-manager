import React, { Component } from 'react';
import { GoogleLogin } from 'react-google-login';
import { connect } from "react-redux";
import { login } from "../actions/authActions";
import config from '../../../config.json';
import { withRouter, Redirect } from "react-router-dom";
import './styles.scss';


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
        });
      })
  };

  render() {
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
            /*onClick={(e) => e.target.classList.toggle("hidden")}*/
          Sign in
            <div className="log-element" >
            <GoogleLogin
              clientId={config.GOOGLE_CLIENT_ID}
              buttonText="Google Login"
              onSuccess={this.googleResponse}
              onFailure={this.googleResponse}
            />
            </div>
          </div>
        </div>
      );

    return (
      <div className="navtile" >
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