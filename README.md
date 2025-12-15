# ✝️ KyrkPortalen
**Fullstack .NET 9 & React-projekt**  
Kurs: *Objektorienterad Programmering – Avancerad*  
Av: Ahmed Ahmed AKA (flowish421)

---

## 📖 Projektbeskrivning
KyrkPortalen är en fullstack-applikation där användare kan:
- Skapa, uppdatera och ta bort aktiviteter i församlingen
- Registrera sig och logga in via JWT-autentisering
- Administratörer kan se alla aktiviteter och användare, samt hantera poster

Systemet är byggt för att efterlikna ett **verkligt fullstack-projekt** enligt modern .NET- och React-standard.

---

## ⚙️ Teknikstack
| Lager | Teknologi |
|-------|------------|
| **Frontend** | React (Vite), Axios |
| **Backend** | .NET 9 Web API |
| **Databas** | SQL Server via EF Core |
| **CI/CD** | GitHub Actions |
| **Tester** | xUnit, Moq, FluentAssertions |

---

##  Arkitekturöversikt
Projektet är uppdelat i flera tydliga lager enligt Clean Architecture-principer:

 KyrkPortalen/
 ┣ API/ → Controllers (hanterar HTTP-anrop)
 ┣ Domain/ → Entiteter & DTOs
 ┣ Infrastructure/ → Repositories & DbContext
 ┣  Services/ → Affärslogik
 ┗ Program.cs → Konfiguration & Dependency Injection
------------------------------------------------
## 📡 API Endpoints

### AuthController
| Metod | Endpoint | Beskrivning |
|-------|-----------|-------------|
| POST | `/api/auth/register` | Registrerar ny användare |
| POST | `/api/auth/login` | Loggar in och returnerar JWT |

### ActivityController
| Metod | Endpoint | Beskrivning |
|-------------------------------
| GET | `/api/activity` | Hämtar alla aktiviteter för inloggad användare |
| GET | `/api/activity/{id}` | Hämtar en specifik aktivitet |
| POST | `/api/activity` | Skapar ny aktivitet |
| PUT | `/api/activity/{id}` | Uppdaterar aktivitet (ägaren eller admin) |
| DELETE | `/api/activity/{id}` | Tar bort aktivitet (ägaren eller admin) |

### AdminController
| Metod | Endpoint | Beskrivning |
|--------------------------------|
| GET | `/api/admin/activities` | Hämtar alla aktiviteter |
| PUT | `/api/admin/activities/{id}` | Uppdaterar aktivitet som admin |
| DELETE | `/api/admin/activities/{id}` | Tar bort aktivitet som admin |
| GET | `/api/admin/users` | Hämtar alla registrerade användare |


------------------------------------------------

## Så kör du projektet lokalt

Backend (.NET)
1. Klona projektet  
   --- bash
   via [ git clone https://github.com/Flowish421/KyrkPortalen.git ]
   
   Fil väg för att sarta måste du skriva detta exat (cd KyrkPortalen/KyrkPortalen) sen skriver du (Donet run)
   glöm inte att uppdatera databasen 

   ## Databas / SQL-script
Du kan skapa databasen med:
bash
dotnet ef database update
------------------------------------------
För Frontend skriver du dessa;
Frontend körs på: http://localhost:5173

cd ../kyrkportalen-client
npm install
npm run dev
------------------------------
För att göra testerna så skriver du detta;

cd KyrkPortalen.Tests
dotnet test

--------------------------
yml
### Kända buggar
markdown
## Kända buggar
Inga kända buggar vid senaste körningen



