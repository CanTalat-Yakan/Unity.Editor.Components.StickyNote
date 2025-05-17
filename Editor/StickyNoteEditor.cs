using UnityEditor;
using UnityEngine;

namespace UnityEssentials
{
    [CustomEditor(typeof(StickyNote))]
    public class StickyNoteEditor : Editor
    {
        private static readonly Color s_noteBackgroundColor = new Color(0.99f, 0.915f, 0.69f, 1f);
        private static readonly Color s_textColor = new Color(0.25f, 0.2f, 0.1f, 1f);

        private const float _minHeaderHeight = 30f;
        private const float _minContentHeight = 50f;
        private const float _minFooterHeight = 0f;
        private const float _padding = 8f;
        private const float _offsetWidth = 35;

        private GUIStyle _backgroundStyle;
        private GUIStyle _headerTextStyle;
        private GUIStyle _contentTextStyle;
        private GUIStyle _footerTextStyle;

        public void OnEnable() => CreateStyles();

        public override void OnInspectorGUI()
        {
            StickyNote stickyNote = (StickyNote)target;
            if (stickyNote == null) return;

            if (_backgroundStyle == null || _headerTextStyle == null || _contentTextStyle == null || _footerTextStyle == null)
                CreateStyles();

            Color oldColor = GUI.color;
            GUI.color = s_noteBackgroundColor;

            EditorGUILayout.BeginVertical(_backgroundStyle);
            GUI.color = oldColor;

            // Calculate header height with padding consideration
            float headerWidth = EditorGUIUtility.currentViewWidth - _padding * 2;
            float headerHeight = _headerTextStyle.CalcHeight(new GUIContent(stickyNote.Header), headerWidth);
            headerHeight = Mathf.Max(headerHeight, _minHeaderHeight);

            // Draw header without background
            Rect headerRect = GUILayoutUtility.GetRect(headerWidth, headerHeight);
            stickyNote.Header = EditorGUI.TextField(headerRect, stickyNote.Header, _headerTextStyle);

            EditorGUILayout.Space(10); // Add spacing

            // Calculate content height with padding consideration
            float contentWidth = EditorGUIUtility.currentViewWidth - _padding * 2;
            float contentHeight = _contentTextStyle.CalcHeight(new GUIContent(stickyNote.Content), contentWidth - _offsetWidth);
            contentHeight = Mathf.Max(contentHeight, _minContentHeight);

            // Draw content without background
            Rect contentRect = GUILayoutUtility.GetRect(contentWidth, contentHeight);
            stickyNote.Content = EditorGUI.TextField(contentRect, stickyNote.Content, _contentTextStyle);

            EditorGUILayout.Space(10); // Add spacing

            // Calculate content height with padding consideration
            float footerWidth = EditorGUIUtility.currentViewWidth - _padding * 2;
            float footerHeight = _footerTextStyle.CalcHeight(new GUIContent(stickyNote.Footer), footerWidth - _offsetWidth);
            contentHeight = Mathf.Max(contentHeight, _minFooterHeight);

            // Draw content without background
            Rect footerRect = GUILayoutUtility.GetRect(footerWidth, footerHeight);
            stickyNote.Footer = EditorGUI.TextField(footerRect, stickyNote.Footer, _footerTextStyle);

            EditorGUILayout.EndVertical();

            if (GUI.changed)
                EditorUtility.SetDirty(stickyNote);
        }

        private void CreateStyles()
        {
            // Background style
            _backgroundStyle = new GUIStyle
            {
                normal = { background = CreateColorTexture(2, 2, s_noteBackgroundColor) },
                border = new RectOffset(5, 5, 5, 5),
                margin = new RectOffset(5, 15, 5, 5),
                padding = new RectOffset(
                    Mathf.RoundToInt(_padding),
                    Mathf.RoundToInt(_padding),
                    Mathf.RoundToInt(_padding),
                    Mathf.RoundToInt(_padding))
            };

            // Header text style
            _headerTextStyle = new GUIStyle
            {
                fontSize = 24,
                wordWrap = true,
                normal = { textColor = s_textColor },
                hover = { textColor = s_textColor },
                focused = { textColor = s_textColor },
                active = { textColor = s_textColor },
                alignment = TextAnchor.UpperLeft,
                padding = new RectOffset(0, 0, 0, 0),
                margin = new RectOffset(0, 0, 0, 0)
            };

            // Remove all background textures
            _headerTextStyle.normal.background = null;
            _headerTextStyle.hover.background = null;
            _headerTextStyle.focused.background = null;
            _headerTextStyle.active.background = null;

            // Content and footer text style
            _contentTextStyle = new GUIStyle(_headerTextStyle) { fontSize = 14 };
            _footerTextStyle = new GUIStyle(_headerTextStyle) { fontSize = 14 };
        }

        private Texture2D CreateColorTexture(int width, int height, Color col)
        {
            Color[] pixels = new Color[width * height];
            for (int i = 0; i < pixels.Length; i++)
                pixels[i] = col;

            Texture2D result = new Texture2D(width, height);
            result.SetPixels(pixels);
            result.Apply();
            return result;
        }
    }
}