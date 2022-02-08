import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'sv-error-alert',
  templateUrl: './error-alert.component.html',
  styleUrls: ['./error-alert.component.scss']
})
export class ErrorAlertComponent implements OnInit {

  @Input()
  errorMessage!:string;

  constructor() { }

  ngOnInit(): void {
  }

}
