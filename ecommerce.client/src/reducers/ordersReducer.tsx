import axios from 'axios';
import { Dispatch } from 'redux';

interface orderProduct {
    idProduct: number;
    idOrder: number;
    quantity: number;

}

export const createOrder = (orderDTO: orderProduct) => {
    return async (dispatch: Dispatch) => {
        try {
            const response = await axios.post<orderProduct>
                ('http://localhost:5203/api/Orders', orderDTO);
            dispatch({
                type: 'CREATE_ORDER',
                payload: response.data
            });
        } catch (error) {
            console.error(error);
        }
    };
};