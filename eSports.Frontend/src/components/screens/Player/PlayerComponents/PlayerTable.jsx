import React from 'react';
import PlayerList from './PlayerList';

const PlayerTable = ({ players, onDeletePlayer }) => {
  return (
    <div>
      <table>
        <thead>
          <tr>
            <th>Имя</th>
            <th>Никнейм</th>
            <th>Возраст</th>
            <th>Команда</th>
            <th>Описание</th>
            <th></th>
            <th></th>
          </tr>
        </thead>
        <PlayerList players={players} onDeletePlayer={onDeletePlayer} />
      </table>
    </div>
  );
};

export default PlayerTable
