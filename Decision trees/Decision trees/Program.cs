using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decision_trees
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int[]> Examples = new List<int[]>();
        }
        Node ID3Rec(int wanted, List<int[]> Examples)
        {
            return new Node(0); 
        }
        List<int[]> ExecludeFromExamples(List<int[]> Examples, int attribute, int status)
        {
            List<int[]> examples2 = Examples;
            foreach (int[] ex in examples2)
            {
                if (ex[attribute] == status)
                {
                    examples2.Remove(ex);
                }
            }
            return examples2;
        }
        Node CheckIfAllExamplesHaveOneClass(List<int[]> Examples)
        {
            Node n = new Node();
            for (int i = 0; i < Examples.First().Count() - 1; i++)
            {
                int checkingVal = -1;
                foreach (int[] exmp in Examples)
                {
                    if (checkingVal == -1)
                    {
                        checkingVal = exmp[i];
                    }
                    if (checkingVal != exmp[i])
                        break;
                    
                }
                if (checkingVal != -1)
                {
                    n.name = checkingVal;
                    break;
                }
            }
            return n;
        }
        class Node
        {
            public Node()
            {

            }
            public Node(int _name)
            {
                name = _name;
            }
            public int name { get; set; }
        }
    }
}
