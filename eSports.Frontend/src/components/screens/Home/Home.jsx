import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { v4 as uuidv4 } from 'uuid';

const fetchPlayers = async (setPlayers, setIsLoading) => {
  try {
    const response = await axios.get('https://localhost:7160/Player/PlayerHandler');
    if (Array.isArray(response.data)) {
      setPlayers(response.data);
    } else if (typeof response.data === 'object') {
      setPlayers(Object.values(response.data));
    } else {
      setPlayers([]);
    }
    setIsLoading(false);
  } catch (error) {
    console.error(error);
  }
};

function Home() {
  const [players, setPlayers] = useState([]);
  const [createPlayerViewModel, setNewPlayer] = useState("");
  const [isLoading, setIsLoading] = useState(true);

  useEffect(() => {
    fetchPlayers(setPlayers, setIsLoading);
  },);

  const handleSubmit = event => {
    event.preventDefault();
    console.log(createPlayerViewModel);
    axios.post('https://localhost:7160/Player/Create', createPlayerViewModel)
      .then(response => {
        setPlayers([...players, response.data]);
        setNewPlayer({});
        fetchPlayers(setPlayers, setIsLoading);
      })
      .catch(error => {
        console.error(error);
      });
  };

  const handleInputChange = event => {
    const { name, value } = event.target;
    setNewPlayer(prevPlayer => ({
      ...prevPlayer,
      [name]: value
    }));
  };

  return (
    <div>
      {isLoading ? ( // Проверяем состояние загрузки данных
        <p>Загрузка данных...</p>
      ) : (
      <div>
        <form onSubmit={handleSubmit}>
          <label>
            Имя игрока:
            <input type="text" name="name" value={createPlayerViewModel.name || ''} onChange={handleInputChange} />
          </label>
          <label>
            Никнейм игрока:
            <input type="text" name="nickName" value={createPlayerViewModel.nickName || ''} onChange={handleInputChange} />
          </label>
          <label>
            Возраст игрока:
            <input type="text" name="age" value={createPlayerViewModel.age || ''} onChange={handleInputChange} />
          </label>
          <label>
            Команда игрока:
            <input type="text" name="team" value={createPlayerViewModel.team || ''} onChange={handleInputChange} />
          </label>
          <label>
            Описание игрока:
            <input type="text" name="description" value={createPlayerViewModel.description || ''} onChange={handleInputChange} />
          </label>
          <button type="submit">Создать</button>
        </form>

        <table>
          <thead>
            <tr>
              <th>Id</th>
              <th>Имя</th>
              <th>Никнейм</th>
              <th>Возраст</th>
              <th>Команда</th>
              <th>Описание</th>
            </tr>
          </thead>
          <tbody>
            {players.map(player => (
              <tr key={uuidv4()}>
                <td>{player.id}</td>
                <td>{player.name}</td>
                <td>{player.nickname}</td>
                <td>{player.age}</td>
                <td>{player.team}</td>
                <td>{player.description}</td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    )}
  </div>
  );
}

export default Home;