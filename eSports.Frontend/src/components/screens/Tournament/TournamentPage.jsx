import { useParams } from "react-router-dom"
import React, { useState, useEffect } from 'react';
import TournamentPageForm from "./TournamentComponents/TournamentPageForm";
import TournamentService from "./TournamentComponents/TournamentService";
import "/src/assets/styles/tournament.css"

const TournamentPage = () => {
  const [tournamentData, setTournamentData] = useState({
    name: "",
    description: "",
    teams: ""
  });

  const {id} = useParams()

  const handleSimulateStage = async (id) => {
    const tournamentData = await TournamentService.simulateStage(id);
    setTournamentData(tournamentData);
  };

  useEffect(() => {
    if (!id) return

    const fetchData = async () => {
      const tournament = await TournamentService.getOneTournament(id);
      setTournamentData(tournament);
    }

    fetchData()
  }, [id, tournamentData])

  return (
    <div>
      <div className="tournament-page">
        <TournamentPageForm
          tournamentData={tournamentData}
          handleSimulateStage={handleSimulateStage}
          id={id}
        />
        <img src="/public/mk-item.png" alt="Your Image" className="right-image" />
      </div>
      <button className="tournament-simulate-button" onClick={() => handleSimulateStage(id)}>
        Симулировать стадию турнира
      </button>
    </div>
  );
}

export default TournamentPage