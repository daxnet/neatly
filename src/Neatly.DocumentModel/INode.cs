﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neatly.DocumentModel
{
    /// <summary>
    /// Represents the document nodes that have navigation properties
    /// </summary>
    public interface INode
    { 
        DateTime DateCreated { get; set; }

        DateTime? DateLastModified { get; set; }

        INode Parent { get; }

        IEnumerable<DocumentNode> ChildNodes { get; }

        void Add(DocumentNode documentNode);
    }
}