import React from 'react';
import PlayerItem from './PlayerItem';

const PlayerList = ({ players, onDeletePlayer }) => {


  return (
    <tbody>
      {players.map((player, index) => (
        <PlayerItem key={index} player={player} onDeletePlayer={onDeletePlayer} />
      ))}
    </tbody>
  );
};

export default PlayerList
