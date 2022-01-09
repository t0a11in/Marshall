using Marshall.Clases;
using SpreadsheetLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marshall.Logica
{
    class Excel
    {
        private static int row = 6;
        //private static int rowOrigenDatosSeguimientoProyecto = 5;
        public Dictionary<int, List<SeguimientoProyecto>> SeguimientoProyecto(String path)
        {
            SLDocument sl = new SLDocument(path);
            return ObtenerInformacion(sl);
        }
        public Object SeguimientoProyecto(String path, int tipo)
        {
            String errores = "";
            try
            {
                var sl = new SLDocument(path);
                switch (tipo)
                {
                    case 1:
                        return LeerOrdenExcel(sl);
                    default:
                        return null;
                }
            }
            catch (Exception ex)
            {
                errores = ex.Message;
            }
            return null;
        }
        private Dictionary<int, List<SeguimientoProyecto>> ObtenerInformacion(SLDocument sl)
        {
            var respuesta = new Dictionary<int, List<SeguimientoProyecto>>();
            while (!string.IsNullOrEmpty(sl.GetCellValueAsString(row, 1)))
            {
                try
                {
                    var KeyMesInicio = 0;
                    var keyMesTermino = 0;
                    var oci = sl.GetCellValueAsInt32(row, 1);
                    var cliente = sl.GetCellValueAsString(row, 2);
                    var nombreGral = sl.GetCellValueAsString(row, 3);
                    var descripcion = sl.GetCellValueAsString(row, 4);
                    var tela = sl.GetCellValueAsString(row, 5);
                    var color = sl.GetCellValueAsString(row, 6);
                    var cantidadRealOci = sl.GetCellValueAsInt32(row, 7);
                    var cantidadPendiente = sl.GetCellValueAsInt32(row, 8);
                    var categoria = sl.GetCellValueAsString(row, 9);
                    var asignado = sl.GetCellValueAsString(row, 10);
                    var estimadoInicio = sl.GetCellValueAsString(row, 11);
                    var estimadoFinalizado = sl.GetCellValueAsString(row, 12);
                    var estimadoDuracionDias = sl.GetCellValueAsInt32(row, 13);
                    var realInicio = sl.GetCellValueAsString(row, 14);
                    var realFinalizacion = sl.GetCellValueAsString(row, 15);
                    var realDuracionDias = sl.GetCellValueAsInt32(row, 16);
                    var notas = sl.GetCellValueAsString(row, 17);
                    var prioridad = sl.GetCellValueAsString(row, 18);
                    var vendedor = sl.GetCellValueAsString(row, 19);
                    var seguimiento = new SeguimientoProyecto();
                    seguimiento.Oci = oci;
                    seguimiento.Cliente = cliente;
                    seguimiento.NombreGral = nombreGral;
                    seguimiento.Descripcion = descripcion;
                    seguimiento.Tela = tela;
                    seguimiento.Color = color;
                    seguimiento.CantidadRealOci = cantidadRealOci;
                    seguimiento.CantidadPendiente = cantidadPendiente;
                    seguimiento.Categoria = categoria;
                    seguimiento.Asignado = asignado;
                    seguimiento.EstimadoInicio = estimadoInicio;
                    seguimiento.EstimadoFinalizado = estimadoFinalizado;
                    seguimiento.EstimadoDuracionDias = estimadoDuracionDias;
                    seguimiento.RealInicio = realInicio;
                    seguimiento.RealFinalizacion = realFinalizacion;
                    seguimiento.RealDuracionDias = realDuracionDias;
                    seguimiento.Notas = notas;
                    seguimiento.Prioridad = prioridad;
                    seguimiento.Vendedor = vendedor;

                    if (!String.IsNullOrEmpty(estimadoInicio))
                    {
                        System.DateTime KeyMesTime = sl.GetCellValueAsDateTime(row, 11);
                        seguimiento.EstimadoInicioFecha = KeyMesTime;
                        var mesStr = KeyMesTime.Month < 10 ? ("0" + KeyMesTime.Month) : KeyMesTime.Month.ToString();
                        KeyMesInicio = Convert.ToInt32(String.Format("{0}{1}", KeyMesTime.Year, mesStr));
                    }

                    if (!String.IsNullOrEmpty(estimadoFinalizado))
                    {
                        System.DateTime KeyMesTime = sl.GetCellValueAsDateTime(row, 12);
                        seguimiento.EstimadoFinalizadoFecha = KeyMesTime;
                        var mesStr = KeyMesTime.Month < 10 ? ("0" + KeyMesTime.Month) : KeyMesTime.Month.ToString();
                        keyMesTermino = Convert.ToInt32(String.Format("{0}{1}", KeyMesTime.Year, mesStr));
                    }
                    if (!String.IsNullOrEmpty(realInicio))
                    {
                        seguimiento.RealInicioFecha = sl.GetCellValueAsDateTime(row, 14); ;
                    }
                    if (!String.IsNullOrEmpty(realFinalizacion))
                    {
                        seguimiento.RealFinalizadoFecha = sl.GetCellValueAsDateTime(row, 15); ;
                    }

                    if (!respuesta.ContainsKey(KeyMesInicio))
                    {
                        respuesta[KeyMesInicio] = new List<SeguimientoProyecto>();
                    }


                    if (!respuesta.ContainsKey(keyMesTermino))
                    {
                        respuesta[keyMesTermino] = new List<SeguimientoProyecto>();
                    }

                    row++;
                    respuesta[KeyMesInicio].Add(seguimiento);
                    if (KeyMesInicio != keyMesTermino)
                        respuesta[keyMesTermino].Add(seguimiento);
                }
                catch (Exception ex)
                {
                    String err = ex.Message;
                    string a = "";
                }
            }
            return respuesta;
        }

        public List<SeguimientoProyecto> LeerOrdenExcel(SLDocument sl)
        {
            var respuesta = new List<SeguimientoProyecto>();
            while (!string.IsNullOrEmpty(sl.GetCellValueAsString(row, 1)))
            {
                try
                {
                    var oci = sl.GetCellValueAsInt32(row, 1);
                    var cliente = sl.GetCellValueAsString(row, 2);
                    var nombreGral = sl.GetCellValueAsString(row, 3);
                    var descripcion = sl.GetCellValueAsString(row, 4);
                    var tela = sl.GetCellValueAsString(row, 5);
                    var color = sl.GetCellValueAsString(row, 6);
                    var cantidadRealOci = sl.GetCellValueAsInt32(row, 7);
                    var cantidadPendiente = sl.GetCellValueAsInt32(row, 8);
                    var categoria = sl.GetCellValueAsString(row, 9);
                    var asignado = sl.GetCellValueAsString(row, 10);
                    var estimadoInicio = sl.GetCellValueAsString(row, 11);
                    var estimadoFinalizado = sl.GetCellValueAsString(row, 12);
                    var estimadoDuracionDias = sl.GetCellValueAsInt32(row, 13);
                    var realInicio = sl.GetCellValueAsString(row, 14);
                    var realFinalizacion = sl.GetCellValueAsString(row, 15);
                    var realDuracionDias = sl.GetCellValueAsInt32(row, 16);
                    var notas = sl.GetCellValueAsString(row, 17);
                    var prioridad = sl.GetCellValueAsString(row, 18);
                    var vendedor = sl.GetCellValueAsString(row, 19);
                    var seguimiento = new SeguimientoProyecto();
                    seguimiento.Oci = oci;
                    seguimiento.Cliente = cliente;
                    seguimiento.NombreGral = nombreGral;
                    seguimiento.Descripcion = descripcion;
                    seguimiento.Tela = tela;
                    seguimiento.Color = color;
                    seguimiento.CantidadRealOci = cantidadRealOci;
                    seguimiento.CantidadPendiente = cantidadPendiente;
                    seguimiento.Categoria = categoria;
                    seguimiento.Asignado = asignado;
                    seguimiento.EstimadoInicio = estimadoInicio;
                    seguimiento.EstimadoFinalizado = estimadoFinalizado;
                    seguimiento.EstimadoDuracionDias = estimadoDuracionDias;
                    seguimiento.RealInicio = realInicio;
                    seguimiento.RealFinalizacion = realFinalizacion;
                    seguimiento.RealDuracionDias = realDuracionDias;
                    seguimiento.Notas = notas;
                    seguimiento.Prioridad = prioridad;
                    seguimiento.Vendedor = vendedor;

                    if (!String.IsNullOrEmpty(estimadoInicio))
                    {
                        System.DateTime KeyMesTime = sl.GetCellValueAsDateTime(row, 11);
                        seguimiento.EstimadoInicioFecha = KeyMesTime;
                    }

                    if (!String.IsNullOrEmpty(estimadoFinalizado))
                    {
                        System.DateTime KeyMesTime = sl.GetCellValueAsDateTime(row, 12);
                        seguimiento.EstimadoFinalizadoFecha = KeyMesTime;
                    }

                    respuesta.Add(seguimiento);
                    row++;
                }
                catch (Exception ex)
                {
                    String err = ex.Message;
                    string a = "";
                }
            }
            return respuesta;
        }


    }
}
