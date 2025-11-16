## Architecture Overview

This document describes the high-level architecture of the UNO card game project and how the main systems interact.

### Layers

The project follows a layered structure:

- **Presentation Layer**
  - Unity UI
  - Menus, panels, and HUD elements
  - Animations (DOTween), visual feedback, and sound effects

- **Game Logic Layer**
  - Turn management
  - Card rules and validation
  - Bot AI behavior
  - Win/lose conditions and scoring

- **Data Layer**
  - Firebase Authentication
  - Firebase Realtime Database for leaderboard and scores
  - Local persistence via `PlayerPrefs`

### Core Systems

- `GameManager`
  - Handles main menu navigation and scene transitions.
  - Opens the different menus through `MenuManager`.

- `MenuManager` / `Menu`
  - Maintains a list of menus in the scene.
  - Ensures only one menu is active at any time (simple state machine).

- `GamePlayManager`
  - Controls the main game loop.
  - Manages player turns, timers, color-selection logic, and game-over flow.
  - Integrates with Firebase to update player scores at the end of a match.

- `CardsManager`
  - Represents a single player's hand.
  - Generates cards, positions them, resizes them based on card count, and handles card drawing.
  - Uses DOTween to animate card movement and layout.

- `Card`
  - Encapsulates UNO card data (color, number, special type).
  - Contains the logic for validating whether the card can be played.
  - Handles the visual transition to the stack when played.

- `StackManager`
  - Manages the discard pile (stack) of played cards.
  - Exposes the current top card for validation and color-selection logic.
  - Tracks stacked draw cards (Draw 2 / Draw 4).

- `BotPlayer`
  - Controls AI players.
  - Chooses a valid card to play when it is the bot's turn.
  - Picks a random color when playing wild cards.

- `AuthManager`
  - Handles Firebase Authentication (register, login, logout).
  - Coordinates with `UI_AuthenticationManager` for input and feedback.
  - Stores and restores login information with `PlayerPrefs` when "remember me" is enabled.

- `LeaderboardManager`
  - Reads leaderboard entries from Firebase Realtime Database.
  - Instantiates `LeaderboardItem` prefabs, sorts them by score, and updates the UI.

- `SoundManager` / `SoundManagerUI`
  - `SoundManager` manages audio sources and volume persistence.
  - `SoundManagerUI` connects sliders and buttons in the UI to `SoundManager`.

### Data Flow (Example: Game End to Leaderboard)

1. `GamePlayManager` calculates scores and calls `ShowGameOverPanel`.
2. Scores are written to Firebase under the current username.
3. When the leaderboard menu is opened, `LeaderboardManager` queries Firebase.
4. `LeaderboardManager` builds the list of `LeaderboardItem` UI entries and sorts by score.

### Extensibility Notes

- New game modes can be added by:
  - Extending `GamePlayManager` with additional state.
  - Creating new scenes or UI flows that still use the same managers.

- Online multiplayer could reuse:
  - The existing card, stack, and UI logic.
  - Replace or extend `GamePlayManager` and `BotPlayer` with network-aware versions.

- Additional analytics or stats:
  - Extend the Data Layer with more Firebase fields or another backend.


