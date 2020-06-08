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
    public partial class Yayinevleri : Form
    {
        public Yayinevleri()
        {
            InitializeComponent();
        }
        #region FormSize
        public void FormTemizle()
        {
            txtCad.Text = "";
            txtDaire.Text = "";
            txtKat.Text = "";
            txtMah.Text = "";
            txtNo.Text = "";
            txtSehir.Text = "";
            txtSemt.Text = "";
            txtSok.Text = "";
            txtYayineviAdi.Text = "";
        }

        public void FormDaraltma()
        {
            if (txtCad.Text != "" || txtDaire.Text != "" || txtKat.Text != "" || txtMah.Text != "" || txtNo.Text != "" || txtSehir.Text != "" || txtSemt.Text != "" || txtSok.Text != "" || txtYayineviAdi.Text != "")
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
            this.Width = 698;
            btnOnay1.Visible = false;
            button1.Visible = false;
            button2.Visible = false;
            label13.Visible = false;
            label14.Visible = false;
            label15.Visible = false;
        }
        #endregion

        YayinevleriBL yayin = new YayinevleriBL();
        private void Yayinevleri_Load(object sender, EventArgs e)
        {
            this.Width = 430;
            GridTable.DataSource = yayin.Listeleme();
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

        private void txtAra_TextChanged(object sender, EventArgs e)
        {
            yayin.Arama(txtAra.Text);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (lblislem.Text == "Ekle")
            {
                if (txtYayineviAdi.Text != "" || txtMah.Text != "" || txtCad.Text != "" || txtSehir.Text != "" || txtNo.Text != "" || txtKat.Text != "" || txtDaire.Text != "" || txtSemt.Text != "")
                {
                    MessageBox.Show(yayin.YayineviEkle(txtYayineviAdi.Text, txtMah.Text, txtCad.Text, txtSok.Text, txtNo.Text, txtKat.Text, txtDaire.Text, txtSehir.Text, txtSemt.Text) + " yayınevi eklendi");
                    GridTable.DataSource = yayin.Listeleme();
                    FormTemizle();
                }
                else
                {
                    MessageBox.Show("Lütfen gerekli alanları doldurunuz... ", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            }

            else if (lblislem.Text == "Güncelle")
            {
                if (txtYayineviAdi.Text != "" || txtMah.Text != "" || txtCad.Text != "" || txtSehir.Text != "" || txtNo.Text != "" || txtKat.Text != "" || txtDaire.Text != "" || txtSemt.Text != "")
                {
                    MessageBox.Show(yayin.YayineviGuncelle(txtYayineviAdi.Text, txtMah.Text, txtCad.Text, txtSok.Text, txtNo.Text, txtKat.Text, txtDaire.Text, txtSehir.Text, txtSemt.Text, Convert.ToInt32(GridTable.CurrentRow.Cells[0].Value.ToString())) + " yayınevi güncellendi");
                    GridTable.DataSource = yayin.Listeleme();
                    FormTemizle();
                }
                else
                {
                    MessageBox.Show("Lütfen gerekli alanları doldurunuz... ", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void GridTable_Click(object sender, EventArgs e)
        {
            if (lblislem.Text=="Güncelle")
            {
                txtDaire.Text = GridTable.CurrentRow.Cells["Daire"].Value.ToString();
                txtCad.Text = GridTable.CurrentRow.Cells["Cadde"].Value.ToString();
                txtKat.Text = GridTable.CurrentRow.Cells["Kat"].Value.ToString();
                txtMah.Text = GridTable.CurrentRow.Cells["Mahalle"].Value.ToString();
                txtNo.Text = GridTable.CurrentRow.Cells["Nosu"].Value.ToString();
                txtSehir.Text = GridTable.CurrentRow.Cells["Il"].Value.ToString();
                txtSemt.Text = GridTable.CurrentRow.Cells["Ilce"].Value.ToString();
                txtSok.Text = GridTable.CurrentRow.Cells["Sokak"].Value.ToString();
                txtYayineviAdi.Text = GridTable.CurrentRow.Cells["YayineviAdi"].Value.ToString();

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult a = MessageBox.Show(GridTable.CurrentRow.Cells[0].Value.ToString() + " numaralı üyeyi silmek istediğinize emin misiniz ? ", "Silme Uyarısı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (a == DialogResult.Yes)
            {
                MessageBox.Show(yayin.YayineviSil(Convert.ToInt32(GridTable.CurrentRow.Cells[0].Value.ToString())) + " kitap silindi..");
                GridTable.DataSource = yayin.Listeleme();
            }
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Anasayfa ana = new Anasayfa();
            ana.Show();
            this.Close();
        }
    }
}
