import { Component, OnDestroy, OnInit } from '@angular/core';
import { OrderDish } from 'src/app/models/orderDish';
import { AppState } from 'src/app/state/app.state';
import { Store } from '@ngrx/store';
import { CateringFacilitiesState } from 'src/app/models/state/cateringFacilitiesState';
import { Subject, takeUntil } from 'rxjs';
import { CateringFacility } from 'src/app/models/cateringFacility';
import { SetOrderDishes } from 'src/app/state/actions/app.actions';
import { OrderState } from 'src/app/models/state/orderState';

interface GroupedOrderDish {
  cafe: CateringFacility;
  dishes: OrderDish[];
}

@Component({
  selector: 'app-shopping-cart-page',
  templateUrl: './shopping-cart-page.component.html',
  styleUrls: ['./shopping-cart-page.component.scss'],
})
export class ShoppingCartPageComponent implements OnInit, OnDestroy {
  private $unsubscribe: Subject<void> = new Subject<void>();
  orderDishes: OrderDish[] = [];
  cafes: CateringFacility[] = [];
  groupedOrderDishes: GroupedOrderDish[] = [];

  constructor(private store: Store<AppState>) {}

  get price() {
    let price = 0;
    this.groupedOrderDishes.forEach((group) => {
      group.dishes.forEach((x) => (price = price + x.dish.price));
    });
    return price;
  }

  get delivery() {
    let delivery = 0;
    this.groupedOrderDishes.forEach(
      (group) => (delivery = delivery + group.cafe.deliveryPrice)
    );
    return delivery;
  }

  ngOnInit(): void {
    this.store
      .select<OrderState>((state) => state.orderState)
      .pipe(takeUntil(this.$unsubscribe))
      .subscribe((orderState) => {
        this.orderDishes = orderState.orderDishes;
        this.groupByCafeId(this.cafes);
      });

    this.store
      .select<CateringFacilitiesState>((state) => state.cateringFacilitiesState)
      .pipe(takeUntil(this.$unsubscribe))
      .subscribe((cateringFacilitiesState) => {
        this.cafes = cateringFacilitiesState.allCateringFacilities;
        this.groupByCafeId(cateringFacilitiesState.allCateringFacilities);
      });
  }

  groupByCafeId(cateringFacilities: CateringFacility[]) {
    let result: GroupedOrderDish[] = [];
    this.orderDishes.forEach((dish) => {
      const cafe = cateringFacilities.find(
        (x) => x.id === dish.cateringFacilityId
      );
      if (cafe) {
        const group = result.find((x) => x.cafe.id === cafe?.id) ?? null;
        if (!group) {
          result.push({ cafe: cafe, dishes: ([] as OrderDish[]).concat(dish) });
          return;
        }

        group.dishes.push(dish);
      }
    });
    result.forEach((r) =>
      r.dishes.sort((a, b) => a.dish.dishName.localeCompare(b.dish.dishName))
    );
    this.groupedOrderDishes = [...result];
  }

  deleteDish(dish: OrderDish) {
    const dishes = [...this.orderDishes];
    this.store.dispatch(
      new SetOrderDishes({
        orderDishes: [...dishes.filter((x) => x.dish.id !== dish?.dish.id)],
      })
    );
  }

  minusDish(dish: OrderDish) {
    const dishes = [...this.orderDishes];
    if (dish) {
      const number = dish.numberOfDishes - 1;

      if (number === 0) {
        this.store.dispatch(
          new SetOrderDishes({
            orderDishes: [...dishes.filter((x) => x.dish.id !== dish?.dish.id)],
          })
        );
        return;
      }

      const newDish: OrderDish = {
        dish: dish.dish,
        numberOfDishes: number,
        cateringFacilityId: dish.cateringFacilityId,
      };
      this.store.dispatch(
        new SetOrderDishes({
          orderDishes: [
            ...dishes
              .filter((x) => x.dish.id !== dish.dish?.id)
              .concat(newDish),
          ],
        })
      );
    }
  }

  plusDish(dish: OrderDish) {
    const dishes = [...this.orderDishes];
    if (dish) {
      const number = dish.numberOfDishes + 1;
      const newDish: OrderDish = {
        dish: dish.dish,
        numberOfDishes: number,
        cateringFacilityId: dish.cateringFacilityId,
      };
      this.store.dispatch(
        new SetOrderDishes({
          orderDishes: [
            ...dishes
              .filter((x) => x.dish.id !== dish.dish?.id)
              .concat(newDish),
          ],
        })
      );
    }
  }

  ngOnDestroy(): void {
    this.$unsubscribe.next();
    this.$unsubscribe.complete();
  }
}
