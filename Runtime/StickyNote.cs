using UnityEngine;

namespace UnityEssentials
{
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
