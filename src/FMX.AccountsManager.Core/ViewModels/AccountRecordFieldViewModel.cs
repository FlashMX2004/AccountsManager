namespace FMX.AccountsManager.Core
{
    /// <summary>
    /// Record (item) of account record
    /// </summary>
    public class AccountRecordFieldViewModel : ViewModelBase
    {
        private string _label = "";

        /// <summary>
        /// Label of record
        /// </summary>
        public string Label
        {
            get => _label;
            set
            {
                // I don't really know why FodyWeavers doesn't invoke PropertyChanged,
                // but properties are updeted when ui is updated
                if (_label != value)
                {
                    _label = value;
                    InvokePropertyChanged(nameof(Label));
                }
            }
        }

        /// <summary>
        /// Value of record
        /// </summary>
        public string Value { get; set; } = "";
    }
}
