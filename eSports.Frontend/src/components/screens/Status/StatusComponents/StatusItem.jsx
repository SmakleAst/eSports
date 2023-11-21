import React from 'react';

const StatusItem = ({ status }) => {
  return (
    <tr>
      <td>{status.serviceName}</td>
      <td>{status.status}</td>
      <td>{status.message}</td>
    </tr>
  );
};

export default StatusItem