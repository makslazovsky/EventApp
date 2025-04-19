EventApp

EventApp — это веб-приложение для управления мероприятиями, регистрации участников и администрирования. Реализована архитектура по принципам "чистой архитектуры" с использованием .NET 8, Entity Framework Core, MediatR, FluentValidation, AutoMapper, JWT, Docker, и React (TypeScript) на клиентской части.

---

Предварительные требования

Для запуска проекта через Docker необходимо:

- [Docker Desktop](https://www.docker.com/products/docker-desktop/)

> Для локальной разработки проекта без Docker дополнительно потребуются:
> - [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download)
> - [Node.js (LTS)](https://nodejs.org/)

---

Быстрый старт (через Docker)

1. Клонируйте репозиторий:

   ```bash
   git clone https://github.com/makslazovsky/EventApp.git
   cd EventApp
   ```

2. Постройте и запустите контейнеры:

   ```bash
   docker-compose up --build
   ```

3. Перейдите в браузере по адресу:

   - API (Swagger): [http://localhost:5000/swagger]
   - Клиент (React): [http://localhost:3000]

---

Структура проекта

```
EventApp/
├── src/
│   ├── API/               # ASP.NET Core Web API
│   ├── Application/       # Бизнес-логика (UseCases, Commands, Queries)
│   ├── Domain/            # Доменные модели и интерфейсы
│   ├── Infrastructure/    # Внешние сервисы
│   ├── Persistence/       # EF Core, DbContext, миграции
|   ├── Tests/			   # xUnit проект для unit-тестов
│   └── clientapp/         # React (TypeScript) клиент
├── docker-compose.yml
└── README.md              # Этот файл
```

---

Основной стек

Backend:
- ASP.NET Core 8
- Entity Framework Core
- MediatR
- FluentValidation
- AutoMapper
- JWT
- MS SQL Server

Frontend:
- React + TypeScript
- TailwindCSS

Инфраструктура:
- Docker
- Docker Compose
- Nginx

---

Авторизация (в процессе)

- На сервере реализована выдача JWT access и refresh токенов.
- Авторизация и аутентификация через JWT Bearer.
- На клиенте механизм хранения токена и отправки его в запросах **ещё не реализован**.

Пока доступ к защищённым эндпоинтам возможен через:
- Swagger UI (c токеном)
- Postman

---

Реализованные возможности

Backend:

Работа с событиями: 
1. Получение списка всех событий; 
2. Получение определенного события по его Id; 
3. Получение события по его названию; 
4. Добавление нового события; 
5. Изменение информации о существующем событии; 
6. Удаление события; 
7. Получение списка событий по определенным критериям (по дате, месту 
проведения, категории события) 
8. Возможность добавления изображений к событиям и их хранение. 

Работа с участниками: 
1. Регистрация участия пользователя в событии; 
2. Получение списка участников события; 
3. Получение определенного участника по его Id; 
4. Отмена участия пользователя в событии;

Работа с пользователями:
1. Регистрация пользователя
2. Логин пользователя
3. Обновление токена

Frontend:
- Отображение списка событий
- Просмотр информации о событии
- !Страницы регистрации и входа — **в процессе разработки**
