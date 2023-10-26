import { BrowserRouter, Route, Routes } from "react-router-dom";
import Player from "./screens/Player/Player";
import Home from "./screens/Home/Home";
import PlayerPage from "./screens/Player/PlayerPage";
import Team from "./screens/Team/Team";

const Router = () => {
    return <BrowserRouter>
        <Home />
        <Routes>
            <Route element={<Player />} path='/players' />
            <Route element={<Team />} path='/teams' />
            <Route element={<PlayerPage />} path='/playerPage/:id' />

            <Route path='*' element={<div>Not found</div>}/>
        </Routes>
    </BrowserRouter>
}

export default Router