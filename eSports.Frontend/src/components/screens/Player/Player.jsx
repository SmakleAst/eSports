import React, { useState, useEffect } from 'react';
import PlayerForm from './PlayerComponents/PlayerForm';
import PlayerTable from './PlayerComponents/PlayerTable';
import PlayerFilter from './PlayerComponents/PlayerFilter';
import PlayerService from './PlayerComponents/PlayerService';

const Player = () => {
  const [players, setPlayers] = useState([]);
  const [filters, setFilters] = useState({
    filterName: '',
    filterNickName: '',
    filterAge: '',
    filterTeam: '',
  });

  const fetchData = async () => {
    const { filterName, filterNickName, filterAge, filterTeam } = filters;
    const players = await PlayerService.getPlayers(filterName, filterNickName, filterAge, filterTeam);
    setPlayers(players);
  };
  
  const handleCreatePlayer = async (playerData) => {
    const success = await PlayerService.createPlayer(playerData);

    if (success) {
      const updatedPlayers = await PlayerService.getPlayers();
      setPlayers(updatedPlayers);
    } else {
      // Действия при ошибке создания игрока
    }
  };

  const handleDeletePlayer = async (playerId) => {
    const success = await PlayerService.deletePlayer(playerId);

    if (success) {
      const updatedPlayers = await PlayerService.getPlayers();
      setPlayers(updatedPlayers);
    } else {
      // Действия при ошибке удаления игрока
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
  }, [players]);

  return (
    <div>
      <PlayerForm onCreatePlayer={handleCreatePlayer} />
      <PlayerFilter filters={filters} handleFilterChange={handleFilterChange} />
      <PlayerTable players={players} onDeletePlayer={handleDeletePlayer} />
    </div>
  );
};

export default Player