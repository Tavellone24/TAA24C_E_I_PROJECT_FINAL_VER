# Snog's Timer Manager

**Snog's Timer Manager** is a lightweight, flexible, and user-friendly timer system for Unity. Designed to be intuitive and with ease-of-use in mind. It allows you to create, manage, and visualize all timers with minimal setup — whether you're working in the editor or building for runtime.

This asset is ideal for triggering timed events, animations, puzzle logic, cooldowns, and more.

---

## Features

- **Easily Create Timers:** Create timers via code or from predefined assets.
  - **ScriptableObject Timers:** Easily create timers ahead of time with UnityEvents directly in the Inspector.
  - **Code Timers:** Simply call `CreateTimer()` in any script.

- **Runtime & Build Compatibility**  
  Automatically loads all timer definitions from `Resources\TimerManager` at runtime — works in both editor and builds.

- **Editor Integration**  
  Custom inspector shows active timers, progress bars, and lets you filter, stop, or reset timers during play mode.

- **UnityEvent Support**  
  Assign events visually in the Inspector — no need to write code for basic timer behaviors.

- **Pause, Resume, Stop, and Remove Timers**  
  Full control over timer lifecycle.

---

## Table of Contents

1. [Core Concepts](#core-concepts)  
2. [Initial Setup Guide](#initial-setup-guide)  
3. [Creating Timer Definitions](#creating-timer-definitions)  
4. [Using Timers in Code](#using-timers-in-code)  
5. [FAQ](#faq)

---

## Core Concepts

- **GameTimer**: The timer code with duration, looping, and UnityEvent callback.  
- **TimerManager**: Singleton that manages all active timers and updates them each frame.  
- **TimerDefinition**: A ScriptableObject that defines a pre-made timer's ID, duration, looping behavior, and UnityEvent.  

---

## Initial Setup Guide

### 1. Import the Asset

Drag the `Snog's Timer Manager` folder into your Unity project.

### 2. Add the TimerManager to Your Scene

- Create an empty GameObject and add the `TimerManager` component.
- It will persist across scenes automatically.

### 3. You're good to go!

- Create your timers via SciptableObject or in-code.

---

## Creating Timer Definitions

TimerDefinitions are ScriptableObjects that define reusable timers.

- **ID**: Unique name for the timer.  
- **Duration**: Time in seconds.  
- **Looping**: Whether the timer repeats.  
- **OnTimerComplete**: UnityEvent to trigger when the timer finishes.

---

## Using Timers in Code

### Create a Timer Manually

```csharp
TimerManager.Instance.CreateTimer("MyTimer", 5f, false, () => Debug.Log("Timer done!"));
```

### Create a Timer from a Definition

```csharp
var def = Resources.Load<TimerDefinition>("TimerManager\MyTimerDefinition");
TimerManager.Instance.CreateTimerFromDefinition(def);
```

---

## FAQ

**Q: Will this work in builds?**  
✅ Yes! All `TimerDefinition` assets in `TimerManager\Resources` are loaded at runtime.

**Q: Can I assign UnityEvents in play mode?**  
✅ Yes, if the timer is created from a `TimerDefinition`.

**Q: Can I create timers dynamically?**  
✅ Yes, use `CreateTimer()` in code.

**Q: Can I pause or resume all timers?**  
✅ Use `PauseAllTimers()` and `ResumeAllTimers()`.

---

## License

This project is licensed under the MIT License. See the LICENSE file for details.
