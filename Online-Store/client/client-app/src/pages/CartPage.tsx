import { useState, useEffect } from 'react';
import { CartItem } from '../types';
import { CartItemCard } from '../components/CartItemCard';

export const CartPage = () => {
  const [cartItems, setCartItems] = useState([] as CartItem[]);

  useEffect(() => {
    const fetchCartItems = async () => {
      try {
        const response = await fetch('/api/cart');
        const data = await response.json();
        setCartItems(data);
      } catch (error) {
        console.error('Error loading cart:', error);
      }
    };

    fetchCartItems();
  }, []);

  return (
    <div>
      <h1>Корзина</h1>
      {cartItems.length === 0 ? (
        <p>Корзина пуста</p>
      ) : (
        <div>
          {cartItems.map((item) => (
            <div key={item.id}>
              <CartItemCard item={item} />
            </div>
          ))}
        </div>
      )}
    </div>
  );
};

export default CartPage;