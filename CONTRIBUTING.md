# Contributing to UNO Card Game

Thank you for your interest in contributing to this project! This document provides guidelines and instructions for contributing.

## Getting Started

1. **Fork the repository** on GitHub
2. **Clone your fork** locally:
   ```bash
   git clone https://github.com/your-username/Software_Eng.git
   cd Software_Eng
   ```
3. **Create a branch** for your changes:
   ```bash
   git checkout -b feature/your-feature-name
   ```

## Development Setup

1. **Unity Version**: Ensure you have Unity 2022.3.27f1 (or compatible version) installed
2. **Open the project** in Unity Hub
3. **Configure Firebase** (if working on backend features):
   - Set up your Firebase project
   - Add `google-services.json` to the appropriate location
   - Enable required Firebase services

## Code Style Guidelines

### C# Coding Standards

- **Naming Conventions**:
  - Classes: `PascalCase` (e.g., `GameManager`)
  - Methods: `PascalCase` (e.g., `OnClickPlayButton`)
  - Variables: `camelCase` for private, `PascalCase` for public
  - Constants: `UPPER_SNAKE_CASE`

- **Documentation**:
  - Add XML documentation comments (`/// <summary>`) to all public classes and methods
  - Use clear, concise descriptions
  - Document parameters and return values

- **Organization**:
  - Keep related functionality grouped
  - Use appropriate namespaces (currently classes are in global namespace, but consider organizing)
  - Separate concerns: Backend logic vs. Frontend presentation

### Unity-Specific Guidelines

- **Component Organization**:
  - Use `[Header]` attributes to organize inspector fields
  - Use `[SerializeField]` for private fields that need inspector access
  - Keep public fields only when necessary for external access

- **Singleton Pattern**:
  - Current managers use singleton pattern
  - Ensure proper initialization and cleanup in `Awake()`

## Commit Message Guidelines

Write clear, descriptive commit messages:

```
Format: <type>(<scope>): <subject>

Types:
- feat: New feature
- fix: Bug fix
- docs: Documentation changes
- style: Code style changes (formatting, etc.)
- refactor: Code refactoring
- test: Adding or updating tests
- chore: Maintenance tasks

Examples:
- feat(gameplay): Add tournament mode
- fix(cards): Fix card validation logic
- docs(readme): Update installation instructions
```

## Pull Request Process

1. **Update Documentation**:
   - Update README.md if you've added features or changed behavior
   - Add or update code comments as needed

2. **Test Your Changes**:
   - Test in Unity Editor
   - Verify gameplay works as expected
   - Check for console errors or warnings

3. **Create Pull Request**:
   - Provide a clear title and description
   - Reference any related issues
   - Include screenshots if UI changes were made

## Areas for Contribution

### High Priority
- [ ] Unit tests for game logic
- [ ] Improved error handling
- [ ] Performance optimizations
- [ ] Additional game modes

### Feature Ideas
- [ ] Online multiplayer support
- [ ] Customizable AI difficulty
- [ ] Achievement system
- [ ] Replay system
- [ ] Mobile platform optimizations

### Code Quality
- [ ] Refactoring for better organization
- [ ] Adding namespaces
- [ ] Improved documentation
- [ ] Code cleanup and optimization

## Questions?

If you have questions or need clarification, please open an issue with the `question` label.

Thank you for contributing! 🎮

