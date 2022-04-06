using System;
using System.Collections.Generic;

namespace lfa_trabalhopratico1_20221
{
    public class AutomatoFND
    {
        public AutomatoFND(List<string> alfabeto, List<string> transicoes)
        {
            Estados = new List<EstadoFND>();
            EstadosFinais = new List<EstadoFND>();
            Alfabeto = new List<string>();
            Transicoes = new List<string>();

            foreach (var entrada in alfabeto)
            {
                Alfabeto.Add(entrada);
            }

            foreach (var transicao in transicoes)
            {
                Transicoes.Add(transicao);
            }
        }

        public List<EstadoFND> Estados { get; set; }
        public EstadoFND EstadoInicial { get; set; }
        public List<EstadoFND> EstadosFinais { get; set; }
        public List<string> Alfabeto { get; set; }
        public List<string> Transicoes { get; set; }

        public void ConverterParaAFD()
        {
            var tabela = new List<EstadoFND>[Estados.Count, Alfabeto.Count];

            for (int e = 0; e < Estados.Count; e++)
            {
                for (int l = 0; l < Alfabeto.Count; l++)
                {
                    var estado = Estados[e];
                    var letra = Alfabeto[l];

                    var index = estado.Entrada.IndexOf(letra);

                    if (index != -1)
                    {
                        tabela[e, l] = estado.ProximoEstado[index];
                    }
                }
            }
        }
    }

    public class EstadoFND
    {
        public EstadoFND(string nome)
        {
            Nome = nome;
            ProximoEstado = new List<List<EstadoFND>>();
            Entrada = new List<string>();
        }

        public string Nome { get; set; }
        public List<List<EstadoFND>> ProximoEstado { get; set; }
        public List<string> Entrada { get; set; }
    }
}
