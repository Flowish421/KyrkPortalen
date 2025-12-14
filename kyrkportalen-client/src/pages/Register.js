import React, { useState } from "react";
import api from "../api";
import "./RegisterPage.css";

function RegisterPage() {
  const [form, setForm] = useState({
    fullName: "",
    email: "",
    password: "",
  });
  const [message, setMessage] = useState("");

  const handleChange = (e) =>
    setForm({ ...form, [e.target.name]: e.target.value });

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      await api.post("/auth/register", {
        FullName: form.fullName,
        Email: form.email,
        Password: form.password,
      });
      setMessage("Registrering lyckades! Du kan nu logga in.");
    } catch {
      setMessage("Något gick fel. Försök igen.");
    }
  };

  return (
    <div className="auth-container">
      <div className="auth-card">
        <h2>📝 Registrera dig</h2>
        {message && <p className="info-msg">{message}</p>}

        <form onSubmit={handleSubmit}>
          <label htmlFor="fullName">Fullständigt namn</label>
          <input
            type="text"
            id="fullName"
            name="fullName"
            value={form.fullName}
            onChange={handleChange}
            required
          />

          <label htmlFor="email">E-post</label>
          <input
            type="email"
            id="email"
            name="email"
            value={form.email}
            onChange={handleChange}
            required
          />

          <label htmlFor="password">Lösenord</label>
          <input
            type="password"
            id="password"
            name="password"
            value={form.password}
            onChange={handleChange}
            required
          />

          <button type="submit">Skapa konto</button>
        </form>
      </div>
    </div>
  );
}

export default RegisterPage;
