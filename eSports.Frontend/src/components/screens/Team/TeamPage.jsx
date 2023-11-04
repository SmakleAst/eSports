import { useParams } from "react-router-dom"
import React, { useEffect, useState } from "react";
import TeamPageForm from "./TeamComponents/TeamPageForm";
import TeamService from "./TeamComponents/TeamService";
import "/src/assets/styles/team.css"

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
    <div className="team-page">
      <TeamPageForm teamData={teamData} />
      <img src="/public/sf2-item.png" alt="Your Image" class="right-image" />
    </div>
  );
}

export default TeamPage