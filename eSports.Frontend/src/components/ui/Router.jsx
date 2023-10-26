import { BrowserRouter, Route, Routes } from "react-router-dom";
import Player from "../screens/Player/Player";
import PlayerPage from "../screens/Player/PlayerPage";
import Team from "../screens/Team/Team";
import TeamPage from "../screens/Team/TeamPage";
import Navbar from "./Navbar";

const Router = () => {
    return <BrowserRouter>
        <Navbar />
        <Routes>
            <Route element={<Player />} path='/players' />
            <Route element={<Team />} path='/teams' />
            <Route element={<PlayerPage />} path='/playerPage/:id' />
            <Route element={<TeamPage />} path='/teamPage/:id' />

            <Route path='*' element={<div>Not found</div>}/>
        </Routes>
    </BrowserRouter>
}

export default Router