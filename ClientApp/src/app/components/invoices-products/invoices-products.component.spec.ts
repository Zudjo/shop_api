import { ComponentFixture, TestBed } from '@angular/core/testing';

import { InvoicesProductsComponent } from './invoices-products.component';

describe('InvoicesProductsComponent', () => {
  let component: InvoicesProductsComponent;
  let fixture: ComponentFixture<InvoicesProductsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ InvoicesProductsComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(InvoicesProductsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
