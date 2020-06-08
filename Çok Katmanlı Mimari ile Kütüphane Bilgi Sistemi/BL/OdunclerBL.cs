using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using System.Data;

namespace BL
{
    public class OdunclerBL
    {
        DAL.DAL dl = new DAL.DAL();
        public int EmanetVer(int KitapNo,int UyeNo)
        {
            DateTime dt = DateTime.Now;
            string Tarih = dt.ToString("dd.MM.yyyy");

            int Sonuc = dl.EkleSilGuncelle("insert into Emanetler (KitapNo,UyeNo,AlisTarihi,Durumu,TeslimTarihi) values (" + Convert.ToInt32(KitapNo) + ","+Convert.ToInt32(UyeNo)+",'"+Convert.ToDateTime(Tarih)+"','Teslim Edilmedi','"+Convert.ToDateTime(Tarih).AddDays(20)+"')", System.Data.CommandType.Text);
            return Sonuc;
        }

        public int EmanetAl(int Id)
        {
            DateTime dt = DateTime.Now;
            string Tarih = dt.ToString("dd.MM.yyyy");
            int Sonuc = dl.EkleSilGuncelle("Update Emanetler set TeslimTarihi = '"+Tarih+"', Durumu='Teslim Edildi' where ID =" + Id, System.Data.CommandType.Text);
            return Sonuc;
        }

        public DataTable Listeleme() {
            string sorgu = "SELECT Emanetler.ID, Uyeler.Adi,Uyeler.Soyadi, Kitaplar.KitapAdi  ,Emanetler.AlisTarihi,Emanetler.TeslimTarihi from Emanetler, Uyeler, Kitaplar where (Emanetler.UyeNo=Uyeler.ID) AND (Emanetler.KitapNo=Kitaplar.ID) AND (Emanetler.Durumu='Teslim Edilmedi')";
            DataTable tablo = dl.DTVeriCek(sorgu,CommandType.Text);
            return tablo;
        }

        public DataTable uyeGoster(){
            DataTable tablo = dl.DTVeriCek("select * from Uyeler  where (ID not in (select UyeNo from Emanetler where Durumu = 'Teslim Edilmedi')) ", CommandType.Text);
            return tablo;
        }

        public DataTable kitapGoster()
        {
            DataTable tablo = dl.DTVeriCek("select * from Kitaplar where ID not in (select KitapNo from Emanetler where Durumu = 'Teslim Edilmedi')", CommandType.Text);
            return tablo;
        }
        public DataTable Uarama(string ara)
        {

            DataTable tablo = dl.DTVeriCek("select * from Uyeler where (ID not in (select UyeNo from Emanetler)) AND (Adi like '%" + ara + "%' OR  Soyadi like '%" + ara + "%')", CommandType.Text);
            return tablo;
        }

        public DataTable Karama(string ara)
        {

            DataTable tablo = dl.DTVeriCek("select * from Kitaplar where (ID not in (select KitapNo from Emanetler)) AND (KitapAdi like '%" + ara + "%' OR  ISBN like '%" + ara + "%')", CommandType.Text);
            return tablo;
        }
        public DataTable OduncArama(string ara) {

            DataTable tablo = dl.DTVeriCek("SELECT Emanetler.ID, Uyeler.Adi,Uyeler.Soyadi, Kitaplar.KitapAdi,Emanetler.AlisTarihi,Emanetler.TeslimTarihi from Emanetler, Uyeler, Kitaplar where ((Emanetler.UyeNo=Uyeler.ID) AND (Emanetler.KitapNo=Kitaplar.ID) AND (Emanetler.Durumu='Teslim Edilmedi')) and (Uyeler.Adi like '%" + ara + "%' OR Uyeler.Soyadi like '%" + ara + "%')", CommandType.Text);
            return tablo;
        
        
        }

    }
}
