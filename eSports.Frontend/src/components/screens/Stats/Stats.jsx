import React, { useState, useEffect } from 'react';
import StatsService from './StatsComponents/StatsService';
import StatsFilter from './StatsComponents/StatsFilter';
import StatsTable from './StatsComponents/StatsTable';
import "/src/assets/styles/stats.css"

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
    <div class="stats-container">
      <StatsFilter filterTeam={filterTeam} handleFilterChange={handleFilterChange}/>
      <div class="stats-content-container">
        <StatsTable stats={allStats}/>
        <img src="/public/lina-item.png" alt="Your Image" class="right-image" />
      </div>
    </div>
    );
};

export default Stats