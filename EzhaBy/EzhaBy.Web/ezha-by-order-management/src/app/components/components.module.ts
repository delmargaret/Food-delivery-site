import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { NavBarComponent } from './nav-bar/nav-bar.component';
import { FooterComponent } from './footer/footer.component';
import { CafePageComponent } from './cafe-page/cafe-page.component';
import { CourierPageComponent } from './courier-page/courier-page.component';
import { MatButtonToggleModule } from '@angular/material/button-toggle';

@NgModule({
  declarations: [
    NavBarComponent,
    FooterComponent,
    CafePageComponent,
    CourierPageComponent,
  ],
  imports: [CommonModule, MatButtonToggleModule],
  exports: [
    NavBarComponent,
    FooterComponent,
    CafePageComponent,
    CourierPageComponent,
  ],
})
export class ComponentsModule {}
