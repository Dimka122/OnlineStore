import { Order } from '../types';

interface OrderCardProps {
  order: Order;
}

export const OrderCard = ({ order }: OrderCardProps) => {
  return (
    <div className="order-card">
      <h3>Заказ #{order.id}</h3>
      <p>Дата: {new Date(order.createdAt).toLocaleDateString()}</p>
    </div>
  );
};