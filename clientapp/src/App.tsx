import React from 'react';
import './App.css';
import { Route, Routes } from 'react-router-dom';
import EventList from './pages/EventList';
import EventDetails from './pages/EventDetails';
import LoginPage from "./pages/LoginPage";


const App: React.FC = () => {
    return (
        <Routes>
            <Route path="/" element={<EventList />} />
            <Route path="/events/:id" element={<EventDetails />} />
            <Route path="/login" element={<LoginPage />} />
        </Routes>
    );
};

export default App;
