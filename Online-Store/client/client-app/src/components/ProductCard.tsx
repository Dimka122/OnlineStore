import { Product } from '../types';

interface ProductCardProps {
  product: Product;
}

export const ProductCard = ({ product }: ProductCardProps) => {
  return (
    <div className="product-card">
      <img 
        src={product.imageUrl || '/placeholder-product.jpg'} 
        alt={product.name}
        className="product-image"
      />
      <h3>{product.name}</h3>
      <p>{product.description}</p>
      <div className="product-price">${product.price.toFixed(2)}</div>
      <button className="add-to-cart">В корзину</button>
    </div>
  );
};