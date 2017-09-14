using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.Permissions;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32.Security;
using System.Security.AccessControl;

namespace TestElevationPermissions
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        [System.Security.Permissions.PermissionSetAttribute(System.Security.Permissions.SecurityAction.Demand,Name="FullTrust")]
        private void executeElevationCode()
        {
            FileSystemAccessRule accessrule = new System.Security.AccessControl.FileSystemAccessRule("IUSR_CRACKLIN", "Modify", 0, 0, "Allow");
            //$acl.AddAccessRule($accessrule)
        }
    }
}
