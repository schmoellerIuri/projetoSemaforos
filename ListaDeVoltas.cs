using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ProjetoSemaforos
{
    public partial class ListaDeVoltas : Form
    {
        public ListaDeVoltas(Carro c)
        {
            InitializeComponent();

            List <string> lista = new List<string>();
            c.tempoVoltas.ForEach(t => lista.Add(t.ToString()));

            lista.ForEach(l => lv_carro.Items.Add(new ListViewItem(l)));
           
        }
    }
}
