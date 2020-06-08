using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DAL;
using Entity;
using System.Data.OleDb;

namespace BL
{
    public class YazarlarBL
    {
        DAL.DAL dl = new DAL.DAL();
        public int YazarEkle(string YazarAdi)
        {
            int Sonuc = dl.EkleSilGuncelle("insert into Yazarlar (YazarAdi) values ('" + YazarAdi + "')", System.Data.CommandType.Text);
            return Sonuc;
        }

        public int YazarSil(int Id)
        {
            int Sonuc = dl.EkleSilGuncelle("Delete from Yazarlar where ID =" + Id, System.Data.CommandType.Text);
            return Sonuc;
        }

        public int YazarGuncelle(string YazarAdi,int Id)
        {

            int Sonuc = dl.EkleSilGuncelle("Update Yazarlar set YazarAdi='" + YazarAdi + "' where ID=" + Id, System.Data.CommandType.Text);
            return Sonuc;
        }

        public int YazarIDCek(string YazarAdi)
        {
            OleDbDataReader dr = dl.DRVeriCek("select * from Yazarlar where YazarAdi='" + YazarAdi + "'", CommandType.Text);
           int Sonuc;
           if (dr.Read())
           {
               Sonuc = Convert.ToInt32(dr["ID"]);
               return Sonuc;
           }
           else
               return 0;

        }

        public string YazarAdiCek(int id)
        {
            OleDbDataReader dr = dl.DRVeriCek("select * from Yazarlar where ID=" + id + "", CommandType.Text);
            string Sonuc;
            if (dr.Read())
            {
                Sonuc = dr["YazarAdi"].ToString();
                return Sonuc;
            }
            else
                return "";

        }

        
        public List<YazarlarEntity> YazarlarıGetir()
        {
            OleDbDataReader dr = dl.DRVeriCek("Select * from Yazarlar", CommandType.Text);
            if (dr.HasRows)
            {
                List<YazarlarEntity> Yazarlar = new List<YazarlarEntity>();
                while (dr.Read())
                {
                    YazarlarEntity ogretmen = new YazarlarEntity { YazarAdi = dr["YazarAdi"].ToString(),  ID = Convert.ToInt32(dr["ID"].ToString()) };

                    Yazarlar.Add(ogretmen);
                }
                return Yazarlar;
            }
            return null;
        }

        public DataTable Listeleme()
        {
            DataTable tablo = dl.DTVeriCek("select * from Yazarlar'", CommandType.Text);
            return tablo;
        }

        public DataTable Arama(string ara)
        {

            DataTable tablo = dl.DTVeriCek("select * from Yazarlar where YazarAdi like '%" + ara + "%'", CommandType.Text);
            return tablo;


        }
    }
}
