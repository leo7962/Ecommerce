import { FC, useState } from 'react';
import { Container, Row, Col, Pagination } from 'react-bootstrap';
import ProductCard from './ProductCard';

interface Product {
    id: string;   
}

interface ProductListProps {
    products: Product[];
    addToCart: (product: Product) => void;
}

const ProductList: FC<ProductListProps> = ({ products, addToCart }) => {
    const [currentPage, setCurrentPage] = useState(1);
    const itemsPerPage = 10;

    const handlePageChange = (pageNumber: number) => {
        setCurrentPage(pageNumber);
    };

    const paginatedProducts = products.slice((currentPage - 1) * itemsPerPage, currentPage * itemsPerPage);

    return (
        <Container>
            <Row>
                {paginatedProducts.map((product) => (
                    <Col key={product.id} sm={12} md={6} lg={4} xl={3}>
                        <ProductCard product={product} addToCart={addToCart} />
                    </Col>
                ))}
            </Row>
            <Pagination>
                {[...Array(Math.ceil(products.length / itemsPerPage)).keys()].map(number => (
                    <Pagination.Item key={number + 1} active={number + 1 === currentPage} onClick={() => handlePageChange(number + 1)}>
                        {number + 1}
                    </Pagination.Item>
                ))}
            </Pagination>
        </Container>
    );
}

export default ProductList;