const Navbar = () => {
  return (
    <div>
        <nav>
            <button onClick={() => window.location.href = `/teams`}>Teams</button>
            <button onClick={() => window.location.href = `/players`}>Players</button>
            <button onClick={() => window.location.href = `/tournaments`}>Tournaments</button>
        </nav>
    </div>
  );
};

export default Navbar