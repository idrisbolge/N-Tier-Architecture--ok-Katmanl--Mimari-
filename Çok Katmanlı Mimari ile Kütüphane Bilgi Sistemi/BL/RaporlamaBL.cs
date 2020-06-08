using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DAL;
using System.Data.OleDb;

namespace BL
{
    public class RaporlamaBL
    {
        DAL.DAL baglanti = new DAL.DAL();

        #region UstKısım
        public DataTable UyeyeGoreListeleme()
        {
            DataTable tablo;
            tablo=baglanti.DTVeriCek("select ID,Adi,Soyadi from Uyeler ",CommandType.Text);
            return tablo;
        }

        public DataTable KitabaGoreListeleme() {
            DataTable tablo;
            tablo = baglanti.DTVeriCek( "select ID,KitapAdi from Kitaplar",CommandType.Text);
            return tablo;
        }

        public DataTable UyeyeGoreListeleme(int id)
        {
            DataTable tablo;
            tablo = baglanti.DTVeriCek("SELECT Kitaplar.ID, Kitaplar.KitapAdi,Emanetler.AlisTarihi,Emanetler.TeslimTarihi,Emanetler.Durumu from Emanetler,Kitaplar where Kitaplar.ID= Emanetler.KitapNo And Emanetler.UyeNo="+id, CommandType.Text);
            return tablo;
        }
        
        public DataTable KitabaGoreListeleme(int id)
        {
            DataTable tablo;
            tablo = baglanti.DTVeriCek("SELECT Uyeler.ID, Uyeler.Adi,Uyeler.Soyadi,Emanetler.AlisTarihi,Emanetler.TeslimTarihi,Emanetler.Durumu from Emanetler,Uyeler where Uyeler.ID= Emanetler.UyeNo And Emanetler.KitapNo="+id, CommandType.Text);
            return tablo;
        }
        #endregion



        public int[] AylaraGoreKitapSatisi()
        {
            int[] dizi = new int[12];
            
            for (int i = 0; i < 12; i++)
            {

               OleDbDataReader okuma=baglanti.DRVeriCek("Select count(*) as Ay from Emanetler where Month([AlisTarihi])=" + (i + 1),CommandType.Text);
                if (okuma.Read())
                {
                    dizi[i] = Convert.ToInt32(okuma[0].ToString());
                }

            }
            return dizi;
        }

        private int KitapSayisiniCek() {
            int KitapSayisi;
            KitapSayisi = Convert.ToInt32(baglanti.IlkSatirIlkSutun("SELECT count(*) from Kitaplar",CommandType.Text));
            return KitapSayisi;
        }

        public int[] OkuyucuSayısıBelirleme() {
            int KitapSayisi = KitapSayisiniCek();

            int [] dizi=AylaraGoreKitapSatisi();
            for (int i = 0; i < dizi.Length; i++)
            {
                dizi[i] = KitapSayisi - dizi[i];
            }
            return dizi;
        
        }

    }
}
