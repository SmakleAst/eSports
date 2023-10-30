import React, { useState, useEffect } from 'react';

const Tournament = () => {
    const [teams, setTeams] = useState([]);
    const [tournaments, setTournaments] = useState([]);
    const [name, setName] = useState('');
    const [description, setDescription] = useState('');
    const [selectedTeams, setSelectedTeams] = useState([]);
    const [filterName, setFilterName] = useState('');

    const fetchTeams = async () => {
        try {
        const response = await fetch(`https://localhost:7246/Team/TeamHandler`, {
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

    const fetchData = async () => {
        try {
            const response = await fetch(`https://localhost:7171/Tournament/TournamentHandler?name=${filterName}`, {
                method: 'GET',
                headers: {
                    'Content-Type': 'application/json',
                },
            });
        if (response.ok) {
            const responseData = await response.json();
            const data = responseData.data;
            const tournamentsArray = Object.values(data);
            setTournaments(tournamentsArray);
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
            const response = await fetch('https://localhost:7171/Tournament/Create', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify({
                    name: name,
                    description: description,
                    teams: selectedTeams
                }),
            });
            if (response.ok) {
                setName('');
                setDescription('');
                fetchData();
            } else {
                console.error('Ошибка при отправке данных');
            }
        } catch (error) {
            console.error('Ошибка при отправке данных', error);
        }
    };

    const handleDelete = async (id) => {
        try {
            const response = await fetch('https://localhost:7171/Tournament/DeleteTournament', {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json',
                    },
                    body: id,
                });
            if (response.ok) {
                const updatedTournaments = [...tournaments];
                const index = updatedTournaments.findIndex(tournament => tournament.id === id);
                updatedTournaments.splice(index, 1);
                setTournaments(updatedTournaments);
                fetchData();
            } else {
                console.error('Ошибка при отправке данных');
            }
        } catch (error) {
            console.error(error);
        }
    };

    const handleCheckboxChange = (event) => {
        const teamName = event.target.value;
        if (event.target.checked) {
          setSelectedTeams([...selectedTeams, teamName]);

        } else {
          setSelectedTeams(selectedTeams.filter(team => team !== teamName));
        }
      };
    
    const handleNameChange = (e) => {
        const value = e.target.value;
        setFilterName(value);
    };
    
    useEffect(() => {
        fetchData();
        fetchTeams();
    }, [selectedTeams, filterName]);

    return (
        <div>
            <form onSubmit={handleSubmit}>
                <input name="name" type="text" value={name} onChange={(e) => setName(e.target.value)} placeholder="Name" />
                <input name="description" type="text" value={description} onChange={(e) => setDescription(e.target.value)} placeholder="Description" />
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
            <input name="filterName" type="text" value={filterName} onChange={handleNameChange} placeholder='Filter by Name' />
            <table>
                <thead>
                <tr>
                    <th>Name</th>
                    <th>Description</th>
                    <th>Teams</th>
                    <th></th>
                    <th></th>
                </tr>
                </thead>
                <tbody>
                {tournaments && tournaments
                .map((tournament, index) => (
                    <tr key={index}>
                        <td>{tournament.name}</td>
                        <td>{tournament.description}</td>
                        <td>{tournament.teams}</td>
                        <td>
                            <button onClick={() => window.location.href = `/tournamentPage/${tournament.id}`}>View</button>
                        </td>
                        <td>
                            <button onClick={() => handleDelete(tournament.id)}>Delete</button>
                        </td>
                    </tr>
                ))}
                </tbody>
            </table>
        </div>
        
    );
}

export default Tournament