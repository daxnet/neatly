using Neatly.Framework;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neatly.DocumentModel
{
    public sealed class DocumentNode : DocumentNodeCollection
    {
        private string content;
        
        private DocumentNode()
        {
            
        }

        public DocumentNode(string title, string content)
        {
            this.title = title;
            this.content = content;
        }

        public DocumentNode(INode parent, string title)
            : this(parent, title, null)
        {

        }

        public DocumentNode(INode parent, string title, string content)
            : this(title, content)
        {
            parent.Add(this);
        }

        public string Content
        {
            get => content;
            set
            {
                if (!string.Equals(content, value))
                {
                    content = value;
                    OnPropertyChanged(nameof(Content));
                }
            }
        }

        public override string ToString() => title;

        public override NodeType Type => NodeType.DocumentNode;
    }
}
