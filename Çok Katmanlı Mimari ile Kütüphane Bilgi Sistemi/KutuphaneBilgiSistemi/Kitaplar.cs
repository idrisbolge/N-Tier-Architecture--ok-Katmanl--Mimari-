using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Entity;
using System.IO;


namespace KutuphaneBilgiSistemi
{
    public partial class Kitaplar : Form
    {
        public Kitaplar()
        {
            InitializeComponent();
        }


        #region FormSize
        public void FormTemizle()
        {
            txtAdi.Text = "";
            txtBasim.Text = "";
            txtDil.Text = "";
            txtISBN.Text = "";
            txtKategori.Text = "";
            txtSayfaSayisi.Text = "";
            txtTur.Text = "";
            txtYayinyili.Text = "";
            cmbYazar.SelectedIndex = -1;
            cmbYayinevi.SelectedIndex = -1;
        }

        public void FormDaraltma()
        {
            if (txtAdi.Text != "" || txtBasim.Text != "" || txtDil.Text != "" || txtISBN.Text != "" || txtKategori.Text != "" || txtSayfaSayisi.Text != "" || txtTur.Text != "" || txtYayinyili.Text != "" || cmbYazar.Text != "" || cmbYayinevi.Text != "")
            {
                DialogResult a = MessageBox.Show("Bazı alanlarda veriler bulunmaktadır. Devam ederseniz tüm veriler kaybolacaktır....", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (a == DialogResult.Yes)
                {
                    FormTemizle();
                    this.Width = 420;
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
                this.Width = 420;
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
            this.Width = 947;
            btnOnay1.Visible = false;
            button1.Visible = false;
            button2.Visible = false;
            label13.Visible = false;
            label14.Visible = false;
            label15.Visible = false;
        }
        #endregion

        BL.YazarlarBL YazarIslemleri = new BL.YazarlarBL();
        BL.YayinevleriBL YayineviIslemleri = new BL.YayinevleriBL();
        public int YazarID;
        public int YayıneviID;
        void YazarGetir()
        {
            List<YazarlarEntity> Yazarlar = YazarIslemleri.YazarlarıGetir();
            cmbYazar.DataSource = Yazarlar;
            cmbYazar.ValueMember = "YazarAdi";
        }

        void YayineviGetir()
        {
            List<YayinevleriEntity> Yayinevleri = YayineviIslemleri.YayinevleriGetir();
            cmbYayinevi.DataSource = Yayinevleri;
            cmbYayinevi.ValueMember = "YayineviAdi";
        }
        private void Kitaplar_Load(object sender, EventArgs e)
        {
            this.Width = 420;
            YazarGetir();
            YayineviGetir();
            cmbYayinevi.SelectedIndex = -1;
            cmbYazar.SelectedIndex = -1;
            GridTable.DataSource = kitap.Listeleme();
        }

        private void btnOnay1_Click(object sender, EventArgs e)
        {
            FormGenisletme("Ekle");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            FormGenisletme("Güncelle");

        }

        private void btnIptal1_Click(object sender, EventArgs e)
        {
            FormDaraltma();

        }


        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }




        BL.KitaplarBL kitap = new BL.KitaplarBL();

        private void button3_Click(object sender, EventArgs e)
        {
            string fotoNo = lblFoto.Text;
            if (lblFoto.Text == "")
                fotoNo = null;


            if (lblislem.Text == "Ekle") {
                if (txtAdi.Text != "" || txtBasim.Text != "" || txtDil.Text != "" || txtISBN.Text != "" || txtKategori.Text != "" || txtSayfaSayisi.Text != "" || txtTur.Text != "" || txtYayinyili.Text != "" || cmbYazar.Text != "" || cmbYayinevi.Text != "")
                {
                    MessageBox.Show(kitap.KitapEkle(txtAdi.Text, txtYayinyili.Text, txtBasim.Text, txtISBN.Text, txtTur.Text, txtSayfaSayisi.Text, txtKategori.Text, txtDil.Text, fotoNo, YazarID, YayıneviID) + " kitap eklendi");
                    GridTable.DataSource = kitap.Listeleme();
                    FormTemizle();
                }
                else
                {
                    MessageBox.Show("Lütfen gerekli alanları doldurunuz... ", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
               
            }

            else if (lblislem.Text == "Güncelle")
            {
                if (txtAdi.Text != "" || txtBasim.Text != "" || txtDil.Text != "" || txtISBN.Text != "" || txtKategori.Text != "" || txtSayfaSayisi.Text != "" || txtTur.Text != "" || txtYayinyili.Text != "" || cmbYazar.Text != "" || cmbYayinevi.Text != "")
                {
                    MessageBox.Show(kitap.KitapGuncelle(txtAdi.Text, txtYayinyili.Text, txtBasim.Text, txtISBN.Text, txtTur.Text, txtSayfaSayisi.Text, txtKategori.Text, txtDil.Text, fotoNo, YazarID, YayıneviID, Convert.ToInt32(GridTable.CurrentRow.Cells[0].Value.ToString())) + " kitap güncellendi");
                    GridTable.DataSource = kitap.Listeleme();
                    FormTemizle();
                }
                else
                {
                    MessageBox.Show("Lütfen gerekli alanları doldurunuz... ", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }


            
            }







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

        
        private void cmbYazar_SelectedIndexChanged(object sender, EventArgs e)
        {
            string YazarAdi=cmbYazar.Text;
            YazarID = YazarIslemleri.YazarIDCek(YazarAdi);

           
        }

        private void cmbYayinevi_SelectedIndexChanged(object sender, EventArgs e)
        {
            string YayineviAdi = cmbYayinevi.Text;
            YayıneviID = YayineviIslemleri.YayıneviIDCek(YayineviAdi);
        }


        private void GridTable_Click(object sender, EventArgs e)
        {
            if (lblislem.Text == "Güncelle")
            {
                txtISBN.Text = GridTable.CurrentRow.Cells[4].Value.ToString();
                txtAdi.Text = GridTable.CurrentRow.Cells[1].Value.ToString();
                txtBasim.Text = GridTable.CurrentRow.Cells[3].Value.ToString();
                txtYayinyili.Text = GridTable.CurrentRow.Cells[2].Value.ToString();
                txtTur.Text = GridTable.CurrentRow.Cells[5].Value.ToString();
                txtKategori.Text = GridTable.CurrentRow.Cells[7].Value.ToString();
                txtSayfaSayisi.Text = GridTable.CurrentRow.Cells[6].Value.ToString();
                txtDil.Text = GridTable.CurrentRow.Cells[8].Value.ToString();
                int yazarid, yayineviid;
                yazarid = Convert.ToInt32(GridTable.CurrentRow.Cells[9].Value.ToString());
                yayineviid = Convert.ToInt32(GridTable.CurrentRow.Cells[10].Value.ToString());
                cmbYazar.Text = YazarIslemleri.YazarAdiCek(yazarid);
                cmbYayinevi.Text = YayineviIslemleri.YayineviAdiCek(yayineviid);
                if (GridTable.CurrentRow.Cells[11].Value != "")
                    pBoxFoto.ImageLocation = Application.StartupPath + "\\resimler\\" + GridTable.CurrentRow.Cells[11].Value.ToString() + ".jpg";

                else
                    pBoxFoto.Image = Properties.Resources.user__3_1;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (this.Width>=900)
            {
                MessageBox.Show("Lütfen Ekleme veya Güncelleme sayfasını iptal Ediniz...");
            }
            else
            {
                DialogResult a = MessageBox.Show(GridTable.CurrentRow.Cells[0].Value.ToString() + " numaralı kitabı silmek istediğinize emin misiniz ? ", "Silme Uyarısı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (a == DialogResult.Yes)
                {
                    MessageBox.Show(kitap.KitapSil(Convert.ToInt32(GridTable.CurrentRow.Cells[0].Value.ToString())) + " kitap silindi..");
                    GridTable.DataSource = kitap.Listeleme();
                }
            }

        }

        private void txtAra_TextChanged(object sender, EventArgs e)
        {

            GridTable.DataSource = kitap.Arama(txtAra.Text);
        }

        private void pBoxGozat_Click(object sender, EventArgs e)
        {
            Camera cmr = new Camera("Kitaplar");
            cmr.Show();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Anasayfa ana = new Anasayfa();
            ana.Show();
            this.Close();
        }


    }
}
