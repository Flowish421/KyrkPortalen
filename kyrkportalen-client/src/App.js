import { Routes, Route, Link } from "react-router-dom";
import Home from "./pages/Home";
import CreateActivity from "./pages/CreateActivity";
import LoginPage from "./pages/LoginPage";
import RegisterPage from "./pages/RegisterPage";
import "./App.css";

function App() {
  return (
    <div className="app-wrapper">
      <header className="navbar">
        <div className="nav-logo">
          ⛪ <span>KyrkPortalen</span>
        </div>
        <nav className="nav-links">
          <Link to="/">🏠 Hem</Link>
          <Link to="/create">➕ Skapa aktivitet</Link>
          <Link to="/login">🔑 Logga in</Link>
          <Link to="/register">📝 Registrera</Link>
        </nav>
      </header>

      <main className="main-content">
        <Routes>
          <Route path="/" element={<Home />} />
          <Route path="/create" element={<CreateActivity />} />
          <Route path="/login" element={<LoginPage />} />
          <Route path="/register" element={<RegisterPage />} />
        </Routes>
      </main>

      <footer className="footer">
        <p>
          © {new Date().getFullYear()} KyrkPortalen – En plats för gemenskap
          och tro 🕊️
        </p>
      </footer>
    </div>
  );
}

export default App;
