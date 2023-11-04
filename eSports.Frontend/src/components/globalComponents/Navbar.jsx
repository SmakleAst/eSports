import './styles/navbar.css'

const Navbar = () => {
  return (
    <div className="navbar">
      <nav>
        <button onClick={() => window.location.href = `/`}>Главная</button>
        <button onClick={() => window.location.href = `/teams`}>Команды</button>
        <button onClick={() => window.location.href = `/players`}>Игроки</button>
        <button onClick={() => window.location.href = `/tournaments`}>Турниры</button>
        <button onClick={() => window.location.href = `/stats`}>Статистика команд</button>
      </nav>
    </div>
  );
};

export default Navbar