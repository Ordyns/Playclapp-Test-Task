public class ObservableProperty<T>
{
    public event System.Action Changed;
    private T _value;

    public T Value { 
        get => _value;
        set { _value = value; Changed?.Invoke(); }
    }

    public override string ToString() => Value.ToString();
}