using Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Logic
{
    public class BuyLog
    {
        BuyDat objBuy = new BuyDat();

        //Metodo para mostrar todas las Compras
        public DataSet showBuy()
        {
            return objBuy.showBuy();
        }

        //Metodo para guardar una nueva Compra
        public bool saveBuy(DateTime _fecha_compra, double _total, int _fkproducto_id, string _numero_factura, int _cantidad, double _precio_unitario)
        {
            return objBuy.saveBuy(_fecha_compra, _total, _fkproducto_id, _numero_factura, _cantidad, _precio_unitario);
        }
        //Metodo para actualizar una Compra
        public bool updateBuy(int _compra_id, DateTime _fecha_compra, double _total, string _numero_factura, int _fkproducto_id, int _cantidad, double _precio_unitario)
        {
            return objBuy.updateBuy(_compra_id,_fecha_compra, _total, _numero_factura, _fkproducto_id, _cantidad, _precio_unitario);
        }
    }
}