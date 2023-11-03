import { useParams } from "react-router-dom"
import React, { useEffect, useState } from "react";
import PlayerPageForm from "./PlayerComponents/PlayerPageForm";
import PlayerService from "./PlayerComponents/PlayerService";

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
    <div>
      <PlayerPageForm playerData={playerData} />
    </div>
  );
}

export default PlayerPage
