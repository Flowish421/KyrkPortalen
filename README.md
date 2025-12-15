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

## Arkitekturöversikt
Projektet följer en **Clean Architecture-inspirerad struktur** med tydlig separering:


------------------------------------------------

## Så kör du projektet lokalt

Backend (.NET)
1. Klona projektet  
   --- bash
   via [ git clone https://github.com/Flowish421/KyrkPortalen.git ]
   
   Fil väg för att sarta måste du skriva detta exat (cd KyrkPortalen/KyrkPortalen) 
   glöm inte att uppdatera databasen (dotnet ef database update)
    sen skriver du (Donet run)
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



