import React from 'react';
import StatsList from './StatsList';
import "/src/assets/styles/stats.css"

const StatsTable = ({ stats }) => {
  return (
    <div className="stats-table-container">
      <table className="stats-table">
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