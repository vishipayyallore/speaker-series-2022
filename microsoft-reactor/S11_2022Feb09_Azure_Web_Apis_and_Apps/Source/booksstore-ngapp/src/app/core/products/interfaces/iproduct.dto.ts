import { Guid } from 'guid-typescript';

export interface IProductDto {
    id: Guid;

    productId: number;

    productName: string;

    productCode: string;

    releaseDate: string;

    price: number;

    description: string;

    starRating: number;

    imageUrl: string;
}
