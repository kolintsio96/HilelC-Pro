using DataStructures.Lib;

namespace DataStructers.Tests
{
    internal class BinaryTreeTests : Tests
    {
        public override string Title => "Binary tree tests run:";

        protected override void RunTests()
        {
            AddToTree();
            ContainsInTree();
            ClearTree();
            TreeToArray();
        }

        private void AddToTree()
        {
            var tree = new BinaryTree<int>();
            tree.Add(1);
            tree.Add(2);
            tree.Add(3);
            tree.Add(4);

            ShowTestResult("Add to tree", tree.Count == 4 && tree.Root?.Key == 1);
        }

        private void ContainsInTree()
        {
            var tree = new BinaryTree<int>();
            tree.Add(1);
            tree.Add(2);
            tree.Add(3);
            tree.Add(4);

            ShowTestResult("Contains in tree", tree.Count == 4 && tree.Contains(2) && !tree.Contains(-2));
        }

        private void ClearTree()
        {
            var tree = new BinaryTree<int>();
            tree.Add(1);
            tree.Add(2);
            tree.Add(3);
            tree.Add(4);

            tree.Clear();

            ShowTestResult("Clear tree", tree.Count == 0 && tree.Root == null);
        }

        private void TreeToArray()
        {
            var tree = new BinaryTree<int>();
            tree.Add(3);
            tree.Add(1);
            tree.Add(4);
            tree.Add(2);

            var array = tree.ToArray();

            ShowTestResult("Clear tree", tree.Count == 4 && array[0] == 1 && array[1] == 2 && array[2] == 3 && array[3] == 4);
        }
    }
}
