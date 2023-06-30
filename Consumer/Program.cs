using Consumer;

var factory = new RabbitMQ.Client.ConnectionFactory()
{
    // Configure the properties of the factory here
    HostName = "localhost",
    UserName = "guest",
    Password = "guest",
    //...
};

var consumer = new RabbitMqConsumer<string>(factory);

consumer.ConsumeQueue(x=>Console.WriteLine(x));

Console.ReadKey();