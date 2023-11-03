import React, { useState, useEffect } from 'react';

const TeamPageForm = ({ teamData }) => {
  return (
    <form>
      <label>
        Название:
        <input type="text" value={teamData.name} readOnly />
      </label>
      <br />
      <label>
        Страна:
        <input type="text" value={teamData.country} readOnly />
      </label>
      <br />
      <label>
        Игроки:
        <input type="text" value={teamData.players} readOnly />
      </label>
      <br />
      <label>
        Участвует в турнирах:
        <input type="text" value={teamData.tournaments} readOnly />
      </label>
    </form>
  );
};

export default TeamPageForm
