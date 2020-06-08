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
using BL;

namespace KutuphaneBilgiSistemi
{
    public partial class Uyeler : Form
    {
        public Uyeler()
        {
            InitializeComponent();
        }

        private void btnOnay1_Click(object sender, EventArgs e)
        {
            FormGenisletme("Ekle");
        }


        UyelerBL uyeIslemleri = new UyelerBL();



        #region FormSize
        public void FormTemizle()
        {
            txtAdi.Text = "";
            txtCad.Text = "";
            txtDaire.Text = "";
            txtDaire.Text = "";
            txtEposta.Text = "";
            txtKat.Text = "";
            txtMah.Text = "";
            txtNo.Text = "";
            txtSehir.Text = "";
            txtSemt.Text = "";
            txtsok.Text = "";
            txtSoyadi.Text = "";
            txtTel.Text = "";
            
        }

        public void FormDaraltma()
        {
            if (txtAdi.Text != "" || txtCad.Text != "" || txtDaire.Text != "" || txtDaire.Text != "" || txtEposta.Text != "" || txtKat.Text != "" || txtMah.Text != "" || txtNo.Text != "" || txtSehir.Text != "" || txtSemt.Text != "" || txtsok.Text != "" || txtSoyadi.Text != "" || txtTel.Text != "")
            {
                DialogResult a = MessageBox.Show("Bazı alanlarda veriler bulunmaktadır. Devam ederseniz tüm veriler kaybolacaktır....", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (a == DialogResult.Yes)
                {
                    FormTemizle();
                    this.Width = 430;
                    btnOnay1.Visible = true;
                    button1.Visible = true;
                    button2.Visible = true;
                    label13.Visible = true;
                    label14.Visible = true;
                    label15.Visible = true;
                }

            }
            else
            {
                this.Width = 430;
                btnOnay1.Visible = true;
                button1.Visible = true;
                button2.Visible = true;
                label13.Visible = true;
                label14.Visible = true;
                label15.Visible = true;
            }
        }
        public void FormGenisletme(string isim)
        {
            lblislem.Text = isim;
            this.Width = 957;
            btnOnay1.Visible = false;
            button1.Visible = false;
            button2.Visible = false;
            label13.Visible = false;
            label14.Visible = false;
            label15.Visible = false;
        }
        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            FormGenisletme("Güncelle");
        }

        private void btnIptal1_Click(object sender, EventArgs e)
        {
            FormDaraltma();
        }

