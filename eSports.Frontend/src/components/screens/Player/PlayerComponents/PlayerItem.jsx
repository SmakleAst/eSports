import React from 'react';

const PlayerItem = ({ player, onDeletePlayer }) => {
  return (
    <tr>
      <td>{player.name}</td>
      <td>{player.nickName}</td>
      <td>{player.age}</td>
      <td>{player.team}</td>
      <td>{player.description}</td>
      <td>
        <button onClick={() => window.location.href = `/playerPage/${player.id}`}>Подробнее</button>
      </td>
      <td>
        <button onClick={() => onDeletePlayer(player.id)}>Удалить</button>
      </td>
    </tr>
  );
};

export default PlayerItem;