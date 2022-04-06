using System;
using System.Collections.Generic;
using System.Linq;

namespace lfa_trabalhopratico1_20221
{
    public class Automato
    {
        public List<string> States { get; set; }
        public List<string> Initial { get; set; }
        public List<string> Accepting { get; set; }
        public List<string> Alphabet { get; set; }
        public List<string> Transitions { get; set; }
        public List<string> TransitionsActualState {  get; private set; }
        public List<string> TransitionsInput { get; private set; }
        public List<string[]> TransitionsFutureState { get; private set; }

        public AutomatoFD AutomatoFD { get; set; }
        public AutomatoFND AutomatoFND { get; set; }

        public Automato(string[] linhas)
        {
            InicializarListas();
            PopularPropriedades(linhas);
            OrganizarTransicoes();
            CriarAutomatoEspecializado();
        }

        public void CriarAutomatoEspecializado()
        {
            var tipo = DeterminarTipo();
            if (tipo == "DFA")
            {
                CriarAutomatoFD();
            }
            if (tipo == "NFA")
            {
                CriarAutomatoFND();
            }
        }

        public void CriarAutomatoFD()
        {
            AutomatoFD = new AutomatoFD(Alphabet, Transitions);
            foreach (var state in States)
            {
                var estado = new EstadoFD(state);
                AutomatoFD.Estados.Add(estado);
                if (Initial.Contains(state)) { AutomatoFD.EstadoInicial = estado; }
                if (Accepting.Contains(state)) { AutomatoFD.EstadosFinais.Add(estado); }
            }

            foreach (var estado in AutomatoFD.Estados)
            {
                for (var i = 0; i < Transitions.Count; i++)
                {
                    if (estado.Nome == TransitionsActualState[i])
                    {
                        estado.Entrada.Add(TransitionsInput[i]);
                        var futureState = TransitionsFutureState[i][0];
                        estado.ProximoEstado.Add(AutomatoFD.Estados.Find(e => e.Nome == futureState));
                    }
                }
            }
        }

        public void CriarAutomatoFND()
        {
            AutomatoFND = new AutomatoFND(Alphabet, Transitions);
            foreach (var stateName in States)
            {
                var estado = new EstadoFND(stateName);
                AutomatoFND.Estados.Add(estado);
                if (Initial.Contains(stateName)) { AutomatoFND.EstadoInicial = estado; }
                if (Accepting.Contains(stateName)) { AutomatoFND.EstadosFinais.Add(estado); }
            }

            foreach (var automatoEstado in AutomatoFND.Estados)
            {
                for (var i = 0; i < Transitions.Count; i++)
                {
                    if (automatoEstado.Nome == TransitionsActualState[i])
                    {
                        automatoEstado.Entrada.Add(TransitionsInput[i]);
                        var estadosFuturos = new List<EstadoFND>();
                        foreach(var state in TransitionsFutureState[i])
                        {
                            estadosFuturos.Add(AutomatoFND.Estados.Find(e => e.Nome == state));
                        }
                        automatoEstado.ProximoEstado.Add(estadosFuturos);
                    }
                }
            }
        }

        public void InicializarListas()
        {
            States = new List<string>();
            Initial = new List<string>();
            Accepting = new List<string>();
            Alphabet = new List<string>();
            Transitions = new List<string>();
            TransitionsActualState = new List<string>();
            TransitionsInput = new List<string>();
            TransitionsFutureState = new List<string[]>();
        }

        public void PopularPropriedades(string[] linhas)
        {
            var comando = "comeco";
            foreach (string linha in linhas)
            {
                if (linha == "#states" || linha == "#initial" || linha == "#accepting" || linha == "#alphabet" || linha == "#transitions")
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
            Console.WriteLine("#states");
            foreach(var state in States)
            {
                Console.WriteLine(state);
            }

            Console.WriteLine("#initial");
            foreach (var initial in Initial)
            {
                Console.WriteLine(initial);
            }

            Console.WriteLine("#accepting");
            foreach (var accepting in Accepting)
            {
                Console.WriteLine(accepting);
            }

            Console.WriteLine("#alphabet");
            foreach (var alphabet in Alphabet)
            {
                Console.WriteLine(alphabet);
            }

            Console.WriteLine("#transitions");
            foreach (var transitions in Transitions)
            {
                Console.WriteLine(transitions);
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

        public void OrganizarTransicoes()
        {
            char[] delimitadores = { ':', '>' };
            foreach(var transition in Transitions)
            {
                var divisao = transition.Split(delimitadores);
                TransitionsActualState.Add(divisao[0]);
                TransitionsInput.Add(divisao[1]);
                TransitionsFutureState.Add(divisao[2].Split(','));
            }
        }
    }
}
