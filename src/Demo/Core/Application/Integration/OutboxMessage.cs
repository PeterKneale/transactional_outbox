namespace Demo.Core.Application.Integration;

public class OutboxMessage
{
    private OutboxMessage()
    {
        
    }
    public OutboxMessage(string type, string data)
    {
        Id = Guid.NewGuid();
        Type = type;
        Data = data;
    }
    public void MarkProcessed()
    {
        Processed = true;
    }
    public string Type { get;}
    public string Data { get; }
    public bool Processed { get; private set; }
    public Guid Id { get; set; }
}