import { BrowserRouter, Route, Routes } from "react-router-dom";
import Player from "../screens/Player/Player";
import PlayerPage from "../screens/Player/PlayerPage";
import Team from "../screens/Team/Team";
import TeamPage from "../screens/Team/TeamPage";
import Tournament from "../screens/Tournament/Tournament";
import TournamentPage from "../screens/Tournament/TournamentPage";
import Navbar from "./Navbar";
import Stats from "../screens/Stats/Stats";
import Home from "../screens/Home/Home";


const Router = () => {
  return <BrowserRouter>
    <Navbar />
    <Routes>
      <Route element={<Home />} path='/' />

      <Route element={<Player />} path='/players' />
      <Route element={<PlayerPage />} path='/playerPage/:id' />

      <Route element={<Team />} path='/teams' />
      <Route element={<TeamPage />} path='/teamPage/:id' />

      <Route element={<Tournament />} path='/tournaments' />
      <Route element={<TournamentPage />} path='/tournamentPage/:id' />

      <Route element={<Stats />} path='/stats' />

      <Route path='*' element={<div>Not found</div>}/>
      </Routes>
  </BrowserRouter>
}

export default Router