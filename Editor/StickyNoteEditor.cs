#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace UnityEssentials
{
    /// <summary>
    /// A custom editor for the <see cref="StickyNote"/> component, providing a styled interface for editing the header,
    /// content, and footer fields in the Unity Inspector.
    /// </summary>
    /// <remarks>This editor customizes the appearance of the <see cref="StickyNote"/> component in the Unity
    /// Inspector by applying a visually distinct background and text styles. It ensures that the header, content, and
    /// footer fields are displayed with appropriate padding, alignment, and minimum dimensions for better readability
    /// and usability.  The editor dynamically calculates the required height for each field based on its content and
    /// enforces minimum height constraints to maintain a consistent layout. Custom styles are created for the
    /// background and text elements to match the visual theme of a sticky note.</remarks>
    [CustomEditor(typeof(StickyNote))]
    public partial class StickyNoteEditor : Editor
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

        /// <summary>
        /// Initializes the necessary styles when the component is enabled.
        /// </summary>
        /// <remarks>This method is called automatically when the component is enabled. It ensures that
        /// any required styles are created and ready for use.</remarks>
        public void OnEnable() => 
            CreateStyles();

        /// <summary>
        /// Draws and handles the custom inspector GUI for the associated object.
        /// </summary>
        /// <remarks>This method overrides the default inspector rendering to provide a custom layout for
        /// editing the object's properties, including a header, content, and footer section. It ensures that serialized
        /// properties are updated and displayed with custom styles, and applies any changes made by the user.  The
        /// method dynamically calculates the layout dimensions for each section based on the current view width and
        /// applies padding and minimum height constraints. Custom styles are created if they are not already
        /// initialized.</remarks>
        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            SerializedProperty headerProp = serializedObject.FindProperty("_header");
            SerializedProperty contentProp = serializedObject.FindProperty("_content");
            SerializedProperty footerProp = serializedObject.FindProperty("_footer");

            if (_backgroundStyle == null || _headerTextStyle == null || _contentTextStyle == null || _footerTextStyle == null)
                CreateStyles();

            var oldColor = GUI.color;
            GUI.color = s_noteBackgroundColor;
            EditorGUILayout.BeginVertical(_backgroundStyle);
            GUI.color = oldColor;

            var headerWidth = EditorGUIUtility.currentViewWidth - _padding * 2;
            var headerHeight = _headerTextStyle.CalcHeight(new GUIContent(headerProp.stringValue), headerWidth);
            headerHeight = Mathf.Max(headerHeight, _minHeaderHeight);
            var headerPosition = GUILayoutUtility.GetRect(headerWidth, headerHeight);
            headerProp.stringValue = EditorGUI.TextField(headerPosition, headerProp.stringValue, _headerTextStyle);

            EditorGUILayout.Space(10);

            var contentWidth = EditorGUIUtility.currentViewWidth - _padding * 2;
            var contentHeight = _contentTextStyle.CalcHeight(new GUIContent(contentProp.stringValue), contentWidth - _offsetWidth);
            contentHeight = Mathf.Max(contentHeight, _minContentHeight);
            var contentPosition = GUILayoutUtility.GetRect(contentWidth, contentHeight);
            contentProp.stringValue = EditorGUI.TextField(contentPosition, contentProp.stringValue, _contentTextStyle);

            EditorGUILayout.Space(10);

            var footerWidth = EditorGUIUtility.currentViewWidth - _padding * 2;
            var footerHeight = _footerTextStyle.CalcHeight(new GUIContent(footerProp.stringValue), footerWidth - _offsetWidth);
            footerHeight = Mathf.Max(footerHeight, _minFooterHeight);
            var footerPosition = GUILayoutUtility.GetRect(footerWidth, footerHeight);
            footerProp.stringValue = EditorGUI.TextField(footerPosition, footerProp.stringValue, _footerTextStyle);

            EditorGUILayout.EndVertical();

            serializedObject.ApplyModifiedProperties();
        }

        /// <summary>
        /// Initializes and configures the styles used for rendering GUI elements, including background, header,
        /// content, and footer styles.
        /// </summary>
        /// <remarks>This method creates and assigns various <see cref="GUIStyle"/> objects to private
        /// fields for use in rendering. The styles include configurations for background textures, text alignment, font
        /// sizes, padding, and margins.</remarks>
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

        /// <summary>
        /// Creates a 2D texture filled with a specified color.
        /// </summary>
        /// <remarks>The returned texture is initialized with the specified dimensions and filled entirely
        /// with the given color. The texture's pixels are applied immediately after being set.</remarks>
        /// <param name="width">The width of the texture, in pixels. Must be greater than 0.</param>
        /// <param name="height">The height of the texture, in pixels. Must be greater than 0.</param>
        /// <param name="color">The color to fill the texture with.</param>
        /// <returns>A <see cref="Texture2D"/> object filled with the specified color.</returns>
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