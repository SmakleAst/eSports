import React from 'react';
import StatsItem from './StatsItem';

const StatsList = ({ stats }) => {
  return (
    <tbody>
      {stats && stats.map((stat, index) => (
        <StatsItem key={index} stat={stat} />
      ))}
    </tbody>
  );
};

export default StatsList
