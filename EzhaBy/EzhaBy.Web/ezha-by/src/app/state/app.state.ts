import { CateringFacilitiesState } from '../models/state/cateringFacilitiesState';
import { LoginState } from '../models/state/loginState';
import { OrderState } from '../models/state/orderState';

export interface AppState {
  loginState: LoginState;
  cateringFacilitiesState: CateringFacilitiesState;
  orderState: OrderState;
}
