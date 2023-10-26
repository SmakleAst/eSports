import React, { useState } from 'react';
import Player from '../Player/Player';
import Team from '../Team/Team';
import PlayerPage from '../Player/PlayerPage';
//import Tournament from '../Tournament/Tournament';

const Home = () => {
  const [currentComponent, setCurrentComponent] = useState('Player');

  const handleComponentChange = (component) => {
    setCurrentComponent(component);
  };

  return (
    <div>
        <nav>
            <button onClick={() => window.location.href = `/players`}>Players</button>
            <button onClick={() => window.location.href = `/teams`}>Teams</button>
            <button onClick={() => window.location.href = `/tournaments`}>Tournaments</button>
        </nav>
    </div>
  );
};

export default Home;
