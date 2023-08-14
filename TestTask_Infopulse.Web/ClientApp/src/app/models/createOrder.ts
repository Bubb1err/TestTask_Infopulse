import { IOrderedProduct } from "./orderedProduct";

export interface ICreateOrder {
  orderNumber: number,
  status: number,
  comment: string,
  customerId: number,
  orderedProducts: IOrderedProduct[]
}
