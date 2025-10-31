# Unity Essentials

This module is part of the Unity Essentials ecosystem and follows the same lightweight, editor-first approach.
Unity Essentials is a lightweight, modular set of editor utilities and helpers that streamline Unity development. It focuses on clean, dependency-free tools that work well together.

All utilities are under the `UnityEssentials` namespace.

```csharp
using UnityEssentials;
```

## Installation

Install the Unity Essentials entry package via Unity's Package Manager, then install modules from the Tools menu.

- Add the entry package (via Git URL)
    - Window → Package Manager
    - "+" → "Add package from git URL…"
    - Paste: `https://github.com/CanTalat-Yakan/UnityEssentials.git`

- Install or update Unity Essentials packages
    - Tools → Install & Update UnityEssentials
    - Install all or select individual modules; run again anytime to update

---

# Sticky Note Component

> Quick overview: Add color‑coded notes to your GameObjects right in the Inspector. Edit a header, content, and footer with word‑wrapped text, and pick from five preset color themes.

A lightweight MonoBehaviour + custom Inspector that lets you attach sticky notes to any GameObject. Great for leaving hints for teammates, TODOs, setup checklists, or contextual documentation that travels with the scene/prefab.

![screenshot](Documentation/Screenshot.png)

## Features
- Three text areas: Header, Content, Footer (word‑wrapped)
- Five color themes: Yellow, Green, Blue, Pink, Purple
- Inspector preview with themed background and matching text color
- Quick color actions via the component’s context menu (… menu on the component header)
- Simple, dependency‑free; serialized with your scene/prefab
- Editor‑only visuals; no runtime UI or rendering

## Requirements
- Unity Editor 6000.0+ (Editor‑only visuals; component lives in Runtime for convenience)
- No external dependencies

Tip: Use notes to guide level assembly, document workaround steps, or track TODO items directly on objects.

## Usage
Add and edit

1) Select a GameObject
2) Add Component → “Sticky Note” (namespace `UnityEssentials`)
3) Fill in Header, Content, and Footer text
4) Choose a color in the Color dropdown, or use quick actions:
   - Open the component context menu (… on the component header) and pick Yellow/Green/Blue/Pink/Purple

Programmatic access

```csharp
using UnityEssentials;
using UnityEngine;

public class AddNote : MonoBehaviour
{
    void Start()
    {
        var note = gameObject.AddComponent<StickyNote>();
        note.Color = StickyNote.NoteColor.Blue;
        note.Header = "Spawner";
        note.Content = "Adjust rate before shipping";
        note.Footer = "Owned by Gameplay";
    }
}
```

## How It Works
- `StickyNote` holds serialized fields for color, header, content, and footer
- A custom inspector draws a rounded, colored background panel using a generated texture
- Text styles are themed per color (larger header font, smaller content/footer)
- Changing the color re‑creates styles to match the theme
- Context menu entries use `[ContextMenu]` to set the color and mark the object dirty

## Notes and Limitations
- Editor‑only rendering: the panel is an Inspector visualization, not a scene/game UI element
- No rich text or Markdown; fields are plain text (word‑wrapped)
- Very long content increases inspector height; collapse component to save space
- Color themes are fixed to presets; customize by editing the editor script
- Standard multi‑object editing rules apply

## Files in This Package
- `Runtime/StickyNote.cs` – Component (color, header, content, footer + context menu setters)
- `Editor/StickyNoteEditor.cs` – Custom inspector (themed background and text styles)
- `Runtime/UnityEssentials.StickyNote.asmdef` – Runtime assembly definition
- `Editor/UnityEssentials.StickyNote.Editor.asmdef` – Editor assembly definition

## Tags
unity, unity-editor, component, sticky-note, notes, documentation, annotation, inspector, editor-only, ui
