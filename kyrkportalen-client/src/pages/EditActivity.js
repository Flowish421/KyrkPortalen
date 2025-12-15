import React, { useEffect, useState } from "react";
import { useParams, useNavigate } from "react-router-dom";
import api from "../api";
import { getUserRole } from "../utils/auth"; // 🧩 Lägg till denna rad!

function EditActivity() {
  const { id } = useParams(); // 🧭 få ID:t från URL
  const navigate = useNavigate();
  const [form, setForm] = useState({
    title: "",
    description: "",
  });
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState("");

  // 🔹 Hämta aktiviteten som ska redigeras
  useEffect(() => {
    const loadActivity = async () => {
      try {
        const res = await api.get(`/activity/${id}`);
        setForm({
          title: res.data.title,
          description: res.data.description,
        });
        setLoading(false);
      } catch (err) {
        setError("Kunde inte hämta aktivitet.");
        setLoading(false);
      }
    };

    loadActivity();
  }, [id]);

  // 🔹 Hantera uppdatering
  const handleSubmit = async (e) => {
    e.preventDefault();

    const role = getUserRole(); // 🧠 Hämtar roll ur JWT-token
    const endpoint =
      role === "Admin"
        ? `/admin/activities/${id}` // Admin får uppdatera allt
        : `/activity/${id}`;        // Vanlig användare bara sina egna

    try {
      await api.put(endpoint, form);
      alert("✅ Aktiviteten uppdaterades!");
      navigate("/");
    } catch (err) {
      console.error("❌ Misslyckades med uppdatering:", err.response?.data || err);
      if (err.response?.status === 403) {
        alert("❌ Du har inte behörighet att uppdatera denna aktivitet.");
      } else {
        alert("⚠️ Något gick fel vid uppdatering.");
      }
    }
  };

  if (loading) return <p>Laddar aktivitet...</p>;
  if (error) return <p>{error}</p>;

  return (
    <div className="edit-activity">
      <h2>✏️ Redigera aktivitet</h2>
      <form onSubmit={handleSubmit} className="edit-form">
        <label>
          Titel:
          <input
            type="text"
            value={form.title}
            onChange={(e) => setForm({ ...form, title: e.target.value })}
          />
        </label>

        <label>
          Beskrivning:
          <textarea
            value={form.description}
            onChange={(e) => setForm({ ...form, description: e.target.value })}
          />
        </label>

        <button type="submit">💾 Spara ändringar</button>
        <button type="button" onClick={() => navigate("/")}>
          🔙 Tillbaka
        </button>
      </form>
    </div>
  );
}

export default EditActivity;
