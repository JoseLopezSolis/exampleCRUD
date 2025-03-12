using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using exampleCRUD.DataAccess;
using exampleCRUD.DTOs;
using exampleCRUD.Utilities;
using exampleCRUD.Views;
using Microsoft.EntityFrameworkCore;

namespace exampleCRUD.ViewModels;

public partial class MainViewModel : ObservableObject
{
     private readonly EmployeeDbContext _dbContext;
     
     [ObservableProperty] 
     private ObservableCollection<EmployeeDTO> employeeList = new ObservableCollection<EmployeeDTO>();

     public MainViewModel(EmployeeDbContext context)
     {
          _dbContext = context;
          MainThread.BeginInvokeOnMainThread(new Action( async()=> await Get()));
          WeakReferenceMessenger.Default.Register<EmployeeMessaging>(this, (r, m) =>
          {
               EmployeeRecibedMessage(m.Value);
          });
     }

     public async Task Get()
     {
          var list = await _dbContext.Employees.ToListAsync();
          if (list.Any())
          {
               foreach (var item in list)
               {
                   employeeList.Add(new EmployeeDTO
                   {
                        idEmployee = item.IdEmployee,
                        completeName = item.CompleteName,
                        email = item.Email,
                        salary = item.Salary,
                        ContractDate = item.ContractDate
                   });
               }
          }
          
     }

     private void EmployeeRecibedMessage(EmployeeMessage employeeMessage)
     {
          var employeeDto = employeeMessage.employeeDto;

          if (employeeMessage.isCreate)
          {
               EmployeeList.Add(employeeDto);
          }
          else
          {
               var findit = EmployeeList
                    .First(e => e.idEmployee == employeeDto.idEmployee);

               findit.CompleteName = employeeDto.completeName;
               findit.email = employeeDto.email;
               findit.salary = employeeDto.salary;
               findit.ContractDate = employeeDto.ContractDate;
          }
     }

     [RelayCommand]
     private async Task Create()
     {
          var uri = $"{nameof(EmployeePage)}?id=0";
          await Shell.Current.GoToAsync(uri);
          Console.WriteLine("Entro al boton");
     }

     [RelayCommand]
     private async Task Edit(EmployeeDTO employeeDto)
     {
          var uri = $"{nameof(EmployeePage)}?id={employeeDto.idEmployee}";
          await Shell.Current.GoToAsync(uri);
     }
     
     [RelayCommand]
     private async Task Delete(EmployeeDTO employeeDto)
     {
          bool answer = await Shell.Current.DisplayAlert("Mensaje", "Desea eliminar el empleado?", "SI", "NO");

          if (answer)
          {
               var findit = await _dbContext.Employees
                    .FirstAsync(e => e.IdEmployee == employeeDto.idEmployee);

               _dbContext.Employees.Remove(findit);
               await _dbContext.SaveChangesAsync();
               employeeList.Remove(employeeDto);
          }
          
     }
}