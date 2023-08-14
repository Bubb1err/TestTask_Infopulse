import { Component, Input, OnInit } from "@angular/core";
import { FormControl, FormGroup, Validators } from "@angular/forms";
import { ICreateOrder } from "../models/createOrder";
import { IOrderedProduct } from "../models/orderedProduct";
import { ISelectListCustomer } from "../models/selectListCustomer";
import { CreateOrderService } from "../services/create-order.service";

@Component({
  selector: 'create-order',
  templateUrl: './create-order.component.html',
  styleUrls: ['./create-order.component.css']
})
export class CreateOrderComponent implements OnInit {
  currentDate: string | undefined;
  selectCustomers: ISelectListCustomer[] = [];
  selectStatuses: any[] = [];
  selectedValue: any;
  selectedStatusValue: any;
  @Input() newOrderNumber!: number;
  defaultCustomer!: number;
  defaultStatus!: number;
  products: IOrderedProduct[] = []
  totalCost!: number;
  formOrder = new FormGroup({
    OrderNumber: new FormControl<number>(this.newOrderNumber),
    Status: new FormControl<number>(this.defaultStatus, [
      Validators.required
    ]),
    Comment: new FormControl<string>(''),
    Customer: new FormControl<number>(this.defaultCustomer, [
      Validators.required
    ]),
    OrderedProducts: new FormControl<IOrderedProduct[]>(this.products)
  })
  formData: ICreateOrder = {
    orderNumber: this.newOrderNumber,
    status: this.defaultStatus,
    comment: '',
    customerId: this.defaultCustomer,
    orderedProducts: this.products
  }
  constructor(private createOrderService: CreateOrderService) { }
  ngOnInit(): void {
    this.createOrderService.getSelectListCustomers().subscribe(data => {
      this.selectCustomers = data;
    });

    this.createOrderService.getSelectListStatuses().subscribe(data => {
      this.selectStatuses = Object.entries(data).map(([id, title]) => ({ id: +id, title }));
    });

    const today = new Date();
    this.currentDate = today.toISOString().substring(0, 10);

    this.totalCost = 0;
    this.getTotalCost()
  }
  getTotalCost() {
    for (let i = 0; i < this.products.length; i++) {
      this.totalCost += this.products[i].product.price
    }
  }
  saveOrder() {
    console.log(this.selectStatuses)
    if (this.formOrder.valid) {
      this.formData = {
        orderNumber: this.newOrderNumber,
        status: this.formOrder.value.Status ?? this.defaultStatus,
        comment: this.formOrder.value.Comment ?? '',
        customerId: this.formOrder.value.Customer ?? this.defaultCustomer,
        orderedProducts: this.products
      };
    }
    console.log(this.formData)

    this.createOrderService.createOrder(this.formData).subscribe(
      response => {
        console.log('Order created successfully', response);
        this.createOrderService.triggerBackToOrders();
      },
      error => {
        console.error('Error creating order', error);
      }
    );
  }
  backToOrders() {
    this.createOrderService.triggerBackToOrders()
  }
  addProducts() {

  }
  get customer() {
    return this.formOrder.controls.Customer as FormControl;
  }
  get status() {
    return this.formOrder.controls.Status as FormControl;
  }
}
