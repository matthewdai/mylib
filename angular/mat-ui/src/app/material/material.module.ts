import { NgModule } from '@angular/core';
import { 
  MatToolbarModule, 
  MatIconModule, 
  MatSidenavModule, 
  MatListModule, 
  MatButtonModule, 
  MatButtonToggleModule,
  MatMenuModule,
  MatDividerModule,
  MatExpansionModule,
  MatCardModule,
  MatTabsModule,
  MatFormFieldModule,
  MatInputModule

 } from '@angular/material';

 import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';

 import {MatGridListModule} from '@angular/material/grid-list';

import { MatBadgeModule } from '@angular/material/badge';

import { MatDialogModule } from '@angular/material/dialog';

const MaterialComponents = [
  MatToolbarModule, 
  MatIconModule, 
  MatSidenavModule, 
  MatListModule, 
  MatButtonModule, 
  MatButtonToggleModule,
  MatBadgeModule,
  MatProgressSpinnerModule,
  MatMenuModule,
  MatDividerModule,
  MatGridListModule,
  MatExpansionModule,
  MatCardModule,
  MatTabsModule,
  MatFormFieldModule,
  MatInputModule,
  MatDialogModule
];

@NgModule({
  imports: [MaterialComponents],
  exports: [MaterialComponents]
})

export class MaterialModule { }
