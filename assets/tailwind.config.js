let purgeEnabled = false;

if (process.argv.length > 0) {
    purgeEnabled = process.argv.includes('--purge=true')
}

module.exports = {
  purge: {
	enabled: purgeEnabled,
	content: ['./project/ContactNumbers.Main/ContactNumbers.Main/Views/**/*.cshtml']
	},
  darkMode: false,
  theme: {
    extend: {},
  },
  variants: {
    extend: {},
  },
  plugins: [],
}