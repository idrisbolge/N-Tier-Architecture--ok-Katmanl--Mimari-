using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;
using Entity;
using DAL;

namespace BL
{
    public class YayinevleriBL
    {
        DAL.DAL dl = new DAL.DAL();
        AdreslerBL Adres = new AdreslerBL();
        public int YayineviEkle(string YayineviAdi, string Mahalle, string Cadde, string Sokak, string Nosu, string Kat, string Daire, string Il, string Ilce)
        {
            Adres.AdresEkle(Mahalle, Cadde, Sokak, Nosu, Kat, Daire, Il, Ilce);
            int AdresNo = Adres.IdGonderme();
            int Sonuc = dl.EkleSilGuncelle("insert into Yayinevleri (YayineviAdi,AdresNo) values ('" + YayineviAdi + "'," + Convert.ToInt32(AdresNo) + ")", System.Data.CommandType.Text);
            return Sonuc;
        }

        public int YayineviSil(int Id)
        {
            int Sonuc = dl.EkleSilGuncelle("Delete from Yayinevleri where ID =" + Id, System.Data.CommandType.Text);
            return Sonuc;
        }

        public int YayineviGuncelle(string YayineviAdi, string Mahalle, string Cadde, string Sokak, string Nosu, string Kat, string Daire, string Il, string Ilce, int Id)
        {
            int Adresver = AdresNoCek(Id);
            Adres.AdresGuncelle(Mahalle, Cadde, Sokak, Nosu, Kat, Daire, Il, Ilce, Adresver);
            int Sonuc = dl.EkleSilGuncelle("Update Yayinevleri set YayineviAdi='" + YayineviAdi + "' where ID=" + Id, System.Data.CommandType.Text);
            return Sonuc;
        }

        public DataTable Listeleme()
        {
            DataTable tablo = dl.DTVeriCek("select Yayinevleri.Id,Yayinevleri.YayineviAdi,Adresler.Mahalle,Adresler.Cadde,Adresler.Sokak,Adresler.Nosu,Adresler.Kat,Adresler.Daire,Adresler.Il,Adresler.Ilce  from Yayinevleri,Adresler where Yayinevleri.AdresNo=Adresler.ID", CommandType.Text);
            return tablo;
        }

        private int AdresNoCek(int Id)
        {
            int Sonuc = dl.EkleSilGuncelle("select AdresNo from Uyeler where ID=" + Id, System.Data.CommandType.Text);
            return Sonuc;
        }


        public List<YayinevleriEntity> YayinevleriGetir()
        {
            OleDbDataReader dr = dl.DRVeriCek("Select * from Yayinevleri", CommandType.Text);
            if (dr.HasRows)
            {
                List<YayinevleriEntity> Yayinevleri = new List<YayinevleriEntity>();
                while (dr.Read())
                {
                    YayinevleriEntity yayinevi = new YayinevleriEntity { YayineviAdi = dr["YayineviAdi"].ToString(), ID = Convert.ToInt32(dr["ID"].ToString()) };

                    Yayinevleri.Add(yayinevi);
                }
                return Yayinevleri;
            }
            return null;
        }

        public int YayıneviIDCek(string Adi)
        {
            OleDbDataReader dr = dl.DRVeriCek("select * from Yayinevleri where YayineviAdi='" + Adi + "'", CommandType.Text);
            int Sonuc;
            if (dr.Read())
            {
                Sonuc = Convert.ToInt32(dr["ID"]);
                return Sonuc;
            }
            else
                return 0;

        }


        public string YayineviAdiCek(int id)
        {
            OleDbDataReader dr = dl.DRVeriCek("select * from Yayinevleri where ID=" + id + "", CommandType.Text);
            string Sonuc;
            if (dr.Read())
            {
                Sonuc = dr["YayineviAdi"].ToString();
                return Sonuc;
            }
            else
                return "";

        }

        public DataTable Arama(string ara)
        {

            DataTable tablo = dl.DTVeriCek("select * from Uyeler where YayineviAdi like '%" + ara + "%'", CommandType.Text);
            return tablo;


        }


    }
}
