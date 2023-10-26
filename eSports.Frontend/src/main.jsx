import React from 'react'
import ReactDOM from 'react-dom/client'
import Home from './components/screens/Home/Home.jsx'
import Team from './components/screens/Team/Team.jsx'
import './assets/styles/global.css'

ReactDOM.createRoot(document.getElementById('root')).render(
  <React.StrictMode>
    <Team />
  </React.StrictMode>,
)
