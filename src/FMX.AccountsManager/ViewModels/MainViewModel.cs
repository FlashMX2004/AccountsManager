﻿using System.Collections.ObjectModel;

namespace FMX.PasswordManager
{
    public class MainViewModel : ViewModelBase
    {
        /// <summary>
        /// Search filter value
        /// </summary>
        public string SearchFilter { get; set; } = "";

        /// <summary>
        /// Password block collection
        /// </summary>
        public ObservableCollection<AccoundRecordViewModel>? Records { get; set; }

        public MainViewModel(IRecordService recordService)
        {

        }
    }
}