using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace Week_7_Programming_Assignment
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            FileProcessor fileProcessor = new FileProcessor();
            MedianMaintenance medianMaintenance = new MedianMaintenance();
            int[] testData = fileProcessor.ReadTextFile(@"C:\Users\Paul\Documents\Open Source Society for Computer Science (OSSU)\Algorithms Coursera\Programming Assignments\Week 7 Programming Assignment\TestCases\Real Test.txt");

            foreach(int num in testData)
            {
                medianMaintenance.FindMedian(num);
            }

            Console.WriteLine("Final Answer: " + medianMaintenance.GetFinalAnswer());

            Console.ReadKey();
        }
    }
    class FileProcessor
    {
        public int[] ReadTextFile(string directory)
        {
            string[] lines = File.ReadAllLines(directory);
            int[] intArray = Array.ConvertAll(lines, s => Convert.ToInt32(s));

            return intArray;
        }
    }

    class MedianMaintenance
    {
        public List<int> _listOfMedian {get; private set;} = new List<int>();
        MaxIntHeap _h1 = new MaxIntHeap();
        MinIntHeap _h2 = new MinIntHeap();
        
        public int FindMedian(int number)
        {
            int medianCandidate;
            if(_listOfMedian.Count == 0)
            {
                _h1.add(number);
                medianCandidate = number;
            }
            else if(_listOfMedian.Count == 1)
            {
                if(number < _h1.peek()) _h1.add(number);
                else _h2.add(number);

                medianCandidate = _h1.peek();
            }
            else
            {
                if (number > _h2.peek()) _h2.add(number);
                else _h1.add(number);
                
                if(_h2._size - _h1._size == 1) medianCandidate = _h2.peek();
                else medianCandidate = _h1.peek();
            }
            if(_h1._size - _h2._size >= 2)
            {
                _h2.add(_h1.pull());
                medianCandidate = _h1.peek();
            }
            else if(_h2._size - _h1._size >= 2)
            {
                _h1.add(_h2.pull());
                medianCandidate = _h1.peek();
            } 

            _listOfMedian.Add(medianCandidate);
            return medianCandidate;
        }

        public int GetFinalAnswer()
        {
            return _listOfMedian.Sum() % 10000;
        }
    }

    public class MinIntHeap
    {
        int _capacity;
        public int _size {get; private set;} = 0; 
        int[] _items;

        public MinIntHeap(int capacity = 10)
        {
            _capacity = capacity;
            _items = new int[_capacity];
        }

        int getLeftChildIndex(int parentIndex) {return 2 * parentIndex + 1;}
        int getRightChildIndex(int parentIndex) {return 2 * parentIndex + 2;}
        int getParentIndex(int childIndex) {return (childIndex - 1)/2;}

        bool hasLeftChild(int index) {return getLeftChildIndex(index) < _size ;}
        bool hasRightChild(int index) {return getRightChildIndex(index) < _size;}
        bool hasParent(int index) {return getParentIndex(index) >= 0;}
        
        int leftChild(int index) {return _items[getLeftChildIndex(index)];}
        int rightChild(int index) {return _items[getRightChildIndex(index)];}
        int parent(int index) {return _items[getParentIndex(index)];}

        void Swap(int indexOne, int indexTwo)
        {
            int temp = _items[indexOne];
            _items[indexOne] = _items[indexTwo];
            _items[indexTwo] = temp;
        }

        void EnsureExtraCapacity()
        {
            if(_size == _capacity)
            {
                Array.Resize(ref _items, _capacity*2);
                _capacity *= 2;
            }
        }

        public int peek()
        {
            if (_size == 0) 
            {
                throw new ArgumentOutOfRangeException("Tree size is zero so there is nothing to see");
            }
            return _items[0];
        }

        public int pull()
        {
            if (_size == 0)
            {
                throw new ArgumentOutOfRangeException("Tree size is zero so we can no longer pull");
            }
            int item = _items[0];
            _items[0] = _items[_size-1];
            _size--;
            HeapifyDown();    
            return item;
        }

        public void add(int item)
        {
            EnsureExtraCapacity();
            _items[_size] = item;
            _size++;
            HeapifyUp();
        }

        public void HeapifyUp()
        {
            int index = _size -1;
            while(hasParent(index) && parent(index) > _items[index])
            {
                Swap(getParentIndex(index), index);
                index = getParentIndex(index);
            }
        }

        public void HeapifyDown()
        {
            int index = 0;
            while(hasLeftChild(index))
            {
                int smallerChildIndex = getLeftChildIndex(index);
                if (hasRightChild(index) && rightChild(index) < leftChild(index))
                {
                    smallerChildIndex = getRightChildIndex(index);    
                }
                if (_items[index] < _items[smallerChildIndex])
                {
                    break;
                }
                else
                {
                    Swap(index, smallerChildIndex);
                }
                index = smallerChildIndex;
            }
        }

        public int GetHeapSize()
        {
            return _items.Length;
        }
    }

    public class MaxIntHeap
    {
        int _capacity;
        public int _size {get; private set;}  = 0; 
        int[] _items;

        public MaxIntHeap(int capacity = 10)
        {
            _capacity = capacity;
            _items = new int[_capacity];
        }

        int getLeftChildIndex(int parentIndex) {return 2 * parentIndex + 1;}
        int getRightChildIndex(int parentIndex) {return 2 * parentIndex + 2;}
        int getParentIndex(int childIndex) {return (childIndex - 1)/2;}

        bool hasLeftChild(int index) {return getLeftChildIndex(index) < _size ;}
        bool hasRightChild(int index) {return getRightChildIndex(index) < _size;}
        bool hasParent(int index) {return getParentIndex(index) >= 0;}
        
        int leftChild(int index) {return _items[getLeftChildIndex(index)];}
        int rightChild(int index) {return _items[getRightChildIndex(index)];}
        int parent(int index) {return _items[getParentIndex(index)];}

        void Swap(int indexOne, int indexTwo)
        {
            int temp = _items[indexOne];
            _items[indexOne] = _items[indexTwo];
            _items[indexTwo] = temp;
        }

        void EnsureExtraCapacity()
        {
            if(_size == _capacity)
            {
                Array.Resize(ref _items, _capacity*2);
                _capacity *= 2;
            }
        }

        public int peek()
        {
            if (_size == 0) 
            {
                throw new ArgumentOutOfRangeException("Tree size is zero so there is nothing to see");
            }
            return _items[0];
        }

        public int pull()
        {
            if (_size == 0)
            {
                throw new ArgumentOutOfRangeException("Tree size is zero so we can no longer pull");
            }
            int item = _items[0];
            _items[0] = _items[_size-1];
            _size--;
            HeapifyDown();    
            return item;
        }

        public void add(int item)
        {
            EnsureExtraCapacity();
            _items[_size] = item;
            _size++;
            HeapifyUp();
        }

        public void HeapifyUp()
        {
            int index = _size -1;
            while(hasParent(index) && parent(index) < _items[index])
            {
                Swap(getParentIndex(index), index);
                index = getParentIndex(index);
            }
        }

        public void HeapifyDown()
        {
            int index = 0;
            while(hasLeftChild(index))
            {
                int smallerChildIndex = getLeftChildIndex(index);
                if (hasRightChild(index) && rightChild(index) > leftChild(index))
                {
                    smallerChildIndex = getRightChildIndex(index);    
                }
                if (_items[index] > _items[smallerChildIndex])
                {
                    break;
                }
                else
                {
                    Swap(index, smallerChildIndex);
                }
                index = smallerChildIndex;
            }
        }

        public int GetHeapSize()
        {
            return _items.Length;
        }
    }


}
