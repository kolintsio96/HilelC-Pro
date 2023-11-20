namespace Game
{
    internal class Player
    {
        private List<Card> cards = new List<Card>();

        public void AddCard(Card card)
        {
            cards.Add(card);
        }

        public int CountPoints()
        {
            int points = 0;
            foreach (Card card in cards)
            {
                points += GetPoint(card.Denomination);
            }
            return points;
        }
        public void Clear()
        {
            cards.Clear();
        }
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
                    return 10;
                case Denominations.Jack:
                    return 2;
                case Denominations.Queen:
                    return 3;
                case Denominations.King:
                    return 4;
                case Denominations.Ace:
                    return 11;
                default:
                    return 0;
            }
        }
    }
}
