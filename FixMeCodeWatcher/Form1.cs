using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;

namespace FixMeCodeWatcher
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // FIXME use regex? 
        string[] extensions = new string[] { ".cs", ".java" };
        bool CheckFileType(string filename)
        {
            foreach (string ext in extensions)
                if (filename.EndsWith(ext))
                    return true;
            return false;
        }

        private void textBoxDirectory_TextChanged(object sender, EventArgs e)
        {
            if (watcher != null)
            {
                watcher.EnableRaisingEvents = false;
                watcher.Dispose();
                watcher = null;
            }
            if (Directory.Exists(textBoxDirectory.Text))
            {
                CreateFileWatcher(textBoxDirectory.Text);
                LoadDirectory(textBoxDirectory.Text);
                UpdateDisplay();
            }
        }

        FileSystemWatcher watcher = null;
        public void CreateFileWatcher(string path)
        {
            watcher = new FileSystemWatcher();
            watcher.Path = path;
            watcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite
               | NotifyFilters.FileName | NotifyFilters.DirectoryName | NotifyFilters.Size;
            watcher.Filter = "*";  
            watcher.IncludeSubdirectories = true;

            watcher.Changed += new FileSystemEventHandler(OnChanged);
            watcher.Created += new FileSystemEventHandler(OnChanged);
            watcher.Deleted += new FileSystemEventHandler(OnChanged);
            watcher.Renamed += new RenamedEventHandler(OnRenamed);

            watcher.EnableRaisingEvents = true;
        }

        // Define the event handlers.
        private void OnChanged(object source, FileSystemEventArgs e)
        {
            Action a = new Action(() => AddToChanged(e.FullPath));
            if (this.InvokeRequired)
            {
                this.Invoke(a);
            }
            else
            {
                a.Invoke();
            }
        }

        public void AddToOutput(string s)
        {
            textBoxOutput.Text += s + Environment.NewLine;

        }

        DateTime last_change = DateTime.Now;
        List<string> changed_filenames = new List<string>();
        public void AddToChanged(string filename)
        {
            if (CheckFileType(filename))
            {
                lock (changed_filenames)
                {
                    if (!changed_filenames.Contains(filename))
                        changed_filenames.Add(filename);
                    last_change = DateTime.Now;
                }
            }
        }

        private void OnRenamed(object source, RenamedEventArgs e)
        {
            Action a = new Action(() => AddToChanged(e.FullPath));
            if (this.InvokeRequired)
            {
                this.Invoke(a);
            }
            else
            {
                a.Invoke();
            }
        }

        private void buttonDirectoryPicker_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if ( fbd.ShowDialog() == DialogResult.OK)
            {
                textBoxDirectory.Text = fbd.SelectedPath;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string dir = Microsoft.Win32.Registry.GetValue(@"HKEY_CURRENT_USER\Software\McCrawCorp\FixMeCodeWatcher", "directory", "").ToString();
            if (dir != "")
                textBoxDirectory.Text = dir;
            string fixme = Microsoft.Win32.Registry.GetValue(@"HKEY_CURRENT_USER\Software\McCrawCorp\FixMeCodeWatcher", "todo", "1").ToString();
            if (fixme == "1")
                checkBoxToDo.Checked = true;
            string todo = Microsoft.Win32.Registry.GetValue(@"HKEY_CURRENT_USER\Software\McCrawCorp\FixMeCodeWatcher", "fixme", "1").ToString();
            if (todo == "1")
                checkBoxFixMe.Checked = true;

        }

        void LoadDirectory(string dirname)
        {
            string[] dirs = Directory.GetDirectories(dirname);
            string[] files = Directory.GetFiles(dirname);
            foreach (string dir in dirs)
                LoadDirectory(dir);
            foreach (string file in files)
                if (CheckFileType(file)) 
                    LoadFixMes(file);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (textBoxDirectory.Text != "")
                Microsoft.Win32.Registry.SetValue(@"HKEY_CURRENT_USER\Software\McCrawCorp\FixMeCodeWatcher", "directory", textBoxDirectory.Text);
            Microsoft.Win32.Registry.SetValue(@"HKEY_CURRENT_USER\Software\McCrawCorp\FixMeCodeWatcher", "todo", checkBoxToDo.Checked ? "1" : "0");
            Microsoft.Win32.Registry.SetValue(@"HKEY_CURRENT_USER\Software\McCrawCorp\FixMeCodeWatcher", "fixme", checkBoxFixMe.Checked ? "1" : "0");

        }

        public class FixMeFile
        {
            public class LineRef
            {
                public int line_number;
                public string line;
            }
            public string filename = "";
            public List<LineRef> FixMe = new List<LineRef>();
            public List<LineRef> ToDo = new List<LineRef>();

            public FixMeFile(string filename)
            {
                LoadFile(filename);
            }

            static Regex FixMeRegex = new Regex(@".*(//\s*[Ff][Ii][Xx][Mm][Ee].*)");
            static Regex ToDoRegex = new Regex(@".*(//\s*[Tt][Oo][Dd][Oo].*)");
            public void LoadFile(string filename)
            {
                this.filename = filename;
                StreamReader sr = null;
                try
                {
                    FixMe.Clear();
                    sr = new StreamReader(filename);
                    int line_number = 0;
                    while (!sr.EndOfStream)
                    {
                        ++line_number;
                        string line = sr.ReadLine();
                        Match m = FixMeRegex.Match(line);
                        if (m.Success)
                            FixMe.Add(new FixMeFile.LineRef() { line_number = line_number, line = m.Groups[1].Value });
                        m = ToDoRegex.Match(line);
                        if (m.Success)
                            ToDo.Add(new FixMeFile.LineRef() { line_number = line_number, line = m.Groups[1].Value });
                    }
                } 
                catch (Exception e)
                {

                }
                if (sr != null)
                    sr.Close();
            }

        }
        List<FixMeFile> FixMes = new List<FixMeFile>();

        private void timerReload_Tick(object sender, EventArgs e)
        {
            lock (changed_filenames)
            {
                if (last_change.AddSeconds(2) < DateTime.Now && changed_filenames.Count > 0)
                {
                    foreach (string filename in changed_filenames)
                    {
                        LoadFixMes(filename);
                    }
                    changed_filenames.Clear();
                }
            }
            UpdateDisplay();
        }


        void LoadFixMes(string filename)
        {

            try
            {
                bool found = false;
                foreach (FixMeFile fmf in FixMes)
                {
                    if (fmf.filename == filename)
                    {
                        found = true;
                        fmf.LoadFile(filename);
                        break;
                    }
                }
                if (!found)
                {
                    FixMes.Add(new FixMeFile(filename));
                }
            }
            catch (Exception e)
            {
                // FIXME put in error message 
            }

        }

        void UpdateDisplay()
        {
            StringBuilder sb = new StringBuilder();
            foreach (FixMeFile fmf in FixMes)
            {
                if ((checkBoxFixMe.Checked && fmf.FixMe.Count > 0) || (checkBoxToDo.Checked && fmf.ToDo.Count > 0))
                {
                    // remove the duplicated part of the full path to the file
                    sb.AppendLine(fmf.filename.Substring(textBoxDirectory.Text.Length + 1));
                    if (checkBoxFixMe.Checked && fmf.FixMe.Count > 0)
                    {
                        foreach (FixMeFile.LineRef lr in fmf.FixMe)
                            sb.AppendLine("   " + lr.line_number + ":" + lr.line);
                    }
                    if (checkBoxToDo.Checked && fmf.ToDo.Count > 0)
                    {
                        foreach (FixMeFile.LineRef lr in fmf.ToDo)
                            sb.AppendLine("   " + lr.line_number + ":" + lr.line);
                    }
                    sb.AppendLine();
                }
            }
            textBoxOutput.Text = sb.ToString();
        }

        private void checkBoxFixMe_CheckedChanged(object sender, EventArgs e)
        {
            UpdateDisplay();
        }

        private void checkBoxToDo_CheckedChanged(object sender, EventArgs e)
        {
            UpdateDisplay();
        }
    }
}
