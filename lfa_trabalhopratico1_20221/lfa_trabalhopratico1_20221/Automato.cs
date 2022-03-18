using System;
using System.Collections.Generic;

namespace lfa_trabalhopratico1_20221
{
    public class Automato
    {
        public List<string> States { get; private set; }
        public List<string> Initial { get; private set; }
        public List<string> Accepting { get; private set; }
        public List<string> Alphabet { get; private set; }
        public List<string> Transitions { get; private set; }

        public Automato(string[] linhas)
        {
            States = new List<string>();
            Initial = new List<string>();
            Accepting = new List<string>();
            Alphabet = new List<string>();
            Transitions = new List<string>();

            var comando = "comeco";
            foreach(string linha in linhas)
            {
                if(linha == "#states" || linha == "#initial" || linha == "#accepting" || linha == "#alphabet" || linha == "#transitions")
                {
                    comando = linha;
                }
                else if (comando == "#states")
                {
                    States.Add(linha);
                }
                else if (comando == "#initial")
                {
                    Initial.Add(linha);
                }
                else if (comando == "#accepting")
                {
                    Accepting.Add(linha);
                }
                else if (comando == "#alphabet")
                {
                    Alphabet.Add(linha);
                }
                else if (comando == "#transitions")
                {
                    Transitions.Add(linha);
                }
            }
        }

        public void Imprimir()
        {
            Console.WriteLine("States");
            foreach(var state in States)
            {
                Console.WriteLine(" -" + state);
            }

            Console.WriteLine("Initial");
            foreach (var initial in Initial)
            {
                Console.WriteLine(" -" + initial);
            }

            Console.WriteLine("Accepting");
            foreach (var accepting in Accepting)
            {
                Console.WriteLine(" -" + accepting);
            }

            Console.WriteLine("Alphabet");
            foreach (var alphabet in Alphabet)
            {
                Console.WriteLine(" -" + alphabet);
            }

            Console.WriteLine("Transitions");
            foreach (var transitions in Transitions)
            {
                Console.WriteLine(" -" + transitions);
            }
        }

        public string DeterminarTipo()
        {
            var tipo = "DFA";

            foreach (var transition in Transitions)
            {
                if (transition.Contains("$"))
                {
                    tipo = "eNFA";
                    break;
                }
                if (transition.Contains(","))
                {
                    tipo = "NFA";
                }
            }

            return tipo;
        }
    }
}
