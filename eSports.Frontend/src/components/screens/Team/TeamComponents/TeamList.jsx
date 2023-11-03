import React from 'react';
import TeamItem from './TeamItem';

const TeamList = ({ teams, onDeleteTeam }) => {
  return (
    <tbody>
      {teams && teams
        .map((team, index) => (
        <TeamItem key={index} team={team} onDeleteTeam={onDeleteTeam} />
      ))}
    </tbody>
  );
};

export default TeamList
