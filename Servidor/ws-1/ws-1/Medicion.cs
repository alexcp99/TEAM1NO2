using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ws_1
{
    public class Medicion
    {
        public int idmedicion { get; set; }


        public int idsensor { get; set; }


        public int date { get; set; }


        public double lat { get; set; }


        public double longi { get; set; }


        public double valor { get; set; }

        public Medicion(int id, int ids, int dat, double lati, double longitud, double val)
        {
            idmedicion = id;
            idsensor = ids;
            date = dat;
            lat = lati;
            longi = longitud;
            valor = val;
        }


    }
}