using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2019
{
    public class Day6
    {
        private static readonly List<string[]> _orbits = File.ReadAllLines(@"Inputs/Day6.txt").Select(x => x.Split(')')).ToList();
        public void Run()
        {
            PartOne();
            PartTwo();
        }

        private void PartOne()
        {
            var root = new Node("COM");
            BuildTree(root, "COM");

            Console.WriteLine(root.SumChild());
        }

        private void PartTwo()
        {
            var root = new Node("COM");
            BuildTree(root, "COM");

            var youPath = root.Find("YOU").PathToRoot();
            var sanPath = root.Find("SAN").PathToRoot();

            var intersect = youPath.Intersect(sanPath);

            var count = youPath.Except(intersect).Count() + sanPath.Except(intersect).Count();

            Console.WriteLine(count);
        }

        private void BuildTree(Node root, string node)
        {
            var children = _orbits.Where(x => x[0] == node);

            var parent = root.Find(node);

            foreach (var child in children)
            {
                parent.Add(new Node(child[1]));
                BuildTree(root, child[1]);
            }
        }
    }

    class Node : IEnumerable<Node>
    {
        public Node Parent { get; set; }
        public string Name { get; set; }
        public List<Node> Children { get; set; }
        public int ParentCount
        {
            get
            {
                if (Parent == null)
                {
                    return 0;
                }

                return 1 + Parent.ParentCount;
            }
        }

        public int ChildrenCount => Children.Count;

        public Node(string name)
        {
            Name = name;
            Children = new List<Node>();
        }

        public void Add(Node node)
        {
            node.Parent = this;
            Children.Add(node);
        }

        public IEnumerable<Node> GetNodeAndChildren()
        {
            return new[] { this }.Concat(Children.SelectMany(x => x.GetNodeAndChildren()));
        }

        public IEnumerator<Node> GetEnumerator()
        {
            return Children.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public Node Find(string name)
        {
            return GetNodeAndChildren().FirstOrDefault(x => x.Name == name);
        }

        public int SumChild()
        {
            var nodes = GetNodeAndChildren();

            var count = 0;

            foreach (var node in nodes)
            {
                count += node.ParentCount;
            }

            return count;
        }

        public List<string> PathToRoot()
        {
            var path = new List<string>();

            if (Parent == null)
            {
                return path;
            }

            path.Add(Parent.Name);
            path.AddRange(Parent.PathToRoot());

            return path;
        }

        public void MakeRoot()
        {
            Parent = null;
        }
    }
}
