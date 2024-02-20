using System;
using System.Collections.Generic;

namespace ConsoleApplication1
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var dummyList = new List<string>();
            var limitedList = new ObservableLimitedList(dummyList, s => s.Contains("s"));

            limitedList.OnListChanged += (s) => Console.WriteLine($"The letter '{s}' was added to the list");

            limitedList.TryAdd("a");
            limitedList.TryAdd("s");
            limitedList.TryAdd("b");
            limitedList.TryAdd("c");
            limitedList.TryAdd("s");
            limitedList.TryAdd("d");
            limitedList.TryAdd("h");
            limitedList.TryAdd("s");
            limitedList.TryAdd("j");
            limitedList.TryAdd("k");
            limitedList.TryAdd("i");
            
            limitedList.PrintAll();
        }
    }

    internal class ObservableLimitedList
    {
        public event Action<string> OnListChanged;

        private readonly Predicate<string> _limitRule;
        private List<string> _list;

        public ObservableLimitedList(List<string> list, Predicate<string> limitRule)
        {
            _list = list;
            _limitRule = limitRule;
        }

        public bool TryAdd(string elementToAdd)
        {
            if (!_limitRule.Invoke(elementToAdd))
            {
                return false;
            }

            if (_list == null)
            {
                _list = new List<string>();
            }
            
            _list.Add(elementToAdd);
            
            OnListChanged?.Invoke(elementToAdd);
            
            return true;
        }

        public void PrintAll()
        {
            if (_list.Count == 0)
            {
                return;
            }

            foreach (var element in _list)
            {
                Console.WriteLine(element);
            }
        }
    }
}