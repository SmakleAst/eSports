import React, { useState, useEffect } from 'react';
import "/src/assets/styles/player.css"

const PlayerForm = ({ teams, onCreatePlayer }) => {
  const [name, setName] = useState('');
  const [nickName, setNickName] = useState('');
  const [age, setAge] = useState('');
  const [team, setTeam] = useState('');
  const [description, setDescription] = useState('');

  const handleSubmit = async (e) => {
    e.preventDefault();

    const playerData = {
      name,
      nickName,
      age,
      team,
      description,
    };

    onCreatePlayer(playerData);

    setName('');
    setNickName('');
    setAge('');
    setTeam('');
    setDescription('');
  };

  return (
    <form onSubmit={handleSubmit} className="create-player-form">
      <input
        name="name"
        type="text"
        value={name}
        onChange={(e) => setName(e.target.value)}
        placeholder='Имя'
      />
      <input
        name="nickName"
        type="text"
        value={nickName}
        onChange={(e) => setNickName(e.target.value)}
        placeholder='Никнейм'
      />
      <input
        name="age"
        type="text"
        value={age}
        onChange={(e) => setAge(e.target.value)}
        placeholder='Возраст'
      />
      {/* <input name="team"
        type="text"
        value={team}
        onChange={(e) => setTeam(e.target.value)}
        placeholder='Команда'
      /> */}
      <select
        name="team"
        value={team}
        onChange={(e) => setTeam(e.target.value)}
        placeholder='Команда'
      >
        <option value="">Выберите команду</option>
        {teams.map((team) => (
          <option key={team.name} value={team.name}>{team.name}</option>
        ))}
      </select>
      <input
        name="description"
        type="text"
        value={description}
        onChange={(e) => setDescription(e.target.value)}
        placeholder='Описание игрока'
      />
      <button type="submit">Создать</button>
    </form>
  );
};

export default PlayerForm
