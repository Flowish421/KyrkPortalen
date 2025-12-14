import React, { useState } from "react";
import api from "../api";
import "./CreateActivity.css";

function CreateActivity() {
  const [form, setForm] = useState({
    title: "",
    category: "",
    description: "",
  });
  const [message, setMessage] = useState("");

  const handleChange = (e) =>
    setForm({ ...form, [e.target.name]: e.target.value });

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      await api.post("/activity", form);
      setMessage("✅ Aktiviteten skapades framgångsrikt!");
      setForm({ title: "", category: "", description: "" });
    } catch (error) {
      console.error("Fel vid skapande:", error);
      setMessage("⚠️ Kunde inte skapa aktivitet. Kontrollera att du är inloggad.");
    }
  };

  return (
    <div className="activity-container">
      <div className="activity-card">
        <h2>➕ Skapa ny aktivitet</h2>
        {message && <p className="message">{message}</p>}

        <form onSubmit={handleSubmit}>
          <label htmlFor="title">Titel</label>
          <input
            type="text"
            id="title"
            name="title"
            value={form.title}
            onChange={handleChange}
            required
            placeholder="Ex: Morgonbön"
          />

          <label htmlFor="category">Kategori</label>
          <input
            type="text"
            id="category"
            name="category"
            value={form.category}
            onChange={handleChange}
            required
            placeholder="Ex: Gudstjänst, kör, barnaktivitet"
          />

          <label htmlFor="description">Beskrivning</label>
          <textarea
            id="description"
            name="description"
            value={form.description}
            onChange={handleChange}
            required
            placeholder="Beskriv aktiviteten kort..."
          />

          <button type="submit">Skapa aktivitet</button>
        </form>
      </div>
    </div>
  );
}

export default CreateActivity;
