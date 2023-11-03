import React from 'react';
import StatsList from './StatsList';

const StatsTable = ({ stats }) => {
  return (
    <div>
      <table>
        <thead>
          <tr>
            <th></th>
            <th>Побед</th>
            <th>Побед</th>
            <th></th>
          </tr>
        </thead>
        <StatsList stats={stats} />
      </table>
    </div>
  );
};

export default StatsTable