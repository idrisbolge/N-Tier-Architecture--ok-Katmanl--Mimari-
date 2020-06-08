using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DAL;

namespace BL
{
    public class KitaplarBL
    {
        DAL.DAL dl = new DAL.DAL();
        public int KitapEkle(string KitapAdi, string YayinYili, string Basim,string ISBN,string Tur,string SayfaSayisi,string Kategori,string Dili,string resimNu ,int YazarId,int YayineviId)
        {
            int Sonuc = dl.EkleSilGuncelle("insert into Kitaplar (KitapAdi,YayinYili,Basim,ISBN,Tur,SayfaSayisi,Kategori,Dili,YazarID,YayineviID,ResimNu) values ('" + KitapAdi + "','" + YayinYili + "','" + Basim + "','" + ISBN + "','" + Tur + "','" + SayfaSayisi + "','" + Kategori + "','" + Dili + "'," + Convert.ToInt32(YazarId) + "," + Convert.ToInt32(YayineviId) + ",'" + resimNu + "')", System.Data.CommandType.Text);
            return Sonuc;
        }

        public int KitapSil(int Id) 
        {
            int Sonuc = dl.EkleSilGuncelle("Delete from Kitaplar where ID ="+Id,System.Data.CommandType.Text); 
            return Sonuc;
        }

        public int KitapGuncelle(string KitapAdi, string YayinYili, string Basim, string ISBN, string Tur, string SayfaSayisi, string Kategori, string Dili, string resimNo, int YazarId, int YayineviId,int Id)
        {

            int Sonuc = dl.EkleSilGuncelle("Update Kitaplar set KitapAdi='" + KitapAdi + "',YayinYili='" + YayinYili + "',Basim='" + Basim + "',ISBN='" + ISBN + "',Tur='" + Tur + "',SayfaSayisi='" + SayfaSayisi + "',Kategori = '" + Kategori + "', Dili='" + Dili + "',ResimNu='" + resimNo + "', YazarID=" + Convert.ToInt32(YazarId) + ", YayineviID=" + Convert.ToInt32(YayineviId) + " where ID="+Id, System.Data.CommandType.Text);
            return Sonuc;
        }

        public DataTable Listeleme()
        {
            DataTable tablo = dl.DTVeriCek("select * from Kitaplar'", CommandType.Text);
            return tablo;
        }

        public DataTable Arama(string ara)
        {

            DataTable tablo = dl.DTVeriCek("select * from Kitaplar where KitapAdi like '%" + ara + "%' OR  ISBN like '%" + ara + "%'", CommandType.Text);
            return tablo;


        }
    }
}
