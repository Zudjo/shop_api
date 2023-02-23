import { ProductInInvoice } from "./product-in-invoice";

export interface InvoiceWorker {
    id: number;
    productsInInvoice: ProductInInvoice[];
    total: number;
}
