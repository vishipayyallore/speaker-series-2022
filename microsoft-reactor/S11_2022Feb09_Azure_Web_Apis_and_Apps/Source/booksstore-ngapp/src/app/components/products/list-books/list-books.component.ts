import { Component, OnInit } from '@angular/core';

import { IBookDto } from '@products/interfaces/ibook.dto';
import { BooksService } from '@products/services/books.service';

@Component({
  selector: 'sv-list-books',
  templateUrl: './list-books.component.html',
  styleUrls: ['./list-books.component.scss']
})
export class ListBooksComponent implements OnInit {

  booksList: IBookDto[];

  constructor(private booksService: BooksService) {
    
    this.booksList = [];
  }

  ngOnInit(): void {

    this.retrieveAllBooks();
  }

  retrieveAllBooks() {

    this.booksService.GetAllBooks()
      .subscribe((data: IBookDto[]) => {

        this.booksList = data;
        console.log(this.booksList);
      });
  }

}
