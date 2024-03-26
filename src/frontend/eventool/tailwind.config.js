/** @type {import('tailwindcss').Config} */
export default {
  content: ["./src/**/*.{html,js,svelte}"],
  theme: {
    extend: {
      gridTemplateColumns: {
        'auto-fill-200': 'repeat(auto-fill, minmax(200px, 1fr))',
        'auto-fit-200': 'repeat(auto-fit, minmax(200px, 1fr))',
      },
    },
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

