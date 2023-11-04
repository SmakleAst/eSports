import React from 'react';
import PlayerList from './PlayerList';
import "/src/assets/styles/player.css"

const PlayerTable = ({ players, onDeletePlayer }) => {
  return (
    <div className="player-table-container">
      <table className="player-table">
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
