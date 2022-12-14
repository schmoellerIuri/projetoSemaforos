using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace ProjetoSemaforos
{
    public partial class Interface : Form
    {
        
        public Stopwatch watch = new Stopwatch();
        public static System.Timers.Timer timer;
        private List<Thread> threadsEmExecucao = new List<Thread>();

        private double tempoTotalDeTreino = 0;

        private List<Carro> carros = new List<Carro>();

        bool finished = false;

        public Interface()
        {
            InitializeComponent();

            InicializaBoxes();

            carroBindingSource.DataSource = carros.Where(x => x.isInBoxes).ToList();
            carroBindingSource1.DataSource = carros.Where(x => !x.isInBoxes).ToList();
        }

        private void AtualizaDataSource() 
        {

            carroBindingSource.DataSource = null;
            carroBindingSource1.DataSource = null;
            carroBindingSource.DataSource = carros.Where(x => x.isInBoxes).ToList();
            carroBindingSource1.DataSource = carros.Where(x => !x.isInBoxes).ToList();

            dataGridView1.Refresh();
            dataGridView2.Refresh();
        }

        private void InicializaBoxes()
        {
            for (int i = 0; i < 6; i++)
            {
                string numeroScuderia = (i + 1).ToString();
                carros.Add(new Carro(i + (i + 1), "E" + numeroScuderia));
                carros.Add(new Carro(i + (i + 2), "E" + numeroScuderia));
            }
        }

        private void ResetButton_Click(object sender, EventArgs e)
        {
            finished = false;
            carros.Clear();
            iniciarButton.Enabled = true;
            watch.Reset();
            InicializaBoxes();
            AtualizaDataSource();
        }

        private void SetTimer() 
        {
            timer = new System.Timers.Timer(60000);

            timer.Elapsed += OnTimedEvent;
            timer.AutoReset = true;
            timer.Enabled = true;
        }

        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            watch.Stop();
            tempoTotalDeTreino = watch.Elapsed.TotalSeconds;
            threadsEmExecucao.ForEach(th => th.Abort());
            
            if (!finished)
            {
                string message = string.Format("Treino finalizado com {0} segundos", Math.Round(tempoTotalDeTreino,2));
                MessageBox.Show(message, "Treino Finalizado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finished = true;
        }

        private void iniciarButton_Click(object sender, EventArgs e)
        {
            RealizarTreino();
        }

        void RealizarTreino()
        {
            iniciarButton.Enabled = false;
            SetTimer();
            watch.Start();
            int i = 0;
            while (watch.Elapsed.TotalSeconds < 60 && carros.Any(x => x.ciclosRealizados < 2))
            {
                AtualizaDataSource();

                Carro atual = carros.ElementAt(i);

                if (atual.ciclosRealizados < 2 && ScuderiaNaoPossuiCarrosNaPista(atual) && !atual.startedCicle)
                {
                    var threadCicloVolta = new Thread(new ThreadStart(atual.RealizaCicloDeVolta));
                    atual.startedCicle = true;
                    threadsEmExecucao.Add(threadCicloVolta);
                    threadCicloVolta.Start();
                }
                i = (i + 1) % 12;
            }

            threadsEmExecucao.ForEach(th => th.Join());
            timer.Stop();
            timer.Dispose();

            if (watch.IsRunning)
                watch.Stop();

            AtualizaDataSource();

            tempoTotalDeTreino = watch.Elapsed.TotalSeconds;

            if (!finished)
            {
                string message = string.Format("Treino finalizado com {0} segundos", Math.Round(tempoTotalDeTreino, 2));
                MessageBox.Show(message, "Treino Finalizado",MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            
        }

        bool ScuderiaNaoPossuiCarrosNaPista(Carro c)
        {
            return carros.Where(x => x.scuderia == c.scuderia && !x.startedCicle).ToList().Count() == 2;
        }

        private void dataGridView1_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (carros.Where(x => x.isInBoxes).Count() < 12) return;

            Carro c = carros.ElementAt(e.RowIndex);
            ListaDeVoltas form = new ListaDeVoltas(c);
            form.Show();
        }
    }
}
