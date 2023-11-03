import React from 'react';
import TournamentList from './TournamentList'

const TournamentTable = ({ tournaments, onDeleteTournament }) => {
  return (
    <div>
      <table>
        <thead>
          <tr>
            <th>Название</th>
            <th>Описание турнира</th>
            <th>Участники</th>
            <th></th>
            <th></th>
          </tr>
        </thead>
        <TournamentList
          tournaments={tournaments}
          onDeleteTournament={onDeleteTournament}
        />
      </table>
    </div>
  );
};

export default TournamentTable
