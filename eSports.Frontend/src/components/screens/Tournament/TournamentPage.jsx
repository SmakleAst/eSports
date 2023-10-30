import React, { useState, useEffect } from 'react';

const TournamentPage = () => {
    const [tournaments, setTournaments] = useState([]);
    const [name, setName] = useState('');
    const [description, setDescription] = useState('');
    const [players, setPlayers] = useState('');

    return (
        <form onSubmit={handleSubmit}>
            <input name="name" type="text" value={name} onChange={(e) => setName(e.target.value)} placeholder='Name' />
            <input name="description" type="text" value={country} onChange={(e) => setDescription(e.target.value)} placeholder='Country'/>
            <input name="platers" type="text" value={country} onChange={(e) => setPlayers(e.target.value)} placeholder='Country'/>
            <button type="submit">Submit</button>
        </form>
    );
}

export default TournamentPage