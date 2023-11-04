import "/src/assets/styles/player.css"

const PlayerPageForm = ({ playerData }) => {
  return (
    <form className="player-page-form">
      <label className="player-page-form-label">Имя:</label>
      <input className="player-page-form-textarea-input" type="text" value={playerData.name} readOnly />
      <br />
      <label className="player-page-form-label">Никнейм:</label>
      <input className="player-page-form-textarea-input" type="text" value={playerData.nickName} readOnly />
      <br />
      <label className="player-page-form-label">Возраст:</label>
      <input className="player-page-form-textarea-input" type="text" value={playerData.age} readOnly />
      <br />
      <label className="player-page-form-label">Команда:</label>
      <input className="player-page-form-textarea-input" type="text" value={playerData.team} readOnly />
      <br />
      <label className="player-page-form-label">Об игроке:</label>
      <textarea className="player-page-form-textarea-input" value={playerData.description} readOnly />
    </form>
  );
};

export default PlayerPageForm
