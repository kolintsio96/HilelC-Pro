namespace Game
{
    internal class BlackJack
    {
        PlayingCards playingCards = new PlayingCards();
        List<Card> user = new List<Card>();
        List<Card> computer = new List<Card>();
        List<string> statistics = new List<string>();
        List<Card> player1;
        List<Card> player2;

        int currentIndex = 4;
        bool userStopped = false;
        bool computerStopped = false;

        public void Play()
        {
            Console.Clear();

            playingCards.OverhandShuffle();
            Card[] cards = playingCards.GetCards();

            Team team = ChoosePlayer();
            bool isUser = team.Equals(Team.User);

            player1 = isUser ? user : computer;
            player2 = isUser ? computer : user;

            player1.Add(cards[0]);
            player1.Add(cards[1]);
            player2.Add(cards[2]);
            player2.Add(cards[3]);

            while (!userStopped && !computerStopped)
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
        private void ShowResult(List<Card> player1, List<Card> player2, bool isUser)
        {
            Console.WriteLine($"{(isUser ? "User" : "Computer")} - {CountPoints(player1)} points!");
            Console.WriteLine($"{(isUser ? "Computer" : "User")} - {CountPoints(player2)} points!");

            if (CountPoints(player1) <= 21 && CountPoints(player2) > 21)
            {
                Console.WriteLine($"{(isUser ? "User" : "Computer")} - Win!");
                statistics.Add($"{(isUser ? "User" : "Computer")} - Win!");
            }
            else if (CountPoints(player2) <= 21 && CountPoints(player1) > 21)
            {
                Console.WriteLine($"{(isUser ? "Computer" : "User")} - Win!");
                statistics.Add($"{(isUser ? "Computer" : "User")} - Win!");
            }
            else if (CountPoints(player1) > 21 && CountPoints(player2) > 21)
            {
                if (CountPoints(player1) < CountPoints(player2))
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
            else if (CountPoints(player1) > CountPoints(player2))
            {
                Console.WriteLine($"{(isUser ? "User" : "Computer")} - Win!");
                statistics.Add($"{(isUser ? "User" : "Computer")} - Win!");
            }
            else if (CountPoints(player1) < CountPoints(player2))
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
        private void PlayUser(List<Card> player, Card[] cards)
        {
            Console.WriteLine($"You have {CountPoints(player)} points!");
            if (!userStopped)
            {
                Command input = ReadCommand();

                switch (input)
                {
                    case Command.Get:
                        player.Add(cards[currentIndex]);
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
            }
        }
        private void PlayComputer(List<Card> player, Card[] cards)
        {
            if (CountPoints(player) > 17)
            {
                Console.WriteLine("Computer passed!");
                computerStopped = true;
            } else
            {
                player.Add(cards[currentIndex]);
                currentIndex++;
            }
        }
        private int CountPoints(List<Card> player)
        {
            int points = 0;
            foreach (Card card in player)
            {
                points += card.Point;
            }
            return points;
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
