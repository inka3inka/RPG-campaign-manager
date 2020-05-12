import { combineReducers} from "redux";
import authReducer from '../reducers/authReducer';

const RootReducer = combineReducers({
  authReducer
});

export default RootReducer;