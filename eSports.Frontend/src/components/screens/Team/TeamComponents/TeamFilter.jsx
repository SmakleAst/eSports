import React from 'react';
import "/src/assets/styles/team.css"

const TeamFilter = ({ filters, handleFilterChange }) => {
  const { filterName, filterCountry } = filters;

  return (
    <div className="team-filter">
      <input
        name="filterName"
        type="text"
        value={filterName}
        onChange={(event) => handleFilterChange('filterName', event.target.value)}
        placeholder='Название команды' />
      <input
        name="filterCountry"
        type="text"
        value={filterCountry}
        onChange={(event) => handleFilterChange('filterCountry', event.target.value)}
        placeholder='Страна'
      />
    </div>
  );
};

export default TeamFilter