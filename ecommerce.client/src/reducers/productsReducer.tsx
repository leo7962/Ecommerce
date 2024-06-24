import { createSlice } from '@reduxjs/toolkit';

interface Product {
    id: number;
    name: string;
    price: number;
    image: string;
}

const initialState: Product[] = [];

const productsSlice = createSlice({
    name: 'products',
    initialState,
    reducers: {
        setProducts: (state, action) => {            
            return action.payload;
        },
    },
});

export const { setProducts } = productsSlice.actions;
export default productsSlice.reducer;