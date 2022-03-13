import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { NavBarComponent } from './nav-bar/nav-bar.component';
import { FooterComponent } from './footer/footer.component';
import { FormsModule } from '@angular/forms';

@NgModule({
  declarations: [NavBarComponent, FooterComponent],
  imports: [CommonModule, FormsModule],
  exports: [NavBarComponent, FooterComponent],
})
export class ComponentsModule {}
