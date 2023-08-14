import { HttpClient } from "@angular/common/http";
import { Inject, Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { IOrder } from "../models/order";

@Injectable({
  providedIn: 'root'
})

export class OrdersService {
  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }
  private orderNumber: number = 0;

  getLastOrderNumber(): number {
    return this.orderNumber;
  }
  setLastOrderNumber(orderNumber: number): void {
    this.orderNumber = orderNumber;
  }
  getAll(): Observable<IOrder[]> {
    return this.http.get<IOrder[]>(this.baseUrl + 'orders')
  }
}
