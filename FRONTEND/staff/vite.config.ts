import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react'
import { TanStackRouterVite } from '@tanstack/router-plugin/vite'
import tailwindcss from '@tailwindcss/vite'
import path from 'path'
import { fileURLToPath } from 'url'

const projectRoot = path.dirname(fileURLToPath(import.meta.url))

export default defineConfig({
  plugins: [
    TanStackRouterVite({ routesDirectory: './src/routes' }),
    react(),
    tailwindcss(),
  ],
  resolve: {
    alias: {
      '@': path.resolve(projectRoot, './src'),
    },
  },
  server: {
    port: 8080,
    fs: {
      allow: [projectRoot],
    },
  },
})
