import React, { useState, useEffect } from 'react';

const Team = () => {
    const [teams, setTeams] = useState([]);
    const [name, setName] = useState('');
    const [country, setCountry] = useState('');
    const [filterName, setFilterName] = useState('');
    const [filterCountry, setFilterCountry] = useState('');

    const fetchData = async () => {
        try {
        const response = await fetch(`https://localhost:7246/Team/TeamHandler?name=${filterName}&country=${filterCountry}`, {
            method: 'GET',
            headers: {
              'Content-Type': 'application/json',
            },
          });
        if (response.ok) {
            const responseData = await response.json();
            const data = responseData.data;
            const teamsArray = Object.values(data);
            setTeams(teamsArray);
        } else {
            console.error('Ошибка при получении данных');
        }
        } catch (error) {
        console.error('Ошибка при получении данных', error);
        }
    };
  
    const handleSubmit = async (e) => {
        e.preventDefault();
        try {
        const response = await fetch('https://localhost:7246/Team/Create', {
            method: 'POST',
            headers: {
            'Content-Type': 'application/json',
            },
            body: JSON.stringify({
            name: name,
            country: country
            }),
        });
        if (response.ok) {
            setName('');
            setCountry('');
            fetchData();
        } else {
            console.error('Ошибка при отправке данных');
        }
        } catch (error) {
        console.error('Ошибка при отправке данных', error);
        }
    };

    const handleDelete = async (index) => {
        try {
            const response = await fetch('https://localhost:7246/Team/DeleteTeam', {
                    method: 'POST',
                    headers: {
                      'Content-Type': 'application/json',
                    },
                    body: index,
                });
            if (response.ok) {
                const updatedTeams = [...teams];
                updatedTeams.splice(index, 1);
                setTeams(updatedTeams);
                fetchData();
            } else {
                console.error('Ошибка при отправке данных');
            }
        } catch (error) {
            console.error(error);
        }
    };
    
    const handleNameChange = (e) => {
        const value = e.target.value;
        setFilterName(value);
    };
      
    const handleCountryChange = (e) => {
        const value = e.target.value;
        setFilterCountry(value);
    };

    useEffect(() => {
        fetchData();
    }, [filterName, filterCountry]);
  

  return (
    <div>
      <form onSubmit={handleSubmit}>
        <input name="name" type="text" value={name} onChange={(e) => setName(e.target.value)} placeholder='Name' />
        <input name="country" type="text" value={country} onChange={(e) => setCountry(e.target.value)} placeholder='Country'/>
        <button type="submit">Submit</button>
      </form>
      <input name="filterName" type="text" value={filterName} onChange={handleNameChange} placeholder='Filter by Name' />
      <input name="filterCountry" type="text" value={filterCountry} onChange={handleCountryChange} placeholder='Filter by Country' />
      <table>
        <thead>
          <tr>
            <th>Name</th>
            <th>Country</th>
            <th>Players</th>
            <th>Tournaments</th>
            <th></th>
            <th></th>
          </tr>
        </thead>
        <tbody>
          {teams && teams
          .map((team, index) => (
            <tr key={index}>
                <td>{team.name}</td>
                <td>{team.country}</td>
                <td>{team.players}</td>
                <td>{team.tournaments}</td>
                <td>
                    <button onClick={() => window.location.href = `/teamPage/${team.id}`}>View</button>
                </td>
                <td>
                    <button onClick={() => handleDelete(team.id)}>Delete</button>
                </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
};

export default Team
