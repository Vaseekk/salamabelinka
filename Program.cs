using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using Spectre.Console; // Je potřeba stáhnout NuGet package

namespace SnakeGame
{
    class Program
    {
        static int stoneCount = 9; // Počet překážek (kamenů) na hracím poli
        static int fieldSize = 30; // Velikost hracího pole bude 30x30
        static string scoreFilePath = "scores.txt"; // Soubor pro ukládání skóre

        static void Main(string[] args)
        {
            // Inicializace hadů (každý had má jinou startovací pozici)
            List<Position> snake1 = new List<Position> { new Position(fieldSize / 4, fieldSize / 2) };
            List<Position> snake2 = new List<Position> { new Position(3 * fieldSize / 4, fieldSize / 2) };
            Position direction1 = new Position(1, 0); // Počáteční směr hada 1
            Position direction2 = new Position(-1, 0); // Počáteční směr hada 2

            // Vytvoření jídla a kamenů na hracím poli
            Position food = CreateFood(fieldSize, snake1, snake2);
            List<Position> stones = CreateStones(fieldSize, stoneCount, snake1, snake2, food);

            int score1 = 0; // Skóre hráče 1
            int score2 = 0; // Skóre hráče 2

            int baseDelay = 300; //0,3 sekundy - zpoždění mezi jednotlivými iteracemi smyčky

            AnsiConsole.MarkupLine("[bold]Console window on full screen recommended![/] ");
            AnsiConsole.MarkupLine("[bold]Press enter to continue:[/] ");
            Console.ReadLine();
            Console.Clear();

            // Vytvoření počáteční tabulky hracího pole
            var table = CreateInitialTable(fieldSize).Centered();

            bool gameRunning = true;

            // Použití AnsiConsole.Live pro aktualizaci obrazovky v reálném čase (jinak se hrací pole strašně seká a není to koukatelné)
            AnsiConsole.Live(table).Start(ctx =>
            {
                while (gameRunning)
                {
                    // Zkontrolujeme stisknuté klávesy pro změnu směru hadů
                    if (Console.KeyAvailable)
                    {
                        ConsoleKeyInfo key = Console.ReadKey(true);
                        if (key.Key == ConsoleKey.W && direction1.Y != 1)
                            direction1 = new Position(0, -1);
                        if (key.Key == ConsoleKey.S && direction1.Y != -1)
                            direction1 = new Position(0, 1);
                        if (key.Key == ConsoleKey.A && direction1.X != 1)
                            direction1 = new Position(-1, 0);
                        if (key.Key == ConsoleKey.D && direction1.X != -1)
                            direction1 = new Position(1, 0);

                        if (key.Key == ConsoleKey.UpArrow && direction2.Y != 1)
                            direction2 = new Position(0, -1);
                        if (key.Key == ConsoleKey.DownArrow && direction2.Y != -1)
                            direction2 = new Position(0, 1);
                        if (key.Key == ConsoleKey.LeftArrow && direction2.X != 1)
                            direction2 = new Position(-1, 0);
                        if (key.Key == ConsoleKey.RightArrow && direction2.X != -1)
                            direction2 = new Position(1, 0);
                    }

                    // Výpočet nových pozic hlav hadů
                    Position newHead1 = new Position(snake1.First().X + direction1.X, snake1.First().Y + direction1.Y);
                    Position newHead2 = new Position(snake2.First().X + direction2.X, snake2.First().Y + direction2.Y);

                    // Kontrola kolizí hadů s hranicemi pole, sebou navzájem, nebo překážkami
                    if (newHead1.X < 0 || newHead1.X >= fieldSize || newHead1.Y < 0 || newHead1.Y >= fieldSize ||
                        snake1.Contains(newHead1) || snake2.Contains(newHead1) || stones.Contains(newHead1) ||
                        newHead2.X < 0 || newHead2.X >= fieldSize || newHead2.Y < 0 || newHead2.Y >= fieldSize ||
                        snake2.Contains(newHead2) || snake1.Contains(newHead2) || stones.Contains(newHead2))
                    {
                        gameRunning = false; // Konec hry při kolizi
                        break;
                    }

                    // Pokud had sežere jídlo, zvýšíme skóre a vytvoříme nové jídlo
                    if (newHead1.Equals(food))
                    {
                        score1++;
                        food = CreateFood(fieldSize, snake1, snake2);
                    }
                    else
                    {
                        snake1.RemoveAt(snake1.Count - 1); // Pokud jídlo není snědeno, had se zkrátí
                    }

                    if (newHead2.Equals(food))
                    {
                        score2++;
                        food = CreateFood(fieldSize, snake1, snake2);
                    }
                    else
                    {
                        snake2.RemoveAt(snake2.Count - 1); // Zkrácení hada 2, pokud nesnědl jídlo
                    }

                    // Přidání nových hlav hadů
                    snake1.Insert(0, newHead1);
                    snake2.Insert(0, newHead2);

                    // Aktualizace zobrazení tabulky
                    UpdateTable(table, snake1, snake2, food, stones);

                    // Osvěžení obrazovky
                    ctx.Refresh();

                    Thread.Sleep(baseDelay);
                }
            });

            // Vyčištění konzole a zobrazení endgameu
            AnsiConsole.Clear();
            Console.Clear();
            EndGame(score1, score2);
        }

        // Vytvoření počáteční tabulky
        static Table CreateInitialTable(int fieldSize)
        {
            int size = fieldSize + 2;
            var table = new Table();
            table.Border(TableBorder.None);

            for (int i = 0; i < size; i++)
            {
                table.AddColumn(new TableColumn(""));
            }

            return table;
        }

