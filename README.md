EventApp

EventApp — это веб-приложение для управления мероприятиями, регистрации участников и администрирования. Реализована архитектура по принципам "чистой архитектуры" с использованием .NET 8, Entity Framework Core, MediatR, FluentValidation, JWT, Docker, и React (TypeScript) на клиентской части.

---

Предварительные требования

Для запуска проекта через Docker необходимо:

- [Docker Desktop](https://www.docker.com/products/docker-desktop/)

> Если вы планируете **разрабатывать или запускать проект локально без Docker**, тогда дополнительно потребуются:
> - [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download)
> - [Node.js (LTS)](https://nodejs.org/)

---

Быстрый старт (через Docker)

1. Клонируйте репозиторий:

   ```bash
   git clone https://github.com/your-username/EventApp.git
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
│   ├── Domain/            # Доменные модели
│   ├── Infrastructure/    # Внешние сервисы (JWT, Email и т.д.)
│   ├── Persistence/       # EF Core, DbContext, миграции
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
- Vite
- TailwindCSS

Инфраструктура:
- Docker
- Docker Compose
- Nginx

---

Авторизация (в процессе)

- На сервере реализована выдача JWT access и refresh токенов.
- Авторизация и аутентификация через `JWT Bearer`.
- На клиенте механизм хранения токена и отправки его в запросах **ещё не реализован**.

Пока доступ к защищённым эндпоинтам возможен через:
- Swagger UI (c токеном)
- Postman

---

Реализованные возможности

Backend:
- Регистрация пользователей
- Авторизация и генерация JWT
- Создание/редактирование/удаление событий
- Регистрация участников (1 пользователь — 1 регистрация на событие)
- Валидация данных
- Автоматическое применение миграций при запуске
- Чистая архитектура

Frontend:
- Отображение списка событий
- Просмотр информации о событии
- !Страницы регистрации и входа — **в процессе разработки**
