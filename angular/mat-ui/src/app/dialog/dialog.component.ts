import { Component, Inject, OnInit } from '@angular/core';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Dialog2Component } from '../dialog2/dialog2.component';

@Component({
  selector: 'app-dialog',
  templateUrl: './dialog.component.html',
  styleUrls: ['./dialog.component.css']
})
export class DialogComponent implements OnInit {

  animal: string;
  name: string;

  constructor(public dialog: MatDialog) { }

  ngOnInit() {
  }

  openDialog() {
      const dialogRef = this.dialog.open(Dialog2Component, {
          width: '250px',
          data: {name: this.name, animal: this.animal}
      });

      dialogRef.afterClosed().subscribe(result => {
        this.animal = result;
      });
  }

}
