import { Link, useNavigate } from "react-router-dom";
import { useAuth } from "../context/AuthContext";

export default function Navbar() {
  const { logout } = useAuth();
  const navigate = useNavigate();

  const handleLogout = () => {
    logout();
    navigate("/login");
  };

  return (
    <nav className="bg-blue-600 text-white px-6 py-3 flex justify-between items-center">
      <span className="font-bold">WK Services</span>
      <div className="flex gap-4">
        <Link to="/new-order" className="hover:underline">
          New Order
        </Link>
        <Link to="/search-order" className="hover:underline">
          Search Order
        </Link>
        <button onClick={handleLogout} className="hover:underline">
          Logout
        </button>
      </div>
    </nav>
  );
}
