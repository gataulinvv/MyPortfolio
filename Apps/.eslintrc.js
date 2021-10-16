module.exports = {
  env: {

    browser: true,
    es2021: true,
  },

  extends: [
    'plugin:react/recommended',
    'airbnb',
  ],
  parser: '@babel/eslint-parser',
  parserOptions: {
    ecmaFeatures: {
      jsx: true,
    },
    ecmaVersion: 12,
    sourceType: 'module',
  },
  plugins: [
    'react',
  ],
  rules: {
    'no-use-before-define': 0,
    'react/prop-types': 0,
    'import/no-absolute-path': 0,
    'jsx-a11y/label-has-associated-control': 0,
    'react/jsx-filename-extension': 0,
    'linebreak-style': 0,
    'import/extensions': 0,
    'import/no-unresolved': 0,
    'no-console': 0,
    'max-classes-per-file': ['error', 100],
    'import/no-extraneous-dependencies': 0,
    'no-unused-vars': 0,
    'no-shadow': 0,
    'class-methods-use-this': 0,
    'no-param-reassign': 0,
    'no-restricted-globals': 0,
    'react/jsx-props-no-spreading': 0,
  },
};
