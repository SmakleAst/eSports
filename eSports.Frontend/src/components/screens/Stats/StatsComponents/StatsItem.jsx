import React from 'react';

const StatsItem = ({ stat }) => {
  return (
    <tr>
      <td>{stat.firstTeam}</td>
      <td>{stat.firstTeamScore}</td>
      <td>{stat.secondTeamScore}</td>
      <td>{stat.secondTeam}</td>
    </tr>
  );
};

export default StatsItem