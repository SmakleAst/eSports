class TournamentService {
  static async getTournaments(filterName) {
    try {
      const response = await fetch(`https://localhost:7171/Tournament/TournamentHandler?name=${filterName}`, {
        method: 'GET',
        headers: {
          'Content-Type': 'application/json',
        },
      });
      if (response.ok) {
        const responseData = await response.json();
        const data = responseData.data;
        const tournamentsArray = Object.values(data);
        return tournamentsArray;
      } else {
        console.error('Ошибка при получении данных');
        return [];
      }
    } catch (error) {
      console.error('Ошибка при получении данных', error);
      return [];
    }
  }

  static async createTournament(tournamentData) {
    try {
      const response = await fetch('https://localhost:7171/Tournament/Create', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify({
          name: tournamentData.name,
          description: tournamentData.description,
          teams: tournamentData.selectedTeams
        }),
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

  static async deleteTournament(tournamentId) {
    try {
      const response = await fetch('https://localhost:7171/Tournament/DeleteTournament', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: tournamentId,
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

  static async getOneTournament(tournamentId) {
    try {
      const response = await fetch(`https://localhost:7171/Tournament/GetTournament/${tournamentId}`, {
        method: 'GET',
        headers: {
          'Content-Type': 'application/json',
        },
      });
      if (response.ok) {
        const responseData = await response.json();
        const tournament = responseData.data;
        return tournament;
      } else {
        console.error('Ошибка при получении данных');
        return [];
      }
    } catch (error) {
        console.error('Ошибка при получении данных', error);
        return [];
    }
  }

  static async getTeams() {
    try {
      const response = await fetch(`https://localhost:7246/Team/TeamHandler`, {
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

  static async simulateStage(tournamentId) {
    try {
      const response = await fetch('https://localhost:7171/Tournament/SimulateStage', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: tournamentId,
      });
      if (response.ok) {
        const responseData = await response.json();
        const tournament = responseData.data;
        return tournament;
      } else {
        console.error('Ошибка при отправке данных');
        return [];
      }
    } catch (error) {
      console.error(error);
      return [];
    }
  }
}

export default TournamentService