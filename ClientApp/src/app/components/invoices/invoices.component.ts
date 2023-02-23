import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Invoice } from 'src/app/models/invoice';
import { InvoicesProductsService } from 'src/app/services/invoices-products.service';

@Component({
  selector: 'invoices',
  templateUrl: './invoices.component.html',
  styleUrls: ['./invoices.component.css']
})
export class InvoicesComponent implements OnInit {
  invoices!: Invoice[];

  constructor(private httpClient: HttpClient, public invoicesProductsService: InvoicesProductsService) { }

  ngOnInit(): void {
    this.httpClient.get<Invoice[]>('http://localhost:5153/invoices')
    .subscribe(result => {
      this.invoices = result;
    })
  }

}
