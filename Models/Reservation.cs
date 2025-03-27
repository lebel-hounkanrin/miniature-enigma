using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace parc.Models;

public class Reservation
{
    // une réservation peut être faite sur un device ou sur une salle entière
    public int Id { get; set; }
    public int? RoomId { get; set; }
    public int? DeviceId { get; set; }
    [Required]
    public DateTime Date { get; set; } 
    [Required]
    public TimeSpan StartTime { get; set; } 
    [Required]
    public TimeSpan EndTime { get; set; } 
    public bool IsRoomReservation { get; set; }
    public bool? isConfirmed { get; set; }  = false;
    public bool? Status { get; set; } = false;
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}