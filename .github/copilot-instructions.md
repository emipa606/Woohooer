# GitHub Copilot Instructions for Woohooer (Continued) Mod

## Mod Overview and Purpose

Woohooer (Continued) is an updated version of the Woohooer mod originally created by Alycecil and later enhanced by AliceTries, Itachi E28s, and others. This mod integrates a Sims-like "WooHoo" mechanic into RimWorld, allowing colonists and prisoners to engage in interactions similar to those found in The Sims universe. It includes functionalities for trying for babies and managing relationships, providing additional social depth to your RimWorld experience.

## Key Features and Systems

1. **WooHoo Options**: Provides context options such as "Woohoo with {Other}" and "Try for Baby {Other}" for pawns, available to both colonists and prisoners.
   
2. **Alien Race Support**: Supports humanoid alien races, extending beyond purely human interactions.

3. **Integration with Other Mods**:
   - **Birds and Bees**: Utilizes fertility capabilities if available.
   - **Children School and Families**: Prefers this child generator over the base game when creating random babies.
   - **Psychology**: Notes compatibility issues resulting in log spam due to Psychology's interaction with joy objects.

4. **Advanced Systems**:
   - Handles pregnancy results and baby factions.
   - Allows for unique scenarios such as male pregnancy with proper installations.
   - Adequate responses to situational context, such as bed occupancy during woohoo interactions.

5. **Social Impact**:
   - Generates moodlets and affects relationships based on interactions.
   - Modifies certain existing game behaviors like amnesty for prisoner mothers.

## Coding Patterns and Conventions

- **Namespaces**: All C# files use structured namespaces corresponding to their functional domain within the mod.
- **Harmony Patching**: Use Harmony for method patches, detailed into `Prefix` and `Postfix` methods.
- **Error Handling**: Includes checks and balances to ensure mods work without causing game-breaking errors.

## XML Integration

- **Defs**: Various XML files define new `HediffDef`, `JobDef`, `JoyKindDef`, `RecipeDef`, and more, integrating smoothly with RimWorld's XML-driven content management system.
- **Structured Organization**: All XML files are clearly labeled and organized, giving each functionality its file for ease of management and clarity.

## Harmony Patching

- **Descriptions**: Uses Harmony to inject new interactions and override base game functions when needed.
- **Customization**: Allows extensive customization and addition of new features without modifying existing game files.
- **Error Prevention**: Comprehensive error prevention and debugging strategies are employed, particularly with known risky interactions such as those with the Psychology mod.

## Suggestions for Copilot

1. **Code Comments**: Encourage detailed comments within C# files to ensure all logic and decision-making processes are understood by future developers.
   
2. **Optimize XML Parsing**: Suggest checking for XML schema consistency and validation to ensure that all mod configurations integrate correctly.

3. **Debugging Tools**: Integrate robust logging practices to aid in identifying and troubleshooting issues, especially when dealing with cross-mod interactions.

4. **Feature Expansion**:
   - Consider expanding support for additional alien race interactions.
   - Propose additional moodlets and relational effects based on player feedback and gameplay testing. 

5. **Refactoring and Performance Optimization**: Use Copilot to suggest code refactoring opportunities to enhance performance and maintainability of the mod's codebase.

By adhering to these instructions, contributions to the Woohooer (Continued) mod will be more systematic, ensuring high-quality improvements and maintaining compatibility with future versions of RimWorld.

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

