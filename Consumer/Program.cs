using Consumer;
using Consumer.Models;
using Microsoft.EntityFrameworkCore;
using System.Xml;

var factory = new RabbitMQ.Client.ConnectionFactory()
{
    // Configure the properties of the factory here
    HostName = "localhost",
    UserName = "guest",
    Password = "guest",
    //...
};

var consumer = new RabbitMqConsumer(factory);

consumer.ConsumeQueue(entity =>
{
    using (var db = new ApplicationDBContext())
    {
        db.scrappedElements.Add(entity);
        db.SaveChangesAsync();
    }
});

while (true)
{
    Thread.Sleep(5000);
}
