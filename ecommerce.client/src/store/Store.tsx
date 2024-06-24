import { configureStore } from '@reduxjs/toolkit';
// Importa tus reducers aqu�

export const store = configureStore({
    reducer: {
        // A�ade tus reducers aqu�
    },
});

export type RootState = ReturnType<typeof store.getState>;
export type AppDispatch = typeof store.dispatch;