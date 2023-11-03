import { useParams } from "react-router-dom"
import React, { useEffect, useState } from "react";
import TeamPageForm from "./TeamComponents/TeamPageForm";
import TeamService from "./TeamComponents/TeamService";

const TeamPage = () => {
  const [teamData, setTeamData] = useState({
    name: "",
    country: "",
    players: "",
    tournaments: ""
  });

  const {id} = useParams()

  useEffect(() => {
    if (!id) return

    const fetchData = async () => {
      const team = await TeamService.getOneTeam(id);
      setTeamData(team);
    }

    fetchData()
  }, [id])

  return (
    <div>
      <TeamPageForm teamData={teamData} />
    </div>
  );
}

export default TeamPage