using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2018
{
    public class Day07
    {
        public string DetermineConstructionOrder(List<string> inputLines)
        {
            /*
             *  "Step C must be finished before step A can begin.",
                "Step C must be finished before step F can begin.",
                "Step A must be finished before step B can begin.",
                "Step A must be finished before step D can begin.",
                "Step B must be finished before step E can begin.",
                "Step D must be finished before step E can begin.",
                "Step F must be finished before step E can begin."
             */
            var orderList = new List<(char, char)>();
            foreach (var inputLine in inputLines)
            {
                var order = ParseLine(inputLine);
                orderList.Add(order);
            }

            Node node = null;

            foreach (var tuple in orderList)
            {
                if (node == null)
                {
                    node = new Node(tuple.Item1, tuple.Item2);
                }
                else
                {
                   UpdateNode(node, tuple.Item1, tuple.Item2);
                }
            }

            return string.Empty;
        }

        private static void UpdateNode(Node node, char idToSearch, char childNodeValue)
        {
            if (node.Id == idToSearch)
            {
                node.Children.Add(new Node(childNodeValue));
            }
            else
            {
                foreach (var childNode in node.Children)
                {
                    UpdateNode(childNode, idToSearch, childNodeValue);
                }
            }
        }

        public (char, char) ParseLine(string line)
        {
            //Step C must be finished before step A can begin.

            var first = line.Skip(5).First();
            var second = line.Skip(36).First();

            return (first, second);
        }
    }

    public class Node
    {
        public Node(char id, char? child = null)
        {
            Id = id;
            Children = child!=null ? new List<Node> { new Node(child.Value)} : new List<Node>();
        }

        public char Id { get; set; }
        public List<Node> Children { get; set; }
    }
}
