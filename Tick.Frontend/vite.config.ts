import { defineConfig } from "vite";
import react from "@vitejs/plugin-react";
import mkcert from "vite-plugin-mkcert";
import { TanStackRouterVite } from "@tanstack/router-plugin/vite";

export default defineConfig({
  base: "./",
  server: {
    https: true,
    port: 6363,
  },
  plugins: [react(), mkcert(), TanStackRouterVite()],
});
