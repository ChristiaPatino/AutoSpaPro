namespace AutoSpaPro.Models;

public class PaymentRecord
{
    public int Id { get; set; }
    public int ParkingCustomerId { get; set; }
    public ParkingCustomer? ParkingCustomer { get; set; }
    public DateTime PaymentDate { get; set; } = DateTime.Today;
    public decimal Amount { get; set; }
}
