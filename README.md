# Strategic Utility System (SUS)

## Table of Contents
- [Introduction](#introduction)
- [Features](#features)
- [Usage](#usage)
    - [Creating an Action](#creating-an-action)
    - [Creating a Context](#creating-a-context)
    - [Decision Making](#decision-making)
    - [Decision Adjustment](#decision-adjustment)

## Introduction
The Strategic Utility System (SUS) is a context-aware decision-making framework designed for game AI. It evaluates potential actions based on their utility and the current game context, enabling AI to adapt dynamically to changing environments.

## Features
- **Contextual and Context-Aware Decision Making**: Adapts to changes in the game environment by evaluating the utility of actions within the current context.
- **Caching and Reuse**: Optimizes performance by caching utility calculations for reuse in similar decision-making scenarios.
- **Adaptive Thresholds**: Dynamically adjusts thresholds to prioritize actions based on the urgency of the situation.
- **Parallel Processing**: Leverages multi-core processors to evaluate actions concurrently, reducing decision-making time.

## Usage

### Creating an Action:

```csharp
var actions = new List<IAction>
{
    new GameAction("Explore", context =>
    {
        if (context is GameContext gameContext && gameContext.EnvironmentType == "Forest")
        {
            return 0.8; // Higher utility in forests
        }
        return 0.5; // Default utility
    }),
};
```

### Creating a Context:

```csharp
var context = new GameContext(numberOfEnemies: 3, numberOfAllies: 1, resourceAvailability: 0.5, environmentType: "Forest");
```

### Decision Making:

```csharp
var decisionMaker = new DecisionMaker();
var bestAction = await decisionMaker.ChooseBestActionAsync(actions, context);
```

### Decision Adjustment:

```csharp
decisionMaker.AdjustThreshold(0.7);
bestAction = await decisionMaker.ChooseBestActionAsync(actions, context);
```
