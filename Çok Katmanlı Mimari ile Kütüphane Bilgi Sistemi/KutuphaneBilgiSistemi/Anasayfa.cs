using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KutuphaneBilgiSistemi
{
    public partial class Anasayfa : Form
    {
        public Anasayfa()
        {
            InitializeComponent();
        }
        BL.KullaniciBL kbl = new BL.KullaniciBL();
        private void Anasayfa_Load(object sender, EventArgs e)
        {
           /* lblAdSoyad.Text = Giris.KullaniciAdi.ToUpper();
            lblUnvan.Text = kbl.YetkiCekme(Giris.KullaniciAdi).ToString().ToUpper();
            pBoxFoto.ImageLocation = Application.StartupPath + "\\resimler\\" + kbl.ResimNoCekme(Giris.KullaniciAdi).ToString()+".jpg";*/
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        Kitaplar kitap = new Kitaplar();
        Uyeler uye = new Uyeler();
        Yazarlar yzr = new Yazarlar();
        Yayinevleri yyn = new Yayinevleri();
        Emanetler emanet = new Emanetler();
        Raporlama rapor = new Raporlama();

        private void btnBook_Click(object sender, EventArgs e)
        {
            kitap.Show();
            this.Hide();
        }

        private void btnWritter_Click(object sender, EventArgs e)
        {
            yzr.Show();
            this.Hide();
        }

        private void btnMember_Click(object sender, EventArgs e)
        {
            uye.Show();
            this.Hide();
        }

        private void btnPress_Click(object sender, EventArgs e)
        {
            yyn.Show();
            this.Hide();
        }

        private void btnOnLoan_Click(object sender, EventArgs e)
        {
            emanet.Show();
            this.Hide();
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            rapor.Show();
            this.Hide();
        }
    }
}
