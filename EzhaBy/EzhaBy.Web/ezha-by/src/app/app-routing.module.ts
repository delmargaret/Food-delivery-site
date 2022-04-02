import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AboutUsPageComponent } from './pages/about-us-page/about-us-page.component';
import { CateringFacilitiesPageComponent } from './pages/catering-facilities-page/catering-facilities-page.component';
import { CateringFacilityPageComponent } from './pages/catering-facility-page/catering-facility-page.component';
import { ContactUsPageComponent } from './pages/contact-us-page/contact-us-page.component';
import { CourierPageComponent } from './pages/courier-page/courier-page.component';
import { HowToOrderPageComponent } from './pages/how-to-order-page/how-to-order-page.component';
import { MainPageComponent } from './pages/main-page/main-page.component';
import { PartnerPageComponent } from './pages/partner-page/partner-page.component';
import { ShoppingCartPageComponent } from './pages/shopping-cart-page/shopping-cart-page.component';
import { TermsOfServicePageComponent } from './pages/terms-of-service-page/terms-of-service-page.component';

const routes: Routes = [
  { path: '', component: MainPageComponent, pathMatch: 'full' },
  { path: 'partner', component: PartnerPageComponent, pathMatch: 'full' },
  { path: 'courier', component: CourierPageComponent, pathMatch: 'full' },
  { path: 'terms', component: TermsOfServicePageComponent, pathMatch: 'full' },
  {
    path: 'how-to-order',
    component: HowToOrderPageComponent,
    pathMatch: 'full',
  },
  { path: 'about-us', component: AboutUsPageComponent, pathMatch: 'full' },
  { path: 'contact-us', component: ContactUsPageComponent, pathMatch: 'full' },
  {
    path: 'cafes',
    component: CateringFacilitiesPageComponent,
    pathMatch: 'full',
  },
  {
    path: 'cafe',
    component: CateringFacilityPageComponent,
    pathMatch: 'full',
  },
  {
    path: 'cart',
    component: ShoppingCartPageComponent,
    pathMatch: 'full',
  },
  { path: '**', redirectTo: '' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
