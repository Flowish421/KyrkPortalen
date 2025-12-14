import React, { useEffect, useState } from "react";
import api from "../api";
import "./Home.css";

function Home() {
  const [activities, setActivities] = useState([]);

  useEffect(() => {
    const fetchActivities = async () => {
      try {
        const response = await api.get("/activity");
        setActivities(response.data);
      } catch (error) {
        console.error("Fel vid hämtning av aktiviteter:", error);
      }
    };

    fetchActivities();
  }, []);

  return (
    <div className="home-container">
      <section className="hero">
        <h1>Välkommen till KyrkPortalen</h1>
        <p>
          En plats för gemenskap, tro och aktiviteter i vår församling.
        </p>
      </section>

      <section className="activities-section">
        <h2>🌿 Aktiviteter</h2>
        {activities.length === 0 ? (
          <p className="no-activities">
            Just nu finns inga planerade aktiviteter.
          </p>
        ) : (
          <div className="activity-grid">
            {activities.map((a) => (
              <div key={a.id} className="activity-card">
                <h3>{a.title}</h3>
                <p className="category">📖 {a.category}</p>
                <p className="desc">{a.description}</p>
              </div>
            ))}
          </div>
        )}
      </section>
    </div>
  );
}

export default Home;
