import { useRouter } from "@tanstack/react-router";
import { useAuth } from "../providers/AuthProvider";

const LogoutButton = () => {
  const router = useRouter();
  const auth = useAuth();

  const handleLogout = async () => {
    await auth.logout().then(() => {
      router.invalidate().finally(() => {
        router.navigate({ to: "/" });
      });
    });
  };
  return <button onClick={handleLogout}>Logout</button>;
};

export default LogoutButton;
