import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { InvoicesProductsService } from 'src/app/services/invoices-products.service';
import { Product } from "src/app/models/product";

@Component({
  selector: 'products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.css']
})
export class ProductsComponent implements OnInit {
  products!: Product[];

  constructor(private httpClient: HttpClient, public invoicesProductsService: InvoicesProductsService) { }

  ngOnInit(): void {
    this.httpClient.get<Product[]>('http://localhost:5153/products')
    .subscribe(result => {
      this.products = result;
    })
  }

}
