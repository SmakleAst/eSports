
const PlayerFilter = ({ filters, handleFilterChange }) => {
  const { filterName, filterNickName, filterAge, filterTeam } = filters;

  return (
    <div>
      <input
        type="text"
        value={filterName}
        onChange={(event) => handleFilterChange('filterName', event.target.value)}
        placeholder='Имя'
      />
      <input
        type="text"
        value={filterNickName}
        onChange={(event) => handleFilterChange('filterNickName', event.target.value)}
        placeholder='Никнейм'
      />
      <input
        type="text"
        value={filterAge}
        onChange={(event) => handleFilterChange('filterAge', event.target.value)}
        placeholder='Возраст'
      />
      <input
        type="text"
        value={filterTeam}
        onChange={(event) => handleFilterChange('filterTeam', event.target.value)} 
        placeholder='Команда'
      />
    </div>
  );
};
  

export default PlayerFilter