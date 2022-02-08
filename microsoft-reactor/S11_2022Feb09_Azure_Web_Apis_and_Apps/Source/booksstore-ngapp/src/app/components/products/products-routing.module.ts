import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { ListBooksComponent } from './list-books/list-books.component';
import { ListProductsComponent } from './list-products/list-products.component';

const routes: Routes = [
  { path: 'list-products', component: ListProductsComponent },
  { path: 'list-books', component: ListBooksComponent },
];

@NgModule({
  exports: [
    RouterModule
  ],
  imports: [
    RouterModule.forChild(routes)
  ]
})
export class ProductsRoutingModule { }
