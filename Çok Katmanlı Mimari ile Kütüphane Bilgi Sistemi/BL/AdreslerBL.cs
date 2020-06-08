using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BL
{
    public class AdreslerBL
    {
        DAL.DAL dl = new DAL.DAL();
        public void AdresEkle(string Mahalle, string Cadde, string Sokak, string Nosu, string Kat, string Daire, string Il, string Ilce)
        {
            dl.EkleSilGuncelle("insert into Adresler (Mahalle,Cadde,Sokak,Nosu,Kat,Daire,Il,Ilce) values ('" + Mahalle + "','" + Cadde + "','" + Sokak + "','" + Nosu + "','" + Kat  + "','" + Daire + "','" + Il + "','" + Ilce + "')", System.Data.CommandType.Text);
        }

        public void AdresGuncelle(string Mahalle, string Cadde, string Sokak, string Nosu, string Kat, string Daire, string Il, string Ilce,int Id)
        {
            dl.EkleSilGuncelle(" Update Adresler set Mahalle='"+Mahalle+"', Cadde='"+Cadde+"', Sokak='"+Sokak+"', Nosu='"+Nosu+"', Kat='"+Kat+"', Daire='"+Daire+"',Il='"+Ilce+"' where ID= "+Id, System.Data.CommandType.Text);
        }


        public int IdGonderme() {
            int Sonuc = dl.EkleSilGuncelle("Select top 1 * From Adresler order by ID desc", System.Data.CommandType.Text);
            return Sonuc;
        }

    }
}
