import { IProduct } from "./product";

export interface IOrderedProduct {
  productId: number,
  product: IProduct,
  quantity: number
}
