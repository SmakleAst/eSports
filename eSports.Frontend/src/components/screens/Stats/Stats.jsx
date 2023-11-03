import React, { useState, useEffect } from 'react';
import StatsService from './StatsComponents/StatsService';
import StatsFilter from './StatsComponents/StatsFilter';
import StatsTable from './StatsComponents/StatsTable';

const Stats = () => {
  const [allStats, setAllStats] = useState([]);
  const [filterTeam, setFilterTeam] = useState('');

  const fetchData = async () => {
    const players = await StatsService.getStats(filterTeam);
    setAllStats(players);
  };

  const handleFilterChange = (team) => {
    setFilterTeam(team);
  };

  useEffect(() => {
      fetchData();
  }, [filterTeam]);

  return (
      <div>
        <StatsFilter filterTeam={filterTeam} handleFilterChange={handleFilterChange}/>
        <StatsTable stats={allStats}/>
      </div>
    );
};

export default Stats