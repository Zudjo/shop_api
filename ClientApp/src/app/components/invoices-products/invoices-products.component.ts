import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { InvoiceWorker } from 'src/app/models/invoice-worker';
import { InvoicesProductsService } from 'src/app/services/invoices-products.service';

@Component({
  selector: 'invoices-products',
  templateUrl: './invoices-products.component.html',
  styleUrls: ['./invoices-products.component.css']
})
export class InvoicesProductsComponent implements OnInit {
  invoiceWorkers!: InvoiceWorker[]

  constructor(private httpClient: HttpClient, public invoicesProductsService: InvoicesProductsService) { }

  ngOnInit(): void {
    this.httpClient.get<InvoiceWorker[]>('http://localhost:5153/invoices-products')
      .subscribe(result => {
        this.invoiceWorkers = result;
      })
  }

}
