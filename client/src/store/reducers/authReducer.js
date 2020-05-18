const authReducer = (state = {
  user: '',
  isAuthenticated: false,
  isVisible: true
}, action) => {
  switch (action.type) {
    case "LOGIN":
      state = { ...state, user: action.payload, isAuthenticated: true };
      break;
    case "LOGOUT":
      state = { ...state, user: '', isAuthenticated: false };
      break;
    case "LOGIN_AUTH_OPTIONS":
      isVisible ? state = {...state, isVisible: true} : state = {...state, isVisible: false};
      break;
    default:
      break;
  };
  return state;
};

export default authReducer;