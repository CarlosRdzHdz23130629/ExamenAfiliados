using OfficeOpenXml;
using System.Data;
using System.IO.Packaging;

namespace Afiliados
{
    public partial class Form1 : Form
    {
        private DataTable afiliadosTable;
        public Form1()
        {
            InitializeComponent();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            FiltrarDatos();
        }

        private void btnCargar_Click(object sender, EventArgs e)
        {
            if (ofdExcel.ShowDialog() == DialogResult.OK)
            {
                string archivo = ofdExcel.FileName;
                //MessageBox.Show("Archivo seleccionado: " + archivo);
                CargarExcel(archivo);

            }
        }

        private void CargarExcel(string path)
        {
            DataTable dt = new DataTable();
            ExcelPackage.License.SetNonCommercialPersonal("Carlos Rodriguez Hernandez");
            using (var package = new ExcelPackage(new System.IO.FileInfo(path)))
            {
                if (package.Workbook.Worksheets.Count == 0)
                {
                    MessageBox.Show("El archivo de Excel no contiene hojas de trabajo.");
                    return;
                }

                ExcelWorksheet worksheet = package.Workbook.Worksheets[0];

                string[] columnas = { "ID", "ENTIDAD", "MUNICIPIO", "NOMBRE", "FECHA DE AFILICION", "ESTATUS" };
                foreach (var col in columnas)
                    dt.Columns.Add(col);

                int inicioFila = 2;
                int ultimaFila = worksheet.Dimension.End.Row;

                for (int i = inicioFila; i <= ultimaFila; i++)
                {
                    if (string.IsNullOrWhiteSpace(worksheet.Cells[i, 1].Text))
                        break;

                    DataRow row = dt.NewRow();
                    for (int j = 1; j <= dt.Columns.Count; j++)
                        row[j - 1] = worksheet.Cells[i, j].Text;

                    dt.Rows.Add(row);
                }
            }

            afiliadosTable = dt;

            //Mostrar en DataGridView
            dgvInformacion.DataSource = afiliadosTable;

            //Obtener la entidad directamente desde la primera fila del Excel
            if (afiliadosTable.Rows.Count > 0)
            {
                txtEntidad.Text = afiliadosTable.Rows[0]["ENTIDAD"].ToString();
            }

            // Llenar municipios normalmente
            LlenarMunicipios();

            // Mostrar total
            txtTotalAfiliados.Text = afiliadosTable.Rows.Count.ToString();
        }



            private void LlenarMunicipios()
        {
            cmbMunicipio.Items.Clear();

            if (afiliadosTable == null) return;

            var municipios = afiliadosTable.AsEnumerable()
                .Select(r => r.Field<string>("MUNICIPIO"))
                .Where(m => !string.IsNullOrWhiteSpace(m)) // ignorar vacios en la lista normal
                .Distinct()
                .OrderBy(m => m)
                .ToList();

           
            cmbMunicipio.Items.Add("Todos");
            cmbMunicipio.Items.Add("Ninguno");

            foreach (var m in municipios)
                cmbMunicipio.Items.Add(m);

           
            cmbMunicipio.SelectedIndex = 0;
        }

        private void FiltrarDatos()
        {
            if (afiliadosTable == null) return;

            var query = afiliadosTable.AsEnumerable();


            // Filtrar por fechas
            if (chkFiltrarFecha.Checked)
            {
                DateTime inicio = dtpInicio.Value.Date;
                DateTime fin = dtpFin.Value.Date;

                query = query.Where(r =>
                {
                    DateTime fecha;
                    if (DateTime.TryParse(r.Field<string>("FECHA DE AFILICION"), out fecha))
                        return fecha >= inicio && fecha <= fin;
                    return false;
                });
            }

            // Cargar en el DataGridView
            if (query.Any())
                dgvInformacion.DataSource = query.CopyToDataTable();
            else
                dgvInformacion.DataSource = null;

            // Actualiza
            txtTotalAfiliados.Text = query.Count().ToString();
        }

        
            private void AplicarFiltros()
        {
            if (afiliadosTable == null) return;

            string municipioSeleccionado = cmbMunicipio.SelectedItem?.ToString();

            var filtrados = afiliadosTable.AsEnumerable()
                .Where(r =>
                    municipioSeleccionado == "Todos" ||
                    (municipioSeleccionado == "Ninguno" && string.IsNullOrWhiteSpace(r.Field<string>("MUNICIPIO"))) ||
                    (municipioSeleccionado != "Todos" && municipioSeleccionado != "Ninguno" &&
                     r.Field<string>("MUNICIPIO") == municipioSeleccionado)
                );

            dgvInformacion.DataSource = filtrados.Any() ? filtrados.CopyToDataTable() : null;
            txtTotalAfiliados.Text = filtrados.Count().ToString();
        }






        private void cmbMunicipio_SelectedIndexChanged(object sender, EventArgs e)
        {
            AplicarFiltros();
        }

        private void dtpInicio_ValueChanged(object sender, EventArgs e)
        {
            if (chkFiltrarFecha.Checked) FiltrarDatos();
        }

        private void dtpFin_ValueChanged(object sender, EventArgs e)
        {
            if (chkFiltrarFecha.Checked) FiltrarDatos();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            if (afiliadosTable == null) return;

            dgvInformacion.DataSource = afiliadosTable;
           
            cmbMunicipio.SelectedIndex = -1;
            chkFiltrarFecha.Checked = false;

            txtTotalAfiliados.Text = afiliadosTable.Rows.Count.ToString();
        }
    }
}
