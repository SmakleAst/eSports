import React from 'react';

const TournamentFilter = ({ filterName, handleFilterChange }) => {
    return (
      <div>
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