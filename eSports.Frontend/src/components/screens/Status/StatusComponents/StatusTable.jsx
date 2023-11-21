import React from 'react';
import StatusList from './StatusList';
import "/src/assets/styles/stats.css"

const StatusTable = ({ status }) => {
  return (
    <div className="stats-table-container">
      <table className="stats-table">
        <thead>
          <tr>
            <th>Микросервис</th>
            <th>Состояние</th>
            <th>Тип ошибки</th>
          </tr>
        </thead>
        <StatusList status={status} />
      </table>
    </div>
  );
};

export default StatusTable