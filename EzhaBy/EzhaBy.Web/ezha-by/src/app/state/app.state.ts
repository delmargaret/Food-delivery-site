import { CateringFacilitiesState } from '../models/state/cateringFacilitiesState';
import { LoginState } from '../models/state/loginState';

export interface AppState {
  loginState: LoginState;
  cateringFacilitiesState: CateringFacilitiesState;
}
