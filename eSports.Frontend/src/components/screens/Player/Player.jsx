import React, { useState, useEffect } from 'react';
import axios from 'axios';

const Player = () => {
    const [name, setName] = useState('');
    const [nickName, setNickname] = useState('');
    const [age, setAge] = useState('');
    const [team, setTeam] = useState('');
    const [description, setDescription] = useState('');
    const [players, setPlayers] = useState([]);

    const fetchData = async () => {
        try {
          const response = await fetch('https://localhost:7160/Player/PlayerHandler');
          if (response.ok) {
            const responseData = await response.json();
            const data = responseData.data;
            const players = Object.values(data);
            setPlayers(players);
          } else {
            console.error('Ошибка при получении данных');
          }
        } catch (error) {
          console.error('Ошибка при получении данных', error);
        }
      };

    const handleSubmit = async (e) => {
        e.preventDefault();
      
        const playerData = {
          name,
          nickName,
          age,
          team,
          description,
        };
      
        try {
            const response = await fetch('https://localhost:7160/Player/Create', {
                method: 'POST',
                headers: {
                  'Content-Type': 'application/json',
                },
                body: JSON.stringify({
                    name: name,
                    nickName: nickName,
                    age: age,
                    team: team,
                    description: description,
                }),
              });


            if (response.ok) {
                setName('');
                setNickname('');
                setAge('');
                setTeam('');
                setDescription('');
                fetchData();
            } else {
                console.error('Ошибка при отправке данных');
            }
        } catch (error) {
          console.error(error);
        }
      };

    useEffect(() => {
    fetchData();
    }, []);

    return (
        <div>
        <form onSubmit={handleSubmit}>
            <input name="name" type="text" value={name} onChange={(e) => setName(e.target.value)} placeholder='Name' />
            <input name="nickName" type="text" value={nickName} onChange={(e) => setNickname(e.target.value)} placeholder='NickName'/>
            <input name="age" type="text" value={age} onChange={(e) => setAge(e.target.value)} placeholder='Age'/>
            <input name="team" type="text" value={team} onChange={(e) => setTeam(e.target.value)} placeholder='Team'/>
            <input name="description" type="text" value={description} onChange={(e) => setDescription(e.target.value)} placeholder='Description'/>
            <button type="submit">Submit</button>
        </form>
    
          <table>
            <thead>
              <tr>
                <th>Name</th>
                <th>NickName</th>
                <th>Age</th>
                <th>Team</th>
                <th>Description</th>
              </tr>
            </thead>
            <tbody>
                {players && players.map((player, index) => (
                <tr key={index}>
                    <td>{player.name}</td>
                    <td>{player.nickName}</td>
                    <td>{player.age}</td>
                    <td>{player.team}</td>
                    <td>{player.description}</td>
                </tr>
            ))}
            </tbody>
          </table>
        </div>
      );
};

export default Player;