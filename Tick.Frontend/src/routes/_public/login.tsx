import {
  createFileRoute,
  useRouter,
  useRouterState,
} from "@tanstack/react-router";
import { useState } from "react";
import { useAuth } from "../../providers/AuthProvider";

export const Route = createFileRoute("/_public/login")({
  component: LoginComponent,
});

function LoginComponent() {
  const [isSubmitting, setIsSubmitting] = useState(false);

  const router = useRouter();
  const isLoading = useRouterState({ select: (s) => s.isLoading });
  const navigate = Route.useNavigate();

  const auth = useAuth();

  const onFormSubmit = async (evt: React.FormEvent<HTMLFormElement>) => {
    setIsSubmitting(true);
    try {
      evt.preventDefault();
      const data = new FormData(evt.currentTarget);

      const emailField = data.get("email");
      const passwordField = data.get("password");

      if (!emailField || !passwordField) return;

      const parsedEmail = emailField.toString();
      const parsedPassword = passwordField.toString();

      await auth.login({ email: parsedEmail, password: parsedPassword });
      router.invalidate();
      //navigate({ to: "/about" });
      setTimeout(navigate, 0, { to: "/about" });
    } catch (error) {
      console.error("Error logging in: ", error);
    } finally {
      setIsSubmitting(false);
    }
  };

  const isLoggingIn = isLoading || isSubmitting;

  return (
    <div className="p-2 grid gap-2 place-items-center">
      <h3 className="text-xl">Login page</h3>
      <form className="mt-4 max-w-lg" onSubmit={onFormSubmit}>
        <fieldset disabled={isLoggingIn} className="w-full grid gap-2">
          <div className="grid gap-2 items-center min-w-[300px]">
            <label htmlFor="email-input" className="text-sm font-medium">
              Email
            </label>
            <input
              id="email-input"
              name="email"
              placeholder="Enter your email"
              content="test@mail.com"
              type="text"
              className="border rounded-md p-2 w-full"
              required
            />
            <label htmlFor="password-input" className="text-sm font-medium">
              Password
            </label>
            <input
              id="password-input"
              name="password"
              placeholder="Enter your password"
              content="#312312312"
              type="text"
              className="border rounded-md p-2 w-full"
              required
            />
          </div>
          <button
            type="submit"
            className="bg-blue-500 text-white py-2 px-4 rounded-md w-full disabled:bg-gray-300 disabled:text-gray-500"
          >
            {isLoggingIn ? "Loading..." : "Login"}
          </button>
        </fieldset>
      </form>
    </div>
  );
}
