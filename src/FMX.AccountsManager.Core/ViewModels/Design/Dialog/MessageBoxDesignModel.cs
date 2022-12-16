namespace FMX.AccountsManager.Core.Design
{
    public class MessageBoxDesignModel
    {
        public static MessageBoxViewModel Instance => new()
        {
            Title = "Design Title",
            Message = "Design Message"
        };
    }
}
