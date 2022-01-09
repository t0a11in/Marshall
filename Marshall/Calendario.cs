using Marshall.Modelos;
using Marshall.Utilitario;
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
    public partial class Calendario : Form
    {
        public Calendario()
        {
            InitializeComponent();
            IniciarCompornente();
        }
        private void IniciarCompornente()
        {
            var fecha = DateTime.Now;
            fecha = fecha.AddYears(-1);
            fecha = fecha.AddMonths(5);
            CambioFecha(fecha);
            //Lbl_Fecha.Text = String.Format("{0} {1}", Fechas.ObtenerNombreMes(DateTime.Now.Month), DateTime.Now.Year);
            //ContruirCalendario(DateTime.Now);
        }
        private void CambioFecha(DateTime fecha)
        {
            Lbl_Fecha.Text = String.Format("{0} {1}", Fechas.ObtenerNombreMes(fecha.Month), fecha.Year);
            lbl_date.Text = String.Format("{0}_{1}", fecha.Year, fecha.Month);
            ContruirCalendario(fecha);
        }
        private List<Proyectos> BuscarProyectosCalendario(DateTime fechaInicio, DateTime fechaTermino)
        {
            using (Modelos.MarshallEntity m = new Modelos.MarshallEntity())
            {
                return m.Proyectos.Where(c => c.EstimadoInicio >= fechaInicio || c.EstimadoFinalizado <= fechaTermino).ToList();
            }
        }
        private void LimpiarCalendarios()
        {
            calendarioLunes.Controls.Clear();
            calendarioMartes.Controls.Clear();
            calendarioMiercoles.Controls.Clear();
            calendarioJueves.Controls.Clear();
            calendarioViernes.Controls.Clear();
            calendarioSabado.Controls.Clear();
            calendarioDomingo.Controls.Clear();
        }

        private void ContruirCalendario(DateTime mes)
        {
            LimpiarCalendarios();
            var diasDelMes = DateTime.DaysInMonth(mes.Year, mes.Month);
            var primerDia = (int)mes.DayOfWeek;
            primerDia = primerDia == 0 ? 7 : primerDia;
            var contador = 1;
            //for (int i = 1; i <= diasDelMes; i++)
            var flag = true;
            var dia = 1;
            //Lista del mes
            var mesFinal = mes.AddMonths(1).AddDays(-1);
            var listaProyectosMes = BuscarProyectosCalendario(mes, mesFinal);
            while (flag)
            {

                if (contador == primerDia)
                {
                    var fechaCurso = new DateTime(mes.Year, mes.Month, dia);
                    var cantidadIniciada = listaProyectosMes.Where(z => z.EstimadoInicio == fechaCurso).ToList();
                    var cantidadFinalizadas = listaProyectosMes.Where(z => z.EstimadoFinalizado == fechaCurso).ToList();
                    var cantidadProceso = listaProyectosMes.Where(z => z.EstimadoInicio >= fechaCurso || z.EstimadoInicio <= fechaCurso).ToList();

                    /*Asigna 3 valores
                     * 1ro: contador de los que inciiar, terminan, curso
                     * 2do: lista de ordenes OCI
                    */
                    //Cantidades Totales
                    var fl = IniciarFlowLayoutPanel(String.Format("CifrasTotales_{0}{1}{2}", dia, mes.Month, mes.Year));
                    fl.Controls.Add(TextoCalendario(dia.ToString()));
                    fl.BackColor = Color.White;
                    var informacionLbl = AsignarTextoInfografia(cantidadIniciada.Count, cantidadFinalizadas.Count, cantidadProceso.Count);
                    fl.Controls.AddRange(informacionLbl);
                    AgregarCalendario(fechaCurso, fl);
                    //Agrega las fichas en curso
                    fl = IniciarFlowLayoutPanel(String.Format("Curso_{0}{1}{2}", dia, mes.Month, mes.Year), false);
                    fl.Controls.Add(TextoCalendario(dia.ToString()));
                    fl.Controls.AddRange(AsignarTextoOci(cantidadProceso));
                    AgregarCalendario(fechaCurso, fl);
                    //Agrega las fichas en finalizadas
                    fl = IniciarFlowLayoutPanel(String.Format("Finalizadas_{0}{1}{2}", dia, mes.Month, mes.Year), false);
                    fl.Controls.Add(TextoCalendario(dia.ToString()));
                    fl.Controls.AddRange(AsignarTextoOci(cantidadFinalizadas));
                    AgregarCalendario(fechaCurso, fl);
                    //Agrega las fichas en Inicia
                    fl = IniciarFlowLayoutPanel(String.Format("Iniciadas_{0}{1}{2}", dia, mes.Month, mes.Year), false);
                    fl.Controls.Add(TextoCalendario(dia.ToString()));
                    fl.Controls.AddRange(AsignarTextoOci(cantidadIniciada));
                    AgregarCalendario(fechaCurso, fl);

                    primerDia++;
                    dia++;
                }
                else if (contador < primerDia)
                {
                    var diaMesAnterior = primerDia * -1;
                    diaMesAnterior += contador;
                    var fecha = new DateTime(mes.Year, mes.Month, dia);
                    fecha = fecha.AddDays(diaMesAnterior);
                    var fl = IniciarFlowLayoutPanel("");
                    fl.Controls.Add(TextoCalendario(fecha.Day.ToString()));
                    fl.BackColor = Color.Transparent;
                    AgregarCalendario(fecha, fl);
                }
                else
                {

                }
                //calendario.Controls.Add(fl);
                contador++;
                flag = diasDelMes >= dia;
            }
            //var tamanio = Convert.ToInt32((contador * 99/7));
            //this.Size = new Size(this.Size.Width, tamanio);

        }
        private FlowLayoutPanel IniciarFlowLayoutPanel(String name, Boolean visible = true)
        {
            var fl = new FlowLayoutPanel();
            fl.Name = name;
            fl.Size = new Size(128, 99);
            fl.BackColor = Color.Transparent;
            fl.BorderStyle = BorderStyle.FixedSingle;
            fl.AutoScroll = true;
            fl.Cursor = Cursors.Hand;
            fl.Visible = visible;
            return fl;
        }
        private void AgregarCalendario(DateTime fecha, FlowLayoutPanel fl)
        {
            var diaSemana = (int)fecha.DayOfWeek;
            switch (diaSemana)
            {
                case 0:
                    fl.Size = new Size(calendarioDomingo.Width,  fl.Height);
                    calendarioDomingo.Controls.Add(fl);
                    break;
                case 1:
                    fl.Size = new Size(calendarioLunes.Width, fl.Height);

                    calendarioLunes.Controls.Add(fl);
                    break;
                case 2:
                    fl.Size = new Size(calendarioMartes.Width, fl.Height);

                    calendarioMartes.Controls.Add(fl);
                    break;
                case 3:
                    fl.Size = new Size(calendarioMiercoles.Width, fl.Height);

                    calendarioMiercoles.Controls.Add(fl);
                    break;
                case 4:
                    fl.Size = new Size(calendarioJueves.Width,  fl.Height);
                    calendarioJueves.Controls.Add(fl);
                    break;
                case 5:
                    fl.Size = new Size(calendarioViernes.Width,  fl.Height);
                    calendarioViernes.Controls.Add(fl);
                    break;
                case 6:
                    fl.Size = new Size(calendarioSabado.Width,  fl.Height);
                    calendarioSabado.Controls.Add(fl);
                    break;
            }
        }
        private Label TextoCalendario(String texto)
        {
            var respuesta = new Label();
            respuesta.Text = texto;
            respuesta.AutoSize = false;
            respuesta.TextAlign = ContentAlignment.MiddleLeft;
            respuesta.Size = new Size(110, 22);
            respuesta.ForeColor = Color.Black;
            return respuesta;

        }

        private Control[] AsignarTextoInfografia(int cantidadIniciada, int cantidadFinalizada, int cantidadCurso)
        {
            var controls = new List<Control>();
            var texto = new Label();
            if (cantidadCurso > 0)
            {
                texto = TextoCalendario(cantidadCurso.ToString());
                texto.Click += buscarInformacion;
                texto.BackColor = Color.Green;
                texto.TextAlign = ContentAlignment.MiddleCenter;

                controls.Add(texto);
            }

            if (cantidadIniciada > 0)
            {

                texto = TextoCalendario(cantidadIniciada.ToString());
                texto.Click += buscarInformacion;
                texto.BackColor = Color.Blue;
                //texto.ForeColor = Color.White;
                texto.TextAlign = ContentAlignment.MiddleCenter;

                controls.Add(texto);
            }
            if (cantidadFinalizada > 0)
            {

                texto = TextoCalendario(cantidadFinalizada.ToString());
                texto.Click += buscarInformacion;
                texto.BackColor = Color.Red;
                texto.TextAlign = ContentAlignment.MiddleCenter;

                controls.Add(texto);
            }

            return controls.ToArray();
        }
        private Control[] AsignarTextoOci(List<Proyectos> proyectos)
        {
            var controls = new List<Control>();

            foreach (Proyectos proyecto in proyectos)
            {
                var texto = TextoCalendario(proyecto.Oci.ToString());
                texto.Click += buscarInformacion;
                texto.BackColor = Color.Green;
                controls.Add(texto);
            }
            return controls.ToArray();
        }
        private void buscarInformacion(object sender, EventArgs e)
        {

        }



        private void Calendario_Load(object sender, EventArgs e)
        {

        }

        private void less_moth_Click(object sender, EventArgs e)
        {
            //lbl_date.Text = String.Format("{0}_{1}", fecha.Year, fecha.Month);
            var fechaTxt = lbl_date.Text.Split('_');
            var fecha = new DateTime(Convert.ToInt32(fechaTxt[0].ToString()), Convert.ToInt32(fechaTxt[1].ToString()), 1);
            fecha = fecha.AddMonths(-1);
            CambioFecha(fecha);

        }

        private void more_moth_Click(object sender, EventArgs e)
        {
            var fechaTxt = lbl_date.Text.Split('_');
            var fecha = new DateTime(Convert.ToInt32(fechaTxt[0].ToString()), Convert.ToInt32(fechaTxt[1].ToString()), 1);
            fecha = fecha.AddMonths(1);
            CambioFecha(fecha);

        }

        private void calendarioMartes_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
