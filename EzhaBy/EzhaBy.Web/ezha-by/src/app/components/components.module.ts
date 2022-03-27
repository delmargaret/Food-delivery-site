import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { NavBarComponent } from './nav-bar/nav-bar.component';
import { FooterComponent } from './footer/footer.component';
import { FormsModule } from '@angular/forms';
import { OffcanvasComponent } from './nav-bar/offcanvas/offcanvas.component';
import { LeavesWrapperComponent } from './leaves-wrapper/leaves-wrapper.component';
import { CafeCardComponent } from './cafe-card/cafe-card.component';

@NgModule({
  declarations: [
    NavBarComponent,
    FooterComponent,
    OffcanvasComponent,
    LeavesWrapperComponent,
    CafeCardComponent,
  ],
  imports: [CommonModule, FormsModule],
  exports: [
    NavBarComponent,
    FooterComponent,
    LeavesWrapperComponent,
    CafeCardComponent,
  ],
})
export class ComponentsModule {}
