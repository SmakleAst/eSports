const Navbar = () => {
  return (
    <div>
        <nav>
            <button onClick={() => window.location.href = `/teams`}>Команды</button>
            <button onClick={() => window.location.href = `/players`}>Игроки</button>
            <button onClick={() => window.location.href = `/tournaments`}>Турниры</button>
            <button onClick={() => window.location.href = `/stats`}>Статистика команд</button>
        </nav>
    </div>
  );
};

export default Navbar