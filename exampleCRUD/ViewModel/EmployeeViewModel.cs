using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using exampleCRUD.DataAccess;
using exampleCRUD.DTOs;
using exampleCRUD.Models;
using exampleCRUD.Utilities;
using Microsoft.EntityFrameworkCore;

namespace exampleCRUD.ViewModels;

/// <summary>
/// ViewModel for managing Employee data in a .NET MAUI application.
/// Implements IQueryAttributable to handle navigation parameters.
/// </summary>
public partial class EmployeeViewModel : ObservableObject, IQueryAttributable
{
    // Private fields
    private readonly EmployeeDbContext _dbContext; // Database context for accessing employee data
    private int EmployeeID; // Stores the current employee's ID
    
    /// <summary>
    /// Employee data transfer object (DTO) used for binding.
    /// </summary>
    [ObservableProperty]
    private EmployeeDTO employeeDto = new EmployeeDTO();

    /// <summary>
    /// Title of the page ("New Employee" or "Edit Employee").
    /// </summary>
    [ObservableProperty] private string pageTitle;

    /// <summary>
    /// Controls the visibility of a loading indicator.
    /// </summary>
    [ObservableProperty] private bool loadingIsVisible = false;

    /// <summary>
    /// Constructor that initializes the ViewModel with a database context.
    /// Sets the default contract date for a new employee.
    /// </summary>
    /// <param name="context">The database context instance.</param>
    public EmployeeViewModel(EmployeeDbContext context)
    {
        _dbContext = context;
        EmployeeDTO.contractDate = DateTime.Now;
    }
    
    /// <summary>
    /// Handles query attributes passed through navigation and loads employee data if editing an existing employee.
    /// </summary>
    /// <param name="query">Dictionary containing query parameters.</param>
    public async void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        var id = int.Parse(query["id"].ToString());
        EmployeeID = id;

        if (EmployeeID == 0)
        {
            PageTitle = "New Employee";
        }
        else
        {
            PageTitle = "Edit Employee";
            LoadingIsVisible = true;
            await Task.Run(async () =>
            {
                var foundit = await _dbContext.Employees.FirstAsync(e => e.IdEmployee == EmployeeID);
                employeeDto.IdEmployee = foundit.IdEmployee;
                employeeDto.CompleteName = foundit.CompleteName;
                employeeDto.Email = foundit.Email;
                employeeDto.Salary = foundit.Salary;
                employeeDto.ContractDate = foundit.ContractDate;
                
                MainThread.BeginInvokeOnMainThread(() => { LoadingIsVisible = false; });
            });
        }
    }
    
    /// <summary>
    /// Saves the employee data, either creating a new record or updating an existing one.
    /// </summary>
    [RelayCommand]
    private async Task Save()
    {
        LoadingIsVisible = true;
        EmployeeMessage message = new EmployeeMessage();

        await Task.Run(async () =>
        {
            if (EmployeeID == 0)
            {
                var tbEmployee = new Employee()
                {
                    CompleteName = EmployeeDto.CompleteName,
                    Email = EmployeeDto.Email,
                    Salary = EmployeeDto.Salary,
                    ContractDate = EmployeeDto.ContractDate,
                };

                _dbContext.Employees.Add(tbEmployee);
                await _dbContext.SaveChangesAsync();

                EmployeeDto.IdEmployee = tbEmployee.IdEmployee;
                message = new EmployeeMessage()
                {
                    isCreate = true,
                    employeeDto = EmployeeDto,
                };
            }
            else
            {
                var findit = await _dbContext.Employees.FirstAsync(e => e.IdEmployee == EmployeeID);
                findit.CompleteName = employeeDto.CompleteName;
                findit.Email = employeeDto.Email;
                findit.Salary = employeeDto.Salary;
                findit.ContractDate = employeeDto.ContractDate;
                
                await _dbContext.SaveChangesAsync();

                message = new EmployeeMessage()
                {
                    isCreate = false,
                    employeeDto = employeeDto,
                };
            }
            
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                LoadingIsVisible = false;
                WeakReferenceMessenger.Default.Send(new EmployeeMessaging(message));
                await Shell.Current.Navigation.PopAsync();
            });
        });
    }
}