import React, { useEffect, useState } from "react";
import api from "../api";
import { isAdmin } from "../utils/auth";

function AdminPanel() {
  const [activities, setActivities] = useState([]);
  const [users, setUsers] = useState([]);
  const [view, setView] = useState("activities");
  const [editing, setEditing] = useState(null);
  const [form, setForm] = useState({ title: "", description: "" });

  useEffect(() => {
    if (isAdmin()) loadActivities();
  }, []);

  if (!isAdmin()) {
    return <h2>🚫 Du har inte behörighet att visa denna sida.</h2>;
  }

  const loadActivities = async () => {
    const res = await api.get("/admin/activities");
    setActivities(res.data);
  };

  const loadUsers = async () => {
    const res = await api.get("/admin/users");
    setUsers(res.data);
  };

  const startEdit = (activity) => {
    setEditing(activity.id);
    setForm({ title: activity.title, description: activity.description });
  };

  const cancelEdit = () => {
    setEditing(null);
    setForm({ title: "", description: "" });
  };

  const handleUpdate = async () => {
    await api.put(`/admin/activities/${editing}`, form);
    cancelEdit();
    loadActivities();
  };

  const handleDelete = async (id) => {
    if (window.confirm("Är du säker på att du vill ta bort denna aktivitet?")) {
      await api.delete(`/admin/activities/${id}`);
      setActivities((prev) => prev.filter((a) => a.id !== id));
    }
  };

  return (
    <div className="admin-dashboard">
      <h2>🛠️ Adminpanel</h2>

      <div className="admin-tabs">
        <button onClick={() => setView("activities")}>📋 Aktiviteter</button>
        <button
          onClick={() => {
            setView("users");
            loadUsers();
          }}
        >
          👥 Användare
        </button>
      </div>

      {/* === Aktiviteter === */}
      {view === "activities" && (
        <table className="table">
          <thead>
            <tr>
              <th>Titel</th>
              <th>Beskrivning</th>
              <th>Kategori</th>
              <th>Skapad av</th>
              <th>Åtgärder</th>
            </tr>
          </thead>
          <tbody>
            {activities.map((a) => (
              <tr key={a.id}>
                <td>
                  {editing === a.id ? (
                    <input
                      value={form.title}
                      onChange={(e) =>
                        setForm({ ...form, title: e.target.value })
                      }
                    />
                  ) : (
                    a.title
                  )}
                </td>
                <td>
                  {editing === a.id ? (
                    <input
                      value={form.description}
                      onChange={(e) =>
                        setForm({ ...form, description: e.target.value })
                      }
                    />
                  ) : (
                    a.description
                  )}
                </td>
                <td>{a.category}</td>
                <td>{a.createdBy}</td>
                <td>
                  {editing === a.id ? (
                    <>
                      <button onClick={handleUpdate}>💾 Spara</button>
                      <button onClick={cancelEdit}>❌ Avbryt</button>
                    </>
                  ) : (
                    <>
                      <button onClick={() => startEdit(a)}>✏️ Redigera</button>
                      <button onClick={() => handleDelete(a.id)}>
                        🗑️ Ta bort
                      </button>
                    </>
                  )}
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      )}

      {/* === Användare === */}
      {view === "users" && (
        <div className="users-section">
          <h3>👥 Registrerade användare</h3>
          <table className="table">
            <thead>
              <tr>
                <th>ID</th>
                <th>E-post</th>
                <th>Roll</th>
              </tr>
            </thead>
            <tbody>
              {users.map((u) => (
                <tr key={u.id}>
                  <td>{u.id}</td>
                  <td>{u.email}</td>
                  <td>{u.role}</td>
                </tr>
              ))}
            </tbody>
          </table>
        </div>
      )}
    </div>
  );
}

export default AdminPanel;
