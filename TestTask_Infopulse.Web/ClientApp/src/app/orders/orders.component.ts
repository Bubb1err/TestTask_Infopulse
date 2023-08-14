import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { IOrder } from '../models/order';
import { OrdersService } from '../services/orders.service';
import { CreateOrderService } from '../services/create-order.service';

@Component({
  selector: 'orders',
  templateUrl: './orders.component.html',
  styleUrls: ['./orders.component.css']
})
export class OrdersComponent {
  public orders: IOrder[] = [];

  constructor(private ordersService: OrdersService, private createOrderService: CreateOrderService) { }


  ngOnInit(): void {
    this.ordersService.getAll().subscribe(orders => {
      console.log(orders);
      this.orders = orders;
    })
  }
  openOrdersForm() {
    const nextOrderNumber = this.getNextOrderNumber();
    this.ordersService.setLastOrderNumber(nextOrderNumber);
    this.createOrderService.setOpenForm(true, nextOrderNumber);
  }
  getNextOrderNumber(): number {
    if (this.orders.length === 0) {
      return 1; 
    }
    const lastOrder = this.orders[this.orders.length - 1];
    return lastOrder.orderNumber + 1;
  }
}
