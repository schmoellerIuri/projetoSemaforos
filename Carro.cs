using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProjetoSemaforos
{
    public class Carro
    {
        public int Id { get; set; }

        public string scuderia { get; set; }

        public int qtdVoltas { get; set; }

        public List<double> tempoVoltas { get; set; }

        public int ciclosRealizados { get; set; }

        public double tempoTotalEspera { get; set; }

        public double tempoTotalTreino { get; set; }

        public bool isInBoxes { get; set; }

        public bool startedCicle { get; set; }

        private static Semaphore semaforo = null;

        public Mutex mutex = new Mutex();

        public const int tempoMin = 1000;

        public const int tempoMax = 1400;
        
        public Carro(int id, string scuderia) {
            Id = id;
            this.scuderia = scuderia;
            qtdVoltas = 0;
            tempoVoltas = new List<double>();
            ciclosRealizados = 0;
            tempoTotalTreino = 0;
            isInBoxes = true;
            startedCicle = false;
        }

        private void InicializaSemaforo()
        {
            try
            {
                semaforo = Semaphore.OpenExisting("ProjetoSemaforos");
            }
            catch
            {
                semaforo = new Semaphore(5, 5, "ProjetoSemaforos");
            }
        }

        public void RealizaCicloDeVolta()
        {
            InicializaSemaforo();

            semaforo.WaitOne();

            if (ciclosRealizados == 2) return;

            Random rd = new Random();
            int time = rd.Next(100, 500);
            tempoTotalEspera += time;
            tempoTotalTreino += time;
            Thread.Sleep(time);

            isInBoxes = false;

            int voltas = rd.Next(4, 12);

            var tempoVolta = double.Parse(rd.Next(tempoMin, tempoMax).ToString());
            bool first = true;
            for (int i = 0; i < voltas; i++)
            {
                if (first)
                    first = false;
                else
                {
                    if (rd.Next(2) == 0)
                    {
                        tempoVolta -= (0.1 * tempoVolta);
                        tempoVolta = tempoVolta <= 1000 ? 1000 : tempoVolta;
                    }
                    else
                    {
                        tempoVolta += (0.1 * tempoVolta);
                        tempoVolta = tempoVolta >= 1400 ? 1400 : tempoVolta;
                    }
                }
                qtdVoltas++;
                tempoVoltas.Add(Math.Round(tempoVolta, 2));
                tempoTotalTreino += tempoVolta;
                tempoTotalTreino = Math.Round(tempoTotalTreino, 2);
                Thread.Sleep((int)tempoVolta);
            }

            isInBoxes = true;
            startedCicle = false;
            ciclosRealizados++;

            tempoTotalTreino = Math.Round(tempoTotalTreino,2);

            semaforo.Release();
        }
    }
}
