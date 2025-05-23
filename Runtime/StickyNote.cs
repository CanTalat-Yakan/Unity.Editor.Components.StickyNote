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
    }
}
