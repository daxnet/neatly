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
    /// <summary>
    /// Represents a document of Neatly.
    /// </summary>
    [WorkspaceModelVersion(1, 0)]
    public sealed class Document : DocumentNodeCollection, IWorkspaceModel
    {

        #region Private Fields

        private string author;
        private string description;

        #endregion Private Fields

        #region Public Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Document"/> class.
        /// </summary>
        public Document()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Document"/> class.
        /// </summary>
        /// <param name="title">The title of the document.</param>
        public Document(string title)
            : this(title, null, null)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Document"/> class.
        /// </summary>
        /// <param name="title">The title of the document.</param>
        /// <param name="description">The description of the document.</param>
        /// <param name="author">The author of the document.</param>
        public Document(string title, string description, string author)
            : this()
        {
            this.title = title;
            this.description = description;
            this.author = author;
        }

        #endregion Public Constructors

        #region Public Properties

        /// <summary>
        /// Gets or sets the author of the current document.
        /// </summary>
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

        /// <summary>
        /// Gets or sets the description of the current document.
        /// </summary>
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

        /// <summary>
        /// Gets the parent of the document, always be null, since there is no parent for a document.
        /// </summary>
        public override INode Parent => null;

        /// <summary>
        /// Gets the type of the node of the current document. It is always be <c>NodeType.Document</c>.
        /// </summary>
        public override NodeType Type => NodeType.Document;

        /// <summary>
        /// Gets the version of the workspace model.
        /// </summary>
        public WorkspaceModelVersion Version { get; set; }

        #endregion Public Properties

        #region Public Methods

        public override string ToString() => title;

        #endregion Public Methods

    }
}
