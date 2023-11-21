import React from 'react';
import StatusItem from './StatusItem';

const StatusList = ({ status }) => {
  return (
    <tbody>
      {status && status.map((stat, index) => (
        <StatusItem key={index} status={stat} />
      ))}
    </tbody>
  );
};

export default StatusList
