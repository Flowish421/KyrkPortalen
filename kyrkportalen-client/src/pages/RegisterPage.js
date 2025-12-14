import React, { useState } from "react";
import api from "../api";

export default function RegisterPage() {
  const [form, setForm] = useState({ username: "", email: "", password: "" });

  const handleChange = (e) => {
    setForm({ ...form, [e.target.name]: e.target.value });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
     await api.post("/auth/register", {
  FullName: form.username, 
  Email: form.email,
  Password: form.password,
});

      alert("Registrering lyckades! Du kan nu logga in.");
    } catch (error) {
      console.error(error.response?.data || error);
      alert("Något gick fel vid registreringen.");
    }
  };

  return (
    <div>
      <h2>Registrera dig</h2>
      <form onSubmit={handleSubmit}>
        <input
          name="username"
          placeholder="Fullständigt namn"
          value={form.username}
          onChange={handleChange}
          required
        />
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
        <button type="submit">Registrera</button>
      </form>
    </div>
  );
}
