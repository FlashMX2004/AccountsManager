﻿namespace FMX.PasswordManager
{
    /// <summary>
    /// Record in password block
    /// </summary>
    public class AccountRecordItemViewModel : ViewModelBase
    {
        /// <summary>
        /// Label of record
        /// </summary>
        public string Label { get; set; } = "";

        /// <summary>
        /// Value of record
        /// </summary>
        public string Value { get; set; } = "";
    }
}