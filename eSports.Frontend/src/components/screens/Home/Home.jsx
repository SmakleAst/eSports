import React, { useState } from 'react';
import Player from '../Player/Player';
import Team from '../Team/Team';
//import Tournament from '../Tournament/Tournament';

const Home = () => {
  const [currentComponent, setCurrentComponent] = useState('Player');

  const handleComponentChange = (component) => {
    setCurrentComponent(component);
  };

  return (
    <div>
      <nav>
        <button onClick={() => handleComponentChange('Player')}>Players</button>
        <button onClick={() => handleComponentChange('Team')}>Teams</button>
        <button onClick={() => handleComponentChange('Tournaments')}>Tournaments</button>
      </nav>
      {currentComponent === 'Player' ? (
        <Player />
      ) : currentComponent === 'Team' ? (
        <Team />
      ) : (
        <Team />
      )}
    </div>
  );
};

export default Home;
