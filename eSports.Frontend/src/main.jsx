import React from 'react'
import ReactDOM from 'react-dom/client'
import Home from './components/screens/Home/Home.jsx'
import Team from './components/screens/Team/Team.jsx'
import Player from './components/screens/Player/Player.jsx'
import './assets/styles/global.css'
import Router from './components/Router.jsx'

ReactDOM.createRoot(document.getElementById('root')).render(
  <React.StrictMode>
    <Router />
  </React.StrictMode>,
)