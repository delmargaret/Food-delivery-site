import { OrderDish } from './orderDish';

export interface Order {
  id: string;
  name: string;
  surname: string;
  patronymic: string;
  phone: string;
  town: number;
  street: string;
  houseNumber: string;
  flatNumber: string;
  paymentType: number;
  comment: string;
  orderStatus: number;
  orderDateTime: string;
  isOrderAccepted: boolean;
  orderDishes: OrderDish[];
}
