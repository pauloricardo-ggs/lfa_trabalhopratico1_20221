using System;
using System.Collections.Generic;

namespace lfa_trabalhopratico1_20221
{
    public class AutomatoFD
    {
        public AutomatoFD(List<string> alfabeto, List<string> transicoes)
        {
            Estados = new List<EstadoFD>();
            EstadosFinais = new List<EstadoFD>();
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

        public List<EstadoFD> Estados { get; set; }
        public EstadoFD EstadoInicial { get; set; }
        public List<EstadoFD> EstadosFinais { get; set; }
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

        public void LerPalavra(string palavra)
        {
            if (!VerificarPalavra(palavra)) return;

            var estadoAtual = EstadoInicial;

            Console.Write("Estado inicial: '");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write($"{estadoAtual.Nome}");
            Console.ResetColor();
            Console.WriteLine("'");

            foreach(var letra in palavra)
            {
                if(estadoAtual != null)
                {
                    if (estadoAtual.Entrada.Contains(letra.ToString()))
                    {
                        var estadoAnterior = estadoAtual;
                        var index = estadoAtual.Entrada.IndexOf(letra.ToString());
                        estadoAtual = estadoAtual.ProximoEstado[index];

                        Console.Write("O estado '");
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write($"{estadoAnterior.Nome}");
                        Console.ResetColor();
                        Console.Write("' recebe como entrada o caractere '");
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write($"{letra}");
                        Console.ResetColor();
                        Console.Write("' e vai para o estado '");
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write($"{estadoAtual.Nome}");
                        Console.ResetColor();
                        Console.WriteLine($"'.");
                    }
                }
            }

            if (EstadosFinais.Contains(estadoAtual)) 
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"A palavra foi aceita e o estado final foi '{estadoAtual.Nome}'.\n"); 
                Console.ResetColor();
            }
            else 
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"A palavra não foi aceita e o estado final foi '{estadoAtual.Nome}'.\n");
                Console.ResetColor();
            }
        }

        public bool VerificarPalavra(string palavra)
        {
            if (string.IsNullOrEmpty(palavra))
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine($"A palavra testada não pode ser vazia.\n");
                Console.ResetColor();
                return false;
            }
            foreach (var letra in palavra)
            {
                if (!Alfabeto.Contains(letra.ToString()))
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine($"O caractere '{letra}' não faz parte do alfabeto.\n");
                    Console.ResetColor();
                    return false;
                }
            }

            return true;
        }
    }

    public class EstadoFD
    {
        public EstadoFD(string nome)
        {
            Nome = nome;
            ProximoEstado = new List<EstadoFD>();
            Entrada = new List<string>();
        }

        public string Nome { get; set; }
        public List<EstadoFD> ProximoEstado { get; set; }
        public List<string> Entrada { get; set; }
    }
}
