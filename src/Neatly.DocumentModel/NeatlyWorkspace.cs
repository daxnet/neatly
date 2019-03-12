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
    public sealed class NeatlyWorkspace : Workspace<Document>
    {
        private static readonly JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.All,
            Formatting = Formatting.Indented,
            ReferenceLoopHandling = ReferenceLoopHandling.Serialize,
            PreserveReferencesHandling = PreserveReferencesHandling.Objects
        };

        protected override string WorkspaceFileDescription => Resources.Workspace_FileDescription;

        protected override string WorkspaceFileExtension => "ndoc";

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
    }
}
