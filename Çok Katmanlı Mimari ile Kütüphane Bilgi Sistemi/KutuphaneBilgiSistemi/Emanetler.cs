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
    public partial class Emanetler : Form
    {
        public Emanetler()
        {
            InitializeComponent();
        }
        KitaplarBL kitap = new KitaplarBL();
        UyelerBL uye = new UyelerBL();
        OdunclerBL odunc = new OdunclerBL();


        private void FotoUye() {
            if (dataGridView1.RowCount>0)
            {
                string FotoNo = dataGridView1.CurrentRow.Cells["ResimNo"].Value.ToString();
                if (FotoNo == "")
                    pBoxFoto.Image = Properties.Resources.user__3_1;
                else
                    pBoxFoto.ImageLocation = Application.StartupPath + "\\resimler\\" + FotoNo + ".jpg";
            }
        }
        private void FotoKitap()
        {
            if (dataGridView2.RowCount>0)
            {
                string FotoNo = dataGridView2.CurrentRow.Cells["ResimNu"].Value.ToString();
                if (FotoNo == "")
                    pictureBox2.Image = Properties.Resources.user__3_1;
                else
                    pictureBox2.ImageLocation = Application.StartupPath + "\\resimler\\" + FotoNo + ".jpg";
            }
        }
        private void UcretBelirle()
        {
            if (dataGridView3.RowCount > 0 && dataGridView3.CurrentRow.DefaultCellStyle.BackColor == Color.Red)
            {
                double Ucret = 0;
                DateTime dt = DateTime.Now;
                DateTime dtTeslim;
                dtTeslim = Convert.ToDateTime(dataGridView3.CurrentRow.Cells["TeslimTarihi"].Value);
                TimeSpan sonuc = dtTeslim - dt;

                double snc = Convert.ToDouble(-sonuc.TotalDays);
                snc = Math.Floor(snc);
                Ucret = snc * 1.0;
                label2.Text = Ucret.ToString()+".00";
            }
            else
            {
                label2.Text = "0.00";

            }
        }
        private void Emanetler_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = odunc.uyeGoster();
            dataGridView2.DataSource = odunc.kitapGoster();
            dataGridView3.DataSource = odunc.Listeleme();
            FotoUye();
            FotoKitap();
            dataGridView3.ClearSelection();
        }

        private void dataGridView3_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            DateTime dt = DateTime.Now;
            DateTime dtTeslim;

            DataGridViewRow dgvrow = dataGridView3.Rows[e.RowIndex];


            foreach (DataGridViewRow satir in dataGridView3.Rows)
            {
                dtTeslim = Convert.ToDateTime(satir.Cells["TeslimTarihi"].Value);
                TimeSpan sonuc = dtTeslim - dt;
                if (sonuc.TotalDays <=0){
                    satir.DefaultCellStyle.ForeColor = Color.White;
                    satir.DefaultCellStyle.BackColor = Color.Red;
                }

                else if ((sonuc).TotalDays <= 2)
                {
                    
                    satir.DefaultCellStyle.ForeColor = Color.White;
                    satir.DefaultCellStyle.BackColor = Color.Yellow;
                }

                else 
                {
                    satir.DefaultCellStyle.ForeColor = Color.White;
                    satir.DefaultCellStyle.BackColor = Color.Green;
                }
            } 
        }

        private void txtUye_TextChanged(object sender, EventArgs e)
        {
            dataGridView1.DataSource = odunc.Uarama(txtUye.Text);

        }

        private void txtKitap_TextChanged(object sender, EventArgs e)
        {
            dataGridView2.DataSource = odunc.Karama(txtKitap.Text);
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            FotoUye();

        }

        private void dataGridView2_Click(object sender, EventArgs e)
        {
            FotoKitap();
        }

        private void btnOnay1_Click(object sender, EventArgs e)
        {
            string kisi, kitap;
            kisi = dataGridView1.CurrentRow.Cells["Adi"].Value.ToString() + dataGridView1.CurrentRow.Cells["Soyadi"].Value.ToString();
            kitap = dataGridView2.CurrentRow.Cells["KitapAdi"].Value.ToString();
            DialogResult a = MessageBox.Show(kisi + "  isimli üyeye " + kitap + " adlı kitabı ödünç vermek istediğinize emin misiniz ? ", "Ödünç verme işlemi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (a==DialogResult.Yes)
            {
                int KitapNo = Convert.ToInt32(dataGridView2.CurrentRow.Cells[0].Value);
                int UyeNo = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
                MessageBox.Show( odunc.EmanetVer(KitapNo,UyeNo)+" Ödünç verme işlemi tamamlandı");
                dataGridView1.DataSource = odunc.uyeGoster();
                dataGridView2.DataSource = odunc.kitapGoster();
                dataGridView3.DataSource = odunc.Listeleme();
            }

        }

        private void dataGridView3_Click(object sender, EventArgs e)
        {
            UcretBelirle();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            UcretBelirle();
            dataGridView3.ClearSelection();

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void btnIptal1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(dataGridView3.CurrentRow.Cells[0].Value);
            MessageBox.Show(odunc.EmanetAl(id)+" Emanet kitap alındı.");
            dataGridView1.DataSource = odunc.uyeGoster();
            dataGridView2.DataSource = odunc.kitapGoster();
            dataGridView3.DataSource = odunc.Listeleme();
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            dataGridView3.ClearSelection();
            dataGridView3.DataSource = odunc.OduncArama(textBox1.Text);
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
