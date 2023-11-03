import React, { useState, useEffect } from 'react';

const TeamForm = ({ onCreateTeam }) => {
  const [name, setName] = useState('');
  const [country, setCountry] = useState('');

  const handleSubmit = async (e) => {
    e.preventDefault();

    const teamData = {
      name,
      country
    };

    onCreateTeam(teamData);

    setName('');
    setCountry('');
  };
  
  return (
    <form onSubmit={handleSubmit}>
      <input
        name="name"
        type="text"
        value={name}
        onChange={(e) => setName(e.target.value)}
        placeholder='Имя'
      />
      <input
        name="country"
        type="text"
        value={country}
        onChange={(e) => setCountry(e.target.value)}
        placeholder='Страна'
      />
      <button type="submit">Submit</button>
    </form>
  );
};

export default TeamForm
