namespace Lotarias.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    using DevExpress.Mvvm.DataAnnotations;
    using DevExpress.Mvvm.Native;

    [POCOViewModel]
    public class MainWindowViewModel
    {
        private const int TotolotoMax = 49;
        private const int TotolotoNumeroSorteMax = 13;
        private const int EuroMax = 50;
        private const int EuroExtrelasMax = 12;

        private readonly Random _randomGenerator = new Random();

        public virtual bool IsTotoloto { get; set; }

        public virtual ObservableCollection<string> Items { get; set; } = new ObservableCollection<string>();

        public void Gerar()
        {
            this.Items.Clear();

            if (this.IsTotoloto)
            {
                Enumerable.Range(0, 10).ForEach(i => Items.Add(this.GerarAposta()));
                this.Items.Add("Número da sorte: \t" + this._randomGenerator.Next(1, TotolotoNumeroSorteMax + 1));
            }
            else
            {
                Enumerable.Range(0, 5).ForEach(i => Items.Add(this.GerarAposta()));
            }
        }

        private string GerarAposta()
        {
            string result = null;

            if (this.IsTotoloto)
            {
                var numeros = new List<int>();

                while (numeros.Count < 5)
                {
                    var randomNumber = this._randomGenerator.Next(1, TotolotoMax + 1);
                    if (!numeros.Contains(randomNumber))
                    {
                        numeros.Add(randomNumber);
                    }
                }

                numeros = numeros.OrderBy(x => x).ToList();
                result = string.Join("\t", numeros);
            }
            else
            {
                var numeros = new List<int>();
                var estrelas = new List<int>();

                while (numeros.Count < 5)
                {
                    var randomNumber = this._randomGenerator.Next(1, EuroMax + 1);
                    if (!numeros.Contains(randomNumber))
                    {
                        numeros.Add(randomNumber);
                    }
                }

                numeros = numeros.OrderBy(x => x).ToList();
                result = string.Join("\t", numeros);

                while (estrelas.Count < 2)
                {
                    var randomNumber = this._randomGenerator.Next(1, EuroExtrelasMax + 1);
                    if (!estrelas.Contains(randomNumber))
                    {
                        estrelas.Add(randomNumber);
                    }
                }

                estrelas = estrelas.OrderBy(x => x).ToList();
                result += "\t+\t" + string.Join("\t", estrelas);
            }

            return result;
        }
    }
}