using Neatly.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neatly.DocumentModel
{
    public abstract class DocumentNodeCollection : PropertyChangedNotifier, INode
    {
        protected Guid id = Guid.NewGuid();
        protected string title;
        protected DateTime dateCreated;
        protected DateTime? dateLastModified;
        protected INode parent;

        #region Protected Fields

        protected ObservableCollection<DocumentNode> children = new ObservableCollection<DocumentNode>();

        #endregion Protected Fields

        #region Public Constructors

        protected DocumentNodeCollection()
        {
            dateCreated = DateTime.UtcNow;
            BindObservableCollection(children);
        }

        #endregion Public Constructors

        #region Public Properties

        public IEnumerable<INode> ChildNodes => children;

        public int Count => children.Count;

        public virtual INode Parent
        {
            get => parent;
            internal set
            {
                parent = value;
            }
        }

        public abstract NodeType Type { get; }

        public string Title
        {
            get => title;
            set
            {
                if (title != value)
                {
                    title = value;
                    OnPropertyChanged(nameof(Title));
                }
            }
        }

        public DateTime DateCreated
        {
            get => dateCreated;
            set
            {
                if (dateCreated != value)
                {
                    dateCreated = value;
                    OnPropertyChanged(nameof(DateCreated));
                }
            }
        }

        public DateTime? DateLastModified
        {
            get => dateLastModified;
            set
            {
                if (dateLastModified != value)
                {
                    dateLastModified = value;
                    OnPropertyChanged(nameof(DateLastModified));
                }
            }
        }

        public Guid Id
        {
            get => id;
            set
            {
                if (id != value)
                {
                    id = value;
                    OnPropertyChanged(nameof(Id));
                }
            }
        }

        #endregion Public Properties

        #region Public Methods

        public void Add(DocumentNode documentNode)
        {
            if (documentNode == null)
            {
                throw new ArgumentNullException(nameof(documentNode));
            }
            if (Contains(documentNode))
            {
                throw new DocumentException("The specified Document Node has already existed.");
            }

            documentNode.Parent = this;
            this.children.Add(documentNode);
        }

        public void Clear()
        {
            foreach(var documentNode in children)
            {
                UnbindObject(documentNode);
            }

            children.Clear();
        }

        public bool Contains(DocumentNode documentNode) => children.Contains(documentNode);

        public IEnumerator<DocumentNode> GetEnumerator() => children.GetEnumerator();

        public bool Remove(DocumentNode documentNode)
        {
            if (documentNode != null)
            {
                UnbindObject(documentNode);
                return children.Remove(documentNode);
            }

            return false;
        }

        #endregion Public Methods
    }
}
