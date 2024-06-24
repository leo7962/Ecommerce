import { FC } from 'react';
import { Card, Button } from 'react-bootstrap';

// Define el tipo del producto
interface Product {
    name: string;
    price: number;
    image: string;
}

// Define el tipo de las props del componente
interface ProductCardProps {
    product: Product;
    addToCart: (product: Product) => void;
}

const ProductCard: FC<ProductCardProps> = ({ product, addToCart }) => {
    return (
        <Card style={{ width: '18rem' }}>
            <Card.Img variant="top" src={product.image} />
            <Card.Body>
                <Card.Title>{product.name}</Card.Title>
                <Card.Text>${product.price}</Card.Text>
                <Button variant="primary" onClick={() => addToCart(product)}>Add to Cart</Button>
            </Card.Body>
        </Card>
    );
}

export default ProductCard;