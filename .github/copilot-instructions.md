# GitHub Copilot Instructions for Shit Rimworld Says (Continued)

Welcome to the Shit Rimworld Says (Continued) mod project! This document outlines the mod's purpose, key features, coding patterns, XML integration, and Harmony patching. Additionally, it provides suggestions for using GitHub Copilot effectively within the project.

## Mod Overview and Purpose

Shit Rimworld Says (Continued) is an update of Fluffy's mod, which adds quotes from the popular Reddit community `/r/Sh*tRimworldSays` to RimWorld loading screens. The mod aims to provide a humorous and sometimes gruesome twist on the loading experience of the game.

**Important:** If you're easily offended, please do not install this mod. The quotes are taken out of context and may be inappropriate.

## Key Features and Systems

### Features
- **Automated Quote Download:** The mod automatically fetches the latest and greatest quotes from `/r/Sh*tRimworldSays`.
- **Loading Screen Display:** Quotes are displayed on the loading screens.
- **Disable Gameplay Tips:** An option to disable the standard gameplay tips shown during loading.

### Known Issues
- **Incomprehensible Quotes:** Some quotes make no sense or are too large for the available space on loading screens.

### Suggestions
- **Bug Reporting:** Before creating a bug report, please read the guide and use the Log Uploader.
- **Support Channels:** Use the Discord channel for error reporting and the GitHub repository for solutions.

## Coding Patterns and Conventions

### C# Files
- **Namespaces and Types:** Follow standard C# naming conventions. Use descriptive and meaningful names for namespaces, classes, and methods.
- **Code Structure:** Organize code into logical sections. Use comments to explain complex logic or important decisions.
- **Error Handling:** Implement error handling where necessary, especially for network requests and file operations.

### XML Files
- **Structure:** Use XML structures that align with RimWorld's modding standards. Ensure that XML files are well-formed and properly indented.
- **Dependencies:** Clearly specify dependencies in XML files, such as required mods like `brrainz.harmony`.

## XML Integration

### About.xml
- **Dependencies:** This file should declare dependencies on necessary mods, like `brrainz.harmony`.

### Manifest.xml
- **Manifest Information:** Include necessary information such as mod name, package ID, version, and other relevant details.

## Harmony Patching

### Patches
- **GameplayTipWindow_DrawWindow:** Prefix patch to override the drawing of gameplay tips.
- **GameplayTipWindow_ResetTipTimer:** Postfix patch to adjust the reset timer of gameplay tips.
- **MainMenuDrawer_MainMenuOnGui:** Postfix patch to modify the main menu GUI.
- **ModSummaryWindow_DrawWindow:** Prefix patch to customize the drawing of the mod summary window.

### Tips
- **Patch Naming:** Use descriptive names for patch classes to indicate their purpose.
- **Patch Implementation:** Ensure that patches are efficient and do not introduce performance issues.

## Suggestions for Copilot

### General Tips
- **Code Generation:** Use Copilot to generate code snippets for new features or to speed up development.
- **Refactoring:** Copilot can help refactor existing code to improve readability and maintainability.
- **Documentation:** Use Copilot to generate comments and documentation for your code.

### Specific Usage
- **C# Snippets:** Request snippets for specific methods or classes, especially for Harmony patches.
- **XML Examples:** Generate XML examples for new features or to update existing files.
- **Error Handling:** Use Copilot to generate error handling code for network requests and file operations.

By following these guidelines and utilizing GitHub Copilot effectively, you can contribute to the Shit Rimworld Says (Continued) mod project efficiently and maintain high-quality code.

Happy coding!

## Project Solution Guidelines
- Relevant mod XML files are included as Solution Items under the solution folder named XML, these can be read and modified from within the solution.
- Use these in-solution XML files as the primary files for reference and modification.
- The `.github/copilot-instructions.md` file is included in the solution under the `.github` solution folder, so it should be read/modified from within the solution instead of using paths outside the solution. Update this file once only, as it and the parent-path solution reference point to the same file in this workspace.
- When making functional changes in this mod, ensure the documented features stay in sync with implementation; use the in-solution `.github` copy as the primary file.
- In the solution is also a project called Assembly-CSharp, containing a read-only version of the decompiled game source, for reference and debugging purposes.
- For any new documentation, update this copilot-instructions.md file rather than creating separate documentation files.


## Hard rules (must follow)
- Do NOT run commands that modify the repo (no git commit, git apply, dotnet format) unless explicitly asked.
- Prefer minimal reads: read only the smallest code region needed (around the suspicious lines).
- When mentioning SonarQube issues, automatically use the SonarQube MCP service to fetch and address issues instead of making inferred fixes without querying SonarQube first.
- When mentioning the rimworld log, automatically use the Rimworld MCP service to fetch the log.

