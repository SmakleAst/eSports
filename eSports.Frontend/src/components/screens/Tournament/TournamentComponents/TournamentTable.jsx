import React from 'react';
import TournamentList from './TournamentList'
import "/src/assets/styles/tournament.css"

const TournamentTable = ({ tournaments, onDeleteTournament }) => {
  return (
    <div className="tournament-table-container">
      <table className="tournament-table">
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
