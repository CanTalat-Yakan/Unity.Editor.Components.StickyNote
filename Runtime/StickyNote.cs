using UnityEngine;

namespace UnityEssentials
{
    /// <summary>
    /// Represents a customizable sticky note with a header, content, and footer.
    /// </summary>
    /// <remarks>This class provides properties to manage the text content of a sticky note, including its
    /// header, main content, and footer. It is designed to be used in Unity projects as a MonoBehaviour
    /// component.</remarks>
    public partial class StickyNote : MonoBehaviour
    {
        public enum NoteColor
        {
            Yellow,
            Green,
            Blue,
            Pink,
            Purple
        }

        [SerializeField] private NoteColor _color = NoteColor.Yellow;
        public NoteColor Color
        {
            get => _color;
            set => _color = value;
        }

        [SerializeField, TextArea()] private string _header = "New Note";
        [SerializeField, TextArea()] private string _content = "Write something here";
        [SerializeField, TextArea()] private string _footer = "Footer";

        public string Header
        {
            get => _header;
            set => _header = value;
        }

        public string Content
        {
            get => _content;
            set => _content = value;
        }

        public string Footer
        {
            get => _footer;
            set => _footer = value;
        }

#if UNITY_EDITOR
        [ContextMenu("Yellow")]
        private void SetColorYellow()
        {
            _color = NoteColor.Yellow;
#if UNITY_EDITOR
            UnityEditor.EditorUtility.SetDirty(this);
#endif
        }

        [ContextMenu("Green")]
        private void SetColorGreen()
        {
            _color = NoteColor.Green;
#if UNITY_EDITOR
            UnityEditor.EditorUtility.SetDirty(this);
#endif
        }

        [ContextMenu("Blue")]
        private void SetColorBlue()
        {
            _color = NoteColor.Blue;
#if UNITY_EDITOR
            UnityEditor.EditorUtility.SetDirty(this);
#endif
        }

        [ContextMenu("Pink")]
        private void SetColorPink()
        {
            _color = NoteColor.Pink;
#if UNITY_EDITOR
            UnityEditor.EditorUtility.SetDirty(this);
#endif
        }

        [ContextMenu("Purple")]
        private void SetColorPurple()
        {
            _color = NoteColor.Purple;
#if UNITY_EDITOR
            UnityEditor.EditorUtility.SetDirty(this);
#endif
        }
#endif
    }
}
