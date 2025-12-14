import React, { useState } from "react";
import api from "../api";
import "./LoginPage.css";

function LoginPage() {
  const [form, setForm] = useState({ email: "", password: "" });
  const [error, setError] = useState("");

  const handleChange = (e) =>
    setForm({ ...form, [e.target.name]: e.target.value });

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      const response = await api.post("/auth/login", {
        Email: form.email,
        Password: form.password,
      });
      const token = response.data.token;
      localStorage.setItem("token", token);
      window.location.href = "/";
    } catch {
      setError("Fel inloggningsuppgifter. Försök igen.");
    }
  };

  return (
    <div className="auth-container">
      <div className="auth-card">
        <h2>🔑 Logga in</h2>
        {error && <p className="error-msg">{error}</p>}

        <form onSubmit={handleSubmit}>
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

          <button type="submit">Logga in</button>
        </form>
      </div>
    </div>
  );
}

export default LoginPage;
