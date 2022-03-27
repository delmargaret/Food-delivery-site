import { Action } from '@ngrx/store';
import { CateringFacilitiesState } from 'src/app/models/state/cateringFacilitiesState';
import { LoginState } from 'src/app/models/state/loginState';

export const ActionTypes = {
  SET_LOGIN_STATE: 'SET_LOGIN_STATE',
  SET_CATERING_FACILITIES: 'SET_CATERING_FACILITIES',
};

export interface PayloadAction extends Action {
  payload?: any;
}

export class SetLoginState implements PayloadAction {
  readonly type = ActionTypes.SET_LOGIN_STATE;
  constructor(public payload: LoginState) {}
}

export class SetCateringFacilities implements PayloadAction {
  readonly type = ActionTypes.SET_CATERING_FACILITIES;
  constructor(public payload: CateringFacilitiesState) {}
}
