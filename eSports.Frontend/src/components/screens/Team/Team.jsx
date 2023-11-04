import React, { useState, useEffect } from 'react';
import TeamTable from './TeamComponents/TeamTable';
import TeamForm from './TeamComponents/TeamForm';
import TeamFilter from './TeamComponents/TeamFilter';
import TeamService from './TeamComponents/TeamService';
import "/src/assets/styles/team.css"

const Team = () => {
  const [teams, setTeams] = useState([]);
  const [filters, setFilters] = useState({
    filterName: '',
    filterCountry: '',
  });

  const fetchData = async () => {
    const { filterName, filterCountry } = filters;
    const teams = await TeamService.getTeams(filterName, filterCountry);
    setTeams(teams);
  };
  
  const handleCreateTeam = async (teamData) => {
    const success = await TeamService.createTeam(teamData);

    if (success) {
      const updatedTeams = await TeamService.getTeams();
      setTeams(updatedTeams);
    } else {
      // Действия при ошибке создания команды
    }
  };

  const handleDeleteTeam = async (teamId) => {
    const success = await TeamService.deleteTeam(teamId);

    if (success) {
      const updatedTeams = await TeamService.getTeams();
      setTeams(updatedTeams);
    } else {
      // Действия при ошибке удаления команды
    }
  };

  const handleFilterChange = (name, value) => {
    setFilters((prevFilters) => ({
      ...prevFilters,
      [name]: value,
    }));
  };
    

    useEffect(() => {
        fetchData();
    }, [teams]);
  

  return (
    <div className="team-container">
      <h1>Создать команду: </h1>
      <TeamFilter filters={filters} handleFilterChange={handleFilterChange}/>
      <div className="team-content-container">
        <TeamForm onCreateTeam={handleCreateTeam} />
        <TeamTable teams={teams} onDeleteTeam={handleDeleteTeam} />
      </div>
    </div>
  );
};

export default Team
