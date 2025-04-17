import axios from "axios";

const API_URL = "http://localhost:5000/api"; // подставь нужный адрес

export const login = async (username: string, password: string) => {
    const response = await axios.post(`${API_URL}/auth/login`, {
        username,
        password,
    });

    return response.data; // ожидаем { accessToken, refreshToken }
};