import React from 'react';
import "/src/assets/styles/stats.css"

const StatsFilter = ({ filterTeam, handleFilterChange }) => {
  return (
    <div className="stats-filter">
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