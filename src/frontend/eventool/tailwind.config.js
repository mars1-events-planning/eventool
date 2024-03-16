/** @type {import('tailwindcss').Config} */
export default {
  content: ["./src/**/*.{html,js,svelte}"],
  theme: {
    extend: {},
  },
  plugins: [require("daisyui")],
  daisyui: {
    themes: [
      {
        "emerald": {
          ...require("daisyui/src/theming/themes")["emerald"],
          "primary": "#3ABD67",
          "font-family": "Nunito, sans-serif",
          "primary-content": "#FFFFFF",
          "error-content": "#FFFFFF"
        },
        "dim":{
          ...require("daisyui/src/theming/themes")["dim"],
          "primary": "#3ABD67",
          "font-family": "Nunito, sans-serif",
          "primary-content": "#FFFFFF",
          "error-content": "#FFFFFF",
          "error": "#D70040"
        },
      }
    ],
  },
}

