import { createFileRoute, Link } from "@tanstack/react-router";

export const Route = createFileRoute("/")({
  component: Index,
});

function Index() {
  return (
    <div className="p-2">
      <h3>Welcome Home!</h3>
      <ol className="list-disc list-inside px-2">
        <li>
          <Link to="/login" className="text-blue-500 hover:opacity-75">
            Go to the public login page.
          </Link>
        </li>
        <li>
          <Link to="/about" className="text-blue-500 hover:opacity-75">
            Go to the auth-only dashboard page.
          </Link>
        </li>
        <li>
          <Link to="/" className="text-blue-500 hover:opacity-75">
            Go to the auth-only invoices page.
          </Link>
        </li>
      </ol>
    </div>
  );
}
