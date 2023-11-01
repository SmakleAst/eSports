import React, { useState, useEffect } from 'react';

const Stats = () => {
    const [firstTeam, setFirstTeam] = useState('');
    const [secondTeam, setSecondTeam] = useState('');
    const [firstTeamWins, setFirstTeamWins] = useState('');
    const [secondTeamWins, setSecondTeamWins] = useState('');
    const [allStats, setAllStats] = useState([]);
    const [filterTeam, setFilterTeam] = useState('');

    const fetchData = async () => {
        try {
          const response = await fetch(`https://localhost:7126/Stats/StatsHandler?team=${filterTeam}`);
          if (response.ok) {
            const responseData = await response.json();
            const data = responseData.data;
            const tmpStats = Object.values(data);
            setAllStats(tmpStats);
          } else {
            console.error('Ошибка при получении данных');
          }
        } catch (error) {
          console.error('Ошибка при получении данных', error);
        }
      };

    const handleTeamChange = (e) => {
        const value = e.target.value;
        setFilterTeam(value);
      };

    useEffect(() => {
        fetchData();
    }, [filterTeam]);

    return (
        <div>
            <input name="filterFirstTeam" type="text" value={filterTeam} onChange={handleTeamChange} placeholder='Filter by team' />
            <table>
            <thead>
                <tr>
                    <th>First Team</th>
                    <th>Wins</th>
                    <th>Wins</th>
                    <th>Second Team</th>
                </tr>
            </thead>
            <tbody>
                {allStats && allStats.map((stat, index) => (
                    <tr key={index}>
                        <td>{stat.firstTeam}</td>
                        <td>{stat.firstTeamScore}</td>
                        <td>{stat.secondTeamScore}</td>
                        <td>{stat.secondTeam}</td>
                    </tr>
                    ))}
            </tbody>
            </table>
        </div>
      );
};

export default Stats