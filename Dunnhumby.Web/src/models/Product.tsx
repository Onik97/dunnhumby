export type Product = {
  productId: number;
  category: string;
  name: string;
  productCode: string;
  price: number;
  stockQuantity: number;
  dateAdded?: Date;
};

export type StockPerCategory = {
  category: string;
  totalStock: number;
};
