import React, { useState, useEffect } from 'react';

const TournamentForm = ({ teams, onCreateTournament }) => {
  const [name, setName] = useState('');
  const [description, setDescription] = useState('');
  const [selectedTeams, setSelectedTeams] = useState([]);

  const handleSubmit = async (e) => {
    e.preventDefault();

    const tournamentData = {
      name,
      description,
      selectedTeams
    };

    onCreateTournament(tournamentData);

    setName('');
    setDescription('');
  };

  const handleCheckboxChange = (event) => {
    const teamName = event.target.value;
    if (event.target.checked) {
        setSelectedTeams([...selectedTeams, teamName]);
    } else {
        setSelectedTeams(selectedTeams.filter(team => team !== teamName));
    }
  };
  
  useEffect(() => {
  }, [selectedTeams]);

  return (
    <form onSubmit={handleSubmit}>
      <input
        name="name"
        type="text"
        value={name}
        onChange={(e) => setName(e.target.value)}
        placeholder="Название"
      />
      <input
        name="description"
        type="text"
        value={description}
        onChange={(e) => setDescription(e.target.value)}
        placeholder="Описание турнира"
      />
      {teams.map((team) => (
        <div key={team.id}>
          <input
            name="teams"
            type="checkbox"
            value={team.name}
            checked={selectedTeams.includes(team.name)}
            onChange={handleCheckboxChange}
          />
          <label>{team.name}</label>
        </div>
      ))}
      <button type="submit">Submit</button>
    </form>
  );
};

export default TournamentForm
