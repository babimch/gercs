using System;
using System.Collections.Generic;

class Jogador
{
    public string Nome { get; set; }
    public string Nickname { get; set; }
    public int Pontos { get; private set; }

    public Jogador(string nome, string nickname)
    {
        Nome = nome;
        Nickname = nickname;
        Pontos = 0;
    }

    public void Jogar()
    {
        Random random = new Random();
        int pontosDaPartida = random.Next(1, 101);
        Pontos += pontosDaPartida;
    }
}

class Equipe
{
    public string NomeEquipe { get; set; }
    public List<Jogador> Jogadores { get; private set; }

    public Equipe(string nomeEquipe)
    {
        NomeEquipe = nomeEquipe;
        Jogadores = new List<Jogador>();
    }

    public int PontosTotal()
    {
        int totalPontos = 0;
        foreach (var jogador in Jogadores)
        {
            totalPontos += jogador.Pontos;
        }
        return totalPontos;
    }

    public void AdicionarJogador(Jogador jogador)
    {
        if (Jogadores.Count >= 5)
        {
            Console.WriteLine("A equipe já tem 5 jogadores. Não é possível adicionar mais jogadores.");
        }
        else if (Jogadores.Contains(jogador))
        {
            Console.WriteLine($"{jogador.Nome} já faz parte da equipe {NomeEquipe}.");
        }
        else
        {
            Jogadores.Add(jogador);
            Console.WriteLine($"{jogador.Nome} foi adicionado à equipe {NomeEquipe}.");
        }
    }
}

class Campeonato
{
    public string NomeCampeonato { get; set; }
    public List<Equipe> EquipesParticipantes { get; private set; }

    public Campeonato(string nomeCampeonato)
    {
        NomeCampeonato = nomeCampeonato;
        EquipesParticipantes = new List<Equipe>();
    }

    public void IniciarPartida(Equipe e1, Equipe e2)
    {
        if (e1.Jogadores.Count < 5 || e2.Jogadores.Count < 5)
        {
            Console.WriteLine("Cada equipe deve ter 5 jogadores para iniciar uma partida.");
            return;
        }

        Console.WriteLine($"Iniciando partida entre {e1.NomeEquipe} e {e2.NomeEquipe}.");
        foreach (var jogador in e1.Jogadores)
        {
            jogador.Jogar();
        }
        foreach (var jogador in e2.Jogadores)
        {
            jogador.Jogar();
        }
    }

    public void Classificacao()
    {
        Console.WriteLine("Classificação do Campeonato:");
        EquipesParticipantes.Sort((e1, e2) => e2.PontosTotal().CompareTo(e1.PontosTotal()));
        int posicao = 1;
        foreach (var equipe in EquipesParticipantes)
        {
            Console.WriteLine($"{posicao}. {equipe.NomeEquipe} - Pontos: {equipe.PontosTotal()}");
            posicao++;
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        Campeonato campeonato = new Campeonato("Campeonato de Jogos");
        List<Equipe> equipes = new List<Equipe>();

        while (true)
        {
            Console.WriteLine("Menu:");
            Console.WriteLine("1. Criar Equipe");
            Console.WriteLine("2. Adicionar Jogador a Equipe");
            Console.WriteLine("3. Iniciar Partida");
            Console.WriteLine("4. Mostrar Classificação");
            Console.WriteLine("5. Sair");
            Console.Write("Escolha uma opção: ");

            if (int.TryParse(Console.ReadLine(), out int escolha))
            {
                switch (escolha)
                {
                    case 1:
                        Console.Write("Nome da Equipe: ");
                        string nomeEquipe = Console.ReadLine();
                        equipes.Add(new Equipe(nomeEquipe));
                        Console.WriteLine("Equipe criada com sucesso.");
                        break;
                    case 2:
                        if (equipes.Count == 0)
                        {
                            Console.WriteLine("Crie uma equipe primeiro.");
                            break;
                        }

                        Console.WriteLine("Escolha uma equipe:");
                        for (int i = 0; i < equipes.Count; i++)
                        {
                            Console.WriteLine($"{i + 1}. {equipes[i].NomeEquipe}");
                        }
                        if (int.TryParse(Console.ReadLine(), out int equipeEscolhida) && equipeEscolhida >= 1 && equipeEscolhida <= equipes.Count)
                        {
                            Console.Write("Nome do Jogador: ");
                            string nomeJogador = Console.ReadLine();
                            Console.Write("Nickname do Jogador: ");
                            string nicknameJogador = Console.ReadLine();
                            equipes[equipeEscolhida - 1].AdicionarJogador(new Jogador(nomeJogador, nicknameJogador));
                        }
                        else
                        {
                            Console.WriteLine("Escolha de equipe inválida.");
                        }
                        break;
                    case 3:
                        if (equipes.Count < 2)
                        {
                            Console.WriteLine("Crie pelo menos duas equipes antes de iniciar uma partida.");
                        }
                        else
                        {
                            Console.WriteLine("Escolha duas equipes para a partida:");
                            for (int i = 0; i < equipes.Count; i++)
                            {
                                Console.WriteLine($"{i + 1}. {equipes[i].NomeEquipe}");
                            }

                            int equipe1, equipe2;
                            if (int.TryParse(Console.ReadLine(), out equipe1) && equipe1 >= 1 && equipe1 <= equipes.Count)
                            {
                                Console.WriteLine("Escolha a segunda equipe:");
                                for (int j = 0; j < equipes.Count; j++)
                                {
                                    if (j != equipe1 - 1) // Irá evitar a seleção da mesma equipe
                                    {
                                        Console.WriteLine($"{j + 1}. {equipes[j].NomeEquipe}");
                                    }
                                }

                                if (int.TryParse(Console.ReadLine(), out equipe2) && equipe2 >= 1 && equipe2 <= equipes.Count && equipe2 != equipe1)
                                {
                                    campeonato.IniciarPartida(equipes[equipe1 - 1], equipes[equipe2 - 1]);
                                    Console.WriteLine("Partida iniciada.");
                                }
                                else
                                {
                                    Console.WriteLine("Escolha da segunda equipe inválida.");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Escolha da primeira equipe inválida.");
                            }
                        }
                        break;
                    case 4:
                        campeonato.Classificacao();
                        break;
                    case 5:
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Opção inválida.");
                        break;
                }
            }
           
            else
            {
                Console.WriteLine("Opção inválida.");
            }
        }
    }
}