﻿namespace FMX.AccountsManager.Core.Design
{
    public class AccountRecordDesignModel
    {
        public static AccountRecordViewModel Instance => new(null)
        {
            Label = "Design Record Label",
            Fields = new()
            {
                AccountRecordFieldDesignModel.Instance,
                AccountRecordFieldDesignModel.Instance,
                AccountRecordFieldDesignModel.Instance,
            }
        };
    }
}
