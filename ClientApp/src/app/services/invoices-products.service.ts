import { Injectable } from '@angular/core';
import { InvoiceWorker } from '../models/invoice-worker';

@Injectable({
  providedIn: 'root'
})
export class InvoicesProductsService {

  constructor() { }

  invoicesProducts!: InvoiceWorker[];
}
