#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

namespace UnityEssentials
{
    /// <summary>
    /// Provides a custom editor for the <see cref="StickyNote"/> component, enabling enhanced visualization and editing
    /// of sticky note properties in the Unity Inspector.
    /// </summary>
    /// <remarks>This editor customizes the appearance and behavior of the <see cref="StickyNote"/> component
    /// in the Unity Inspector, including support for color-coded backgrounds and text styles based on the note's color.
    /// It allows users to edit the header, content, and footer text fields directly within the Inspector, while
    /// dynamically updating the styles to match the selected note color.</remarks>
    [CustomEditor(typeof(StickyNote))]
    public partial class StickyNoteEditor : Editor
    {
        private static readonly Dictionary<StickyNote.NoteColor, Color> s_noteColors =
            new Dictionary<StickyNote.NoteColor, Color>
            {
                { StickyNote.NoteColor.Yellow, new Color(0.99f, 0.915f, 0.69f, 1f) },
                { StickyNote.NoteColor.Green,  new Color(0.80f, 0.97f, 0.80f, 1f) },
                { StickyNote.NoteColor.Blue,   new Color(0.80f, 0.90f, 0.98f, 1f) },
                { StickyNote.NoteColor.Pink,   new Color(0.98f, 0.80f, 0.90f, 1f) },
                { StickyNote.NoteColor.Purple, new Color(0.92f, 0.85f, 0.98f, 1f) },
            };

        private static readonly Dictionary<StickyNote.NoteColor, Color> s_textColors =
            new Dictionary<StickyNote.NoteColor, Color>
            {
                { StickyNote.NoteColor.Yellow, new Color(0.25f, 0.2f, 0.1f, 1f) },   // dark brown
                { StickyNote.NoteColor.Green,  new Color(0.18f, 0.28f, 0.18f, 1f) }, // dark green
                { StickyNote.NoteColor.Blue,   new Color(0.18f, 0.22f, 0.28f, 1f) }, // dark blue
                { StickyNote.NoteColor.Pink,   new Color(0.32f, 0.18f, 0.22f, 1f) }, // dark pink
                { StickyNote.NoteColor.Purple, new Color(0.25f, 0.18f, 0.28f, 1f) }, // dark purple
            };

        private StickyNote.NoteColor _currentColor = StickyNote.NoteColor.Yellow;
        private Color _lastBgColor = s_noteColors[StickyNote.NoteColor.Yellow];
        private Color _lastTextColor = s_textColors[StickyNote.NoteColor.Yellow];

        private GUIStyle _backgroundStyle;
        private GUIStyle _headerTextStyle;
        private GUIStyle _contentTextStyle;
        private GUIStyle _footerTextStyle;

        public void OnEnable() =>
            CreateStyles(s_noteColors[StickyNote.NoteColor.Yellow], s_textColors[StickyNote.NoteColor.Yellow]);

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            SerializedProperty headerProp = serializedObject.FindProperty("_header");
            SerializedProperty contentProp = serializedObject.FindProperty("_content");
            SerializedProperty footerProp = serializedObject.FindProperty("_footer");
            SerializedProperty colorProp = serializedObject.FindProperty("_color");

            _currentColor = (StickyNote.NoteColor)colorProp.enumValueIndex;

            var backgroundColor = s_noteColors.TryGetValue(_currentColor, out var c) ? c : s_noteColors[StickyNote.NoteColor.Yellow];
            var textColor = s_textColors.TryGetValue(_currentColor, out var t) ? t : s_textColors[StickyNote.NoteColor.Yellow];

            if (_backgroundStyle == null ||
                _headerTextStyle == null ||
                _contentTextStyle == null ||
                _footerTextStyle == null ||
                _backgroundStyle.normal.background == null ||
                (_backgroundStyle.normal.background is Texture2D tex && tex == null) ||
                _lastBgColor != backgroundColor ||
                _lastTextColor != textColor)
            {
                CreateStyles(backgroundColor, textColor);
                _lastBgColor = backgroundColor;
                _lastTextColor = textColor;
            }

            var oldColor = GUI.color;
            GUI.color = backgroundColor;
            EditorGUILayout.BeginVertical(_backgroundStyle);
            GUI.color = oldColor;

            headerProp.stringValue = EditorGUILayout.TextArea(headerProp.stringValue, _headerTextStyle);
            EditorGUILayout.Space(10);
            contentProp.stringValue = EditorGUILayout.TextArea(contentProp.stringValue, _contentTextStyle);
            EditorGUILayout.Space(10);
            footerProp.stringValue = EditorGUILayout.TextArea(footerProp.stringValue, _footerTextStyle);

            EditorGUILayout.EndVertical();

            serializedObject.ApplyModifiedProperties();
        }

        private void CreateStyles(Color bgColor, Color textColor)
        {
            // Background style
            _backgroundStyle = new GUIStyle
            {
                normal = { background = CreateColorTexture(2, 2, bgColor) },
                border = new RectOffset(5, 5, 5, 5),
                margin = new RectOffset(5, 15, 5, 5),
                padding = new RectOffset(
                    Mathf.RoundToInt(8),
                    Mathf.RoundToInt(8),
                    Mathf.RoundToInt(8),
                    Mathf.RoundToInt(8))
            };

            // Text styles
            _headerTextStyle = new GUIStyle
            {
                fontSize = 24,
                wordWrap = true,
                normal = { textColor = textColor },
                hover = { textColor = textColor },
                focused = { textColor = textColor },
                active = { textColor = textColor },
                alignment = TextAnchor.UpperLeft,
                padding = new RectOffset(0, 0, 0, 0),
                margin = new RectOffset(0, 0, 0, 0)
            };

            _headerTextStyle.normal.background = null;
            _headerTextStyle.hover.background = null;
            _headerTextStyle.focused.background = null;
            _headerTextStyle.active.background = null;

            _contentTextStyle = new GUIStyle(_headerTextStyle) { fontSize = 14 };
            _footerTextStyle = new GUIStyle(_headerTextStyle) { fontSize = 14 };
        }

        private Texture2D CreateColorTexture(int width, int height, Color color)
        {
            var pixels = new Color[width * height];
            for (int i = 0; i < pixels.Length; i++)
                pixels[i] = color;

            var result = new Texture2D(width, height);
            result.SetPixels(pixels);
            result.Apply();
            return result;
        }
    }
}
#endif