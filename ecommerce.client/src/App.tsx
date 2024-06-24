import 'bootstrap/dist/css/bootstrap.min.css';
import React, { useEffect, useState } from 'react';
import {
    BrowserRouter as Router,
    Route,
    Routes
} from 'react-router-dom';
import NavBarCom from './components/NavBar';
import ProductList from './components/ProductList';
import Cart from './components/Cart';
import Login from './components/Login';
import Register from './components/Register';
import { Provider } from 'react-redux';
import { store } from './store/Store';

// Define the type of the product
interface Product {
    id: number;
    name: string;
    price: number;
    image: string;
}

const App = () => {
    const [products, setProducts] = useState<Product[]>([
        { id: 1, name: 'Product 1', price: 100, image: 'https://via.placeholder.com/150' },
        { id: 2, name: 'Product 1', price: 100, image: 'https://via.placeholder.com/150' },
        { id: 3, name: 'Product 1', price: 100, image: 'https://via.placeholder.com/150' },
        { id: 4, name: 'Product 1', price: 100, image: 'https://via.placeholder.com/150' },
        { id: 5, name: 'Product 1', price: 100, image: 'https://via.placeholder.com/150' },
        { id: 6, name: 'Product 1', price: 100, image: 'https://via.placeholder.com/150' },
        { id: 7, name: 'Product 1', price: 100, image: 'https://via.placeholder.com/150' },
        { id: 8, name: 'Product 1', price: 100, image: 'https://via.placeholder.com/150' },
        { id: 9, name: 'Product 1', price: 100, image: 'https://via.placeholder.com/150' },
        { id: 10, name: 'Product 1', price: 100, image: 'https://via.placeholder.com/150' },
        { id: 11, name: 'Product 1', price: 100, image: 'https://via.placeholder.com/150' },
        { id: 12, name: 'Product 1', price: 100, image: 'https://via.placeholder.com/150' },
        { id: 13, name: 'Product 1', price: 100, image: 'https://via.placeholder.com/150' },
        { id: 14, name: 'Product 1', price: 100, image: 'https://via.placeholder.com/150' },
        { id: 15, name: 'Product 1', price: 100, image: 'https://via.placeholder.com/150' },
        { id: 16, name: 'Product 1', price: 100, image: 'https://via.placeholder.com/150' },
        // Rest of the products...
    ]);

    const [cartItems, setCartItems] = useState<Product[]>([]);

    const addToCart = (product: Product) => {
        setCartItems([...cartItems, product]);
    };

    const removeFromCart = (product: Product) => {
        setCartItems(cartItems.filter(item => item !== product));
    };

    useEffect(() => {
        // request bd
    }, [])

    const checkout = () => {
        alert('Proceeding to checkout!');
        setCartItems([]);
    };

    const handleLogin = (email: string, password: string) => {
        // Here you can handle the login
    };

    const handleRegister = (username: string, email: string, password: string) => {
        // Here you can handle the registration
    };

    return (
        <Provider store={store}>
        <Router>            
            <NavBarCom />
            <Routes>
                <Route path="/" element={<ProductList products={products} addToCart={addToCart} />} />
                <Route path="/cart" element={<Cart cartItems={cartItems} removeFromCart={removeFromCart} checkout={checkout} />} />
                <Route path="/login" element={<Login onLogin={handleLogin} />} /> {/* Add the route for login */}
                <Route path="/register" element={<Register onRegister={handleRegister} />} /> {/* Add the route for registration */}
            </Routes>
            </Router>
        </Provider>
    );
};

export default App;