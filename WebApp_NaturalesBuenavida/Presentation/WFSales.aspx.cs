using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Logic;

namespace Presentation
{
    public partial class WFSales : System.Web.UI.Page
    {
        private SalesLog salesLog = new SalesLog();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadSalesData();
            }
        }

        private void LoadSalesData()
        {
            var salesData = salesLog.ShowDDL();
            GVSales.DataSource = salesData;
            GVSales.DataBind();
        }
        
        protected void BtnAddSale_Click(object sender, EventArgs e)
        {
            DateTime fecha;
            if (DateTime.TryParseExact(TxtFecha.Text, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out fecha))
            {
                decimal total = decimal.Parse(TxtTotal.Text);
                string descripcion = TxtDescripcion.Text;
                int clienteId = int.Parse(TxtClienteId.Text);
                int empleadoId = int.Parse(TxtEmpleadoId.Text);

                bool result = salesLog.SaveSale(fecha, total, descripcion, clienteId, empleadoId);

                if (result)
                {
                    LblMsg.Text = "Venta agregada correctamente.";
                    ClearForm();
                    //LoadSalesData();
                }
                else
                {
                    LblMsg.Text = "Error al agregar la venta.";
                }
            }
            else
            {
                LblMsg.Text = "La fecha ingresada no es válida. Use el formato yyyy-MM-dd.";
            }
        }


        private void ClearForm()
        {
            TxtDescripcion.Text = "";
            TxtClienteId.Text = "";
            TxtEmpleadoId.Text = "";
            TxtFecha.Text = DateTime.Now.ToString("yyyy-MM-dd");
            TxtTotal.Text = "";
        }

        protected void GVSales_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Button btnUpdate = (Button)e.Row.FindControl("BtnUpdate");
                Button btnDelete = (Button)e.Row.FindControl("BtnDelete");

                if (btnUpdate != null)
                {
                    btnUpdate.CommandArgument = DataBinder.Eval(e.Row.DataItem, "Referencia").ToString();
                }

                if (btnDelete != null)
                {
                    btnDelete.CommandArgument = DataBinder.Eval(e.Row.DataItem, "Referencia").ToString();
                }
            }
        }

        protected void GVSales_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Update")
            {
                int referencia = Convert.ToInt32(e.CommandArgument);
                DateTime fecha = DateTime.Parse(((Label)GVSales.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("LblFecha")).Text);
                decimal total = decimal.Parse(((Label)GVSales.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("LblTotal")).Text);
                string descripcion = ((TextBox)GVSales.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("TxtDescripcion")).Text;
                int clienteId = int.Parse(((TextBox)GVSales.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("TxtClienteId")).Text);
                int empleadoId = int.Parse(((TextBox)GVSales.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("TxtEmpleadoId")).Text);

                bool result = salesLog.UpdateSale(referencia, fecha, total, descripcion, clienteId, empleadoId);

                LblMsg.Text = result ? "Venta actualizada correctamente." : "Error al actualizar la venta.";
            }
            else if (e.CommandName == "Delete")
            {
                int referencia = Convert.ToInt32(e.CommandArgument);
                bool result = salesLog.DeleteSale(referencia);

                LblMsg.Text = result ? "Venta eliminada correctamente." : "Error al eliminar la venta.";
            }

            LoadSalesData();
        }
    }
}
