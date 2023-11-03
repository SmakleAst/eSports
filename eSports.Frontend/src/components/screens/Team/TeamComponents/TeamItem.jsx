import React from 'react';

const TeamItem = ({ team, onDeleteTeam }) => {
  return (
    <tr>
      <td>{team.name}</td>
      <td>{team.country}</td>
      <td>{team.players}</td>
      <td>{team.tournaments}</td>
      <td>
        <button onClick={() => window.location.href = `/teamPage/${team.id}`}>Подробнее</button>
      </td>
      <td>
        <button onClick={() => onDeleteTeam(team.id)}>Удалить</button>
      </td>
    </tr>
  );
};

export default TeamItem