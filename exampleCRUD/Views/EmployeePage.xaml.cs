using exampleCRUD.ViewModels;

namespace exampleCRUD.Views;

public partial class EmployeePage : ContentPage
{
    public EmployeePage(EmployeeViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}