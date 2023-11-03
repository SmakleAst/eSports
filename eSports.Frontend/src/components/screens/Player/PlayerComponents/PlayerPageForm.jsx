import React, { useState, useEffect } from 'react';

const PlayerPageForm = ({ playerData }) => {
  return (
    <form>
      <label>
        Имя:
        <input type="text" value={playerData.name} readOnly />
      </label>
      <br />
      <label>
        Никнейм:
        <input type="text" value={playerData.nickName} readOnly />
      </label>
      <br />
      <label>
        Возраст:
        <input type="text" value={playerData.age} readOnly />
      </label>
      <br />
      <label>
        Команда:
        <input type="text" value={playerData.team} readOnly />
      </label>
      <br />
      <label>
        Об игроке:
        <textarea value={playerData.description} readOnly />
      </label>
    </form>
  );
};

export default PlayerPageForm
