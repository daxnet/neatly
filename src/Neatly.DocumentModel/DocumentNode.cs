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
        private string title;
        private string content;
        private INode parent;
        
        private DocumentNode()
        {
            
        }

        public DocumentNode(INode parent, string title)
            : this(parent, title, null)
        {

        }

        public DocumentNode(INode parent, string title, string content)
        {
            this.parent = parent;
            this.parent.Add(this);

            this.title = title;
            this.content = content;
        }

        public string Title
        {
            get => title;
            set
            {
                if (!string.Equals(title, value))
                {
                    title = value;
                    OnPropertyChanged(nameof(Title));
                }
            }
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

        public override INode Parent => parent;
    }
}
