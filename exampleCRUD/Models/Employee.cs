namespace exampleCRUD.Models;
using System.ComponentModel.DataAnnotations;

public class Employee
{
    public int IdEmployee { get; set; }
    public string CompleteName { get; set; }
    public string Email { get; set; }
    public decimal Salary { get; set; }
    public DateTime ContractDate { get; set; }
}