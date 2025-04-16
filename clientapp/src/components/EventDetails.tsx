import React, { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';

interface Event {
    id: string;
    title: string;
    description: string;
    date: string;
    location: string;
    category: string;
    maxParticipants: number;
    imagePath: string | null;
    currentParticipants: number;
}

const EventDetails: React.FC = () => {
    const { id } = useParams<{ id: string }>();
    const [event, setEvent] = useState<Event | null>(null);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState<string | null>(null);

    useEffect(() => {
        const fetchEvent = async () => {
            try {
                const response = await fetch(`/api/events/by-id/${id}`);

                if (!response.ok) {
                    throw new Error(`Ошибка запроса: ${response.status}`);
                }

                const data: Event = await response.json();
                setEvent(data);
            } catch (error) {
                console.error('Error fetching event:', error);
                setError('Ошибка при загрузке события');
            } finally {
                setLoading(false);
            }
        };

        fetchEvent();
    }, [id]);


    if (loading) return <p>Загрузка...</p>;
    if (error) return <p>Ошибка: {error}</p>;
    if (!event) return <p>Событие не найдено</p>;

    return (
        <div>
            <h2>{event.title}</h2>
            <p>{event.description}</p>
            <p>Дата: {new Date(event.date).toLocaleString()}</p>
            <p>Место: {event.location}</p>
            <p>Категория: {event.category}</p>
            <p>
                Участников: {event.currentParticipants ?? 0}/{event.maxParticipants}
            </p>
            {event.currentParticipants >= event.maxParticipants && (
                <p style={{ color: 'red' }}>Свободных мест нет</p>
            )}
        </div>
    );
};

export default EventDetails;
