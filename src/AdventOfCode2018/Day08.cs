using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2018
{
    public class Day08
    {
        private List<Node08> nodes;
        private int sum;
        private int rootSum;

        public Day08()
        {
            nodes = new List<Node08>();
            sum = 0;
            rootSum = 0;
        }

        public int CalculateMetaData(string inputPath)
        {
            var input = File.ReadAllText(inputPath);

            return CalculateSumOfMetaData(input);
        }

        public int CalculateRoot(string inputPath)
        {
            var input = File.ReadAllText(inputPath);

            return CalculateSumOfRootNode(input);
        }



        public int CalculateSumOfMetaData(string input)
        {
            var inputNumbers = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            var i = 0;
            i = CalculateNode(inputNumbers, i);

            return sum;
        }

        public int CalculateSumOfRootNode(string input)
        {
            var inputNumbers = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            var i = 0;
            Node08 node = null;
            i = CalculateRootNode(inputNumbers, i, out node);

            return node.Value;
        }

        /*
         *Specifically, a node consists of:

            A header, which is always exactly two numbers:
            The quantity of child nodes.
            The quantity of metadata entries.
            Zero or more child nodes (as specified in the header).
            One or more metadata entries (as specified in the header).

            Each child node is itself a node that has its own header, child nodes, and metadata. For example:
            0   2            7   9
            2 3 0 3 10 11 12 1 1 0 1 99 2 1 1 2
            A----------------------------------
                B----------- C-----------
                                 D-----
            
            A, which has 2 child nodes (B, C) and 3 metadata entries (1, 1, 2).
            B, which has 0 child nodes and 3 metadata entries (10, 11, 12).
            C, which has 1 child node (D) and 1 metadata entry (2).
            D, which has 0 child nodes and 1 metadata entry (99).
         *
         */

        private int CalculateNode(string[] inputNumbers, int i)
        {
            var node = CreateNode(i, inputNumbers);
            int metaData = 0;
            if (node.NrOfChildren == 0)
            {
                metaData = i+2;
            }
            else
            {
                var childNodeIndex = i + 2;
                metaData = childNodeIndex;
                for (int j = 0; j < node.NrOfChildren; j++)
                {
                    metaData = CalculateNode(inputNumbers, metaData);
                }
            }

            AddMetaData(node, metaData, inputNumbers);
            nodes.Add(node);

            return metaData + node.NrOfMetaData;
        }

        /*
         *  0   2            7   9
            2 3 0 3 10 11 12 1 1 0 1 99 2 1 1 2
            A----------------------------------
                B----------- C-----------
                                 D-----
         */

        private int CalculateRootNode(string[] inputNumbers, int i, out Node08 node)
        {
            node = CreateNode(i, inputNumbers);
            
            int metaData = 0;
            if (node.NrOfChildren == 0)
            {
                metaData = i + 2;
            }
            else
            {
                var childNodeIndex = i + 2;
                metaData = childNodeIndex;
                for (int j = 0; j < node.NrOfChildren; j++)
                {
                    metaData = CalculateRootNode(inputNumbers, metaData, out Node08 childNode);
                    node.ChildNodes.Add(childNode);
                }
            }

            AddMetaData(node, metaData, inputNumbers);

            return metaData + node.NrOfMetaData;
        }

        public void AddMetaData(Node08 node, int index, string[] inputNumbers)
        {
            for (int m = 0; m < node.NrOfMetaData; m++)
            {
                int metaData = int.Parse(inputNumbers[index + m]);
                node.MetaData.Add(metaData);
                sum += metaData;
            }
        }
        
        public static Node08 CreateNode(int i, string[] inputNumbers)
        {
            int nrOfChildren = int.Parse(inputNumbers[i]);
            int nrOfMetadata = int.Parse(inputNumbers[i + 1]);
            var node = new Node08(nrOfChildren, nrOfMetadata);

            return node;
        }
    }

    

    public class Node08
    {
        public Node08(int nrOfchildren, int nrOfMetadata)
        {
            NrOfChildren = nrOfchildren;
            NrOfMetaData = nrOfMetadata;
            MetaData = new List<int>(nrOfMetadata);
            ChildNodes = new List<Node08>(nrOfchildren);
        }

        public int NrOfChildren { get; set; }
        public int NrOfMetaData { get; set; }

        public List<int> MetaData { get; set; }

        public List<Node08> ChildNodes { get; set; }

        public int Value
        {
            /*
             * If a node has no child nodes, its value is the sum of its metadata entries.
             * So, the value of node B is 10+11+12=33, and the value of node D is 99.
             * However, if a node does have child nodes, the metadata entries become indexes which refer to those child nodes.
             * A metadata entry of 1 refers to the first child node, 2 to the second, 3 to the third, and so on.
             * The value of this node is the sum of the values of the child nodes referenced by the metadata entries.
             * If a referenced child node does not exist, that reference is skipped.
             * A child node can be referenced multiple time and counts each time it is referenced.
             * A metadata entry of 0 does not refer to any child node.
             */

            get
            {
                int sum=0;
                if (!ChildNodes.Any())
                {
                    foreach (var metaDataIndex in MetaData)
                    {
                        sum += metaDataIndex;
                    }
                }
                else
                {
                    foreach (var metaDataIndex in MetaData)
                    {

                        if (metaDataIndex <= ChildNodes.Count)
                        {
                            var childNode = ChildNodes[metaDataIndex - 1];
                            sum += childNode.Value;
                        }
                    }
                }

                return sum;
            }
        }
    }
}
