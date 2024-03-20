using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Unideckbuildduel.View
{
    /// <summary>
    /// A simple startup dialog to be called at the start of a new game.
    /// </summary>
    public partial class StartupDialog : Form
    {
        /// <summary>
        /// The limit of turns selected by the user
        /// </summary>
        public int TurnLimit { get { return (int)turnLimitNumericUpDown.Value; } }

        public string Player1Name { get { return (string)player1Name.Text; } }

        public string Player2Name { get { return (string)player2Name.Text; } }  
        /// <summary>
        /// Empty-parametered constructor
        /// </summary>
        public StartupDialog()
        {
            InitializeComponent();
        }
    }
}
