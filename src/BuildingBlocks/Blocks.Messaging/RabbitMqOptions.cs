using System.ComponentModel.DataAnnotations;

namespace Blocks.Messaging;

public sealed class RabbitMqOptions
{
    [Required]
    public string Host { get; set; } = "localhost"; // Default to localhost
    
    [Required]
    public string Username { get; set; } = "guest"; // Default RabbitMQ 
    
    [Required]
    public string Password { get; set; } = "guest"; // Default RabbitMQ password
     
    [Required]
    public string VirtualHost { get; set; } = "/"; // Default RabbitMQ virtual host
}