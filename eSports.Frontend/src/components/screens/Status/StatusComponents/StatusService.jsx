class StatusService {
  static async getStatus() {
    try {
    const response = await fetch(`http://localhost:8081/status`);
      if (response.ok) {
        const data = await response.json();
        return data;
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

export default StatusService