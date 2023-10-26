import { useParams } from "react-router-dom"
import React, { useEffect, useState } from "react";

const PlayerPage = () => {
    const [name, setName] = useState('');
    const [country, setCountry] = useState('');
    const [players, setPlayers] = useState([]);

    const {id} = useParams()

    useEffect(() => {
        if (!id) return

        const fetchData = async () => {
            try {
                const response = await fetch(`https://localhost:7246/Team/GetTeam/${id}`, {
                    method: 'GET',
                    headers: {
                        'Content-Type': 'application/json',
                    },
                });
                if (response.ok) {
                    const responseData = await response.json();
                    const team = responseData.data;
                    setName(team.name);
                    setCountry(team.country);
                    setPlayers(team.players);
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
              Country:
              <input type="text" value={country} readOnly />
            </label>
            <br />
            <label>
              Players:
              <input type="text" value={players} readOnly />
            </label>
          </form>
        </div>
      );
}

export default PlayerPage