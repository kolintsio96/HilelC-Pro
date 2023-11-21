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
                points += (int)card.Denomination;
            }
            return points;
        }
        public void Clear()
        {
            cards.Clear();
        }
    }
}
