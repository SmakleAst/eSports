import React, { useState, useEffect } from 'react';
import TournamentForm from './TournamentComponents/TournamentForm';
import TournamentTable from './TournamentComponents/TournamentTable';
import TournamentService from './TournamentComponents/TournamentService';
import TournamentFilter from './TournamentComponents/TournamentFilter';
import "/src/assets/styles/tournament.css"

const Tournament = () => {
  const [teams, setTeams] = useState([]);
  const [tournaments, setTournaments] = useState([]);
  const [filterName, setFilterName] = useState('');

  const fetchTeams = async () => {
    const teams = await TournamentService.getTeams();
    setTeams(teams);
  };

  const fetchData = async () => {
    const tournaments = await TournamentService.getTournaments(filterName);
    setTournaments(tournaments);
  };

  const handleCreateTournament = async (tournamentData) => {
    const success = await TournamentService.createTournament(tournamentData);

    if (success) {
      const updatedTournaments = await TournamentService.getTournaments();
      setTournaments(updatedTournaments);
    } else {
      // Действия при ошибке создания турнира
    }
  };

  const handleDeleteTournament = async (tournamentId) => {
    const success = await TournamentService.deleteTournament(tournamentId);

    if (success) {
      const updatedTournaments = await TournamentService.getTournaments();
      setTeams(updatedTournaments);
    } else {
      // Действия при ошибке создания турнира
    }
  };

  const handleFilterChange = (name) => {
    setFilterName(name);
  };

  useEffect(() => {
    fetchData();
    fetchTeams();
  }, [tournaments]);

  return (
    <div className="tournament-container">
      <h1>Создать Турнир: </h1>
      <TournamentFilter filterName={filterName} handleFilterChange={handleFilterChange} />
      <div className="tournament-content-container">
        <TournamentForm teams={teams} onCreateTournament={handleCreateTournament} />
        <TournamentTable tournaments={tournaments} onDeleteTournament={handleDeleteTournament} />
      </div>
    </div>
  );
}

export default Tournament