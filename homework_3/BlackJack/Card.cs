namespace Game
{
    readonly struct Card
    {
        public Suits Suit { get; }
        public Denominations Denomination { get; }
        public Card(Suits suit, Denominations denomination)
        {
            Suit = suit;
            Denomination = denomination;
        }
    }
}
