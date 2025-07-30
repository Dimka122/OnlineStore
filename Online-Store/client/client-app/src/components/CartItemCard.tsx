import { CartItem } from '../types';

interface CartItemCardProps {
  item: CartItem;
}

export const CartItemCard = ({ item }: CartItemCardProps) => {
  return (
    <div className="cart-item">
      <img src={item.product.imageUrl} alt={item.product.name} />
      <div>
        <h3>{item.product.name}</h3>
        <p>Количество: {item.quantity}</p>
      </div>
    </div>
  );
};