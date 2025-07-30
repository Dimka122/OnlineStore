import { apiClient } from './client'; // Или import { api } from './client', если выбрали Вариант 2

export const addToCart = async (productId: string, quantity: number) => {
  const response = await apiClient.post('/cart', { productId, quantity }); // Используйте api или apiClient в зависимости от выбранного варианта
  return response.data;
};