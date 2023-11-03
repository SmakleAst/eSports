import { useParams } from "react-router-dom"
import React, { useState, useEffect } from 'react';
import TournamentPageForm from "./TournamentComponents/TournamentPageForm";
import TournamentService from "./TournamentComponents/TournamentService";

const TournamentPage = () => {
  const [tournamentData, setTournamentData] = useState({
    name: "",
    description: "",
    teams: ""
  });

  const {id} = useParams()

  const handleSimulateStage = async (id) => {
    const tournamentData = await TournamentService.simulateStage(id);
    setTournamentData(tournament);
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
      <TournamentPageForm tournamentData={tournamentData} />
      <button onClick={() => handleSimulateStage(id)}>Симулировать стадию турнира</button>
    </div>
  );
}

export default TournamentPage