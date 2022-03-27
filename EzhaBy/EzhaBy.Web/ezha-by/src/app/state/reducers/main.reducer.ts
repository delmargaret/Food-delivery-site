import { loginStateReducer } from '../reducers/login-state.reducer';
import { cateringFacilitiesStateReducer } from '../reducers/catering-facilities.reducer';

export const mainReducer = {
  loginState: loginStateReducer,
  cateringFacilitiesState: cateringFacilitiesStateReducer,
};
