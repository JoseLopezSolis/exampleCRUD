using CommunityToolkit.Mvvm.Messaging.Messages;

namespace exampleCRUD.Utilities;

public class EmployeeMessaging : ValueChangedMessage<EmployeeMessage>
{
    public EmployeeMessaging(EmployeeMessage value) : base(value)
    {
        
    }
}