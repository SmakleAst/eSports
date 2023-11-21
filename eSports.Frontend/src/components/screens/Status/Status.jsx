import React, { useState, useEffect } from 'react';
import StatusService from './StatusComponents/StatusService';
import StatusTable from './StatusComponents/StatusTable';
import "/src/assets/styles/stats.css"

const Status = () => {
  const [allStatus, setAllStatus] = useState([]);

  const fetchData = async () => {
    const status = await StatusService.getStatus();
    setAllStatus(status);
  };

  useEffect(() => {
      fetchData();
  }, []);

  return (
    <div class="stats-container">
        <StatusTable status={allStatus}/>
    </div>
    );
};

export default Status