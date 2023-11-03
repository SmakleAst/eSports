import React from 'react';

const TournamentItem = ({ tournament, onDeleteTournament }) => {
  return (
    <tr>
      <td>{tournament.name}</td>
      <td>{tournament.description}</td>
      <td>{tournament.teams}</td>
      <td>
        <button onClick={() => window.location.href = `/tournamentPage/${tournament.id}`}>Подробнее</button>
      </td>
      <td>
        <button onClick={() => onDeleteTournament(tournament.id)}>Удалить</button>
      </td>
    </tr>
  );
};

export default TournamentItem