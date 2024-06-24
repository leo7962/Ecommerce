import { FC, useState, useEffect } from 'react';
import { Container, Row, Col, Pagination } from 'react-bootstrap';
import ProductCard from './ProductCard';
import { useDispatch, useSelector } from 'react-redux';
import { setProducts } from '../reducers/productsReducer';
import axios  from 'axios';

interface Product {
    id: number;
    name: string;
    price: number;
    description: string;
    image: string;   
}

interface ProductListProps {
    products: Product[];
    setProducts(value: Product[] | ((prevVar: Product[]) => Product[])) : void;
    addToCart: (product: Product) => void;
}

const ProductList: FC<ProductListProps> = ({ products, setProducts, addToCart }) => {
    const [currentPage, setCurrentPage] = useState(1);
    const itemsPerPage = 10;

    const dispatch = useDispatch();
    const productsDto = useSelector((state) => state.products);
    const handlePageChange = (pageNumber: number) => {
        setCurrentPage(pageNumber);
    };

    useEffect(() => {
        axios.get('http://localhost:5203/api/Products')
            .then(response => {                
                const getProducts = response.data;
                dispatch(setProducts(getProducts)); 
            })
            .catch(error => {
                console.error('Error:', error);
            });
    }, []);

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