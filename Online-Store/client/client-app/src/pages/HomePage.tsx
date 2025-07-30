// src/pages/HomePage.tsx
import { useEffect, useState } from 'react';
import { getProducts } from '../api/products';
import { Product } from '../types';

export const HomePage = () => {
  const [products, setProducts] = useState([] as Product[]);

  useEffect(() => {
    const loadProducts = async () => {
      const data = await getProducts(); // Запрос к вашему API
      setProducts(data);
    };
    loadProducts();
  }, []);

  return (
    <div>
      <h1>Товары</h1>
      <div className="product-grid">
        {products.map((product) => (
          <div key={product.id} className="product-card">
            <img src={product.imageUrl} alt={product.name} />
            <h3>{product.name}</h3>
            <p>Цена: ${product.price}</p>
          </div>
        ))}
      </div>
    </div>
  );
};