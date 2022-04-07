using System;
using System.IO;

namespace lfa_trabalhopratico1_20221
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var automato = CriarAutomato();
            var tipo = automato.DeterminarTipo();

            ImprimirTipo(tipo);
            ImprimirDoArquivo(automato);

            if (tipo == "NFA") 
            {
                automato.AutomatoFD = automato.AutomatoFND.ConverterParaAFD();
                ImprimirAutomatoConvertido(automato);
            }

            while (true)
            {
                Console.Write("\nInsira uma palavra para teste: ");
                var palavra = Console.ReadLine();
                automato.AutomatoFD.LerPalavra(palavra);
            }
        }

        public static Automato CriarAutomato()
        {
            var caminho = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            var linhas = File.ReadAllLines($@"{caminho}\automato.txt");
            return new Automato(linhas);
        }

        public static void ImprimirTipo(string tipo)
        {
            Console.Write("O autômato é um ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"{tipo}\n");
            Console.ResetColor();
        }

        public static void ImprimirDoArquivo(Automato automato)
        {
            Console.Write("Imprimindo autômato do aquivo ");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("automato.txt");
            Console.ResetColor();
            Console.WriteLine("...");
            Console.ForegroundColor = ConsoleColor.Blue;
            automato.Imprimir();
            Console.WriteLine();
            Console.ResetColor();
        }

        public static void ImprimirAutomatoConvertido(Automato automato)
        {
            Console.Write("\n\nImprimindo NFA convertido para DFA");
            Console.WriteLine("...");
            Console.ForegroundColor = ConsoleColor.Blue;
            automato.AutomatoFD.Imprimir();
            Console.WriteLine();
            Console.ResetColor();
        }
    }
}
