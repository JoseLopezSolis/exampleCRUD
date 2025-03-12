using System.ComponentModel.DataAnnotations;
using CommunityToolkit.Mvvm.ComponentModel;

namespace exampleCRUD.DTOs;

public partial  class EmployeeDTO : ObservableObject
{
    [ObservableProperty] public int idEmployee;
    [ObservableProperty] public string completeName;
    [ObservableProperty] public string email;
    [ObservableProperty] public decimal salary;
    [ObservableProperty] public static DateTime contractDate;
};
