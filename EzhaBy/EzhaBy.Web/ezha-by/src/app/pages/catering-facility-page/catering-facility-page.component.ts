import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CateringFacility } from 'src/app/models/cateringFacility';
import { AppState } from 'src/app/state/app.state';
import { Store } from '@ngrx/store';
import { CateringFacilitiesState } from 'src/app/models/state/cateringFacilitiesState';
import { of, Subject, switchMap, takeUntil, zip } from 'rxjs';
import { Dish } from 'src/app/models/dish';
import { CateringFacilitiesService } from 'src/app/services/catering-facilities.service';
import TownsDict from 'src/app/models/townsDict';
import { CateringFacilityCategory } from 'src/app/models/cateringFacilityCategory';

@Component({
  selector: 'app-catering-facility-page',
  templateUrl: './catering-facility-page.component.html',
  styleUrls: ['./catering-facility-page.component.scss'],
})
export class CateringFacilityPageComponent implements OnInit {
  private $unsubscribe: Subject<void> = new Subject<void>();
  cateringFacility: CateringFacility | null = null;
  dishes: Dish[] = [];
  listIsLoaded: boolean = false;
  towns = TownsDict;
  isLoading: boolean = true;

  constructor(
    private store: Store<AppState>,
    private route: ActivatedRoute,
    private cateringFacilitiesService: CateringFacilitiesService
  ) {}

  ngOnInit(): void {
    this.store
      .select<CateringFacilitiesState>((state) => state.cateringFacilitiesState)
      .pipe(
        takeUntil(this.$unsubscribe),
        switchMap((cafesState) => {
          return zip(of(cafesState), this.route.queryParams);
        })
      )
      .subscribe(([cateringFacilitiesState, params]) => {
        const id = params['id'];
        this.cateringFacility =
          cateringFacilitiesState.allCateringFacilities.find(
            (x) => x.id === id
          ) ?? null;

        if (!this.listIsLoaded && this.cateringFacility) {
          this.getDishes(id);
        }
      });
  }

  get mainPicture() {
    return this.cateringFacility?.cateringFacilityIconUrl
      ? `url('${this.cateringFacility?.cateringFacilityIconUrl}')`
      : `linear-gradient(0deg, rgba(0, 0, 0, 0.4), rgb(212 212 212)), url('../../../assets/19.png')`;
  }

  get allCategories() {
    let categories: CateringFacilityCategory[] = [];

    this.dishes
      .map((x) => x.cateringFacilityCategory)
      .forEach((c) => {
        if (!categories.some((x) => x.categoryId === c.categoryId)) {
          categories.push(c);
        }
      });

    return categories;
  }

  getDishes(cafeId: string) {
    this.cateringFacilitiesService.GetAllDishes(cafeId).subscribe({
      next: (dishes) => {
        this.dishes = dishes;
        this.isLoading = false;
      },
      error: (err) => {
        console.log(err);
        this.dishes = [];
        this.isLoading = false;
      },
    });
  }
}
