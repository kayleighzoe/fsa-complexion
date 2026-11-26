import { defineConfig } from 'cypress'

export default defineConfig({
  e2e: {
    baseUrl: 'http://localhost:5173',
    setupNodeEvents(on, config) {
      // keep whatever was generated here
    },
  },
  env: {
    apiUrl: 'https://localhost:8080',
  },
})