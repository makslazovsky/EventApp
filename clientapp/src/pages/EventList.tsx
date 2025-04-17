import React, { useEffect, useState } from 'react';
import { Link } from 'react-router-dom';

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

const EventList: React.FC = () => {
    const [events, setEvents] = useState<Event[]>([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState<string | null>(null);

    useEffect(() => {
        const fetchEvents = async () => {
            try {
                const response = await fetch('/api/events');
                const data: { items: Event[]; totalCount: number } = await response.json();

                if (!Array.isArray(data.items)) {
                    throw new Error('Expected "items" to be an array');
                }

                setEvents(data.items);
            } catch (error) {
                console.error('Error fetching events:', error);
                setError('Ошибка при загрузке данных');
            } finally {
                setLoading(false);
            }
        };

        fetchEvents();
    }, []);

    if (loading) return <p>Загрузка...</p>;
    if (error) return <p>Ошибка: {error}</p>;

    return (
        <div>
            <h2>Список событий</h2>
            <ul>
                {events.map((event) => (
                    <li key={event.id}>
                        <h3>
                            <Link to={`/events/${event.id}`}>{event.title}</Link>
                        </h3>
                        <p>{event.description}</p>
                        <p>Дата: {new Date(event.date).toLocaleDateString()}</p>
                        <p>Место: {event.location}</p>
                        <p>
                            Участников: {event.currentParticipants ?? 0}/{event.maxParticipants}
                        </p>
                        {event.currentParticipants >= event.maxParticipants && (
                            <p style={{ color: 'red' }}>Свободных мест нет</p>
                        )}
                    </li>
                ))}
            </ul>
        </div>
    );
};

export default EventList;
