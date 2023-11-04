import React from 'react';
import "/src/assets/styles/tournament.css"

const TournamentFilter = ({ filterName, handleFilterChange }) => {
    return (
      <div className="tournament-filter">
        <input
          name="filterName"
          type="text"
          value={filterName}
          onChange={(event) => handleFilterChange(event.target.value)}
          placeholder='Название команды'
        />
      </div>
    );
  };
  

export default TournamentFilter