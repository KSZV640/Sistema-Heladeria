﻿using BL.Heladeria;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace heladeria
{
    public partial class FormProductos : Form
    {
        ProductosBL _productos;

        public object ToolStripButtonCancelar { get; private set; }

        public FormProductos()
        {
            InitializeComponent();


            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.Items.Add("    ");
            comboBox1.Items.Add("chispa de chocolates");
            comboBox1.Items.Add("Toffee");
            comboBox1.Items.Add("Caramelo de chocolate y calvados");
            comboBox1.Items.Add("Coulis de fresa");



            _productos = new ProductosBL();
            listaProductosBindingSource.DataSource = _productos.ObtenerProductos();
        }

        private void FormProductos_Load(object sender, EventArgs e)
        {

        }

        private void activoCheckBox_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void listaProductosBindingNavigator_RefreshItems(object sender, EventArgs e)
        {

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            DeshabilitarHabilitarBotones(true);
            Eliminar(0);
        }

        private void listaProductosBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            listaProductosBindingSource.EndEdit();

            var producto = (Producto)listaProductosBindingSource.Current;


            if (fotoPictureBox.Image != null)
            {
                producto.foto = Program.imageToByteArray(fotoPictureBox.Image);
            }
            else
            {
                producto.foto = null;
            }


            var resultado = _productos.GuardarProducto(producto);

            if (resultado.Exitoso == true)
            {
                listaProductosBindingSource.ResetBindings(false);
                DeshabilitarHabilitarBotones(true);
                MessageBox.Show("PRODUCTO GUARDADO");
            }
            else
            {
                MessageBox.Show(resultado.Mensaje);

            }

        }

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            _productos.AgregarProducto();
            listaProductosBindingSource.MoveLast();

            DeshabilitarHabilitarBotones(false);
        }

        private void DeshabilitarHabilitarBotones(bool valor)
        {
            bindingNavigatorMoveFirstItem.Enabled = valor;
            bindingNavigatorMoveLastItem.Enabled = valor;
            bindingNavigatorMovePreviousItem.Enabled = valor;
            bindingNavigatorMoveNextItem.Enabled = valor;
            bindingNavigatorPositionItem.Enabled = valor;

            bindingNavigatorAddNewItem.Enabled = valor;
            bindingNavigatorDeleteItem.Enabled = valor;
            toolStripButton1Cancelar.Visible = !valor;
      
        }

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            if (idTextBox.Text != "")
            
            {
                var resultado = MessageBox.Show("Desea eliminar este registro", "Eliminar", MessageBoxButtons.YesNo);
                if (resultado == DialogResult.Yes)
                    if (idTextBox.Text != "")
                {
                    var id = Convert.ToInt32(idTextBox.Text);
                    Eliminar(id);


                }
            }
            
        }

        private void Eliminar(int id)
        {
            var resultado = _productos.EliminarProducto(id);

            if (resultado == true)
            {
                listaProductosBindingSource.ResetBindings(false);
            }
            else
            {
                MessageBox.Show("Ocurrio un error al eliminar el producto");
            }
           
        }

        private void idTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var producto = (Producto)listaProductosBindingSource.Current;

            if (producto != null)
            {
                openFileDialog1.ShowDialog();
                var archivo = openFileDialog1.FileName;

                if (archivo != "")
                {
                    var fileInfo = new FileInfo(archivo);
                    var filestream = fileInfo.OpenRead();

                    fotoPictureBox.Image = Image.FromStream(filestream);
                }
            }
            else
            {
                MessageBox.Show("CREE UN PRODUCTO ANTES DE ASIGNARLE UNA IMAGEN");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            fotoPictureBox.Image = null;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
