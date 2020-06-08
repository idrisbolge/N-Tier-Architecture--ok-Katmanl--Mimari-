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
using ZedGraph;

namespace KutuphaneBilgiSistemi
{
    public partial class Raporlama : Form
    {
        public Raporlama()
        {
            InitializeComponent();
            plotgraph();
        }


        private void plotgraph()
        {
            RaporlamaBL rapor = new RaporlamaBL();
            GraphPane myPane = zedGraphControl1.GraphPane;
            myPane.Title = "Aylara Göre Okuyucular ve Kitaplar Grafiği";
            myPane.XAxis.Title = "Aylar";
            myPane.YAxis.Title = "Okuyucu ve Kitap Sayısı";
            PointPairList KitapPairList = new PointPairList();
            PointPairList OkuyucuPairList = new PointPairList();
            int[] KitapData = rapor.AylaraGoreKitapSatisi();
            int[] OkuyucuData = rapor.OkuyucuSayısıBelirleme();

            for (int i = 0; i < 12; i++)
            {
                KitapPairList.Add(i + 1, KitapData[i]);
                OkuyucuPairList.Add(i + 1, OkuyucuData[i]);

            }

            LineItem KitapCurve = myPane.AddCurve("Toplam Okuyucu Sayısı", KitapPairList, Color.Red, SymbolType.Circle);
            LineItem OkuyucuCurve = myPane.AddCurve("Kütüphanede bulunan Kitap Sayısı", OkuyucuPairList, Color.Blue, SymbolType.Diamond);

            zedGraphControl1.AxisChange();
        }

        public void listeleme()
        {        RaporlamaBL Raporlama = new RaporlamaBL();
            if (comboBox1.Text == "Uyeler")
            {

                dataGridView1.DataSource = Raporlama.UyeyeGoreListeleme();
            }
            else
            {
                dataGridView1.DataSource = Raporlama.KitabaGoreListeleme();
            }
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            listeleme();
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            RaporlamaBL rapor = new RaporlamaBL();
            int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
            if (comboBox1.Text=="Uyeler")
            {
                dataGridView2.DataSource = rapor.UyeyeGoreListeleme(id);
            }
            else if(comboBox1.Text=="Kitaplar")
            {
                dataGridView2.DataSource = rapor.KitabaGoreListeleme(id);
            }
        }

        private void Raporlama_Load(object sender, EventArgs e)
        {
            
        }
        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void uzaklaştırToolStripMenuItem_Click(object sender, EventArgs e)
        {
            double rangeX = zedGraphControl1.GraphPane.XAxis.Max - zedGraphControl1.GraphPane.XAxis.Min;
            zedGraphControl1.GraphPane.XAxis.Max -= rangeX / 20.0;
            zedGraphControl1.GraphPane.XAxis.Min += rangeX / 20.0;

            double rangeY = zedGraphControl1.GraphPane.YAxis.Max - zedGraphControl1.GraphPane.YAxis.Min;
            zedGraphControl1.GraphPane.YAxis.Max -= rangeY / 20.0;
            zedGraphControl1.GraphPane.YAxis.Min += rangeY / 20.0;

            zedGraphControl1.AxisChange();
            zedGraphControl1.Refresh();
        }

        private void uzaklaştırToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            double rangeX = zedGraphControl1.GraphPane.XAxis.Max - zedGraphControl1.GraphPane.XAxis.Min;
            zedGraphControl1.GraphPane.XAxis.Max += rangeX / 20.0;
            zedGraphControl1.GraphPane.XAxis.Min -= rangeX / 20.0;

            double rangeY = zedGraphControl1.GraphPane.YAxis.Max - zedGraphControl1.GraphPane.YAxis.Min;
            zedGraphControl1.GraphPane.YAxis.Max += rangeY / 20.0;
            zedGraphControl1.GraphPane.YAxis.Min -= rangeY / 20.0;

            zedGraphControl1.AxisChange();
            zedGraphControl1.Refresh();
        }
    }

   
}
