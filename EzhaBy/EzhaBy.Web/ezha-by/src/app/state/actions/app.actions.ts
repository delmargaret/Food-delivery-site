import { Action } from '@ngrx/store';
import { CateringFacilitiesState } from 'src/app/models/state/cateringFacilitiesState';
import { LoginState } from 'src/app/models/state/loginState';
import { OrderState } from 'src/app/models/state/orderState';

export const ActionTypes = {
  SET_LOGIN_STATE: 'SET_LOGIN_STATE',
  SET_CATERING_FACILITIES: 'SET_CATERING_FACILITIES',
  SET_ORDER_DISHES: 'SET_ORDER_DISHES',
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

export class SetOrderDishes implements PayloadAction {
  readonly type = ActionTypes.SET_ORDER_DISHES;
  constructor(public payload: OrderState) {}
}
