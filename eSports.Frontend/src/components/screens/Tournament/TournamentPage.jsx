import { useParams } from "react-router-dom"
import React, { useState, useEffect } from 'react';

const TournamentPage = () => {
    const [tournaments, setTournaments] = useState([]);
    const [name, setName] = useState('');
    const [description, setDescription] = useState('');
    const [teams, setTeams] = useState('');

    const {id} = useParams()

    useEffect(() => {
        if (!id) return

        const fetchData = async () => {
            try {
                const response = await fetch(`https://localhost:7171/Tournament/GetTournament/${id}`, {
                    method: 'GET',
                    headers: {
                        'Content-Type': 'application/json',
                    },
                });
                if (response.ok) {
                    const responseData = await response.json();
                    const tournament = responseData.data;
                    setName(tournament.name);
                    setDescription(tournament.description);
                    setTeams(tournament.teams);
                } else {
                  console.error('Ошибка при получении данных');
                }
              } catch (error) {
                console.error('Ошибка при получении данных', error);
              }
        }

        fetchData()
    }, [id])

    return (
        <div>
          <form>
            <label>
              Name:
              <input type="text" value={name} readOnly />
            </label>
            <br />
            <label>
              Description:
              <input type="text" value={description} readOnly />
            </label>
            <br />
            <label>
              Teams:
              <input type="text" value={teams} readOnly />
            </label>
          </form>
        </div>
      );
}

export default TournamentPage