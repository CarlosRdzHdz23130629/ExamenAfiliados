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
            ExcelPackage.License.SetNonCommercialPersonal("Jose Luis Mota Espeleta");
            using (var package = new ExcelPackage(new System.IO.FileInfo(path)))
            {
                if (package.Workbook.Worksheets.Count == 0)
                {
                    MessageBox.Show("El archivo de Excel no contiene hojas de trabajo.");
                    return;
                }

                ExcelWorksheet worksheet = package.Workbook.Worksheets[0];

                // Definir nombres de columnas
                string[] columnas = { "ID", "ENTIDAD", "MUNICIPIO", "NOMBRE", "FECHA DE AFILICION", "ESTATUS" };
                foreach (var col in columnas)
                    dt.Columns.Add(col);

                // Fila donde empiezan los datos (ajusta a 3 si tu Excel lo requiere)
                int inicioFila = 2;
                int ultimaFila = worksheet.Dimension.End.Row;

                for (int i = inicioFila; i <= ultimaFila; i++)
                {
                    // si la celda de ID está vacía, asumimos fin de los registros
                    if (string.IsNullOrWhiteSpace(worksheet.Cells[i, 1].Text))
                        break;

                    DataRow row = dt.NewRow();

                    for (int j = 1; j <= dt.Columns.Count; j++)
                    {
                        row[j - 1] = worksheet.Cells[i, j].Text;
                    }

                    dt.Rows.Add(row);
                }
            }

            // Guardamos tabla global para filtros
            afiliadosTable = dt;

            // Mostrar en DataGridView
            dgvInformacion.DataSource = afiliadosTable;

            // Llenar combo ENTIDAD
            cmbEntidad.Items.Clear();
            var entidades = afiliadosTable.AsEnumerable()
                .Select(r => r.Field<string>("ENTIDAD"))
                .Distinct()
                .OrderBy(e => e);

            foreach (var e1 in entidades)
                cmbEntidad.Items.Add(e1);

            // Llenar combo MUNICIPIO
            LlenarMunicipios();

            // Mostrar total de afiliados
            txtTotalAfiliados.Text = afiliadosTable.Rows.Count.ToString();
        }

       
            private void LlenarMunicipios()
        {
            cmbMunicipio.Items.Clear();

            if (afiliadosTable == null) return;

            var municipios = afiliadosTable.AsEnumerable()
                .Select(r => r.Field<string>("MUNICIPIO"))
                .Distinct()
                .OrderBy(m => m);

            foreach (var m in municipios)
                cmbMunicipio.Items.Add(m);
        }

        //private void LlenarMunicipios()
        //{
        //    cmbMunicipio.Items.Clear();

        //    if (afiliadosTable == null) return;

        //    // Insertar opción "Todos"
        //    cmbMunicipio.Items.Add("Todos");

        //    // Agregar municipios únicos
        //    var municipios = afiliadosTable.AsEnumerable()
        //        .Select(r => r.Field<string>("MUNICIPIO"))
        //        .Distinct()
        //        .OrderBy(m => m);

        //    foreach (var m in municipios)
        //        cmbMunicipio.Items.Add(m);

        //    // Seleccionar "Todos" por defecto
        //    cmbMunicipio.SelectedIndex = 0;
        //}

        //private void LlenarMunicipios()
        //{
        //    cmbMunicipio.Items.Clear();

        //    var municipios = afiliadosTable.AsEnumerable()
        //        .Select(r => r.Field<string>("MUNICIPIO"))
        //        .Distinct()
        //        .OrderBy(m => m);

        //    foreach (var m in municipios)
        //        cmbMunicipio.Items.Add(m);

        //// llenar combo MUNICIPIO
        //LlenarMunicipios();

        //    // total afiliados
        //    txtTotalAfiliados.Text = afiliadosTable.Rows.Count.ToString();
        //}

        private void cmbEntidad_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbEntidad.SelectedItem != null)
            {
                string entidad = cmbEntidad.SelectedItem.ToString();

                cmbMunicipio.Items.Clear();

                var municipios = afiliadosTable.AsEnumerable()
                    .Where(r => r.Field<string>("ENTIDAD") == entidad)
                    .Select(r => r.Field<string>("MUNICIPIO"))
                    .Distinct()
                    .OrderBy(m => m);

                foreach (var m in municipios)
                    cmbMunicipio.Items.Add(m);
            }
        }

        

        private void FiltrarDatos()
        {
            if (afiliadosTable == null) return;

            var query = afiliadosTable.AsEnumerable();

            // Filtrar por entidad
            if (cmbEntidad.SelectedItem != null)
                query = query.Where(r => r.Field<string>("ENTIDAD") == cmbEntidad.SelectedItem.ToString());

            // Filtrar por municipio
            if (cmbMunicipio.SelectedItem != null)
                query = query.Where(r => r.Field<string>("MUNICIPIO") == cmbMunicipio.SelectedItem.ToString());

            // Filtrar por rango de fechas
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

            // Actualizar total de afiliados
            txtTotalAfiliados.Text = query.Count().ToString();
        }

        
            private void AplicarFiltros()
        {
            if (afiliadosTable == null) return;

            string entidadSeleccionada = cmbEntidad.SelectedItem?.ToString();
            string municipioSeleccionado = cmbMunicipio.SelectedItem?.ToString();

            var filtrados = afiliadosTable.AsEnumerable()
                .Where(r =>
                    (string.IsNullOrEmpty(entidadSeleccionada) || r.Field<string>("ENTIDAD") == entidadSeleccionada) &&
                    (string.IsNullOrEmpty(municipioSeleccionado) || r.Field<string>("MUNICIPIO") == municipioSeleccionado)
                );

            dgvInformacion.DataSource = filtrados.Any() ? filtrados.CopyToDataTable() : null;
            txtTotalAfiliados.Text = filtrados.Count().ToString();
        }
        private void cmbMunicipio_SelectedIndexChanged(object sender, EventArgs e)
        {
            FiltrarDatos();
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
            cmbEntidad.SelectedIndex = -1;
            cmbMunicipio.SelectedIndex = -1;
            chkFiltrarFecha.Checked = false;

            txtTotalAfiliados.Text = afiliadosTable.Rows.Count.ToString();
        }
    }
}
