import { useState } from "react";
import { login } from "../services/authService";
import { saveTokens } from "../utils/tokenStorage";
import { useNavigate } from "react-router-dom";

export const useAuth = () => {
    const navigate = useNavigate();
    const [error, setError] = useState<string | null>(null);

    const loginUser = async (username: string, password: string) => {
        try {
            const { accessToken, refreshToken } = await login(username, password);
            saveTokens(accessToken, refreshToken);
            navigate("/"); // на главную после входа
        } catch (err: any) {
            setError("Неверные данные для входа");
        }
    };

    return { loginUser, error };
};