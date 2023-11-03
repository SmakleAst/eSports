const TournamentPageForm = ({ tournamentData }) => {
  return (
    <form>
      <label>
        Название:
        <input type="text" value={tournamentData.name} readOnly />
      </label>
      <br />
      <label>
        Описание:
        <input type="text" value={tournamentData.description} readOnly />
      </label>
      <br />
      <label>
        Участники:
        <input type="text" value={tournamentData.teams} readOnly />
      </label>
    </form>
          
  );
};

export default TournamentPageForm
