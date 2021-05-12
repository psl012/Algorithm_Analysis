using System;

namespace HeapLibrary
{
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
