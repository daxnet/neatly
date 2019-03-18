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
        #region Protected Fields

        protected string tag;
        protected ObservableCollection<DocumentNode> children = new ObservableCollection<DocumentNode>();
        protected DateTime dateCreated;
        protected DateTime? dateLastModified;
        protected Guid id = Guid.NewGuid();
        protected INode parent;
        protected string title;

        #endregion Protected Fields

        #region Protected Constructors

        protected DocumentNodeCollection()
        {
            dateCreated = DateTime.UtcNow;
            BindObservableCollection(children);
        }

        #endregion Protected Constructors

        #region Public Properties

        public IEnumerable<INode> ChildNodes => children;

        public int Count => children.Count;

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

        public virtual INode Parent
        {
            get => parent;
            internal set
            {
                parent = value;
            }
        }

        public string Tag
        {
            get => tag;
            set
            {
                if (tag != value)
                {
                    tag = value;
                    OnPropertyChanged(nameof(Tag));
                }
            }
        }

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

        public abstract NodeType Type { get; }

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

        public DocumentNode GetChildDocumentNodeById(Guid id)
        {
            return children.Where(c => c.Id.Equals(id)).FirstOrDefault();
        }

        public IEnumerable<DocumentNode> GetChildDocumentNodesByTitle(string title)
        {
            return children.Where(c => string.Equals(c.Title, title));
        }

        public bool Contains(DocumentNode documentNode) => children.Contains(documentNode);

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj == null)
            {
                return false;
            }

            return obj is DocumentNodeCollection dnc && Guid.Equals(id, dnc.id);
        }

        public IEnumerator<DocumentNode> GetEnumerator() => children.GetEnumerator();

        public override int GetHashCode()
        {
            var hashCode = 999554665;
            hashCode = hashCode * -1521134295 + EqualityComparer<Guid>.Default.GetHashCode(id);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(title);
            hashCode = hashCode * -1521134295 + dateCreated.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<DateTime?>.Default.GetHashCode(dateLastModified);
            hashCode = hashCode * -1521134295 + EqualityComparer<INode>.Default.GetHashCode(parent);
            hashCode = hashCode * -1521134295 + EqualityComparer<ObservableCollection<DocumentNode>>.Default.GetHashCode(children);
            return hashCode;
        }

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
