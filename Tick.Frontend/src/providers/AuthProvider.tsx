import {
  createContext,
  useCallback,
  useContext,
  useEffect,
  useState,
} from "react";
import { LoginCredentials } from "../types/authTypes";
import { fetchAuthStatusFn, loginUserFn, logoutUserFn } from "../auth";

export interface AuthContext {
  login: (c: { email: string; password: string }) => Promise<void>;
  logout: () => Promise<void>;
  user: string | null;
}

const AuthContext = createContext<AuthContext | null>(null);

export function AuthProvider({ children }: { children: React.ReactNode }) {
  const [user, setUser] = useState<string | null>(null);
  const [isPending, setIsPending] = useState(true);

  const logout = useCallback(async () => {
    await logoutUserFn();
    setUser(null);
  }, []);

  const login = useCallback(async (credentials: LoginCredentials) => {
    const username = await loginUserFn(credentials);
    setUser(username);
  }, []);

  useEffect(() => {
    const fetchUser = async () => {
      const userData = await fetchAuthStatusFn();
      setUser(userData);
      setIsPending(false);
    };

    fetchUser();
  }, []);

  if (isPending) return <></>;

  return (
    <AuthContext.Provider value={{ user, login, logout }}>
      {children}
    </AuthContext.Provider>
  );
}

export function useAuth() {
  const context = useContext(AuthContext);
  if (!context) {
    throw new Error("useAuth must be used within an AuthProvider");
  }
  return context;
}
