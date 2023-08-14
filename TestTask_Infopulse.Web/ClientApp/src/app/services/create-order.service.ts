import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { BehaviorSubject, InteropObservable, Observable, Subject } from 'rxjs';
import { ICreateOrder } from '../models/createOrder';
import { IOrder } from '../models/order';
import { ISelectListCustomer } from '../models/selectListCustomer';
import { IStatus } from '../models/status';

@Injectable({
  providedIn: 'root'
})
export class CreateOrderService {
  private _openForm = new BehaviorSubject<{ isOpen: boolean, number: number }>({ isOpen: false, number: 0 });
  openForm$ = this._openForm.asObservable();
  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }

  private _backToOrders = new Subject<void>();

  get backToOrders$() {
    return this._backToOrders.asObservable();
  }
  triggerBackToOrders() {
    this._backToOrders.next();
  }
  setOpenForm(value: boolean, nextNumber: number) {
    this._openForm.next({ isOpen: value, number: nextNumber });
  }
  getSelectListCustomers(): Observable<ISelectListCustomer[]> {
    return this.http.get<ISelectListCustomer[]>(this.baseUrl + 'select-customers')
  }
  getSelectListStatuses(): Observable<IStatus[]> {
    return this.http.get<IStatus[]>(this.baseUrl + 'statuses');
  }
  createOrder(orderData: ICreateOrder): Observable<ICreateOrder> {
    return this.http.post<ICreateOrder>(this.baseUrl + 'order', orderData);
  }
}
