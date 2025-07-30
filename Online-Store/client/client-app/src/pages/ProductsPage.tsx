import { useEffect, useState } from 'react';
import { Product } from '../types';
import { getProducts } from '../api/products';
import { ProductCard } from '../components/ProductCard';

export const ProductsPage = () => {
  const [products, setProducts] = useState([] as Product[]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const loadProducts = async () => {
      try {
        const data = await getProducts();
        setProducts(data);
      } catch (error) {
        console.error('Ошибка загрузки товаров:', error);
      } finally {
        setLoading(false);
      }
    };

    loadProducts();
  }, []);

  if (loading) return <div>Загрузка...</div>;

  return (
    <div className="products-container">
      <h1>Наши товары</h1>
      {products.map((product) => (
        <div key={product.id}>
        <ProductCard product={product} />
    </div>
))}
    </div>
  );
};

export default ProductsPage;
