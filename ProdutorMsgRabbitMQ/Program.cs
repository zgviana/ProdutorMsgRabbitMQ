
using RabbitMQ.Client;
using System.Text;

internal class Program
{
    private static async Task Main(string[] args)
    {
        Console.WriteLine("Digite algo:");
        string msg = Console.ReadLine();
    
        //nó da conexão no rabbiMQ
        var factory = new ConnectionFactory()
        {
            HostName = "localhost",
        };

        using var connection = await factory.CreateConnectionAsync();
        using var canal = await connection.CreateChannelAsync();

        //abrindo conexão

        await canal.QueueDeclareAsync(
                        queue: "MensagemEnviada_1", //nome da fila
                        durable: false, //se o servidor for reiniciado a msg continua na fila
                        exclusive: false, //so pode ser acessada via conexão atual
                        autoDelete: true,//deletada quando o consumidor usa na fila
                        arguments: null
                    );

                var bodyMsg = Encoding.UTF8.GetBytes(msg);

        await canal.BasicPublishAsync(
                            exchange:"",
                           routingKey: "MensagemEnviada_1",                    
                            body: bodyMsg
                        );       
       
        Console.WriteLine($"Mensagem enviada : {msg}");
        Console.ReadLine();


    }

}