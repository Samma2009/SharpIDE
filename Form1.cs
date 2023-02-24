using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Interop;

namespace WindowsFormsApp5
{
    public partial class Form1 : Form
    {

        string savepath = "";

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            if (Directory.Exists(Application.StartupPath + @"\DllFiles"))
            { }
            else
            {

                Directory.CreateDirectory(Application.StartupPath + @"\DllFiles");

            }

        }

        private void button1_Click(object sender, EventArgs e)
        {

            CSharpCodeProvider provider = new CSharpCodeProvider();
            CompilerParameters parameters = new CompilerParameters();
            parameters.ReferencedAssemblies.Add(Assembly.GetEntryAssembly().Location);
            foreach (var item in Directory.GetFiles(Application.StartupPath + @"\DllFiles"))
            {

                if(Path.GetExtension(item) == ".dll")
                parameters.ReferencedAssemblies.Add(Path.GetFileName(item));

            }
            parameters.GenerateInMemory = true;
            parameters.GenerateExecutable = true;
            parameters.OutputAssembly = Application.StartupPath + @"\Program.exe";

            CompilerResults results = provider.CompileAssemblyFromSource(parameters, fastColoredTextBox1.Text);
            if (results.Errors.HasErrors)
            {
                string errors = "";
                foreach (CompilerError error in results.Errors)
                {
                    errors += string.Format("Error #{0}: {1}\n", error.ErrorNumber, error.ErrorText);
                }
                //Console.Write(errors);
                richTextBox1.Text += errors + "\n";
            }
            else
            {
                Assembly assembly = results.CompiledAssembly;
                //Type program = assembly.GetType("CTFGame.MyPlayer");
                //MethodInfo main = program.GetMethod("Main");
                //main.Invoke(null, null);

                foreach (var item in assembly.GetTypes())
                {

                    var a = item.GetMethod("Main");

                    //a.Invoke(null,null);
                    Process.Start(Application.StartupPath + @"\Program.exe");

                }

            }

        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

            if (savepath == "")
            {

                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {

                    File.WriteAllText(saveFileDialog1.FileName, fastColoredTextBox1.Text);
                    this.Text = Path.GetFileNameWithoutExtension(saveFileDialog1.FileName) + " - SharpIDE";
                    savepath = saveFileDialog1.FileName;

                }

            }
            else
                File.WriteAllText(savepath, fastColoredTextBox1.Text);

        }

        public void ALT()
        {

            richTextBox1.Text += "ALT! da qui non si passa \n";

        }

        private void button4_Click(object sender, EventArgs e)
        {

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {

                fastColoredTextBox1.Text = File.ReadAllText(openFileDialog1.FileName);
                this.Text = Path.GetFileNameWithoutExtension(openFileDialog1.FileName) + " - SharpIDE";
                savepath = openFileDialog1.FileName;

            }
                

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
