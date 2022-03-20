import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AboutUsPageComponent } from './pages/about-us-page/about-us-page.component';
import { CourierPageComponent } from './pages/courier-page/courier-page.component';
import { HowToOrderPageComponent } from './pages/how-to-order-page/how-to-order-page.component';
import { MainPageComponent } from './pages/main-page/main-page.component';
import { PartnerPageComponent } from './pages/partner-page/partner-page.component';
import { TermsOfServicePageComponent } from './pages/terms-of-service-page/terms-of-service-page.component';

const routes: Routes = [
  { path: '', component: MainPageComponent, pathMatch: 'full' },
  { path: 'partner', component: PartnerPageComponent, pathMatch: 'full' },
  { path: 'courier', component: CourierPageComponent, pathMatch: 'full' },
  { path: 'terms', component: TermsOfServicePageComponent, pathMatch: 'full' },
  { path: 'howToOrder', component: HowToOrderPageComponent, pathMatch: 'full' },
  { path: 'aboutUs', component: AboutUsPageComponent, pathMatch: 'full' },
  { path: '**', redirectTo: '' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
