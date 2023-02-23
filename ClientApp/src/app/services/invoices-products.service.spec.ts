import { TestBed } from '@angular/core/testing';

import { InvoicesProductsService } from './invoices-products.service';

describe('InvoicesProductsService', () => {
  let service: InvoicesProductsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(InvoicesProductsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
