namespace FMX.AccountsManager.Core.Design
{
    internal class RecordDesignService : IRecordService
    {
        public IEnumerable<AccountRecordViewModel> GetAllRecords() => new List<AccountRecordViewModel>();

        public void AddRecord(string newLabel)
        {
            throw new NotImplementedException();
        }

        public void AddRecordField(string recordLabel, string newRecordField)
        {
            throw new NotImplementedException();
        }

        public void DeleteRecord(string recordLabel)
        {
            throw new NotImplementedException();
        }

        public void DeleteRecordField(string recordLabel, string fieldLabel)
        {
            throw new NotImplementedException();
        }

        public void UpdateAllRecords(IEnumerable<AccountRecordViewModel> records)
        {
            throw new NotImplementedException();
        }

        public void UpdateRecordFieldLabel(string recordLabel, string fieldLabel, string newFieldLabel)
        {
            throw new NotImplementedException();
        }

        public void UpdateRecordFieldValue(string recordLabel, string fieldLabel, string newFieldValue)
        {
            throw new NotImplementedException();
        }

        public void UpdateRecordLabel(string recordLabel, string newLabel)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AccountRecordViewModel> FilterRecords(string filter)
        {
            throw new NotImplementedException();
        }
    }
}
