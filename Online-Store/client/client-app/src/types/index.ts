// src/types/index.ts

// Товар
export interface Product {
  id: string;
  name: string;
  price: number;
  description?: string; // Опциональное поле
  imageUrl: string;
  categoryId: string;
  createdAt: string;
  updatedAt: string;
}

// Элемент корзины
export interface CartItem {
  id: string; // ID элемента корзины (не товара!)
  product: Product; // Сам товар
  quantity: number; // Количество
}

// Пользователь
export interface User {
  id: string;
  email: string;
  name: string;
}

// Заказ
export interface Order {
  id: string;
  items: CartItem[];
  total: number;
  createdAt: string;
}
