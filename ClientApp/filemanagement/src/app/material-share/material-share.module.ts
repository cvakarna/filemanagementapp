import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

//material
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatTableModule } from '@angular/material/table';
import { MatTooltipModule } from '@angular/material/tooltip';


@NgModule({
  declarations: [],
  imports: [

  ],
  exports: [
    MatToolbarModule,
    MatButtonModule,
    MatIconModule,
    MatCardModule,
    MatProgressBarModule,
    MatFormFieldModule,
    MatInputModule,
    MatTableModule,
    MatTooltipModule

  ]
})
export class MaterialShareModule { }
