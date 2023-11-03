import React from 'react';
import TournamentItem from './TournamentItem';

const TournamentList = ({ tournaments, onDeleteTournament }) => {
  return (
    <tbody>
      {tournaments && tournaments
      .map((tournament, index) => (
        <TournamentItem
          key={index}
          tournament={tournament}
          onDeleteTournament={onDeleteTournament}
        />
      ))}
    </tbody>
  );
};

export default TournamentList
