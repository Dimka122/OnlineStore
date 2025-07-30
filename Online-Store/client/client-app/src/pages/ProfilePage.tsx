import { useState, useEffect } from 'react';
import { apiClient } from '../api/client';
import { Order, User } from '../types';
import { OrderCard } from '../components/OrderCard';

export const ProfilePage = () => {
  const [user, setUser] = useState(null as User | null);
  const [orders, setOrders] = useState([] as Order[]);

  useEffect(() => {
    const fetchData = async () => {
      try {
        const [userRes, ordersRes] = await Promise.all([
          apiClient.get('/profile'),
          apiClient.get('/orders'),
        ]);
        setUser(userRes.data);
        setOrders(ordersRes.data);
      } catch (error) {
        console.error('Ошибка загрузки данных:', error);
      }
    };

    fetchData();
  }, []);

  return (
    <div>
      <h1>Профиль</h1>
      {user && (
        <>
          <p>Имя: {user.name}</p>
          <p>Email: {user.email}</p>
        </>
      )}

      <h2>История заказов</h2>
      {orders.map((order) => (
        <div key={order.id}>
          <OrderCard order={order} />
        </div>
      ))}
    </div>
  );
};