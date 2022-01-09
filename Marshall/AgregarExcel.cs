using Marshall.Clases;
using Marshall.Logica;
using Marshall.Modelos;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Marshall
{
    public partial class AgregarExcel : Form
    {
        public AgregarExcel()
        {
            InitializeComponent();
            GenerarExcel();
        }

        private void AgregarExcel_Load(object sender, EventArgs e)
        {

        }
        private void GenerarExcel()
        {
            var path = @"C:\xls\Original1.xlsx";
            var ex = new Excel();
            List<SeguimientoProyecto> listSeguimientoProyecto = (List<SeguimientoProyecto>)ex.SeguimientoProyecto(path, 1);
            GuardarInformacion(listSeguimientoProyecto);
        }
        private void GuardarInformacion(List<SeguimientoProyecto> listSeguimientoProyecto)
        {
            if (listSeguimientoProyecto != null)
                foreach (SeguimientoProyecto sp in listSeguimientoProyecto)
                {
                    using (Modelos.MarshallEntity m = new Modelos.MarshallEntity())
                    {


                        var clientes = m.Clientes.Where(c => c.Nombre.Equals(sp.Cliente.Trim().ToUpper())).FirstOrDefault();
                        if (clientes == null)
                        {
                            clientes = new Clientes();
                            clientes.Nombre = sp.Cliente.Trim().ToUpper();
                            m.Clientes.Add(clientes);
                            m.SaveChanges();
                        }
                        var prendas = m.Prendas.Where(c => c.NombreGeneral == sp.NombreGral.Trim() && c.Descripcion == sp.Descripcion.Trim()).FirstOrDefault();
                        if (prendas == null)
                        {
                            prendas = new Prendas();
                            prendas.NombreGeneral = sp.NombreGral.Trim();
                            prendas.Descripcion = sp.Descripcion.Trim();
                            m.Prendas.Add(prendas);
                            m.SaveChanges();
                        }
                        var telas = m.Telas.Where(c => c.Descripcion == sp.Tela.Trim()).FirstOrDefault();
                        if (telas == null)
                        {
                            telas = new Telas();
                            telas.Descripcion = sp.Tela.Trim();
                            m.Telas.Add(telas);
                            m.SaveChanges();
                        }
                        var proyectos = m.Proyectos.Where(c => c.Oci == sp.Oci).FirstOrDefault();
                        if (proyectos == null)
                        {
                            proyectos = new Proyectos();
                            proyectos.Oci = sp.Oci;
                            proyectos.Nota = sp.Notas;
                            proyectos.CantidaReal = sp.CantidadRealOci;
                            proyectos.CantidadPendiente = sp.CantidadPendiente;
                            proyectos.EstimadoInicio = sp.EstimadoInicioFecha;
                            proyectos.EstimadoFinalizado = sp.EstimadoFinalizadoFecha;
                            proyectos.RealInicio = sp.RealInicioFecha;
                            proyectos.RealFinalizado = sp.RealFinalizadoFecha;
                            proyectos.Clientes = clientes;
                            m.Proyectos.Add(proyectos);
                        }
                        else
                        {
                            if (sp.EstimadoInicioFecha != null)
                                proyectos.EstimadoInicio = proyectos.EstimadoInicio != null && proyectos.EstimadoInicio < sp.EstimadoInicioFecha ? proyectos.EstimadoInicio : sp.EstimadoInicioFecha;
                            if (sp.EstimadoFinalizado != null)
                                proyectos.EstimadoFinalizado = proyectos.EstimadoFinalizado != null && proyectos.EstimadoFinalizado > sp.EstimadoFinalizadoFecha ? proyectos.EstimadoFinalizado : sp.EstimadoFinalizadoFecha;
                            if (sp.RealInicioFecha != null)
                                proyectos.RealInicio = proyectos.RealInicio != null && proyectos.RealInicio < sp.RealInicioFecha ? proyectos.RealInicio : sp.RealInicioFecha;
                            if (sp.RealFinalizadoFecha != null)
                                proyectos.RealFinalizado = proyectos.RealFinalizado != null && proyectos.RealFinalizado > sp.RealFinalizadoFecha ? proyectos.RealFinalizado : sp.RealFinalizadoFecha;
                        }
                        m.SaveChanges();
                        var pp = m.Proyectos_Prendas.Where(c => c.ProyectosId == proyectos.Id && c.PrendaId == prendas.Id && c.TelaId == telas.Id).FirstOrDefault();
                        if (pp == null)
                        {
                            pp = new Proyectos_Prendas();
                            pp.Proyectos = proyectos;
                            pp.Prendas = prendas;
                            pp.Telas = telas;
                            pp.CantidaReal = sp.CantidadRealOci;
                            pp.CantidadPendiente = sp.CantidadPendiente;
                            pp.EstimadoInicio = sp.EstimadoInicioFecha;
                            pp.EstimadoFinalizado = sp.EstimadoFinalizadoFecha;
                            pp.RealInicio = sp.RealInicioFecha;
                            pp.RealFinalizado = sp.RealFinalizadoFecha;
                            m.Proyectos_Prendas.Add(pp);
                            m.SaveChanges();
                        }
                    }
                }

        }
    }
}
