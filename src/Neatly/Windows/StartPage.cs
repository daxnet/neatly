﻿using Neatly.Sdk;
using Neatly.Sdk.Windows;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Neatly.Windows
{
    public partial class StartPage : BaseWindow
    {
        public StartPage(INeatlyShell shell)
            : base(shell)
        {
            InitializeComponent();
        }
    }
}
