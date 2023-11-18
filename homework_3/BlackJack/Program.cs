using BlackJack;

namespace BankAccount
{
    class Program
    {
        private static void Main(string[] args)
        {
            #region task-1
            //Згенерувати впорядковану колоду карт
            PlayingCards cards = new PlayingCards();
            cards.PrintCards();
            Console.WriteLine();
            #endregion

            #region task-2
            //Перемішати колоду карт
            cards.OverhandShuffle();
            cards.PrintCards();
            Console.WriteLine();
            #endregion

            #region task-3
            //Знайти позиції всіх тузів у колоді
            cards.FindIndexOfCard(Denominations.Ace);
            Console.WriteLine();
            #endregion

            #region task-4
            //Перемістити всі пікові карти на початок колоди
            cards.SortBySuit(Suits.Spade);
            cards.PrintCards();
            Console.WriteLine();
            #endregion

            #region task-5
            //Відсортувати колоду
            cards.Sort();
            cards.PrintCards();
            Console.WriteLine();
            #endregion
        }
    }
}