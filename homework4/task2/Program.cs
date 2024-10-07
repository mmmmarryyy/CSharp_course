using System;

class EventArguments
{
    public string Message { get; }

    public EventArguments(string message)
    {
        Message = message;
    }
}

class Publisher
{
    public void Post(string message)
    {
        EventBus.Instance.Publish(this, new EventArguments(message));
    }
}

class Subscriber
{
    private string infoMessage;

    public Subscriber(string message)
    {
        infoMessage = message;
    }

    public void onEvent(EventArguments e)
    {
        Console.WriteLine(infoMessage + ": " + e.Message);
    }
}

class EventBus
{
    private static EventBus _instance; //for singleton pattern
    private readonly Dictionary<Publisher, List<Action<EventArguments>>> _events = new Dictionary<Publisher, List<Action<EventArguments>>>();

    public static EventBus Instance //for singleton pattern
    {
        get
        {
            if (_instance == null)
            {
                _instance = new EventBus();
            }
            return _instance;
        }
    }

    public void Publish(Publisher publisher, EventArguments args)
    {
        if (_events.ContainsKey(publisher))
        {
            foreach (var eventHandler in _events[publisher])
            {
                eventHandler(args);
            }
        }
    }

    public void Subsribe(Publisher publisher, Action<EventArguments> eventHandler)
    {
        if (!_events.ContainsKey(publisher))
        {
            _events.Add(publisher, new List<Action<EventArguments>>());
        }
        _events[publisher].Add(eventHandler);
    }

    public void Unsubsribe(Publisher publisher, Action<EventArguments> eventHandler)
    {
        if (_events.ContainsKey(publisher))
        {
            _events[publisher].Remove(eventHandler);
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        Publisher publisher1 = new Publisher();
        Publisher publisher2 = new Publisher();
        Publisher publisher3 = new Publisher();
        Subscriber subscriber1 = new Subscriber("Subscriber 1");
        Subscriber subscriber2 = new Subscriber("Subscriber 2");

        EventBus.Instance.Subsribe(publisher1, subscriber1.onEvent);
        EventBus.Instance.Subsribe(publisher1, subscriber2.onEvent);
        EventBus.Instance.Subsribe(publisher2, subscriber1.onEvent);

        publisher1.Post("This is post 1 from publisher1");
        publisher2.Post("This is post 1 from publisher2");

        EventBus.Instance.Unsubsribe(publisher1, subscriber1.onEvent);

        publisher1.Post("This is post 2 from publisher1");
        publisher2.Post("This is post 2 from publisher2");

        EventBus.Instance.Unsubsribe(publisher1, subscriber2.onEvent);
        EventBus.Instance.Unsubsribe(publisher2, subscriber1.onEvent);

        publisher1.Post("This is post 3 from publisher1");
        publisher2.Post("This is post 3 from publisher2");

        Console.WriteLine("Success");
    }
}
