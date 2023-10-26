import React from 'react';

const TeamPage = ({ team }) => {
  return (
    <div>
      <h1>{team.name}</h1>
      <p>Country: {team.country}</p>
      {/* Дополнительная информация о команде */}
    </div>
  );
};

export default TeamPage;