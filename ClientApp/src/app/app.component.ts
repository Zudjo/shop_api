import { Component } from '@angular/core';
import { InvoicesProductsComponent } from './components/invoices-products/invoices-products.component';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent {

  title = 'app';
  showInvoices = false;
  showProducts = false;
  showInvoicesProducts = false;

  constructor() {}

  showComponent(componentName: string): void {
    this.showInvoices = false;
    this.showProducts = false;
    this.showInvoicesProducts = false;

    switch (componentName) {
      case "invoices":
        this.showInvoices = true;
        break;
      case "products":
        this.showProducts = true;
        break;
      case "invoicesProducts":
        this.showInvoicesProducts = true;
        break;

      default:
        break;
    }
  }  

}
