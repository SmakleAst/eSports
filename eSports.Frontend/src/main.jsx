import React from 'react'
import ReactDOM from 'react-dom/client'
import Team from './components/screens/Team/Team.jsx'
import './assets/styles/global.css'
import Player from './components/screens/Player/Player.jsx'

ReactDOM.createRoot(document.getElementById('root')).render(
  <React.StrictMode>
    <Player />
  </React.StrictMode>,
)