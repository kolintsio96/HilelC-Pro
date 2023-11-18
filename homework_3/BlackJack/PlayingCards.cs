using System;

namespace BlackJack
{
    readonly struct Card
    {
        public int Id { get; }
        public byte Point { get; }
        public Suits Suit { get; }
        public Denominations Denomination { get; }
        public Card(int index, Suits suit, Denominations denomination, byte point)
        { 
            Id = index;
            Point = point;
            Suit = suit;
            Denomination = denomination;
        }
    }
    internal class PlayingCards
    {
        private Card[] cards = new Card[36];
        private byte GetPoint(Denominations denomination)
        {
            switch (denomination)
            {
                case Denominations.Six:
                    return 6;
                case Denominations.Seven:
                    return 7;
                case Denominations.Eight:
                    return 8;
                case Denominations.Nine:
                    return 9;
                case Denominations.Ten:
                case Denominations.Jack:
                case Denominations.Queen:
                case Denominations.King:
                    return 10;
                case Denominations.Ace:
                    return 11;
                default:
                    return 0;
            }
        }
        public Card[] GetCards()
        {
            return cards;
        }
        public void OverhandShuffle()
        {
            Random random = new Random();
            cards = cards.OrderBy(x => random.Next()).ToArray();
        }
        public void Sort()
        {
            cards = cards.OrderBy(card => card.Id).ToArray();
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
                Console.WriteLine($"{cards[i].Id} - {cards[i].Denomination} - {cards[i].Suit}");
            }
        }
        public PlayingCards() {
            int suitLength = Enum.GetNames(typeof(Suits)).Length;
            int denominationLength = Enum.GetNames(typeof(Denominations)).Length;

            for (int i = 0; i < denominationLength; i++)
            {
                for (int j = 0; j < suitLength; j++)
                {
                    int index = i * suitLength + j;
                    cards[index] = new Card(index, (Suits)j, (Denominations)i, GetPoint((Denominations)i));
                }
            }
        }
    }
}
