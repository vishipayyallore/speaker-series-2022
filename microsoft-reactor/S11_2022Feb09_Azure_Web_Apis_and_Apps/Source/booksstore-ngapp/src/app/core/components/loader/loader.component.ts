import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'sv-loader',
  templateUrl: './loader.component.html',
  styleUrls: ['./loader.component.scss']
})
export class LoaderComponent implements OnInit {

  @Input()
  showSpinner!: boolean;

  constructor() { }

  ngOnInit(): void {
  }

}
