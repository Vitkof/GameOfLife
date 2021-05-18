using GameOfLife.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;


namespace GameOfLife.Implement
{
    public sealed class Game : IGame<InfiniteToroidalGrid, Rules, Cell>
    {
        private readonly Rules _rules;
        public InfiniteToroidalGrid Initial { get; }

        public Game(InfiniteToroidalGrid grid, Rules rules)
        {
            _rules = rules;
            Initial = grid;
        }


        public IEnumerator<InfiniteToroidalGrid> GetEnumerator()
        {
            var curr = Initial;

            while(true)
            {
                yield return curr;
                curr = _rules.Apply(curr);
            }
        }

        IEnumerator IEnumerable.GetEnumerator() 
            => GetEnumerator();

        public static Game Default { get; } = new(InfiniteToroidalGrid.Default, Rules.Instance);
    }
}
