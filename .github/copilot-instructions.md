# GitHub Copilot Instructions for "Shit Rimworld Says (Continued)"

## Mod Overview and Purpose

The "Shit Rimworld Says (Continued)" mod is an update of Fluffy's original mod, tailored for RimWorld. Its primary function is to enhance the game's loading screens with humorous and often outrageous quotes sourced from the subreddit /r/Sh*tRimWorldSays. These quotes reflect some of the bizarre and hilarious aspects of the RimWorld experience, often taken out of context. It's important to note that the content of the quotes is uncontrolled and may be offensive to some; therefore, users are advised to install the mod at their discretion.

## Key Features and Systems

- **Dynamic Quote Display**: The mod automatically fetches the latest quotes from /r/Sh*tRimWorldSays and displays them during the game's loading screens.
- **Gameplay Tips Toggle**: Users have the option to disable the default gameplay tips to make room for the quotes.
- **User Feedback and Updates**: The mod acknowledges potential issues with quote sizing and context due to the source material. Users are encouraged to report bugs through the prescribed process.
- **Compatibility Alert**: The mod is designed for RimWorld and depends on data from online sources, which might introduce compatibility variances across game versions or operating systems.

## Coding Patterns and Conventions

- **C# Programming Language**: The project utilizes C# as its primary development language, targeting the .NET Framework v4.8.
- **Class Structuring**: The mod organizes functionalities through a series of classes, each handling specific tasks or data structures (e.g., `Post`, `Reply`, `Tip`).
- **Modular Design**: Files like `GameplayTipWindow_DrawWindow` and `GameplayTipWindow_ResetTipTimer` demonstrate a pattern of splitting more significant functionalities into smaller, focused modules.
- **Conventions**: Standard C# conventions are followed, such as PascalCase for class and method names and camelCase for method parameters and local variables.

## XML Integration

- **Settings Management**: This mod utilizes XML for managing user settings, as seen in the `Settings` class, which inherits from `ModSettings`. The `DoWindowContents(Rect canvas)` method likely involves XML for storing or retrieving configurations, enabling personalization while maintaining structure.
- **Data Exposure**: XML is used for saving and loading game data with methods like `ExposeData()` in classes like `Tip_Quote` and `TipDatabase`, employing the `IExposable` interface for seamless integration with RimWorld's data management system.

## Harmony Patching

- **Harmony Integration**: The mod likely leverages Harmony for patching core game methods to insert its quotes into the loading screen, using patching hooks provided by RimWorld's API.
- **Patching Files**: Classes like `MainMenuDrawer_MainMenuOnGui` indicate spots where Harmony patches are applied to modify or extend existing game functionality without altering base game files, ensuring compatibility and ease of updates.

## Suggestions for Copilot

When using GitHub Copilot in this mod, it can assist with:

- **Class and Method Autocompletion**: Copilot can suggest extensions and new methods for classes like `Tip_Quote` and `Post` based on the existing code structure.
- **Pattern Recognition**: Automatically detect and replicate established coding patterns when introducing new features, such as new quote types or settings options.
- **XML Manipulation**: Providing templates for XML data management functions, helping streamline the integration of user settings and data exposure.
- **Harmony Patches**: Suggesting appropriate hooks and methods to patch based on the RimWorld API and existing Harmony usage, making sure that patches don't conflict with other mods.
- **Error Detection**: Identifying possible points of failure in network requests for quotes or in data handling that could benefit from error handling or fallback logic.

---
This file provides guidelines and insights to help streamline the development process for the "Shit Rimworld Says (Continued)" mod using GitHub Copilot.
