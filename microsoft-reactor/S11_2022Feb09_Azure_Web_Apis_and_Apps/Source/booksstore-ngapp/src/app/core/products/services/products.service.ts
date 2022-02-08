import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { retry, catchError } from 'rxjs/operators';

import { IProductDto } from '@products/interfaces/iproduct.dto';
import { environment } from '@environments/environment';

const baseUrl = environment.productsWebApiUrl;

const httpOptions = {
    headers: new HttpHeaders({
        'Content-Type': 'application/json',
    }),
};

@Injectable({
    providedIn: 'root'
})
export class ProductsService {

    constructor(private httpClient: HttpClient) {
    }

    // GET All Books
    getAllProducts(): Observable<IProductDto[]> {

        console.log(`Get All Products request received.`);

        return this.httpClient
            .get<IProductDto[]>(`${baseUrl}/products`)
            .pipe(retry(1), catchError(this.errorHandler));

    }

    // Error handling
    errorHandler(error: any) {

        let errorMessage = '';
        if (error.error instanceof ErrorEvent) {
            // Get client-side error
            errorMessage = error.error.message;
        } else {
            // Get server-side error
            errorMessage = `Error Code: ${error.status}\nMessage: ${error.message}`;
        }
        console.log(errorMessage);
        return throwError(errorMessage);
    }

}
