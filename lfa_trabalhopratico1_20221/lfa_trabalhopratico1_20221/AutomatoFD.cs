using System;
using System.Collections.Generic;

namespace lfa_trabalhopratico1_20221
{
    public class AutomatoFD
    {
        public AutomatoFD(List<string> alfabeto, List<string> transicoes)
        {
            Estados = new List<Estado>();
            EstadosFinais = new List<Estado>();
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

        public List<Estado> Estados { get; set; }
        public Estado EstadoInicial { get; set; }
        public List<Estado> EstadosFinais { get; set; }
        public List<string> Alfabeto { get; set; }
        public List<string> Transicoes { get; set; }

        public void Imprimir()
        {
            Console.WriteLine("#states");
            foreach (var estado in Estados)
            {
                Console.WriteLine(estado.Nome);
            }

            Console.WriteLine("#initial");
            Console.WriteLine(EstadoInicial.Nome);

            Console.WriteLine("#accepting");
            foreach (var estadoFinal in EstadosFinais)
            {
                Console.WriteLine(estadoFinal.Nome);
            }

            Console.WriteLine("#alphabet");
            foreach (var alfabeto in Alfabeto)
            {
                Console.WriteLine(alfabeto);
            }

            Console.WriteLine("#transitions");
            foreach (var transicao in Transicoes)
            {
                Console.WriteLine(transicao);
            }
        }
    }

    public class Estado
    {
        public Estado(string nome)
        {
            Nome = nome;
            ProximoEstado = new List<Estado>();
            Entrada = new List<string>();
        }

        public string Nome { get; set; }
        public List<Estado> ProximoEstado { get; set; }
        public List<string> Entrada { get; set; }
    }
}
