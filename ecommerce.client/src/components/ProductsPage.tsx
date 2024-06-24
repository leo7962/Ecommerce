import React, { useEffect, useState } from 'react';

interface Product {
    id: number;
    name: string;
    price: number;
    image: string;
}

const ProductsPage: React.FC = () => {
    const [products, setProducts] = useState<Product[]>([]);

    useEffect(() => {
        fetch('/api/Products')
            .then(response => response.json())
            .then(data => setProducts(data));
    }, []);

    return (
        <div>

        </div>
    )
}

export default ProductsPage;