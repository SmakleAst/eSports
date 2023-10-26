import React, { useState, useEffect } from 'react';

const Team = () => {
  const [teams, setTeams] = useState([]);
  const [name, setName] = useState('');
  const [country, setCountry] = useState('');

//   const handleSubmit = async (e) => {
//     e.preventDefault();
//     try {
//       const response = await fetch('https://localhost:7246/Team/Create', {
//         method: 'POST',
//         headers: {
//           'Content-Type': 'application/json',
//         },
//         body: JSON.stringify({
//             name: name,
//             country: country
//         }),
//       });
//       if (response.ok) {
//         // Отправка данных успешна
//         setName('');
//         setCountry('');

//         useEffect();
//       } else {
//         // Обработка ошибки
//         console.error('Ошибка при отправке данных');
//       }
//     } catch (error) {
//       console.error('Ошибка при отправке данных', error);
//     }
//   };

//   useEffect(() => {
//     const fetchData = async () => {
//       try {
//         const response = await fetch('https://localhost:7246/Team/TeamHandler');
//         if (response.ok) {
//             const responseData = await response.json();
//             const data = responseData.data;
//             //const teamsArray = Object.values(data).map((team) => Object.values(team)); // Преобразование объектов в массивы
//             const teamsArray = Object.values(data);
//             console.log(teamsArray);
//             setTeams(teamsArray);
//         } else {
//           // Обработка ошибки
//           console.error('Ошибка при получении данных');
//         }
//       } catch (error) {
//         console.error('Ошибка при получении данных', error);
//       }
//     };

//     fetchData();
//   }, []);

const fetchData = async () => {
    try {
      const response = await fetch('https://localhost:7246/Team/TeamHandler');
      if (response.ok) {
        const responseData = await response.json();
        const data = responseData.data;
        const teamsArray = Object.values(data);
        setTeams(teamsArray);
      } else {
        console.error('Ошибка при получении данных');
      }
    } catch (error) {
      console.error('Ошибка при получении данных', error);
    }
  };
  
  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      const response = await fetch('https://localhost:7246/Team/Create', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify({
          name: name,
          country: country
        }),
      });
      if (response.ok) {
        setName('');
        setCountry('');
        fetchData(); // Обновление данных команды
      } else {
        console.error('Ошибка при отправке данных');
      }
    } catch (error) {
      console.error('Ошибка при отправке данных', error);
    }
  };
  
  useEffect(() => {
    fetchData(); // Вызов fetchData() при загрузке компонента
  }, []);
  

  return (
    <div>
      <form onSubmit={handleSubmit}>
        <input name="name" type="text" value={name} onChange={(e) => setName(e.target.value)} placeholder='Name' />
        <input name="country" type="text" value={country} onChange={(e) => setCountry(e.target.value)} placeholder='Country'/>
        <button type="submit">Submit</button>
      </form>
      <table>
        <thead>
          <tr>
            <th>Name</th>
            <th>Country</th>
            <th>Players</th>
          </tr>
        </thead>
        <tbody>
          {teams && teams.map((team, index) => (
            <tr key={index}>
                <td>{team.name}</td>
                <td>{team.country}</td>
                <td>
                    {/* <ul>
                    {team.players && Array.from(JSON.parse(team.players)).map((player) => (
                        <li key={player.id}>{player.name}</li>
                    ))}
                    </ul> */}
                    {team.players}
                </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
};

export default Team;
