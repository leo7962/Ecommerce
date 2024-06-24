import React from 'react';
import { Container, Row, Col, Button, ListGroup } from 'react-bootstrap';

interface CartItem {
    name: string;
    price: number;
}

interface CartProps {
    cartItems: CartItem[];
    removeFromCart: (item: CartItem) => void;
    checkout: () => void;
}

const Cart: React.FC<CartProps> = ({ cartItems, removeFromCart, checkout }) => {
    return (
        <Container>
            <h2>Shopping Cart</h2>
            <ListGroup>
                {cartItems.map((item, index) => (
                    <ListGroup.Item key={index}>
                        <Row>
                            <Col md={6}>{item.name}</Col>
                            <Col md={2}>${item.price}</Col>
                            <Col md={2}>
                                <Button variant="danger" onClick={() => removeFromCart(item)}>Remove</Button>
                            </Col>
                        </Row>
                    </ListGroup.Item>
                ))}
            </ListGroup>
            {cartItems.length > 0 && (
                <Button variant="success" onClick={checkout}>Proceed to Checkout</Button>
            )}
        </Container>
    );
}

export default Cart;
