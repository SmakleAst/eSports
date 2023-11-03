class StatsService {
  static async getStats(filterTeam) {
    try {
    const response = await fetch(`https://localhost:7126/Stats/StatsHandler?team=${filterTeam}`);
      if (response.ok) {
        const responseData = await response.json();
        const data = responseData.data;
        const tmpStats = Object.values(data);
        return tmpStats;
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

export default StatsService