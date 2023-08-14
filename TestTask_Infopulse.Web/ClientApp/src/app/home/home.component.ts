import { Component, ComponentFactoryResolver, OnInit, ViewChild, ViewContainerRef } from '@angular/core';
import { CreateOrderComponent } from '../create-order/create-order.component';
import { CustomersComponent } from '../customers/customers.component';
import { OrdersComponent } from '../orders/orders.component';
import { ProductsComponent } from '../products/products.component';
import { CreateOrderService } from '../services/create-order.service';
import { OrdersService } from '../services/orders.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {
  @ViewChild('contentContainer', { read: ViewContainerRef }) contentContainer!: ViewContainerRef;
  @ViewChild(OrdersComponent) ordersComponent!: OrdersComponent;

  nextNumber!: number;
  selectedButton = 'orders';
  constructor(private componentFactoryResolver: ComponentFactoryResolver,
    private createOrderService: CreateOrderService,
    private ordersService: OrdersService) { }

  ngAfterViewInit() {
    this.renderComponent(this.selectedButton);
    this.createOrderService.openForm$.subscribe(openForm => {
      if (openForm.isOpen) {
        this.nextNumber = openForm.number;
        this.renderComponent('create-order');
      }
    })
    this.createOrderService.backToOrders$.subscribe(() => {
      this.selectButton('orders')
    });
  }
  selectButton(componentName: string) {
    this.selectedButton = componentName;
    this.renderComponent(componentName);
  }

  renderComponent(componentName: string) {
    this.contentContainer.clear();

    let componentClass;

    switch (componentName) {
      case 'orders':
        componentClass = OrdersComponent;
        break;
      case 'products':
        componentClass = ProductsComponent;
        break;
      case 'customers':
        componentClass = CustomersComponent;
        break;
      case 'create-order':
        componentClass = CreateOrderComponent;
        break;
    }

    if (componentClass) {
      const componentFactory = this.componentFactoryResolver.resolveComponentFactory(componentClass);
      const componentRef = this.contentContainer.createComponent(componentFactory);
      if (componentClass === CreateOrderComponent) {
        (componentRef.instance as CreateOrderComponent).newOrderNumber = this.nextNumber;
      }
    }
  }
}
