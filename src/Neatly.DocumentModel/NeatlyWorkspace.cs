using Neatly.DocumentModel.Properties;
using Neatly.Framework.Workspaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neatly.DocumentModel
{
    /// <summary>
    /// Represents the Neatly workspace.
    /// </summary>
    /// <seealso cref="Neatly.Framework.Workspaces.Workspace{Neatly.DocumentModel.Document}" />
    public sealed class NeatlyWorkspace : Workspace<Document>
    {
        #region Private Fields

        private static readonly JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.All,
            Formatting = Formatting.Indented,
            ReferenceLoopHandling = ReferenceLoopHandling.Serialize,
            PreserveReferencesHandling = PreserveReferencesHandling.Objects
        };

        #endregion Private Fields

        #region Public Events

        /// <summary>
        /// Occurs when the node has been opened.
        /// </summary>
        public event EventHandler<NodeEventArgs> NodeOpened;

        /// <summary>
        /// Occurs when the node has been selected.
        /// </summary>
        public event EventHandler<NodeEventArgs> NodeSelected;

        public event EventHandler<DocumentNodeAddedEventArgs> DocumentNodeAdded;

        #endregion Public Events

        #region Protected Properties

        /// <summary>
        /// Gets the description of the workspace file.
        /// </summary>
        /// <value>
        /// The workspace file description.
        /// </value>
        protected override string WorkspaceFileDescription => Resources.Workspace_FileDescription;

        /// <summary>
        /// Gets the file name extension of the workspace files. For example, "txt" or "csv", if
        /// the workspace is saved in .txt or .csv extensions.
        /// </summary>
        /// <value>
        /// The workspace file extension.
        /// </value>
        protected override string WorkspaceFileExtension => "ndoc";

        #endregion Protected Properties

        #region Public Methods

        /// <summary>
        /// Opens a node in the current workspace.
        /// </summary>
        /// <param name="node">The node which needs to be opened.</param>
        public void OpenNode(INode node)
        {
            NodeOpened?.Invoke(this, new NodeEventArgs(node));
        }

        /// <summary>
        /// Selects a node in the current workspace.
        /// </summary>
        /// <param name="node">The node which needs to be selected.</param>
        public void SelectNode(INode node)
        {
            NodeSelected?.Invoke(this, new NodeEventArgs(node));
        }

        public void AddDocumentNode(INode parent, string title, string content)
        {
            var documentNode = new DocumentNode(title, content);
            AddDocumentNode(parent, documentNode);
        }

        /// <summary>
        /// Adds the document node under the specified parent.
        /// </summary>
        /// <param name="parent">The parent node to which the document node should be added.</param>
        /// <param name="documentNode">The document node to be added.</param>
        public void AddDocumentNode(INode parent, DocumentNode documentNode)
        {
            parent.Add(documentNode);
            this.DocumentNodeAdded?.Invoke(this, new DocumentNodeAddedEventArgs(parent, documentNode));
        }

        #endregion Public Methods

        #region Protected Methods

        protected override (bool, Document) Create(WorkspaceModelEnricher<Document> enricher)
        {
            var document = new Document
            {
                Version = CurrentModelVersion
            };

            if (enricher != null)
            {
                return enricher(document);
            }

            return (true, document);
        }
                              
        protected override Document OpenFromFile(string fileName)
        {
            using (var zipToOpen = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                using (var archive = new ZipArchive(zipToOpen, ZipArchiveMode.Read))
                {
                    ZipArchiveEntry modelEntry = archive.GetEntry("model.json");
                    using (var reader = new StreamReader(modelEntry.Open()))
                    {
                        var json = reader.ReadToEnd();
                        var document = JsonConvert.DeserializeObject<Document>(json, jsonSerializerSettings);
                        return document;
                    }
                }
            }
        }

        protected override void SaveToFile(Document model, string fileName)
        {
            using (var zipToSave = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            {
                using (ZipArchive archive = new ZipArchive(zipToSave, ZipArchiveMode.Create))
                {
                    // Writes the model entry to the archive.
                    ZipArchiveEntry modelEntry = archive.CreateEntry("model.json", CompressionLevel.Optimal);
                    using (var writer = new StreamWriter(modelEntry.Open()))
                    {
                        writer.WriteLine(JsonConvert.SerializeObject(model, jsonSerializerSettings));
                    }

                    // Writers the version information to the archive.
                    ZipArchiveEntry versionEntry = archive.CreateEntry("VERSION", CompressionLevel.Optimal);
                    using (var writer = new StreamWriter(versionEntry.Open()))
                    {
                        writer.WriteLine(CurrentModelVersion.ToString());
                    }
                }
            }
        }

        #endregion Protected Methods
    }
}
