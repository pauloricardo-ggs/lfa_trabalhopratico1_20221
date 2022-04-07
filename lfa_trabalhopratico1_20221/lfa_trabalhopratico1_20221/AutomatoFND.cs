using System;
using System.Collections.Generic;
using System.Linq;

namespace lfa_trabalhopratico1_20221
{
    public class AutomatoFND
    {
        public AutomatoFND(List<string> alfabeto, List<string> transicoes)
        {
            Estados = new List<EstadoFND>();
            EstadosFinais = new List<EstadoFND>();
            Alfabeto = new List<string>();

            foreach (var entrada in alfabeto)
            {
                Alfabeto.Add(entrada);
            }
        }

        public List<EstadoFND> Estados { get; set; }
        public EstadoFND EstadoInicial { get; set; }
        public List<EstadoFND> EstadosFinais { get; set; }
        public List<string> Alfabeto { get; set; }

        public void ConverterParaAFD()
        {
            var qtdLinhas = Estados.Count;
            var qtdColunas = Alfabeto.Count;
            var continuarLoop = true;

            ImprimirTabela(qtdLinhas, qtdColunas);

            while (continuarLoop)
            {
                var dimensaoTabela = TransformarEstadosMultiplosEmUmNovoEstado(qtdLinhas, qtdColunas);
                continuarLoop = false;
                qtdLinhas = dimensaoTabela[0];
                qtdColunas = dimensaoTabela[1];
                ImprimirTabela(qtdLinhas, qtdColunas);

                PopularNovosEstados();
                ImprimirTabela(qtdLinhas, qtdColunas);

                foreach (var estado in Estados)
                {
                    foreach (var caractere in Alfabeto)
                    {
                        if (estado.Transicoes.ContainsKey(caractere) && estado.Transicoes[caractere].Count > 1)
                        {
                            continuarLoop = true;
                        }
                    }
                }
            }
        }

        public void PopularNovosEstados()
        {
            foreach (var estado in Estados)
            {
                foreach (var caractere in Alfabeto)
                {
                    if (estado.EstadosCriadores.Any())
                    {
                        foreach (var criador in estado.EstadosCriadores)
                        {
                            if (criador.Transicoes.ContainsKey(caractere))
                            {
                                if (!estado.Transicoes.ContainsKey(caractere))
                                {
                                    var x = new List<EstadoFND>();
                                    foreach (var y in criador.Transicoes[caractere])
                                    {
                                        x.Add(y);
                                    }
                                    estado.Transicoes.Add(caractere, x);
                                }
                                else
                                {
                                    estado.Transicoes[caractere].AddRange(criador.Transicoes[caractere]);
                                }
                            }
                        }
                    }
                    if (estado.Transicoes.ContainsKey(caractere))
                    {
                        estado.Transicoes[caractere] = estado.Transicoes[caractere].OrderBy(e => e.Nome).ToList();
                    }
                }
                estado.EstadosCriadores.Clear();
            }
        }

        public int[] TransformarEstadosMultiplosEmUmNovoEstado(int qtdLinhas, int qtdColunas)
        {
            var estadosASeremAdicionados = new List<EstadoFND>();
            foreach (var estado in Estados)
            {
                var estadoFinal = false;
                foreach (var caractere in Alfabeto)
                {
                    if (estado.Transicoes.ContainsKey(caractere))
                    {
                        var proximosEstados = estado.Transicoes[caractere];
                        if (proximosEstados.Count > 1)
                        {
                            var nome = "";
                            var estadosCriadores = new List<EstadoFND>();
                            foreach (var proximoEstado in proximosEstados)
                            {
                                nome += proximoEstado.Nome;
                                if (EstadosFinais.Contains(proximoEstado))
                                {
                                    estadoFinal = true;
                                }
                                if (!estadosCriadores.Contains(proximoEstado))
                                {
                                    estadosCriadores.Add(proximoEstado);
                                }
                            }
                            var estadoComMesmoNome = Estados.Find(x => x.Nome == nome);
                            if (estadoComMesmoNome == null)
                            {
                                qtdLinhas++;
                                var estadoNovo = new EstadoFND(nome);
                                estadoNovo.EstadosCriadores.AddRange(estadosCriadores);
                                estadosASeremAdicionados.Add(estadoNovo);
                                estado.Transicoes[caractere].Clear();
                                estado.Transicoes[caractere].Add(estadoNovo);
                                if (estadoFinal)
                                {
                                    EstadosFinais.Add(estadoNovo);
                                }
                            }
                            else
                            {
                                estado.Transicoes[caractere].Clear();
                                estado.Transicoes[caractere].Add(estadoComMesmoNome);
                            }
                        }
                    }
                }
            }
            Estados.AddRange(estadosASeremAdicionados);

            var dimensaoTabela = new int[2] { qtdLinhas, qtdColunas };

            return dimensaoTabela;
        }

        public void ImprimirTabela(int qtdLinhas, int qtdColunas)
        {
            var espacamento = "\t\t\t";
            Console.Write($"\n{espacamento}");
            Console.ForegroundColor = ConsoleColor.Green;
            foreach (var l in Alfabeto)
            {
                Console.Write(l + espacamento);
            }
            Console.ResetColor();
            Console.WriteLine();
            foreach (var estado in Estados)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(estado.Nome + espacamento);
                Console.ResetColor();
                foreach (var caractere in Alfabeto)
                {
                    if (estado.Transicoes.ContainsKey(caractere))
                    {
                        var nome = "";
                        foreach (var value in estado.Transicoes[caractere])
                        {
                            nome += value.Nome + ",";
                        }
                        Console.Write(nome + espacamento);
                    }
                    else
                    {
                        Console.Write($"-{espacamento}");
                    }
                }
                Console.WriteLine();
            }
        }
    }

    public class EstadoFND
    {
        public EstadoFND(string nome)
        {
            Nome = nome;
            Transicoes = new Dictionary<string, List<EstadoFND>>();
            EstadosCriadores = new List<EstadoFND>();
        }

        public string Nome { get; set; }
        public Dictionary<string, List<EstadoFND>> Transicoes { get; set; }
        public List<EstadoFND> EstadosCriadores { get; set; }
    }
}
