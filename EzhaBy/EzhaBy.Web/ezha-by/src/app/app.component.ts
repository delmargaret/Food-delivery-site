import { Component, OnInit } from '@angular/core';
import { CateringFacilitiesService } from './services/catering-facilities.service';
import { AppState } from './state/app.state';
import { Store } from '@ngrx/store';
import { SetCateringFacilities } from './state/actions/app.actions';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent implements OnInit {
  title = 'ezha-by';
  isLoading: boolean = true;

  constructor(
    private cateringFacilitiesService: CateringFacilitiesService,
    private store: Store<AppState>
  ) {}

  ngOnInit(): void {
    this.getAllCateringFacilities();
  }

  getAllCateringFacilities() {
    this.cateringFacilitiesService.GetAllCateringFacilities().subscribe({
      next: (cateringFacilities) => {
        this.store.dispatch(
          new SetCateringFacilities({
            allCateringFacilities: cateringFacilities,
          })
        );
        this.isLoading = false;
      },
      error: (err) => console.log(err),
    });
  }
}
