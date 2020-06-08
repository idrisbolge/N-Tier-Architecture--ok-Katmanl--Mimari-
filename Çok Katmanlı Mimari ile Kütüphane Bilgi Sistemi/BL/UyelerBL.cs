using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using System.Data;

namespace BL
{
    public class UyelerBL
    {
        DAL.DAL dl = new DAL.DAL();
        AdreslerBL Adres = new AdreslerBL();
        public int UyeEkle(string Adi, string Soyadi, string GSM, string Mail, string Cinsiyet, string Mahalle, string Cadde, string Sokak, string Nosu, string Kat, string Daire, string Il, string Ilce,string ResimNo)
        {
            DateTime date = DateTime.Now;
            string dateString = date.ToString("dd.MM.yyyy");

            Adres.AdresEkle(Mahalle,Cadde,Sokak,Nosu,Kat,Daire,Il,Ilce);
            int AdresNo =Adres.IdGonderme();
            int Sonuc = dl.EkleSilGuncelle("insert into Uyeler (Adi,Soyadi,GSM,Mail,Cinsiyet,AdresNo,ResimNo,UyelikTarihi) values ('"+Adi+"','"+Soyadi+"','"+GSM+"','"+Mail+"','"+Cinsiyet+"',"+Convert.ToInt32(AdresNo)+",'"+ResimNo+"','"+dateString+"') ", System.Data.CommandType.Text);
            return Sonuc;
        }

        public int UyeSil(int Id)
        {
            int Sonuc = dl.EkleSilGuncelle("Delete from Uyeler where ID =" + Id, System.Data.CommandType.Text);
            return Sonuc;
        }

        public int UyeGuncelle(string Adi, string Soyadi, string GSM, string Mail, string Cinsiyet, string Mahalle, string Cadde, string Sokak, string Nosu, string Kat, string Daire, string Il, string Ilce,int Id,string ResimNo)
        {
            int Adresver = AdresNoCek(Id);
            Adres.AdresGuncelle(Mahalle, Cadde, Sokak, Nosu, Kat, Daire, Il, Ilce,Adresver);
            int Sonuc = dl.EkleSilGuncelle("Update Uyeler set Adi='" +Adi + "',Soyadi='" + Soyadi + "',GSM='" + GSM + "',Mail='" + Mail + "',Cinsiyet='" + Cinsiyet + "', ResimNo='"+ResimNo+"' where ID = "+Id, System.Data.CommandType.Text);
            return Sonuc;
        }

        private int AdresNoCek(int Id) {
            int Sonuc = dl.EkleSilGuncelle("select AdresNo from Uyeler where ID="+Id,System.Data.CommandType.Text);
            return Sonuc;
        }

        public DataTable Listeleme()
        {
            DataTable tablo = dl.DTVeriCek("SELECT Uyeler.ID,Uyeler.Adi,Uyeler.Soyadi,Uyeler.Cinsiyet,Uyeler.GSM, Uyeler.Mail,Uyeler.UyelikTarihi,Uyeler.ResimNo,Adresler.Mahalle,Adresler.Cadde,Adresler.Sokak,Adresler.Nosu,Adresler.Kat,Adresler.Daire,Adresler.Il,Adresler.Ilce FROM Uyeler,Adresler where Uyeler.AdresNo=Adresler.ID", CommandType.Text);
            return tablo;
        }
        public DataTable Arama(string ara)
        {

            DataTable tablo = dl.DTVeriCek("select * from Uyeler where Adi like '%" + ara + "%' OR  Soyadi like '%" + ara + "%'", CommandType.Text);
            return tablo;


        }


    }
}
