using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using System.Data;
using System.Data.OleDb;

namespace BL
{
    public class KullaniciBL
    {
        DAL.DAL dl = new DAL.DAL();
        public int KullaniciEkle(string KullaniciAdi,string Sifre,string Yetki,string ResimNo)
        {
            int Sonuc = dl.EkleSilGuncelle("insert into Kullanicilar (KullaniciAdi,Sifre,Yetki,ResimNo) values ('" + KullaniciAdi + "','"+Sifre+"','"+Yetki+"','"+ResimNo+"')", System.Data.CommandType.Text);
            return Sonuc;
        }

        public int KullaniciSil(int Id)
        {
            int Sonuc = dl.EkleSilGuncelle("Delete from Kullanicilar where ID =" + Id, System.Data.CommandType.Text);
            return Sonuc;
        }

        public int KullaniciGuncelle(string KullaniciAdi, string Sifre, string Yetki, string ResimNo, int Id)
        {

            int Sonuc = dl.EkleSilGuncelle("Update Kullanicilar set KullaniciAdi='" + KullaniciAdi + "', Sifre = '"+Sifre+"', Yetki = '"+Yetki+"', ResimNo = '"+ResimNo+"' where ID=" + Id, System.Data.CommandType.Text);
            return Sonuc;
        }

        public DataTable Listeleme()
        {
            DataTable tablo = dl.DTVeriCek("select * from Kullanicilar'", CommandType.Text);
            return tablo;
        }
        public int OturumAcma(string KullaniciAdi, string Sifre) 
        {
            int islem=0;
            OleDbDataReader Sonuc = dl.DRVeriCek("select Sifre from Kullanicilar where KullaniciAdi= '"+KullaniciAdi+"' and Sifre='"+Sifre+"'", CommandType.Text);
            if (Sonuc.Read())
            {
                islem = 1;
            }
            else
                islem = 0;


            return islem;
        }

        public object YetkiCekme(string KullaniciAdi) 
        {
            object Yetki = dl.IlkSatirIlkSutun("select Yetki from Kullanicilar where KullaniciAdi='"+KullaniciAdi+"'", CommandType.Text);
            return Yetki;
        }

        public object ResimNoCekme(string KullaniciAdi)
        {
            object ResimNo = dl.IlkSatirIlkSutun("select ResimNo from Kullanicilar where KullaniciAdi='" + KullaniciAdi + "'", CommandType.Text);
            return ResimNo;
        }
    }
}
