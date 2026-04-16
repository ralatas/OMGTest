# OMG Games Test Task

This repository contains a Unity test project for the OMG Games assignment focused on a puzzle start dialog and UI organization.

The current implementation demonstrates:

- opening a puzzle details dialog from scene UI
- showing puzzle preview and reward
- selecting puzzle slicing options
- starting the puzzle in different ways: free play, coins, or rewarded ad
- separating window lifecycle, prefab creation, and view rendering

## Goal

Implement a dialog system and UI organization approach for a puzzle start flow with:

- slice selection
- preview image of the puzzle
- option to start for free, for coins, or after watching an ad

The project uses a lightweight service-driven UI architecture with Zenject for dependency injection.

## Implemented Flow

The main demo entry point is [`WindowOpener.cs`](Assets/Scriprs/View/WindowOpener.cs), which opens [`PuzzleDetailsWindow.cs`](Assets/Scriprs/View/PuzzleDetailsWindow/PuzzleDetailsWindow.cs) with runtime payload data.

The payload currently contains:

- puzzle id
- preview sprite
- reward value
- available slice counts

Inside the puzzle dialog the player can:

- inspect the preview image
- switch between slicing variants
- start the puzzle directly
- start via rewarded ad flow
- start via coins flow

At the demo level these actions are represented with logs and local button handlers, which keeps the implementation easy to review while leaving clear extension points for real game services.

## Architecture

The project is split into a few focused roles:

- installers register runtime services in Zenject
- `WindowService` opens and closes windows
- `WindowFactory` instantiates configured prefabs
- `StaticDataService` resolves prefabs from config assets
- window views render payload data and subscribe to UI events

This keeps the dialog logic explicit and easy to extend without introducing a heavy UI framework.

## Main Files

- [`BootstrapInstaller.cs`](Assets/Scriprs/Installers/BootstrapInstaller.cs)
- [`SceneInitializationInstaller.cs`](Assets/Scriprs/Installers/SceneInitializationInstaller.cs)
- [`UIInitializer.cs`](Assets/Scriprs/Installers/UIInitializer.cs)
- [`IWindowService.cs`](Assets/Scriprs/Service/Windows/IWindowService.cs)
- [`WindowService.cs`](Assets/Scriprs/Service/Windows/WindowService.cs)
- [`IWindowFactory.cs`](Assets/Scriprs/Service/Windows/IWindowFactory.cs)
- [`WindowFactory.cs`](Assets/Scriprs/Service/Windows/WindowFactory.cs)
- [`WindowId.cs`](Assets/Scriprs/Service/Windows/WindowId.cs)
- [`WindowConfig.cs`](Assets/Scriprs/Service/Windows/Configs/WindowConfig.cs)
- [`WindowsConfig.cs`](Assets/Scriprs/Service/Windows/Configs/WindowsConfig.cs)
- [`StaticDataService.cs`](Assets/Scriprs/Service/StaticData/StaticDataService.cs)
- [`PuzzleDetailsWindow.cs`](Assets/Scriprs/View/PuzzleDetailsWindow/PuzzleDetailsWindow.cs)
- [`WindowWithParamsPayload.cs`](Assets/Scriprs/View/PuzzleDetailsWindow/WindowWithParamsPayload.cs)
- [`WindowOpener.cs`](Assets/Scriprs/View/WindowOpener.cs)

## Runtime Flow

1. [`WindowOpener.cs`](Assets/Scriprs/View/WindowOpener.cs) prepares puzzle payload data.
2. `WindowService.Open(...)` is called with `WindowId.PuzzleDetailsWindow`.
3. `WindowFactory` resolves the prefab from static data and instantiates it under the configured UI root.
4. [`PuzzleDetailsWindow.cs`](Assets/Scriprs/View/PuzzleDetailsWindow/PuzzleDetailsWindow.cs) reads the payload and renders:
   - preview image
   - reward text
   - slice selection buttons
5. The dialog exposes three start actions:
   - direct play
   - rewarded ad
   - coins

## Demo

To review the current task implementation:

1. Open the sample scene.
2. Enter Play Mode.
3. Press the custom open button wired to [`WindowOpener.cs`](Assets/Scriprs/View/WindowOpener.cs).
4. Verify that the puzzle details window shows:
   - puzzle preview
   - reward value
   - several slice count buttons
   - play, ad, and coins actions
5. Switch between slice options and confirm the grid preview changes.
6. Press each action button and verify the expected debug flow is triggered.

## Extension Points

The current version is intentionally a first iteration. It is structured so that the next steps can be added without rewriting the flow:

- connect ad button to a real rewarded ads service
- connect coins button to wallet/economy validation
- load puzzle data from game content instead of hardcoded demo payload
- replace logs with real scene/gameplay loading
- move payload creation out of scene button code into a presenter/controller layer if the UI grows

## Known Limitations

- `WindowService.Close(WindowId)` currently closes by type and assumes one active instance per window type
- the demo payload is created directly in `WindowOpener`
- ad and coins flows are stubbed with logs, not real integrations
- slice-to-grid visuals currently depend on a local sprite list configured on the window prefab

## Summary

This project currently fits the OMG Games task as a practical prototype of a puzzle start dialog:

- preview image is shown
- slice selection is implemented
- free, coins, and ad entry points are present
- UI responsibilities are separated into services, factory, and view logic
