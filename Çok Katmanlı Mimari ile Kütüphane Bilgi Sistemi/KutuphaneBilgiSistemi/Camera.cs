using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AForge.Video;  //Referansları ekliyoruz
using AForge.Video.DirectShow; //Referansları ekliyoruz,
using System.IO;

namespace KutuphaneBilgiSistemi
{
    public partial class Camera : Form
    {

        public Camera(string Formadi)
        {
            InitializeComponent();
            this.Text = Formadi;
        }

        private FilterInfoCollection webcam;//webcam isminde tanımladığımız değişken bilgisayara kaç kamera bağlıysa onları tutan bir dizi. 
        private VideoCaptureDevice cam;//cam ise bizim kullanacağımız aygıt.


        private void Camera_Load(object sender, EventArgs e)
        {

            webcam = new FilterInfoCollection(FilterCategory.VideoInputDevice);//webcam dizisine mevcut kameraları dolduruyoruz.
            foreach (FilterInfo videocapturedevice in webcam)
            {
                comboBox1.Items.Add(videocapturedevice.Name);//kameraları combobox a dolduruyoruz.
            }
            comboBox1.SelectedIndex = 0; //Comboboxtaki ilk index numaralı kameranın ekranda görünmesi için
        }

        private void button2_Click(object sender, EventArgs e)
        {
            cam = new VideoCaptureDevice(webcam[comboBox1.SelectedIndex].MonikerString);
            cam.NewFrame += new NewFrameEventHandler(cam_NewFrame);
            cam.Start();
        }
        private void cam_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            Bitmap bit = (Bitmap)eventArgs.Frame.Clone();
            pictureBox2.Image = bit;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (cam.IsRunning) //kamera açıksa kapatıyoruz.
            {
                cam.Stop();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            pictureBox3.Image = pictureBox2.Image;
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            if (cam.IsRunning)
                cam.Stop();
            int a=0;
            for (int i = 0; i < 1000; i++)
            {
                if (File.Exists(@Application.StartupPath + "\\resimler\\" + a.ToString() + ".jpg"))
                    a++;
                else
                {
                    pictureBox3.Image.Save(Application.StartupPath + "\\resimler\\" + a.ToString() + ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg);

                    if (this.Text == "Kitaplar")
                    {
                        Kitaplar k = (Kitaplar)Application.OpenForms["Kitaplar"];
                        k.pBoxFoto.Image = new Bitmap(Application.StartupPath + "\\resimler\\" + a.ToString() + ".jpg");
                        k.lblFoto.Text = a.ToString();
                        this.Hide();
                        break;
                    }
                    else if (this.Text == "Uyeler")
                    {
                        Uyeler p = (Uyeler)Application.OpenForms["Uyeler"];
                        p.pBoxFoto.Image = new Bitmap(Application.StartupPath + "\\resimler\\" + a.ToString() + ".jpg");
                        this.Hide();
                        break;
                    }

                }
            }
        }

        private void Camera_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (cam.IsRunning)
            {
                cam.Stop();
            }

        }


    }
}
