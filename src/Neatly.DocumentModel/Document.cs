using Neatly.Framework;
using Neatly.Framework.Workspaces;
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
    [WorkspaceModelVersion(1, 0)]
    public sealed class Document : DocumentNodeCollection, IWorkspaceModel
    {
        #region Private Fields
        private string author;
        private string description;
        private string title;

        #endregion Private Fields

        public Document()
        {
        }

        public Document(string title)
            : this(title, null, null)
        { }

        public Document(string title, string description, string author)
            : this()
        {
            this.title = title;
            this.description = description;
            this.author = author;
        }

        #region Public Properties

        public string Author
        {
            get => author;
            set
            {
                if (!string.Equals(author, value))
                {
                    author = value;
                    this.OnPropertyChanged(nameof(Author));
                }
            }
        }

        public string Description
        {
            get => description;
            set
            {
                if (!string.Equals(description, value))
                {
                    description = value;
                    this.OnPropertyChanged(nameof(Description));
                }
            }
        }

        public override INode Parent => null;

        public string Title
        {
            get => title;
            set
            {
                if (!string.Equals(title, value))
                {
                    title = value;
                    this.OnPropertyChanged(nameof(Title));
                }
            }
        }
        public WorkspaceModelVersion Version { get; set; }

        public override string ToString() => title;

        #endregion Public Properties

    }
}
