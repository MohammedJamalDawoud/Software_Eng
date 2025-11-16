<div align="center">

# 🎮 UNO Card Game - Unity Implementation

**A full-featured multiplayer UNO card game built with Unity and Firebase**

[![Unity](https://img.shields.io/badge/Unity-2022.3.27f1-000000?style=for-the-badge&logo=unity&logoColor=white)](https://unity.com/)
[![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white)](https://docs.microsoft.com/en-us/dotnet/csharp/)
[![Firebase](https://img.shields.io/badge/Firebase-039BE5?style=for-the-badge&logo=Firebase&logoColor=white)](https://firebase.google.com/)
[![License](https://img.shields.io/badge/License-MIT-blue.svg?style=for-the-badge)](LICENSE)
[![Status](https://img.shields.io/badge/Status-Completed-success?style=for-the-badge)](https://github.com/MohammedJamalDawoud/Software_Eng)

[![Repository Stats](https://img.shields.io/github/stars/MohammedJamalDawoud/Software_Eng?style=social)](https://github.com/MohammedJamalDawoud/Software_Eng)
[![Repository Forks](https://img.shields.io/github/forks/MohammedJamalDawoud/Software_Eng?style=social)](https://github.com/MohammedJamalDawoud/Software_Eng)

</div>

---

## 📋 Table of Contents

- [Overview](#-overview)
- [Key Features](#-key-features)
- [Tech Stack](#-tech-stack)
- [Project Architecture](#-project-architecture)
- [Getting Started](#-getting-started)
- [How to Play](#-how-to-play)
- [Project Structure](#-project-structure)
- [Screenshots](#-screenshots)
- [Future Improvements](#-future-improvements)
- [What I Learned](#-what-i-learned)
- [For Recruiters](#-for-recruiters)
- [License](#-license)

---

## 🎯 Overview

This is a **Software Engineering course project** - a complete implementation of the classic UNO card game in Unity. The project demonstrates proficiency in game development, software architecture, cloud integration, and user interface design. Players can enjoy multiplayer matches with AI opponents, track scores on a global leaderboard, and experience smooth gameplay with polished animations and sound effects.

The game features:
- **Real-time multiplayer gameplay** with 4-player support
- **Firebase backend integration** for authentication and leaderboard
- **AI opponents** with intelligent card-playing logic
- **Complete UNO rules implementation** including special cards and stacking mechanics
- **Professional UI/UX** with smooth animations and responsive design

---

## ✨ Key Features

### 🎮 Core Gameplay
- ✅ **Full UNO card game rules** implementation
- ✅ **4-player multiplayer** support (1 human + 3 AI bots)
- ✅ **Turn-based gameplay** with timer system
- ✅ **Special cards**: Wild, Draw 2, Draw 4, Skip, Reverse
- ✅ **Card stacking mechanics** (accumulating draw cards)
- ✅ **Color selection** for wild cards
- ✅ **Win condition detection** and scoring system

### 🔐 Authentication & User Management
- ✅ **Firebase Authentication** (email/password)
- ✅ **User registration** and login system
- ✅ **Persistent login** with secure credential storage
- ✅ **User profile management**

### 🏆 Leaderboard & Scoring
- ✅ **Global leaderboard** powered by Firebase Realtime Database
- ✅ **Score tracking** and accumulation
- ✅ **Real-time leaderboard updates**
- ✅ **Player ranking system**

### 🎨 User Interface
- ✅ **Polished menu system** (Main, Settings, Leaderboard, Game)
- ✅ **Smooth animations** using DOTween
- ✅ **Loading indicators** and transitions
- ✅ **Sound management** (Music and SFX controls)
- ✅ **Responsive card layouts** with dynamic sizing
- ✅ **Visual feedback** for all interactions

### 🤖 AI System
- ✅ **Intelligent bot players** with card-playing logic
- ✅ **Randomized decision timing** for natural gameplay
- ✅ **Wild card color selection** by AI

---

## 🛠 Tech Stack

### Core Technologies
- **Unity 2022.3.27f1** - Game engine and development platform
- **C#** - Primary programming language
- **Firebase Authentication** - User authentication and management
- **Firebase Realtime Database** - Cloud database for leaderboards and scores

### Libraries & Tools
- **DOTween** - Tweening and animation library
- **TextMesh Pro** - Advanced text rendering
- **Unity UI** - User interface system
- **Firebase Unity SDK** - Firebase integration

### Development Practices
- **Object-Oriented Programming (OOP)** - Class-based architecture
- **Singleton Pattern** - Manager classes for centralized control
- **Separation of Concerns** - Backend, Frontend, and GameDesign layers
- **Event-Driven Architecture** - UI interactions and game events

---

## 🏗 Project Architecture

The project follows a **layered architecture** with clear separation of concerns:

```
┌─────────────────────────────────────┐
│         Presentation Layer          │
│  (UI, Animations, Visual Effects)   │
├─────────────────────────────────────┤
│         Game Logic Layer            │
│  (Gameplay, Rules, Turn Management) │
├─────────────────────────────────────┤
│         Data Layer                  │
│  (Firebase, Local Storage, State)   │
└─────────────────────────────────────┘
```

### Key Components

1. **GameManager** - Main menu navigation and scene management
2. **GamePlayManager** - Core game logic, turn management, and win conditions
3. **CardsManager** - Individual player card handling and display
4. **AuthManager** - Firebase authentication and user management
5. **LeaderboardManager** - Score tracking and leaderboard display
6. **MenuManager** - Dynamic menu system with state management
7. **StackManager** - Discard pile and card stack management
8. **BotPlayer** - AI opponent logic and decision-making

---

## 🚀 Getting Started

### Prerequisites

- **Unity Hub** installed on your system
- **Unity 2022.3.27f1** (or compatible version)
- **Visual Studio** or **Rider** for C# development (optional but recommended)
- **Firebase account** with a project set up (for authentication and database)

### Installation Steps

1. **Clone the repository**
   ```bash
   git clone https://github.com/MohammedJamalDawoud/Software_Eng.git
   cd Software_Eng
   ```

2. **Open in Unity**
   - Launch Unity Hub
   - Click "Add" and select the `Software_Eng` folder
   - Ensure Unity 2022.3.27f1 is installed
   - Click "Open" to load the project

3. **Configure Firebase** (if using backend features)
   - Create a Firebase project at [Firebase Console](https://console.firebase.google.com/)
   - Download `google-services.json` (Android) or `GoogleService-Info.plist` (iOS)
   - Place in `Assets/` folder (for Android) or appropriate location
   - Enable Authentication and Realtime Database in Firebase Console

4. **Install Dependencies**
   - The project uses Unity Package Manager
   - DOTween should be automatically imported
   - If missing, import via Window → Asset Store or Package Manager

5. **Build and Run**
   - Open the scene: `Assets/Scenes/Week3.unity` (main menu) or `Assets/Scenes/Game.unity` (game scene)
   - Click Play ▶️ in Unity Editor
   - Or build for your target platform (File → Build Settings)

### Project Settings

- **Target Platform**: Windows, Android, iOS (configurable)
- **API Compatibility Level**: .NET Standard 2.1
- **Scripting Backend**: Mono

---

## 🎲 How to Play

### Basic Controls

- **Click cards** to play them during your turn
- **Select color** when playing wild cards by clicking color selector buttons
- **Timer system**: Each player has 15 seconds per turn
- **Automatic turn progression** when valid cards are played

### Game Rules (Implemented)

- Match cards by **color** or **number**
- Special cards: **Wild**, **Wild Draw 4**, **Draw 2**, **Skip**, **Reverse**
- **Stacking**: Draw cards accumulate (Draw 2 + Draw 2 = Draw 4)
- First player to **empty their hand** wins
- Scoring: 
  - Empty hand: 1000 points
  - 2-5 cards remaining: 500 points
  - 6+ cards remaining: 100 points

---

## 📁 Project Structure

```
Software_Eng/
│
├── Assets/
│   ├── scripts/
│   │   ├── BackendScripts/      # Core game logic & managers
│   │   │   ├── AuthManager.cs
│   │   │   ├── GameManager.cs
│   │   │   ├── MenuManager.cs
│   │   │   ├── BetManager.cs
│   │   │   ├── BotPlayer.cs
│   │   │   └── SoundManager.cs
│   │   │
│   │   ├── FrontEndScripts/     # UI & gameplay presentation
│   │   │   ├── GamePlayManager.cs
│   │   │   ├── CardsManager.cs
│   │   │   ├── Card.cs
│   │   │   ├── StackManager.cs
│   │   │   ├── UI_AuthenticationManager.cs
│   │   │   └── LoadingManager.cs
│   │   │
│   │   └── GameDesign/          # UI panels & components
│   │       ├── LeaderboardManager.cs
│   │       ├── GameOverPanel.cs
│   │       └── ErrorBoxManager.cs
│   │
│   ├── Scenes/                  # Unity scenes
│   │   ├── Game.unity
│   │   ├── Week3.unity (Main Menu)
│   │   └── ...
│   │
│   ├── Prefabs/                 # Reusable game objects
│   │   ├── Card.prefab
│   │   ├── PlayerLeaderBoardPrefab.prefab
│   │   └── ...
│   │
│   ├── CardSprites/             # Card artwork assets
│   ├── Fonts/                   # Typography assets
│   ├── Plugins/                 # Third-party libraries
│   │   ├── Demigiant/ (DOTween)
│   │   └── Firebase/
│   │
│   └── Resources/               # Runtime-loaded assets
│
├── ProjectSettings/             # Unity project configuration
├── Packages/                    # Package dependencies
└── README.md                    # This file

```

---

## 📸 Screenshots

> **Note**: Add screenshots or GIFs of your game here to showcase the UI and gameplay.
> 
> Recommended images:
> - Main menu screen
> - In-game gameplay view
> - Leaderboard display
> - Settings menu
> - Authentication screen
> 
> You can add images to a `screenshots/` folder and reference them like:
> ```markdown
> ![Main Menu](screenshots/main-menu.png)
> ![Gameplay](screenshots/gameplay.png)
> ```

---

## 🔮 Future Improvements

### Planned Features
- [ ] **Online Multiplayer** - Real-time multiplayer with Firebase or Photon
- [ ] **Customizable AI Difficulty** - Easy, Medium, Hard AI levels
- [ ] **Tournament Mode** - Bracket-style competitions
- [ ] **Achievement System** - Unlock achievements for various accomplishments
- [ ] **Card Animations** - Enhanced card flip and deal animations
- [ ] **Sound Effects** - Additional audio feedback for game events
- [ ] **Mobile Optimization** - Improved touch controls and UI scaling
- [ ] **Localization** - Multi-language support
- [ ] **Replay System** - Record and replay matches
- [ ] **Statistics Dashboard** - Detailed player statistics and history

### Technical Improvements
- [ ] **Unit Tests** - Comprehensive test coverage for game logic
- [ ] **Performance Optimization** - Profiling and optimization passes
- [ ] **Code Refactoring** - Further namespace organization and SOLID principles
- [ ] **Error Handling** - Enhanced error handling and user feedback
- [ ] **Network Resilience** - Better offline support and reconnection logic

---

## 💡 What I Learned

This Software Engineering course project provided hands-on experience with:

### Software Engineering Principles
- **Object-Oriented Design** - Class hierarchies, inheritance, and polymorphism
- **Design Patterns** - Singleton pattern for manager classes
- **Separation of Concerns** - Backend, frontend, and presentation layer separation
- **Code Organization** - Modular architecture and maintainable codebase

### Unity Development
- **Game Engine Architecture** - MonoBehaviour lifecycle, component system
- **UI/UX Design** - Menu systems, responsive layouts, animations
- **Game Logic Implementation** - Turn-based systems, state management
- **Performance Optimization** - Efficient rendering and memory management

### Cloud Integration
- **Firebase Authentication** - User management and security
- **Firebase Realtime Database** - Real-time data synchronization
- **Async Programming** - Async/await patterns for cloud operations
- **Error Handling** - Network request handling and retry logic

### Collaboration & Version Control
- **Git Workflow** - Version control best practices
- **Project Management** - Feature planning and implementation
- **Documentation** - Code comments and project documentation

---

## 👔 For Recruiters

### Why This Project Matters

This project demonstrates **full-stack game development capabilities** with a focus on:

1. **Software Architecture** - Clean, organized codebase with clear separation of concerns
2. **Backend Integration** - Firebase cloud services for authentication and data persistence
3. **Game Development** - Complete game implementation with complex rules and mechanics
4. **User Experience** - Polished UI/UX with animations and feedback
5. **Problem-Solving** - Implementation of complex card game logic and AI behavior

### Skills Demonstrated

- ✅ **C# Programming** - Object-oriented programming, LINQ, async/await
- ✅ **Unity Game Engine** - MonoBehaviour, UI system, scene management, animations
- ✅ **Firebase** - Authentication, Realtime Database, cloud integration
- ✅ **Software Design** - Design patterns, architecture, code organization
- ✅ **Version Control** - Git workflows and collaboration
- ✅ **UI/UX Design** - Menu systems, responsive design, user feedback
- ✅ **Game Logic** - Rule implementation, state machines, AI decision-making

### Project Relevance

This project showcases skills directly applicable to:
- **Game Development** - Full Unity game development pipeline
- **Full-Stack Development** - Frontend (Unity) + Backend (Firebase) integration
- **Software Engineering** - Architecture, design patterns, clean code
- **Mobile Development** - Cross-platform Unity development

### Course Context

This was developed as part of a **Software Engineering course**, requiring:
- Team collaboration and version control
- Software design documentation
- Implementation of complex requirements
- Testing and quality assurance
- Professional code standards

---

## 📄 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

---

## 🙏 Acknowledgments

- Unity Technologies for the Unity game engine
- Firebase team for backend services
- Demigiant for DOTween animation library
- Card artwork and assets used for educational purposes

---

<div align="center">

**Built with ❤️ as a Software Engineering course project**

[![GitHub](https://img.shields.io/badge/GitHub-Repository-181717?style=flat&logo=github)](https://github.com/MohammedJamalDawoud/Software_Eng)
[![Unity](https://img.shields.io/badge/Unity-Download-000000?style=flat&logo=unity)](https://unity.com/download)

Made by [Mohammed Jamal Dawoud](https://github.com/MohammedJamalDawoud)

</div>
