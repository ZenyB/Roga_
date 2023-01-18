using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roga
{
    public partial class CustomTextbox : RichTextBox
    {
        public CustomTextbox()
        {
            InitializeComponent();
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            BackColor = Color.Transparent;

            ForeColor = Color.Black;
        }

        public sealed override Color BackColor
        {
            get => base.BackColor;
            set => base.BackColor = value;
        }

        public CustomTextbox(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
    }
}
