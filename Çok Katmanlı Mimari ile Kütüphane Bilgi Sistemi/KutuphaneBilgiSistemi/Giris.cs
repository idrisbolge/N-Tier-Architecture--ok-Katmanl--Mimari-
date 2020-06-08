using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BL;
namespace KutuphaneBilgiSistemi
{
    public partial class Giris : Form
    {
        public Giris()
        {
            InitializeComponent();
        }
        BL.KullaniciBL kbl = new KullaniciBL();
        public static string KullaniciAdi;
        private void BtnGiris_Click(object sender, EventArgs e)
        {

            int Sonuc = kbl.OturumAcma(txtUser.Text, txtPassword.Text);
            if (Sonuc==1)
            {
                KullaniciAdi = txtUser.Text;
                Anasayfa anasayfa = new Anasayfa();
                anasayfa.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Hatalı giriş yaptınız. Lütfen Tekrar deneyiniz..","Hatalı Giriş",MessageBoxButtons.OK,MessageBoxIcon.Error);
                txtUser.Text = "";
                txtPassword.Text = "";
            }
        }
    }
}
