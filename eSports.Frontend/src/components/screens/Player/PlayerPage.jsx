import { useParams } from "react-router-dom"
import React, { useEffect, useState } from "react";

const PlayerPage = () => {
    const [name, setName] = useState("");
    const [nickName, setNickname] = useState("");
    const [age, setAge] = useState("");
    const [team, setTeam] = useState("");
    const [description, setDescription] = useState("");

    const {id} = useParams()

    useEffect(() => {
        if (!id) return

        const fetchData = async () => {
            try {
                const response = await fetch(`https://localhost:7160/Player/GetPlayer/${id}`, {
                    method: 'GET',
                    headers: {
                        'Content-Type': 'application/json',
                    },
                });
                if (response.ok) {
                    const responseData = await response.json();
                    const player = responseData.data;
                    setName(player.name);
                    setNickname(player.nickName);
                    setAge(player.age);
                    setTeam(player.team);
                    setDescription(player.description);
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
              Nickname:
              <input type="text" value={nickName} readOnly />
            </label>
            <br />
            <label>
              Age:
              <input type="text" value={age} readOnly />
            </label>
            <br />
            <label>
              Team:
              <input type="text" value={team} readOnly />
            </label>
            <br />
            <label>
              Description:
              <textarea value={description} readOnly />
            </label>
          </form>
        </div>
      );
}

export default PlayerPage