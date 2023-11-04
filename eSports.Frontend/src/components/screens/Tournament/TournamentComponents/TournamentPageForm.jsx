import "/src/assets/styles/tournament.css"

const TournamentPageForm = ({ tournamentData }) => {
  return (
    <form className="tournament-page-form">
      <input
        className="tournament-page-form-textarea-input"
        type="text"
        value={tournamentData.name}
        readOnly
      />
      <br />
      <textarea
        className="tournament-page-form-textarea-input"
        type="text"
        value={tournamentData.description}
        readOnly
      />
      <br />
      <label className="team-page-form-label">Участники:</label>
      <textarea
        className="tournament-page-form-textarea-participants"
        type="text"
        value={tournamentData.teams}
        readOnly
      />
    </form>     
  );
};

export default TournamentPageForm
