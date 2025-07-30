import { useState, useEffect } from 'react';
import { useSearchParams } from 'react-router-dom';
import { apiClient } from '../api/client';
import { Product } from '../types';
import { Filters } from '../components/Filters';
import { Spinner } from '../components/Spinner';
import { ProductGrid } from '../components/ProductGrid';
import { Pagination } from '../components/Pagination';

export const CatalogPage = () => {
  const [products, setProducts] = useState([] as Product[]);
  const [loading, setLoading] = useState(false);
  const [searchParams] = useSearchParams();

  useEffect(() => {
    const fetchProducts = async () => {
      setLoading(true);
      try {
        const category = searchParams.get('category');
        const minPrice = searchParams.get('minPrice');
        const maxPrice = searchParams.get('maxPrice');
        const page = searchParams.get('page') || 1;

        const response = await apiClient.get('api/Products', {
          params: { category, minPrice, maxPrice, page },
        });
        setProducts(response.data);
      } catch (error) {
        console.error('Ошибка загрузки товаров:', error);
      } finally {
        setLoading(false);
      }
    };

    fetchProducts();
  }, [searchParams]);

  return (
    <div>
      <Filters />
      {loading ? <Spinner /> : <ProductGrid products={products} />}
      <Pagination currentPage={Number(searchParams.get('page')) || 1} />
    </div>
  );
};