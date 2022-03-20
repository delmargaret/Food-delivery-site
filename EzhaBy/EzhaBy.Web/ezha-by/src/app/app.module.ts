import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ComponentsModule } from './components/components.module';
import { AuthModule } from './security/auth.module';
import { StoreModule } from '@ngrx/store';
import { mainReducer } from './state/reducers/main.reducer';
import { HttpClientModule } from '@angular/common/http';
import { MainPageComponent } from './pages/main-page/main-page.component';
import { PartnerPageComponent } from './pages/partner-page/partner-page.component';
import { ReactiveFormsModule } from '@angular/forms';
import { RequestsService } from './services/requests.service';
import { CourierPageComponent } from './pages/courier-page/courier-page.component';
import { TermsOfServicePageComponent } from './pages/terms-of-service-page/terms-of-service-page.component';
import { HowToOrderPageComponent } from './pages/how-to-order-page/how-to-order-page.component';
import { AboutUsPageComponent } from './pages/about-us-page/about-us-page.component';

@NgModule({
  declarations: [AppComponent, MainPageComponent, PartnerPageComponent, CourierPageComponent, TermsOfServicePageComponent, HowToOrderPageComponent, AboutUsPageComponent],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ComponentsModule,
    AuthModule,
    HttpClientModule,
    ReactiveFormsModule,
    StoreModule.forRoot(mainReducer),
  ],
  providers: [RequestsService],
  bootstrap: [AppComponent],
})
export class AppModule {}
