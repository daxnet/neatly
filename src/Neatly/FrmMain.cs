using Neatly.Controls;
using Neatly.Properties;
using Neatly.Windows;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace Neatly
{
    public partial class FrmMain : Form
    {
        private ActionComponent newDocumentAction;
        private ActionComponent openDocumentAction;
        private ActionComponent saveDocumentAction;

        public FrmMain()
        {
            InitializeComponent();

            
            InitializeActionComponent();
            InitializeDockingSurface();
        }

        private void InitializeDockingSurface()
        {
            var navigationWindow = new DocumentNavigatorWindow();
            navigationWindow.Show(dockPanel, DockState.DockLeft);
        }

        private void InitializeActionComponent()
        {
            newDocumentAction = new ActionComponent(mnuNewDocument, tbtnNewDocument, Resources.Tooltip_NewDocument, Action_NewDocument);
            openDocumentAction = new ActionComponent(mnuOpenDocument, tbtnOpenDocument, Resources.Tooltip_OpenDocument, Action_OpenDocument);
            saveDocumentAction = new ActionComponent(mnuSaveDocument, tbtnSaveDocument, Resources.Tooltip_SaveDocument, Action_SaveDocument);
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);

            newDocumentAction.Dispose();
            openDocumentAction.Dispose();
            saveDocumentAction.Dispose();
        }

        private void Action_NewDocument(object sender, EventArgs e)
        {

        }

        private void Action_OpenDocument(object sender, EventArgs e)
        {

        }

        private void Action_SaveDocument(object sender, EventArgs e)
        {
            
        }
    }
}
