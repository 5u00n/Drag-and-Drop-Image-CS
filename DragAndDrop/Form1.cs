using System;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Windows.Forms;
//using Microsoft.VisualStudio.TestTools.UITesting.HtmlControls;
namespace DragAndDrop
{
    public partial class Form1 : Form
    {
        DisplayScreen ds;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            listBox1.AllowDrop = true;
            //pictureBox1.Load("http://www.yinandyangliving.com/wp-content/uploads/2015/09/Intentions670X400.jpg");

            ds = new DisplayScreen();
            ds.StartPosition = FormStartPosition.Manual;
          //  ds.StartPosition = FormStartPosition.Manual;
            ds.Location = new Point(2000, 10);
            ds.WindowState = FormWindowState.Maximized;
        }



        public static string getBetween(string strSource)
        {
            string strStart = "src=\"";
            string strEnd = "\" data-noaft=\"1\"";
            int Start, End,Rstr;
            if (strSource.Contains(strStart) && strSource.Contains(strEnd))
            {
                Start = strSource.IndexOf(strStart, 0) + strStart.Length;
                End = strSource.IndexOf(strEnd, Start);
                if (strSource.Substring(Start, End - Start).Contains("?"))
                {
                    
                    return strSource.Substring(Start, End - Start).Substring(0, strSource.Substring(Start, End - Start).IndexOf("?"));
                }
                else
                {
                    return strSource.Substring(Start, End - Start);
                }
            }
            else
            {
                return "";
            }
        }
        private void listBox1_DragDrop(object sender, DragEventArgs e)
        {
            AllowDrop = false;
            GC.Collect();
            GC.Collect();
            AllowDrop = true;
            #region For gettin values from html source

            // Console.WriteLine(getBetween((string)e.Data.GetData(DataFormats.Html)));
            // Console(str.Contains("src"))
            /*foreach (Match m in Regex.Matches((string)e.Data.GetData(DataFormats.Html), "<!--StartFragment--><img<img>.</img>.<!--EndFragment-->"))
            {
                // Output your values here
                Console.WriteLine(m.Groups[1].Value);
            }*/
            #endregion
            #region Finding approprite string from source
            //cvhcjv
            /* Console.WriteLine("--Text--");
              Console.WriteLine(e.Data.GetData(DataFormats.Text));
              Console.WriteLine("--Unocode Text--");
              Console.WriteLine(e.Data.GetData(DataFormats.UnicodeText));
              Console.WriteLine("--Html--");
              Console.WriteLine(e.Data.GetData(DataFormats.Html));
              Console.WriteLine("--Bitmap--");
              Console.WriteLine(e.Data.GetData(DataFormats.Bitmap));
              Console.WriteLine("--FileDrop--");
              Console.WriteLine(e.Data.GetData(DataFormats.FileDrop));
              Console.WriteLine("--String Format--");
              Console.WriteLine(e.Data.GetData(DataFormats.StringFormat));
              Console.WriteLine("--Dib--");
              Console.WriteLine(e.Data.GetData(DataFormats.Dib));
              Console.WriteLine("--Dif--");
              Console.WriteLine(e.Data.GetData(DataFormats.Dif));
              Console.WriteLine("--Enhanced Meta File--");
              Console.WriteLine(e.Data.GetData(DataFormats.EnhancedMetafile));
              Console.WriteLine("--Riff--");
              Console.WriteLine(e.Data.GetData(DataFormats.Riff));
              Console.WriteLine("--Rtf--");
              Console.WriteLine(e.Data.GetData(DataFormats.Rtf));
              Console.WriteLine("--Tiff--");
              Console.WriteLine(e.Data.GetData(DataFormats.Tiff));*/
            #endregion
            #region Getting files from a drag and drop (working)
            var dd = e.Data.GetData(DataFormats.Text);
            if (dd == null)
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop, false);
                foreach (string file in files)
                {
                    listBox1.Items.Add(file);
                    Console.WriteLine(file);
                    pictureBox1.Image = Image.FromFile(file);
                    //THEN DO WHATEVER YOU WANT TO EACH file in files
                    //e.g. 

                }
            }
            else
            {
                String s = getBetween((string)e.Data.GetData(DataFormats.Html));
                Bitmap b = getbitmap(s);
                if (s.Contains("http") && b!=null) { 
                 listBox1.Items.Add(s);
                 pictureBox1.Image=b;
                }

            }

            #endregion
            load_second();

            #region See type of object ( not using )
            /*   Type[] types = { typeof(String), typeof(int[]),
                          typeof(ArrayList), typeof(Array),
                          typeof(List<String>),
                          typeof(IEnumerable<Char>) };
               foreach (var t in types)
                   Console.WriteLine("{0,-15} IsArray = {1}", t.Name + ":",
                                     t.IsArray);*/

            //Console.WriteLine(dd.GetType());

            #endregion
            #region Load Pictues and get info (previous)
            //pictureBox1.Image = LoadPicture((string)e.Data.GetData(DataFormats.Text));
            //  pictureBox1.Load((string)e.Data.GetData(DataFormats.Text));
            //  listBox1.Items.Add((string)e.Data.GetData(DataFormats.Text,false));
            /*     string[] files = (string[])e.Data.GetData(DataFormats.Text, false);
                 //string[] filePaths = (e.Data.GetData(DataFormats.Html) as string[]);
                // Console.WriteLine(filePaths);
                  foreach (string file in files)
                  {
                       listBox1.Items.Add(file);
                       Console.WriteLine(file);
                     // pictureMain.Image = Image.FromFile(file);
                      //THEN DO WHATEVER YOU WANT TO EACH file in files
                      //e.g. 



                  }*/
            #endregion
        }
        public Bitmap getbitmap(String s)
        {
            Bitmap b=null;
            try
            {
                System.Net.WebRequest request = System.Net.WebRequest.Create(s);
                System.Net.WebResponse response = request.GetResponse();
                System.IO.Stream responseStream = response.GetResponseStream();
                b = new Bitmap(responseStream);
             //   b.SetResolution(b.Width, ds.Height);
            }
            catch
            {

            }
            return b;
        }
        private void listBox1_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = e.AllowedEffect;
        }

        #region Load picure for test 
        /*  private Bitmap LoadPicture(string url)
          {
              HttpWebRequest wreq;
              HttpWebResponse wresp;
              Stream mystream;
              Bitmap bmp;

              bmp = null;
              mystream = null;
              wresp = null;
              try
              {
                  wreq = (HttpWebRequest)WebRequest.Create(url);
                  wreq.AllowWriteStreamBuffering = true;

                  wresp = (HttpWebResponse)wreq.GetResponse();

                  if ((mystream = wresp.GetResponseStream()) != null)
                      bmp = new Bitmap(mystream);
              }
              finally
              {
                  if (mystream != null)
                      mystream.Close();

                  if (wresp != null)
                      wresp.Close();
              }

              return (bmp);
          }*/
        #endregion
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            GC.Collect();
            ListBox listbox = (ListBox)sender;
            if (listbox.SelectedItem.ToString() != null)
            {
                String fileUrl = listbox.SelectedItem.ToString();
                if (fileUrl.Contains("http"))
                {
                    pictureBox1.Load(fileUrl);
                }
                else
                {
                    pictureBox1.Image = Image.FromFile(fileUrl);
                }
            }
            load_second();
            // MessageBox.Show(listbox.SelectedItem.ToString());
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (button4.BackColor != Color.Aqua)
            {
                
                //  ds.WindowState= MaximumSize.
                
                ds.Show();
                button4.BackColor = Color.Aqua;
            }
            else
            {
                ds.Hide();
                button4.BackColor = Color.LightGray;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            button4.BackColor = Color.Aqua;
            ds.pictureBox1.Image = pictureBox1.Image;
            ds.Show();
        }

        public void load_second()
        {
            if (checkBox1.Checked)
            {
                button4.BackColor = Color.Aqua;
                ds.pictureBox1.Image = pictureBox1.Image;
                ds.Show();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string path = "C:\\Users\\mesur\\source\\repos\\DragAndDrop C# (Success)\\DragAndDrop\\bin\\Debug\\ji.txt";
            string text = "";
            foreach (var item in listBox1.Items)
            {
                text += item.ToString() + "\n"; // /n to print each item on new line or you omit /n to print text on same line
                String uri = item.ToString();
                if (Path.HasExtension(uri))
                {
                    string filename = System.IO.Path.GetFileName(uri);
                    Console.WriteLine(filename);
                }
            }
            File.WriteAllText(path, text);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            foreach(var item in listBox1.Items)
            {
               // text += item.ToString() + "\n"; // /n to print each item on new line or you omit /n to print text on same line
               String  uri = item.ToString();
                if (Path.HasExtension(uri))
                {
                    string filename = System.IO.Path.GetFileName(uri);
                    using (var client = new WebClient())
                    {
                        client.DownloadFile(uri,CharacterFilter(filename));
                    }   
                     Console.WriteLine(uri+"  :  "+ CharacterFilter(filename));
                }
                else
                {

                }
            }
           /** using (var client = new WebClient())
            {

                //client.DownloadFile("http://example.com/file/song/a.mpeg", "a.mpeg");
            }*/
        }
        public String CharacterFilter(String s)
        {
            String[] uC = { "<", ">", ":", "\"", "/", "\\", "|", "?", "*" };
            for(int i = 0; i < uC.Length; i++)
            {
                if (s.Contains(uC[i]))
                {
                    s = s.Replace(uC[i], "");
                }
            }
            return s;
        }
    }
}