        private void Uyeler_Load(object sender, EventArgs e)
        {
            this.Width = 430;
            GridTable.DataSource = uyeIslemleri.Listeleme();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        public static string ısbnno;
        private void pBoxGozat_Click(object sender, EventArgs e)
        {
            Camera cm = new Camera("Uyeler");
            cm.Show();
        }

        private void pBoxCek_Click(object sender, EventArgs e)
        {
            OpenFileDialog dosya = new OpenFileDialog();
            dosya.Filter = "Resim Dosyası |*.jpg | Video|*.avi| Tüm Dosyalar |*.*";
            dosya.Title = "Fotoğraf Seçiniz";
            dosya.ShowDialog();
            string DosyaYolu = dosya.FileName;
            pBoxFoto.ImageLocation = DosyaYolu;
            int a = 0;
            for (int i = 0; i < 1000; i++)
            {
                if (File.Exists(@Application.StartupPath + "\\resimler\\" + a.ToString() + ".jpg"))
                    a++;
                else
                {
                    pBoxFoto.Image.Save(Application.StartupPath + "\\resimler\\" + a.ToString() + ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
                    break;
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

            string fotoNo = lblFoto.Text;
            if (lblFoto.Text == "")
                fotoNo = null;
            string cins;
            if (radioButton1.Checked)
                cins = "Erkek";
            else
                cins = "Kadın";
                


            if (lblislem.Text == "Ekle")
            {
                if (txtAdi.Text != "" || txtSoyadi.Text != "" || txtTel.Text != "" || txtEposta.Text != "" || txtMah.Text != "" || txtCad.Text != "" || txtSehir.Text != "" || txtNo.Text != "" || txtKat.Text != "" || txtDaire.Text != "" || txtSemt.Text != "")
                {
                    
                    MessageBox.Show(uyeIslemleri.UyeEkle(txtAdi.Text,txtSoyadi.Text,txtTel.Text,txtEposta.Text,cins,txtMah.Text,txtCad.Text,txtsok.Text,txtNo.Text,txtKat.Text,txtDaire.Text,txtSehir.Text,txtSemt.Text,fotoNo) + " üye eklendi");
                    GridTable.DataSource = uyeIslemleri.Listeleme();
                    FormTemizle();
                }
                else
                {
                    MessageBox.Show("Lütfen gerekli alanları doldurunuz... ", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            }

            else if (lblislem.Text == "Güncelle")
            {

                if (txtAdi.Text != "" || txtSoyadi.Text != "" || txtTel.Text != "" || txtEposta.Text != "" || txtMah.Text != "" || txtCad.Text != "" || txtSehir.Text != "" || txtNo.Text != "" || txtKat.Text != "" || txtDaire.Text != "" || txtSemt.Text != "")
                {

                    MessageBox.Show(uyeIslemleri.UyeGuncelle(txtAdi.Text, txtSoyadi.Text, txtTel.Text, txtEposta.Text, cins, txtMah.Text, txtCad.Text, txtsok.Text, txtNo.Text, txtKat.Text, txtDaire.Text, txtSehir.Text, txtSemt.Text,Convert.ToInt32(GridTable.CurrentRow.Cells[0].Value.ToString()),fotoNo) + " üye güncellendi");
                    GridTable.DataSource = uyeIslemleri.Listeleme();
                    FormTemizle();
                }
                else
                {
                    MessageBox.Show("Lütfen gerekli alanları doldurunuz... ", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }


            }
        }

        private void txtAra_TextChanged(object sender, EventArgs e)
        {
            GridTable.DataSource = uyeIslemleri.Arama(txtAra.Text);
        }

        private void GridTable_Click(object sender, EventArgs e)
        {
            if (lblislem.Text=="Güncelle")
            {
                txtAdi.Text = GridTable.CurrentRow.Cells["Adi"].Value.ToString();
                txtSoyadi.Text = GridTable.CurrentRow.Cells["Soyadi"].Value.ToString();
                txtTel.Text = GridTable.CurrentRow.Cells["GSM"].Value.ToString();
                txtEposta.Text = GridTable.CurrentRow.Cells["Mail"].Value.ToString();
                txtDaire.Text = GridTable.CurrentRow.Cells["Daire"].Value.ToString();
                txtCad.Text = GridTable.CurrentRow.Cells["Cadde"].Value.ToString();
                txtKat.Text = GridTable.CurrentRow.Cells["Kat"].Value.ToString();
                txtMah.Text = GridTable.CurrentRow.Cells["Mahalle"].Value.ToString();
                txtNo.Text = GridTable.CurrentRow.Cells["Nosu"].Value.ToString();
                txtSehir.Text = GridTable.CurrentRow.Cells["Il"].Value.ToString();
                txtSemt.Text = GridTable.CurrentRow.Cells["Ilce"].Value.ToString();
                txtsok.Text = GridTable.CurrentRow.Cells["Sokak"].Value.ToString();
                if (GridTable.CurrentRow.Cells["Cinsiyet"].Value.ToString() == "Erkek")
                    radioButton1.Checked = true;
                else
                    radioButton2.Checked = true;

                if (GridTable.CurrentRow.Cells["ResimNo"].Value.ToString() != "")
                    pBoxFoto.ImageLocation = Application.StartupPath + "\\resimler\\" + GridTable.CurrentRow.Cells["ResimNo"].Value.ToString() + ".jpg";

                else
                    pBoxFoto.Image = Properties.Resources.user__3_1;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult a = MessageBox.Show(GridTable.CurrentRow.Cells[0].Value.ToString() + " numaralı üyeyi silmek istediğinize emin misiniz ? ", "Silme Uyarısı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (a == DialogResult.Yes)
            {
                MessageBox.Show(uyeIslemleri.UyeSil (Convert.ToInt32(GridTable.CurrentRow.Cells[0].Value.ToString())) + " kitap silindi..");
                GridTable.DataSource = uyeIslemleri.Listeleme();
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Anasayfa ana = new Anasayfa();
            ana.Show();
            this.Close();
        }

    }
}
