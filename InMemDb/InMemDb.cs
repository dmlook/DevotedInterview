namespace LookInterview;

public class InMemDb: Dictionary<string, string>
{
    Stack<Dictionary<string, string>?> _transactions = new();
    Dictionary<string, string>? _currentTransactionRollback;
    
    public bool CanRollback => _currentTransactionRollback != null; 

    /// <summary>
    /// Sets the name in the database to the given value
    /// </summary>
    /// <param name="name">Name of entry</param>
    /// <param name="value">Value of entry</param>
    /// <returns>Rollback command</returns>
    public void Set(string name, string value)
    {
        var rollbackValue = "";
        // updating a value
        if (ContainsKey(name))
        {
            rollbackValue = this[name];
            this[name] = value;
        }
        else
        {
            // adding a new entry
            Add(name, value);
        }
        
        // if we are in a transaction and we haven't established a rollback value yet, save it - empty string is for deletion
        if (_currentTransactionRollback != null && !_currentTransactionRollback.ContainsKey(name))
            _currentTransactionRollback.Add(name, rollbackValue);
    }

    /// <summary>
    /// Prints the value for the given name. If the value is not in the database, prints NULL
    /// </summary>
    /// <param name="name">Name to look up</param>
    /// <returns></returns>
    public string Get(string name)
    {
        return ContainsKey(name) ? this[name] : "NULL";
    }

    /// <summary>
    /// Deletes the value from the database
    /// </summary>
    /// <param name="name">Name of entry to delete</param>
    /// <returns>Rollback command, null if no value was deleted</returns>
    public void Delete(string name)
    {
        var v = Get(name);
        
        Remove(name);
        
        // if we are in a transaction and we haven't established a rollback value yet, save it
        if (_currentTransactionRollback != null && !_currentTransactionRollback.ContainsKey(name))
            _currentTransactionRollback.Add(name, v);
    }

    /// <summary>
    /// Returns the number of names that have the given value assigned to them.
    /// If that value is not assigned anywhere, prints 0
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public int CountValues(string value)
    {
        return Values.Count(v => v == value);
    }

    public void BeginNewTransaction()
    {
        if(_currentTransactionRollback != null)
            _transactions.Push(_currentTransactionRollback);

        _currentTransactionRollback = new Dictionary<string, string>();
    }

    public void Rollback()
    {
        if (_currentTransactionRollback == null) 
            return;

        foreach (var kvp in _currentTransactionRollback)
        {
            if(string.IsNullOrEmpty(kvp.Value))
                Delete(kvp.Key);
            else
                Set(kvp.Key, kvp.Value);
        }

        _transactions.TryPop(out _currentTransactionRollback);
    }

    public void CommitTransactions()
    {
        _currentTransactionRollback = null;
        _transactions = new Stack<Dictionary<string, string>?>();
    }
}