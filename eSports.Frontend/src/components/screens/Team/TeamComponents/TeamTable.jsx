import React from 'react';
import TeamList from './TeamList'

const TeamTable = ({ teams, onDeleteTeam }) => {
  return (
    <div>
      <table>
      <thead>
          <tr>
            <th>Название</th>
            <th>Страна</th>
            <th>Игроки:</th>
            <th>Участвует в турнирах:</th>
            <th></th>
            <th></th>
          </tr>
        </thead>
        <TeamList teams={teams} onDeleteTeam={onDeleteTeam} />
      </table>
    </div>
  );
};

export default TeamTable
