import React, { useState } from "react";
import api from "../api";

export default function LoginPage() {
  const [form, setForm] = useState({ email: "", password: "" });

  const handleChange = (e) => {
    setForm({ ...form, [e.target.name]: e.target.value });
  };

const handleSubmit = async (e) => {
  e.preventDefault();
  try {
    const response = await api.post("/auth/login", {
      Email: form.email,
      Password: form.password,
    });

    const token = response.data.token; // 👈 liten bokstav
    localStorage.setItem("token", token);

    alert("Inloggning lyckades!");
    window.location.href = "/";
  } catch (error) {
    console.error(error.response?.data || error);
    alert("Fel användarnamn eller lösenord.");
  }
};


  return (
    <div>
      <h2>Logga in</h2>
      <form onSubmit={handleSubmit}>
        <input
          name="email"
          type="email"
          placeholder="E-post"
          value={form.email}
          onChange={handleChange}
          required
        />
        <input
          name="password"
          type="password"
          placeholder="Lösenord"
          value={form.password}
          onChange={handleChange}
          required
        />
        <button type="submit">Logga in</button>
      </form>
    </div>
  );
}
