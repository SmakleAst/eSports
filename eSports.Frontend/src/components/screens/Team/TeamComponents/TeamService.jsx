class TeamService {
    static async getTeams(filterName, filterCountry) {
      try {
        const response = await fetch(`https://localhost:7246/Team/TeamHandler?name=${filterName}&country=${filterCountry}`, {
          method: 'GET',
          headers: {
            'Content-Type': 'application/json',
          },
        });
        if (response.ok) {
          const responseData = await response.json();
          const data = responseData.data;
          const teamsArray = Object.values(data);
          return teamsArray;
        } else {
          console.error('Ошибка при получении данных');
          return [];
        }
      } catch (error) {
          console.error('Ошибка при получении данных', error);
          return [];
      }
    }
  
    static async createTeam(teamData) {
      try {
        const response = await fetch('https://localhost:7246/Team/Create', {
          method: 'POST',
          headers: {
            'Content-Type': 'application/json',
          },
          body: JSON.stringify(teamData),
        });
        if (response.ok) {
          return true;
        } else {
          console.error('Ошибка при отправке данных');
          return false;
        }
      } catch (error) {
        console.error('Ошибка при отправке данных', error);
        return false;
      }
    }
  
    static async deleteTeam(teamId) {
      try {
        const response = await fetch('https://localhost:7246/Team/DeleteTeam', {
          method: 'POST',
          headers: {
            'Content-Type': 'application/json',
          },
          body: teamId,
        });
        if (response.ok) {
          return true;
        } else {
          console.error('Ошибка при отправке данных');
          return false;
        }
      } catch (error) {
        console.error(error);
        return false;
      }
    }

    static async getOneTeam(teamId) {
      try {
        const response = await fetch(`https://localhost:7246/Team/GetTeam/${teamId}`, {
          method: 'GET',
          headers: {
            'Content-Type': 'application/json',
          },
        });
        if (response.ok) {
          const responseData = await response.json();
          const team = responseData.data;
          return team;
        } else {
          console.error('Ошибка при получении данных');
          return [];
        }
      } catch (error) {
          console.error('Ошибка при получении данных', error);
          return [];
      }
    }
  }
  
  export default TeamService