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
    public partial class Yazarlar : Form
    {
        public Yazarlar()
        {
            InitializeComponent();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }



        YazarlarBL yazarIslemleri = new YazarlarBL();
        private void Yazarlar_Load(object sender, EventArgs e)
        {
            this.Width = 430;
            GridTable.DataSource = yazarIslemleri.Listeleme();
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

        private void btnClear1_Click(object sender, EventArgs e)
        {
            FormTemizle();
        }






        #region FormSize
        public void FormTemizle()
        {
            txtAdi.Text = "";
           // txtSoyadi.Text = "";
        }

        public void FormDaraltma()
        {
            if (txtAdi.Text != "" )
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
            this.Width = 693;
            btnOnay1.Visible = false;
            button1.Visible = false;
            button2.Visible = false;
            label13.Visible = false;
            label14.Visible = false;
            label15.Visible = false;
        }
        #endregion





        private void txtAra_TextChanged(object sender, EventArgs e)
        {
            GridTable.DataSource = yazarIslemleri.Arama(txtAra.Text);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (lblislem.Text=="Ekle")
            {
                if (txtAdi.Text == "")
                    MessageBox.Show("Lütfen Gerekli alanları doldurunuz.");
                else
                {
                    MessageBox.Show(yazarIslemleri.YazarEkle(txtAdi.Text) + " Yazar eklendi.");
                    GridTable.DataSource = yazarIslemleri.Listeleme();
                    FormTemizle();

                }
            }

            else if (lblislem.Text == "Güncelle")
            {
                if (txtAdi.Text == "")
                    MessageBox.Show("Lütfen Gerekli alanları doldurunuz.");
                else {
                    MessageBox.Show(yazarIslemleri.YazarGuncelle(txtAdi.Text, Convert.ToInt32(GridTable.CurrentRow.Cells[0].Value.ToString())) + " Yazar Güncellendi.");
                    GridTable.DataSource = yazarIslemleri.Listeleme();
                    FormTemizle();

                }
            }

                
        }

      

        private void GridTable_Click(object sender, EventArgs e)
        {
            if (lblislem.Text=="Güncelle")
                txtAdi.Text = GridTable.CurrentRow.Cells[1].Value.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
          
           
                DialogResult a = MessageBox.Show(GridTable.CurrentRow.Cells[0].Value.ToString() + " numaralı kitabı silmek istediğinize emin misiniz ? ", "Silme Uyarısı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (a == DialogResult.Yes)
                {
                    MessageBox.Show(yazarIslemleri.YazarSil(Convert.ToInt32(GridTable.CurrentRow.Cells[0].Value.ToString())) + " kitap silindi..");
                    GridTable.DataSource = yazarIslemleri.Listeleme();
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
