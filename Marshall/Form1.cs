using Marshall.Clases;
using Marshall.Formulario;
using Marshall.Logica;
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
    public partial class Form1 : Form
    {
        private Dictionary<int, List<SeguimientoProyecto>> informacion;
        public Form1()
        {
            InitializeComponent();
            IniciarExcel();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void IniciarExcel()
        {
    
            CargarFechas();
            ContruirCalendario(0);
        }
        //private void CargarCalendario()
        //{
        //    List<int> mesesSort = new List<int>(informacion.Keys);
        //    mesesSort.Sort();
        //    int menor = mesesSort[0] == 0 ? mesesSort[1] : mesesSort[0];
        //    calendario.SelectionStart = GenerarFecha(menor);
        //    //DateTime[] fechas = new DateTime();
        //    foreach (SeguimientoProyecto seguimientoProyecto in informacion[menor])
        //    {
        //        calendario.SetCalendarDimensions
        //    }

        //    //calendario.AnnuallyBoldedDates =
        //}

        private DateTime GenerarFecha(int key)
        {
            var anioStr = Convert.ToInt32(key.ToString().Substring(0, 4));
            var mesStr = Convert.ToInt32(key.ToString().Substring(4));
            return new DateTime(anioStr, mesStr, 1);

        }
        private void CargarFechas()
        {

            List<int> mesesSort = new List<int>(informacion.Keys);
            mesesSort.Sort();
            //for(int i =0; i <= meses.Count; i++)
            foreach (int mes in mesesSort)
            {
                var mesName = "";
                if (mes != 0)
                {
                    var anioStr = mes.ToString().Substring(0, 4);
                    var mesStr = mes.ToString().Substring(4);
                    mesName = String.Format("{0}-{1}", anioStr, mesStr);
                }
                ComboboxItem item = new ComboboxItem();
                item.Text = mesName;
                item.Value = mes;
                //Meses.Items.Add(item);
            }
        }

        private void Meses_SelectedIndexChanged(object sender, EventArgs e)
        {
            var obj = (ComboBox)sender;
            var item = obj.SelectedItem;
        }

        private void ContruirCalendario(int key)
        {
            LimpiarCalendarios();
            int mes = 1;
            int anio = 2022;
            var diasDelMes = DateTime.DaysInMonth(anio, mes);
            var primerDia = (int)(new DateTime(anio, mes, 1)).DayOfWeek;
            var informacionMes = informacion[key];
            primerDia = primerDia == 0 ? 7 : primerDia;
            var contador = 1;
            //for (int i = 1; i <= diasDelMes; i++)
            var flag = true;
            var dia = 1;
            while (flag)
            {
                var fl = new FlowLayoutPanel();
                fl.Size = new Size(128, 99);
                fl.BackColor = Color.Transparent;
                fl.BorderStyle = BorderStyle.FixedSingle;
                fl.AutoScroll = true;
                fl.Cursor = Cursors.Hand;
                if (contador == primerDia)
                {
                    fl.Name = String.Format("{0}{1}{2}", dia, mes, anio);
                    fl.Controls.Add(TextoCalendario(dia.ToString()));
                    fl.BackColor = Color.White;
                    var informacionLbl = AsignarTextoOCI(new DateTime(anio, mes, dia));
                    if (informacionLbl != null)
                    {
                        fl.Controls.AddRange(informacionLbl);
                    }

                    AgregarCalendario(new DateTime(anio, mes, dia), fl);


                    primerDia++;
                    dia++;
                }
                else if (contador < primerDia)
                {
                    var diaMesAnterior = primerDia * -1;
                    diaMesAnterior += contador;
                    var fecha = new DateTime(anio, mes, dia);
                    fecha = fecha.AddDays(diaMesAnterior);
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
        }

        private void AgregarCalendario(DateTime fecha, FlowLayoutPanel fl)
        {
            var diaSemana = (int)(new DateTime(fecha.Year, fecha.Month, fecha.Day)).DayOfWeek;
            switch (diaSemana)
            {
                case 0:
                    calendarioDomingo.Controls.Add(fl);
                    break;
                case 1:
                    calendarioLunes.Controls.Add(fl);
                    break;
                case 2:
                    calendarioMartes.Controls.Add(fl);
                    break;
                case 3:
                    calendarioMiercoles.Controls.Add(fl);
                    break;
                case 4:
                    calendarioJueves.Controls.Add(fl);
                    break;
                case 5:
                    calendarioViernes.Controls.Add(fl);
                    break;
                case 6:
                    calendarioSabado.Controls.Add(fl);
                    break;
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

        private Label TextoCalendario(String texto)
        {
            var respuesta = new Label();
            respuesta.Text = texto;
            respuesta.AutoSize = false;
            respuesta.TextAlign = ContentAlignment.MiddleRight;
            respuesta.Size = new Size(110, 22);
            return respuesta;

        }
        private Dictionary<String, Control[]> AsignarInfoOCI(DateTime KeyMesTime)
        {
            var mesStr = KeyMesTime.Month < 10 ? ("0" + KeyMesTime.Month) : KeyMesTime.Month.ToString();
            var KeyMesInicio = Convert.ToInt32(String.Format("{0}{1}", KeyMesTime.Year, mesStr));
            var ociList = new List<int>();
            if (informacion.ContainsKey(KeyMesInicio))
            {
                var contadorInicio = 0;
                var contadorTermino = 0;
                var contadorProceso = 0;
                var controlsOci = new List<Control>();
                var controlsTotales = new List<Control>();
                foreach (SeguimientoProyecto seguimiento in informacion[KeyMesInicio])
                {
                    var flagInicio = false;
                    var flagTermino = false;
                    if (seguimiento.EstimadoInicioFecha != null)
                    {
                        flagInicio = seguimiento.EstimadoInicioFecha == KeyMesTime;
                        contadorInicio++;
                    }
                    if (seguimiento.EstimadoFinalizadoFecha != null)
                    {
                        flagTermino = seguimiento.EstimadoFinalizadoFecha == KeyMesTime;
                        contadorTermino++;

                    }
                    if (flagInicio || flagTermino)
                    {
                        if (!ociList.Contains(seguimiento.Oci))
                        {
                            ociList.Add(seguimiento.Oci);
                            var texto = TextoCalendario(seguimiento.Oci.ToString());
                            texto.Click += buscarInformacion;
                            texto.BackColor = flagInicio ? Color.Blue : Color.Red;
                            controlsOci.Add(texto);
                        }
                    }
                }

                var textoTotales = TextoCalendario("Total Inicio:" + contadorInicio);
                textoTotales.Click += buscarInformacion;
                textoTotales.BackColor = Color.Blue;
                controlsOci.Add(textoTotales);


                textoTotales = TextoCalendario("Total Terminada:" + contadorTermino);
                textoTotales.Click += buscarInformacion;
                textoTotales.BackColor = Color.Blue;
                controlsOci.Add(textoTotales);


                contadorTermino++;

                var respuesta = new Dictionary<String, Control[]>();
                respuesta["OCI"] = controlsOci.ToArray();

                return respuesta;
            }
            else
            {
                return null;
            }



        }
        private Control[] AsignarTextoOCI(DateTime KeyMesTime)
        {
            var mesStr = KeyMesTime.Month < 10 ? ("0" + KeyMesTime.Month) : KeyMesTime.Month.ToString();
            var KeyMesInicio = Convert.ToInt32(String.Format("{0}{1}", KeyMesTime.Year, mesStr));
            var ociList = new List<int>();
            if (informacion.ContainsKey(KeyMesInicio))
            {
                var controls = new List<Control>();
                foreach (SeguimientoProyecto seguimiento in informacion[KeyMesInicio])
                {
                    var flagInicio = false;
                    var flagTermino = false;
                    if (seguimiento.EstimadoInicioFecha != null)
                    {
                        flagInicio = seguimiento.EstimadoInicioFecha == KeyMesTime;

                    }
                    if (seguimiento.EstimadoFinalizadoFecha != null)
                    {
                        flagTermino = seguimiento.EstimadoFinalizadoFecha == KeyMesTime;

                    }
                    if (flagInicio || flagTermino)
                    {
                        if (!ociList.Contains(seguimiento.Oci))
                        {
                            ociList.Add(seguimiento.Oci);
                            var texto = TextoCalendario(seguimiento.Oci.ToString());
                            texto.Click += buscarInformacion;
                            texto.BackColor = flagInicio ? Color.Blue : Color.Red;
                            controls.Add(texto);
                        }
                    }
                }
                return controls.ToArray();
            }
            else
            {
                return null;
            }

        }
        private void buscarInformacion(object sender, EventArgs e)
        {

        }


        private void AddRowToPanel(TableLayoutPanel panel, string[] rowElements)
        {
            panel.RowCount += 1;
            for (int i = 0; i < rowElements.Length; i++)
            {
                panel.Controls.Add(new Label() { Text = rowElements[i] }, i, panel.RowCount - 1);
            }
        }

        private void calendario_DateChanged(object sender, DateRangeEventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Lbl_Fecha_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void calendarioJueves_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
