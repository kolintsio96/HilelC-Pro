namespace Game
{
    internal class Deck
    {
        public Card[] cards = new Card[36];
       
        public void OverhandShuffle()
        {
            Random random = new Random();
            cards = cards.OrderBy(x => random.Next()).ToArray();
        }
        public void Sort()
        {
            cards = cards.OrderBy(card => card.Denomination).ToArray();
        }
        public void FindIndexOfCard(Denominations denomination)
        {
            for (int i = 0; i < cards.Length; i++)
            {
                if (cards[i].Denomination == denomination)
                {
                    Console.Write($"{i} ");
                }
            }
            Console.WriteLine();
        }
        public void SortBySuit(Suits suit)
        {
            cards = cards.OrderBy(card => card.Suit != suit).ToArray();
        }
        public void PrintCards()
        {
            for (int i = 0; i < cards.Length; i++)
            {
                Console.WriteLine($"{cards[i].Denomination} - {cards[i].Suit}");
            }
        }
        public Deck() {
            int suitLength = Enum.GetNames(typeof(Suits)).Length;
            int denominationLength = Enum.GetNames(typeof(Denominations)).Length;

            for (int i = 0; i < denominationLength; i++)
            {
                for (int j = 0; j < suitLength; j++)
                {
                    int index = i * suitLength + j;
                    cards[index] = new Card((Suits)j, (Denominations)i);
                }
            }
        }
    }
}
