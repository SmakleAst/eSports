class PlayerService {
  static async getPlayers(filterName, filterNickName, filterAge, filterTeam) {
    try {
      const response = await fetch(`https://localhost:7160/Player/PlayerHandler?name=${filterName}&nickName=${filterNickName}&age=${filterAge}&team=${filterTeam}`);
      if (response.ok) {
        const responseData = await response.json();
        const data = responseData.data;
        const players = Object.values(data);
        return players;
      } else {
        console.error('Ошибка при получении данных');
        return [];
      }
    } catch (error) {
      console.error('Ошибка при получении данных', error);
      return [];
    }
  }
  
  static async createPlayer(playerData) {
    try {
      const response = await fetch('https://localhost:7160/Player/Create', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify(playerData),
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

  static async deletePlayer(playerId) {
    try {
      const response = await fetch('https://localhost:7160/Player/DeletePlayer', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: playerId,
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

  static async getOnePlayer(playerId) {
    try {
      const response = await fetch(`https://localhost:7160/Player/GetPlayer/${playerId}`, {
          method: 'GET',
          headers: {
              'Content-Type': 'application/json',
          },
      });
      if (response.ok) {
          const responseData = await response.json();
          const player = responseData.data;
          return player;
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
  
export default PlayerService