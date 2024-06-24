import React, { useState, FC } from 'react';
import { Container, Form, Button, Row, Col } from 'react-bootstrap';

// Define the props for the Login component
interface LoginProps {
    onLogin: (email: string, password: string) => void;
}

// Define the Login component
const Login: FC<LoginProps> = ({ onLogin }) => {
    // Define state variables for email and password
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');

    // Handle form submission
    const handleSubmit = (event: React.FormEvent) => {
        event.preventDefault();
        onLogin(email, password);
    };

    // Render the component
    return (
        <Container>
            <Row className="justify-content-md-center">
                <Col xs lg="6">
                    <h3>Sign In</h3>
                    <Form onSubmit={handleSubmit}>
                        <Form.Group controlId="formBasicEmail">
                            <Form.Label>Email Address</Form.Label>
                            <Form.Control type="email" placeholder="Enter your email" value={email} onChange={e => setEmail(e.target.value)} required />
                        </Form.Group>

                        <Form.Group controlId="formBasicPassword">
                            <Form.Label>Password</Form.Label>
                            <Form.Control type="password" placeholder="Password" value={password} onChange={e => setPassword(e.target.value)} required />
                        </Form.Group>
                        <Button variant="primary" type="submit">
                            Submit
                        </Button>
                    </Form>
                </Col>
            </Row>
        </Container>
    );
};

export default Login;
