import React from 'react';

const StatsFilter = ({ filterTeam, handleFilterChange }) => {
  return (
    <div>
      <input
          name="filterFirstTeam"
          type="text"
          value={filterTeam}
          onChange={(event) => handleFilterChange(event.target.value)}
          placeholder='Название команды'
      />
    </div>
  );
};
  

export default StatsFilter