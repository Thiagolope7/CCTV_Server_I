using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CCTV_Server
{
    public partial class VideosDai : Form
    {
        string valor; 
        public VideosDai()
        {
            InitializeComponent();
        }
        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnVerVideo_Click(object sender, EventArgs e)
        {
            // https://www.youtube.com/watch?v=WE2ho8jmLcQ

            //MessageBox.Show(ConfigurationManager.AppSettings["RutaVideos"]);
            //OpenFileDialog abrirVideo = new OpenFileDialog();
            //abrirVideo.Filter = "MP4 Files|*.mp4";
            //if (abrirVideo.ShowDialog() == DialogResult.OK)
            //{
            //    string sArchivo = abrirVideo.FileName;
            //    Process prEjecutar = new Process();
            //    prEjecutar.StartInfo.FileName = sArchivo;
            //    prEjecutar.Start();
            //}
        }

        private void ListViewInci_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ListViewInci.SelectedItems.Count > 0)
            {
                valor = ListViewInci.SelectedItems[0].SubItems[1].Text;
            }
        }
        public void CargarIncidentes()
        {

            DateTime dtHora;
            dtHora = DateTime.Now;
            // String stHoraForma =dtHora.ToString("yyyy-MM-dd HH");
            // string stHoraForma = "2020-07-17 16";
            ListViewInci.SmallImageList = imageList1;
            DirectoryInfo di = new DirectoryInfo(ConfigurationManager.AppSettings["RutaVideos"]);
            FileInfo[] TxtFiles = di.GetFiles("*.MP4").OrderByDescending(p => p.CreationTime).ToArray();
            if (TxtFiles.Length != 0)
            {
               if( TxtFiles.Length < 100)
                {
                    for(int i = 0; i < TxtFiles.Length; i++)
                    {
                        string name = TxtFiles[i].Name.Replace(".mp4", "");
                        ListViewInci.Items.Add(new ListViewItem(new string[] {"", name }));
                        ListViewItem lvi = ListViewInci.Items[i];
                        lvi.ImageIndex = 0;

                    }
                }
                else
                {
                    for(int i = 0; i < 100; i++)
                    {
                        string name = TxtFiles[i].Name.Replace(".mp4", "");
                        ListViewInci.Items.Add(new ListViewItem(new string[] { "", name }));
                        ListViewItem lvi = ListViewInci.Items[i];
                        lvi.ImageIndex = 0;
                    }

                }
                     
            }
            //ListViewInci.Items.Add(new ListViewItem(new string[] { L.ToString(), leer[0].ToString() }));
            //ListViewItem item = ListViewInci.Items[L];
            //item.BackColor = Color.DarkBlue;


        }

        private void ListViewInci_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Process.Start(ConfigurationManager.AppSettings["RutaVideos"] + valor + ".MP4");
        }

        private void VideosDai_Load(object sender, EventArgs e)
        {
            this.btnVerVideo.Visible = false;
            CargarIncidentes();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            ListViewInci.Items.Clear();
            CargarIncidentes();
            ListViewInci.Refresh();
        }
    }
}
