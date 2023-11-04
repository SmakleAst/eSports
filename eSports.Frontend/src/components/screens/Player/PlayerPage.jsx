import { useParams } from "react-router-dom"
import React, { useEffect, useState } from "react";
import PlayerPageForm from "./PlayerComponents/PlayerPageForm";
import PlayerService from "./PlayerComponents/PlayerService";
import "/src/assets/styles/player.css"

const PlayerPage = () => {
  const [playerData, setPlayerData] = useState({
    name: "",
    nickName: "",
    age: "",
    team: "",
    description: ""
  });

  const { id } = useParams()

  useEffect(() => {
    if (!id) return

    const fetchData = async () => {
      const player = await PlayerService.getOnePlayer(id);
      setPlayerData(player);
    }

    fetchData()
  }, [id])

  return (
    <div className="player-page">
      <PlayerPageForm playerData={playerData} />
      <img src="/public/morph-item.png" alt="Your Image" class="right-image" />
    </div>
  );
}

export default PlayerPage
