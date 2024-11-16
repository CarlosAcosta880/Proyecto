using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace Data
{
    public class SalesDat
    {
        Persistence objPer = new Persistence();

        // Método para mostrar las ventas desde la base de datos.
        public DataSet ShowSales()
        {
            DataSet objData = new DataSet();

            try
            {
                // Abrir la conexión y configurar el comando.
                using (MySqlConnection connection = objPer.openConnection())
                {
                    if (connection == null)
                    {
                        Console.WriteLine("No se pudo establecer la conexión.");
                        return objData;
                    }

                    MySqlDataAdapter objAdapter = new MySqlDataAdapter();
                    MySqlCommand objSelectCmd = new MySqlCommand
                    {
                        Connection = connection,
                        CommandText = "sp_mostrar_ventas",
                        CommandType = CommandType.StoredProcedure
                    };

                    objAdapter.SelectCommand = objSelectCmd;    
                    objAdapter.Fill(objData);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al mostrar las ventas: " + ex.Message);
            }

            return objData;
        }

        // Método para mostrar las ventas desde el procedimiento DDL
        public DataSet ShowDDLSales()
        {
            DataSet objData = new DataSet();

            try
            {
                using (MySqlConnection connection = objPer.openConnection())
                {
                    if (connection == null)
                    {
                        Console.WriteLine("No se pudo establecer la conexión.");
                        return objData;
                    }

                    MySqlDataAdapter objAdapter = new MySqlDataAdapter();
                    MySqlCommand objSelectCmd = new MySqlCommand
                    {
                        Connection = connection,
                        CommandText = "spDDL_mostrar_ventas",
                        CommandType = CommandType.StoredProcedure
                    };

                    objAdapter.SelectCommand = objSelectCmd;
                    objAdapter.Fill(objData);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al mostrar las ventas DDL: " + ex.Message);
            }

            return objData;
        }

        // Método para guardar una nueva venta
        public bool SaveSale(DateTime fecha, decimal total, string descripcion, int clienteId, int empleadoId)
        {
            bool executed = false;

            try
            {
                using (MySqlConnection connection = objPer.openConnection())
                {
                    if (connection == null)
                    {
                        Console.WriteLine("No se pudo establecer la conexión.");
                        return executed;
                    }

                    MySqlCommand objSelectCmd = new MySqlCommand
                    {
                        Connection = connection,
                        CommandText = "sp_insertar_venta",
                        CommandType = CommandType.StoredProcedure
                    };

                    objSelectCmd.Parameters.Add("p_vent_fecha", MySqlDbType.Date).Value = fecha;
                    objSelectCmd.Parameters.Add("p_vent_total", MySqlDbType.Decimal).Value = total;
                    objSelectCmd.Parameters.Add("p_vent_descripcion", MySqlDbType.Text).Value = descripcion;
                    objSelectCmd.Parameters.Add("p_cli_id", MySqlDbType.Int32).Value = clienteId;
                    objSelectCmd.Parameters.Add("p_emp_id", MySqlDbType.Int32).Value = empleadoId;

                    // Ejecutar el comando y comprobar el resultado
                    executed = objSelectCmd.ExecuteNonQuery() == 1;
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error de MySQL al guardar la venta: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al guardar la venta: " + ex.Message);
            }

            return executed;
        }


        // Método para actualizar una venta
        public bool UpdateSale(int ventaId, DateTime fecha, decimal total, string descripcion, int clienteId, int empleadoId)
        {
            bool executed = false;

            try
            {
                using (MySqlConnection connection = objPer.openConnection())
                {
                    if (connection == null)
                    {
                        Console.WriteLine("No se pudo establecer la conexión.");
                        return executed;
                    }

                    MySqlCommand objSelectCmd = new MySqlCommand
                    {
                        Connection = connection,
                        CommandText = "sp_actualizar_venta",
                        CommandType = CommandType.StoredProcedure
                    };

                    objSelectCmd.Parameters.Add("p_ventaId", MySqlDbType.Int32).Value = ventaId;
                    objSelectCmd.Parameters.Add("p_fecha", MySqlDbType.Date).Value = fecha;
                    objSelectCmd.Parameters.Add("p_total", MySqlDbType.Decimal).Value = total;
                    objSelectCmd.Parameters.Add("p_descripcion", MySqlDbType.Text).Value = descripcion;
                    objSelectCmd.Parameters.Add("p_clienteId", MySqlDbType.Int32).Value = clienteId;
                    objSelectCmd.Parameters.Add("p_empleadoId", MySqlDbType.Int32).Value = empleadoId;

                    executed = objSelectCmd.ExecuteNonQuery() == 1;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error al actualizar la venta: " + e.Message);
            }

            return executed;
        }

        // Método para eliminar una venta
        public bool DeleteSale(int ventaId)
        {
            bool executed = false;

            try
            {
                using (MySqlConnection connection = objPer.openConnection())
                {
                    if (connection == null)
                    {
                        Console.WriteLine("No se pudo establecer la conexión.");
                        return executed;
                    }

                    MySqlCommand objSelectCmd = new MySqlCommand
                    {
                        Connection = connection,
                        CommandText = "sp_eliminar_venta",
                        CommandType = CommandType.StoredProcedure
                    };

                    objSelectCmd.Parameters.Add("p_ventaId", MySqlDbType.Int32).Value = ventaId;

                    executed = objSelectCmd.ExecuteNonQuery() == 1;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error al eliminar la venta: " + e.Message);
            }

            return executed;
        }
    }
}
