namespace HSetWork;

public enum CollisionApproach { SeparateChaining, LinearProbing }
public class IHashSet<T>
{
    private readonly int _size;
    private readonly List<List<T>?>? _slots;
    private readonly CollisionApproach _approach;
    private int _collisionCount = 0;
    private const int REHASH_THRESHOLD = 3;

    public IHashSet(int size, CollisionApproach approach = CollisionApproach.SeparateChaining)
    {
        _size = size;
        _approach = approach;
        _slots = [.. Enumerable.Repeat<List<T>?>(null, size)];
    }

    private int GetSlotIndex(T item, int attempt = 0)
    {
        int hash = Math.Abs(item!.GetHashCode());
        return (hash + attempt) % _size;
    }

    public void Add(T item)
    {
        if(_approach == CollisionApproach.SeparateChaining)
            AddChaining(item);
        else
            AddProbing(item);
    }

    private void AddChaining(T item)
    {
        int index = GetSlotIndex(item);

        if (_slots![index] == null)
        {
            _slots[index] = new List<T> { item };
        }
        else
        {
            if (_slots[index]!.Contains(item)) return;
            _collisionCount++;
            _slots[index]!.Add(item);
            if (_collisionCount >= REHASH_THRESHOLD) Rehash();
        }
    }
    private void AddProbing(T item)
    {
        for (int attempt = 0; attempt < _size; attempt++)
        {
            int index = GetSlotIndex(item, attempt);
            if (_slots![index] == null)
            {
                _slots[index] = new List<T> { item };
                return;
            }
            if (_slots[index]!.Contains(item)) return;

            _collisionCount++;
            if (_collisionCount >= REHASH_THRESHOLD) {
                Rehash();
                Add(item);
                return;
            }
        }
        throw new Exception("HashSet is full!");
    }

    public bool IsPresent(T item)
    {
        if (_approach == CollisionApproach.SeparateChaining)
        {
            int index = GetSlotIndex(item);
            return _slots![index] != null && _slots[index]!.Contains(item);
        }
        else
        {
            for (int attempt = 0; attempt < _size; attempt++)
            {
                int index = GetSlotIndex(item, attempt);
                if (_slots![index] == null) return false;
                if (_slots[index]!.Contains(item)) return true;
            }
            return false;
        }
    }

    private void Rehash()
    {
        IHashSet<T> newHashSet = new IHashSet<T>(_size * 2, _approach);

        foreach (List<T> slot in _slots!)
            if (slot != null)
                foreach (T item in slot)
                    newHashSet.Add(item);
        
        _slots.Clear();
        for (int i = 0; i < _size * 2; i++)
            _slots.Add(i < newHashSet._slots!.Count ? newHashSet._slots[i] : null);
        
        _collisionCount = 0;
    }
}