import "/src/assets/styles/team.css"

const TeamPageForm = ({ teamData }) => {
  return (
    <form className="team-page-form">
      <input className="team-page-form-textarea-input" type="text" value={teamData.name} readOnly />
      <br />
      <input className="team-page-form-textarea-input" type="text" value={teamData.country} readOnly />
      <br />
      <label className="team-page-form-label">Игроки:</label>
      <textarea className="team-page-form-textarea-input" type="text" value={teamData.players} readOnly />
      <br />
      <label className="team-page-form-label">Участвует в турнирах:</label>
      <textarea className="team-page-form-textarea-input" type="text" value={teamData.tournaments} readOnly />
    </form>
  );
};

export default TeamPageForm