        // Aktualizace tabulky s pozicemi hadů, jídla a překážek
        static void UpdateTable(Table table, List<Position> snake1, List<Position> snake2, Position food, List<Position> stones)
        {
            int size = fieldSize + 2;
            table.Rows.Clear();

            for (int y = 0; y < size; y++)
            {
                var row = new List<string>();
                for (int x = 0; x < size; x++)
                {
                    if (x == 0 || x == size - 1 || y == 0 || y == size - 1)
                    {
                        row.Add("[blue]■[/]"); // Ohraničení hracího pole
                    }
                    else
                    {
                        var position = new Position(x - 1, y - 1);
                        if (snake1.Contains(position))
                        {
                            row.Add("[green]O[/]");
                        }
                        else if (snake2.Contains(position))
                        {
                            row.Add("[red]X[/]");
                        }
                        else if (food.Equals(position))
                        {
                            row.Add("[yellow]#[/]");
                        }
                        else if (stones.Contains(position))
                        {
                            row.Add("[gray]■[/]");
                        }
                        else
                        {
                            row.Add(" "); //Mezera v hracím poli
                        }
                    }
                }
                table.AddRow(row.ToArray());
            }
        }

        // Vytvoření překážek na náhodných pozicích
        static List<Position> CreateStones(int fieldSize, int count, List<Position> snake1, List<Position> snake2, Position food)
        {
            Random rand = new Random();
            List<Position> stones = new List<Position>();
            for (int i = 0; i < count; i++)
            {
                Position stone;
                do
                {
                    stone = new Position(rand.Next(0, fieldSize), rand.Next(0, fieldSize));
                }
                while (snake1.Contains(stone) || snake2.Contains(stone) || stones.Contains(stone) || stone.Equals(food));
                stones.Add(stone);
            }
            return stones;
        }

        // Vytvoření jídla na náhodné pozici
        static Position CreateFood(int fieldSize, List<Position> snake1, List<Position> snake2)
        {
            Random rand = new Random();
            Position food;
            do
            {
                food = new Position(rand.Next(0, fieldSize), rand.Next(0, fieldSize));
            }
            while (snake1.Contains(food) || snake2.Contains(food));
            return food;
        }

        // Závěrečná funkce pro ukončení hry a zobrazení skóre
        static void EndGame(int score1, int score2)
        {
            AnsiConsole.Clear();
            Console.Clear();

            AnsiConsole.MarkupLine("[bold][red]GAME OVER![/][/]");
            AnsiConsole.MarkupLine("");

            if (score1 > score2)
            {
                AnsiConsole.MarkupLine("[bold][yellow]SNAKE 1 (O) WINS![/][/]");
            }
            else if (score1 < score2)
            {
                AnsiConsole.MarkupLine("[bold][yellow]SNAKE 2 (X) WINS![/][/]");
            }
            else if (score1 == score2)
            {
                AnsiConsole.MarkupLine("[bold][yellow]IT'S A TIE![/][/]");
            }
            AnsiConsole.MarkupLine("");
            AnsiConsole.Markup("[bold]Enter name for Snake 1 (O):[/] ");
            string name1 = Console.ReadLine();
            AnsiConsole.Markup("[bold]Enter name for Snake 2 (X):[/] ");
            string name2 = Console.ReadLine();

            SaveScore(name1, score1);
            SaveScore(name2, score2);

            DisplayTopScores();

            AnsiConsole.MarkupLine("[bold]Press enter to exit...[/]");
            Console.ReadKey();
        }

        // Uložení skóre do souboru
        static void SaveScore(string playerName, int score)
        {
            using (StreamWriter writer = new StreamWriter(scoreFilePath, append: true))
            {
                writer.WriteLine($"{playerName}:{score}");
            }
        }

        // Načtení skóre ze souboru
        static List<ScoreEntry> LoadScores()
        {
            List<ScoreEntry> scores = new List<ScoreEntry>();

            if (File.Exists(scoreFilePath))
            {
                foreach (var line in File.ReadAllLines(scoreFilePath))
                {
                    var parts = line.Split(':');
                    if (parts.Length == 2 && int.TryParse(parts[1], out int score))
                    {
                        scores.Add(new ScoreEntry { PlayerName = parts[0], Score = score });
                    }
                }
            }
            return scores;
        }

        // Zobrazení 5 nejvyšších skóre
        static void DisplayTopScores()
        {
            AnsiConsole.Clear();
            Console.Clear();
            List<ScoreEntry> scores = LoadScores();
            var topScores = scores.OrderByDescending(s => s.Score).Take(5);

            AnsiConsole.MarkupLine("[bold underline]Top 5 Scores:[/]");
            foreach (var score in topScores)
            {
                AnsiConsole.MarkupLine($"[yellow]{score.PlayerName}[/]: {score.Score}");
            }
        }

        // Struktura pro pozice hadů a objektů na hracím poli
        struct Position
        {
            public int X { get; set; }
            public int Y { get; set; }

            public Position(int x, int y)
            {
                X = x;
                Y = y;
            }

            public override bool Equals(object obj)
            {
                if (obj is Position)
                {
                    Position other = (Position)obj;
                    return X == other.X && Y == other.Y;
                }
                return false;
            }

            public override int GetHashCode()
            {
                return X.GetHashCode() ^ Y.GetHashCode();
            }
        }

        // Třída pro uchovávání skóre jednotlivých hráčů
        class ScoreEntry
        {
            public string PlayerName { get; set; }
            public int Score { get; set; }
        }
    }
}
