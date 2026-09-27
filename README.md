# WK Services

A full-stack web application for managing client orders across WK's core services — **AM**, **LL**, **FB**, and **Old LL**. Each client has one or more contacts who can log in, submit new orders, and search their own client's orders only.

Built as a Senior Full-Stack Assessment project.

---

## Features

- **Login** — Contact-level authentication (JWT-based).
- **New Order** — Create an order with order type, service, quantity, and an optional delivery date, validated on both the client and server.
- **Search by Order Number** — Look up an order and view its full details. A contact can only see orders belonging to their own client.
- **Auto-generated Order Numbers** — Pattern: `clientname_ddMMyyyy_serial` (unique per client, per day).

---

## Tech Stack

| Layer | Technology |
| --- | --- |
| Backend | ASP.NET Core Web API (C#), Entity Framework Core |
| Database | SQL Server |
| Frontend | React (Vite) + Tailwind CSS |
| Auth | JWT (JSON Web Tokens) |
| HTTP Client | Axios |

---

## Project Structure

```
WK Services/
├── Backend/           # ASP.NET Core API, EF Core, Domain layer
├── Frontend/          # React + Vite + Tailwind app
├── Documentation/      # ERD image and test case screenshots
├── .github/            # GitHub configuration
└── .gitignore
```

---

## Business Rules

- A **Client** can have one or more **Contacts**.
- Each **Contact** logs in independently and can only search or submit orders for **their own client**.
- **Order Type** must be one of: `Top`, `Sun`, `Fox`.
- **Service** must be one of: `AM`, `LL`, `FB`, `Old LL`.
- **Quantity** must be between **1 and 5**.
- **Requested Delivery Date** is optional, but if provided it must be **after today**.
- **Order Number** is generated automatically and is unique per client, per day.

All rules above are enforced on **both** the frontend (for user experience) and the backend (for security).

---

## Prerequisites

- [.NET SDK](https://dotnet.microsoft.com/download) (8.0 or later)
- [Node.js](https://nodejs.org/) (18 or later) and npm
- SQL Server (LocalDB, Express, or full instance)

---

## Getting Started — Backend

1. Navigate to the backend folder:
   ```bash
   cd Backend
   ```
2. Update the connection string in `appsettings.json` under `ConnectionStrings` to match your local SQL Server instance.
3. Restore packages and apply migrations:
   ```bash
   dotnet restore
   dotnet ef database update --project Infrastructure --startup-project API
   ```
   This creates the database and seeds sample **Services**, **Clients**, and **Contacts**.
4. Run the API:
   ```bash
   dotnet run
   ```
5. The API will start on a URL shown in the terminal (e.g. `https://localhost:7136`). Swagger UI is available at `/swagger` for testing endpoints directly.

---

## Getting Started — Frontend

1. Navigate to the frontend folder:
   ```bash
   cd Frontend
   npm install
   ```
2. Open `src/api/axiosClient.js` and make sure `baseURL` matches the port your backend is running on:
   ```javascript
   baseURL: "https://localhost:7136/api",
   ```
3. Start the development server:
   ```bash
   npm run dev
   ```
4. Open the URL shown in the terminal (e.g. `http://localhost:5173`).

---

## Demo Credentials

Seeded automatically with the database migration:

| Client | Username | Password | Contact |
| --- | --- | --- | --- |
| client1 | `contact1_1` | `pws123` | ahmed |
| client2 | `contact2_1` | `pws123` | sara |
| client2 | `contact2_2` | `pws123` | mona |

Use `contact1_1` and `contact2_1` to verify that each contact only sees orders belonging to their own client.

---

## API Endpoints

| Method | Endpoint | Description | Auth |
| --- | --- | --- | --- |
| POST | `/api/auth/login` | Log in with username & password, returns a JWT | No |
| POST | `/api/orders` | Create a new order for the logged-in contact's client | Yes |
| GET | `/api/orders/{orderNumber}` | Search for an order by its number (own client only) | Yes |

---

## Entity Relationship Diagram

See [`Documentation/erd.png`](./Documentation/erd.png) for the full ERD (Client, Contact, Service, Order and their relationships).

---

## Test Cases

Screenshots covering the main flow and edge cases are available in [`Documentation/screenshots/`](./Documentation/screenshots/):

1. Successful login
2. Invalid login credentials
3. Successful order creation
4. Invalid quantity rejected (outside 1–5)
5. Invalid delivery date rejected (not in the future)
6. Successful order search
7. Order number not found
8. A contact attempting to search an order belonging to a different client (correctly blocked)

---

## Notes

- This project was built entirely without AI-assisted code generation, per the assessment requirements.
- Passwords are stored as SHA-256 hashes (sufficient for this assessment's scope; a production system would use a stronger algorithm such as BCrypt).
