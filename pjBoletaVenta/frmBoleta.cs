namespace pjBoletaVenta
{
    public partial class frmBoleta : Form
    {
        //Variables Globales
        int num;
        ListViewItem item;

        //Objeto de la clase boleta
        Boleta objB = new Boleta();

        public frmBoleta()
        {
            InitializeComponent();
        }

        private void frmBoleta_Load(object sender, EventArgs e)
        {
            num++;
            lblNumero.Text = num.ToString("D5");
            txtFecha.Text = DateTime.Now.ToShortDateString();
        }

        private void cboProducto_SelectedIndexChanged(object sender, EventArgs e)
        {
            objB.Producto = cboProducto.Text;
            txtPrecio.Text = objB.DeterminaPrecio().ToString("C");
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            DialogResult r = MessageBox.Show("¿Estas seguro que desea salir?",
                                             "Control de Venta",
                                             MessageBoxButtons.YesNo,
                                             MessageBoxIcon.Question);
            if (r == DialogResult.Yes)
                this.Close();
        }

        private void btnAñadir_Click(object sender, EventArgs e)
        {
            if (Valida() == "")
            {
                //Capturar datos
                CapturarDatos();

                //Determinar los calculos
                double precio = objB.DeterminaPrecio();
                double importe = objB.CalcularImporte();

                //Imprimir el detalle de la venta
                ImprimirDetalle(precio, importe);

                //Imprimir el total acumulado
                lblTotal.Text = AcumuladoImportes().ToString("C");
            }
            else
                MessageBox.Show("El error se encuentra en " + Valida(), "Boleta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        //Impresion de los detalles 
        private void ImprimirDetalle(double precio, double importe)
        {
            ListViewItem fila = new ListViewItem(objB.Cantidad.ToString());
            fila.SubItems.Add(objB.Producto);
            fila.SubItems.Add(precio.ToString("0.00"));
            fila.SubItems.Add(importe.ToString("0.00"));
            lvDetalles.Items.Add(fila);
        }

        //Capturar los datos del formulario
        private void CapturarDatos()
        {
            objB.Numero = int.Parse(lblNumero.Text);
            objB.Nombre = txtCliente.Text;
            objB.Direccion = txtDireccion.Text;
            objB.Fecha = DateTime.Parse(txtFecha.Text);
            objB.Cedula = txtCedula.Text;
            objB.Producto = cboProducto.Text;
            objB.Cantidad = int.Parse(txtCantidad.Text);
        }

        //Validae el ingreso de los datos
        private string Valida()
        {
            if(txtCliente.Text.Trim().Length == 0)
            {
                txtCliente.Focus();
                return "Nombre del Cliente";
            }
            else if(cboProducto.SelectedIndex == -1)
            {
                cboProducto.Focus();
                return "Descripcion del producto";
            }
            else if (txtCedula.Text.Trim().Length == 0)
            {
                txtCedula.Focus();
                return "Cedula del cliente";
            }
            else if (txtCantidad.Text.Trim().Length == 0)
            {
                txtCantidad.Focus();
                return "Cantidad Comprada";
            }
            return "";
        }

        private void lvDetalles_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            item = lvDetalles.GetItemAt(e.X, e.Y);
            string producto = lvDetalles.Items[item.Index].SubItems[1].Text;
            DialogResult r = MessageBox.Show("¿Estas seguro de eliminar el producto " +
                                             "> " + producto + "?", "Boleta",
                                             MessageBoxButtons.YesNo,
                                             MessageBoxIcon.Information);
            if(r == DialogResult.Yes)
            {
                lvDetalles.Items.Remove(item);
                lblTotal.Text = AcumuladoImportes().ToString("C");
                MessageBox.Show("¡Detalle eliminado correctamente!");
            }


        }

        //Monto acumulado de los importes por boleta 
        private double AcumuladoImportes()
        {
            double acumulado = 0;
            for (int i = 0; i < lvDetalles.Items.Count; i++)
            {
                acumulado += double.Parse(lvDetalles.Items[i].SubItems[3].Text);
            }
            return acumulado;
        }
    }
}