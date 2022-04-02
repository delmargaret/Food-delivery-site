import { CateringFacility } from '../cateringFacility';
import { OrderDish } from '../orderDish';

export interface OrderState {
  orderDishes: OrderDish[];
}
