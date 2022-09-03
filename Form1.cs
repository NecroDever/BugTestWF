using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BugTestWF
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Environment.GetCommandLineArgs().Where(x => x.EndsWith("key")).ToList().ForEach(x =>
            {
                MessageBox.Show("test");
                Application.Exit();
                Environment.Exit(0); // Выйдет, а остальное вызовет баг с автонажатием кнопки да
                Close();
                return;
            });

            var path = @"Directory\background\shell\Useless ARW\command";

            using (var key = Registry.ClassesRoot.OpenSubKey(path, true))
            {
                if (key == null)
                {
                    if (MessageBox.Show("Хотите добавить?", Text, MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        Registry.ClassesRoot.CreateSubKey(path).SetValue("", $@"{Assembly.GetEntryAssembly().Location} /key");
                    }
                }
                else
                {
                    if (MessageBox.Show("Хотите удалить?", Text, MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        Registry.ClassesRoot.DeleteSubKeyTree(@"Directory\background\shell\Useless ARW\", false);
                    }
                }
            }
            Application.Exit();
            //Close();
        }
    }
}
