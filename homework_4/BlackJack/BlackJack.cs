namespace Game
{
    internal class BlackJack
    {
        Deck deck = new Deck();
        Player user = new Player();
        Player computer = new Player();
        List<string> statistics = new List<string>();
        Player player1;
        Player player2;

        int currentIndex = 4;
        bool userStopped = false;
        bool computerStopped = false;

        public void Play()
        {
            Console.Clear();

            deck.OverhandShuffle();
            Card[] cards = deck.cards;

            Team team = ChoosePlayer();
            bool isUser = team.Equals(Team.User);

            player1 = isUser ? user : computer;
            player2 = isUser ? computer : user;

            player1.AddCard(cards[0]);
            player1.AddCard(cards[1]);
            player2.AddCard(cards[2]);
            player2.AddCard(cards[3]);

            while (!(userStopped && computerStopped))
            {
                if (isUser)
                {
                    PlayUser(player1, cards);
                    PlayComputer(player2, cards);
                } else
                {
                    PlayComputer(player1, cards);
                    PlayUser(player2, cards);
                }
            }

            ShowResult(player1, player2, isUser);

            if (PlayAgain() == Again.Yes) {
                Reset(); 
                Play();
            } else
            {
                ShowStatisctics();
            }
        }
        private void Reset()
        {
            user.Clear();
            computer.Clear();

            userStopped = false;
            computerStopped = false;
            currentIndex = 4;
        }
        private void ShowResult(Player player1, Player player2, bool isUser)
        {
            Console.WriteLine($"{(isUser ? "User" : "Computer")} - {player1.CountPoints()} points!");
            Console.WriteLine($"{(isUser ? "Computer" : "User")} - {player2.CountPoints()} points!");

            if (player1.CountPoints() <= 21 && player2.CountPoints() > 21)
            {
                Console.WriteLine($"{(isUser ? "User" : "Computer")} - Win!");
                statistics.Add($"{(isUser ? "User" : "Computer")} - Win!");
            }
            else if (player2.CountPoints() <= 21 && player1.CountPoints() > 21)
            {
                Console.WriteLine($"{(isUser ? "Computer" : "User")} - Win!");
                statistics.Add($"{(isUser ? "Computer" : "User")} - Win!");
            }
            else if (player1.CountPoints() > 21 && player2.CountPoints() > 21)
            {
                if (player1.CountPoints() < player2.CountPoints())
                {
                    Console.WriteLine($"{(isUser ? "User" : "Computer")} - Win!");
                    statistics.Add($"{(isUser ? "User" : "Computer")} - Win!");
                }
                else
                {
                    Console.WriteLine($"{(isUser ? "Computer" : "User")} - Win!");
                    statistics.Add($"{(isUser ? "Computer" : "User")} - Win!");
                }
            }
            else if (player1.CountPoints() > player2.CountPoints())
            {
                Console.WriteLine($"{(isUser ? "User" : "Computer")} - Win!");
                statistics.Add($"{(isUser ? "User" : "Computer")} - Win!");
            }
            else if (player1.CountPoints() < player2.CountPoints())
            {
                Console.WriteLine($"{(isUser ? "Computer" : "User")} - Win!");
                statistics.Add($"{(isUser ? "Computer" : "User")} - Win!");
            }
            else
            {
                Console.WriteLine("Draw!");
                statistics.Add($"Draw!");
            }
            Console.ReadLine();
        }
        private void PlayUser(Player player, Card[] cards)
        {
            Console.WriteLine($"You have {player.CountPoints()} points!");
            if (!userStopped && player.CountPoints() < 20)
            {
                Command input = ReadCommand();

                switch (input)
                {
                    case Command.Get:
                        player.AddCard(cards[currentIndex]);
                        currentIndex++;
                        break;
                    case Command.Stop:
                        userStopped = true;
                        break;
                    default:
                        Console.WriteLine("Invalid option");
                        PlayUser(player, cards);
                        break;
                }
            } else
            {
                userStopped = true;
            }
        }
        private void PlayComputer(Player player, Card[] cards)
        {
            if (player.CountPoints() > 17)
            {
                Console.WriteLine("Computer passed!");
                computerStopped = true;
            } else
            {
                player.AddCard(cards[currentIndex]);
                currentIndex++;
            }
        } 
        private void ShowStatisctics()
        {
            Console.WriteLine("--------------------------");
            foreach (string item in statistics)
            {
                Console.WriteLine(item);
                Console.WriteLine("--------------------------");
            }
            Console.ReadLine();
        }
        private static Command ReadCommand()
        {
            Console.Write("Enter command 1(Get) or 2(Stop):");
            string input = Console.ReadLine();
            bool succesfullParsing = Enum.TryParse<Command>(input, out Command result);
            if (!succesfullParsing)
            {
                Console.Clear();
                Console.WriteLine("You enter wrong number");
                Console.WriteLine();
                return ReadCommand();
            }
            else if (result < Command.Get || result > Command.Stop)
            {
                Console.Clear();
                Console.WriteLine($"Please enter number 1 or 2");
                Console.WriteLine();
                return ReadCommand();
            }
            return result;

        }
        private static Team ChoosePlayer()
        {
            Console.Write("Who will start 1(User) or 2(Computer):");
            string input = Console.ReadLine();
            bool succesfullParsing = Enum.TryParse<Team>(input, out Team result);
            if (!succesfullParsing)
            {
                Console.Clear();
                Console.WriteLine("You enter wrong number");
                Console.WriteLine();
                return ChoosePlayer();
            }
            else if (result < Team.User || result > Team.Computer)
            {
                Console.Clear();
                Console.WriteLine($"Please enter number 1 or 2");
                Console.WriteLine();
                return ChoosePlayer();
            }
            return result;

        }private static Again PlayAgain()
        {
            Console.Write("Do you want play again? 1(Yes), 2(No):");
            string input = Console.ReadLine();
            bool succesfullParsing = Enum.TryParse<Again>(input, out Again result);
            if (!succesfullParsing)
            {
                Console.Clear();
                Console.WriteLine("You enter wrong number");
                Console.WriteLine();
                return PlayAgain();
            }
            else if (result < Again.Yes || result > Again.No)
            {
                Console.Clear();
                Console.WriteLine($"Please enter number 1 or 2");
                Console.WriteLine();
                return PlayAgain();
            }
            return result;

        }
    }
}
