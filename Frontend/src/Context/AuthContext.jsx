import { createContext, useContext, useState } from "react";

const AuthContext = createContext(null);

export function AuthProvider({ children }) {
  const [auth, setAuth] = useState(() => {
    const token = localStorage.getItem("token");
    const contactId = localStorage.getItem("contactId");
    const clientId = localStorage.getItem("clientId");
    return token ? { token, contactId, clientId } : null;
  });

  const login = (data) => {
    localStorage.setItem("token", data.token);
    localStorage.setItem("contactId", data.contactId);
    localStorage.setItem("clientId", data.clientId);
    setAuth(data);
  };

  const logout = () => {
    localStorage.removeItem("token");
    localStorage.removeItem("contactId");
    localStorage.removeItem("clientId");
    setAuth(null);
  };

  return (
    <AuthContext.Provider value={{ auth, login, logout }}>
      {children}
    </AuthContext.Provider>
  );
}

export function useAuth() {
  return useContext(AuthContext);
}
