using Microsoft.Win32;

namespace FMX.AccountsManagerTests
{
    public class RegistryRecordServiceTests
    {
        private const string ACCOUNT_RECORD_LABEL = "TEST_ACCOUNT_RECORD";

        private RegistryRecordService _recordService;

        [SetUp]
        public void Setup()
        {
            _recordService = new RegistryRecordService();
        }

        #region Account Records Managing Tests

        [Test]
        public void AccountRecordIsAdded_CreatedRegistryKeyIsNotNull()
        {
            // Assign
            string recordPath = RegistryRecordService.REGISTRY_PATH + '\\' + ACCOUNT_RECORD_LABEL;

            // Act
            _recordService.AddRecord(ACCOUNT_RECORD_LABEL);

            // Assert
            {
                using var created = RegistryRecordService.RegistryScope.OpenSubKey(recordPath);
                Assert.That(created, Is.Not.Null);
            }

            // Clean
            Registry.CurrentUser.DeleteSubKey(recordPath);
        }

        [Test]
        public void AccountRecordIsDeleted_RegistryKeyIsNull()
        {
            // Assign
            string recordPath = RegistryRecordService.REGISTRY_PATH + '\\' + ACCOUNT_RECORD_LABEL;

            // Act
            _recordService.AddRecord(ACCOUNT_RECORD_LABEL);
            _recordService.DeleteRecord(ACCOUNT_RECORD_LABEL);

            // Assert
            {
                using var key = RegistryRecordService.RegistryScope.OpenSubKey(recordPath);
                Assert.That(key, Is.Null);
            }
        }

        [Test]
        public void AccountRecordIsUpdated_LabelIsTEST_RegistryKeyNameEqualsToTEST()
        {
            // Assign
            string newRecordPath = RegistryRecordService.REGISTRY_PATH + '\\' + "TEST";

            // Act
            _recordService.AddRecord(ACCOUNT_RECORD_LABEL);
            _recordService.UpdateRecordLabel(ACCOUNT_RECORD_LABEL, "TEST");

            // Assert
            {
                using var key = RegistryRecordService.RegistryScope.OpenSubKey(newRecordPath);
                Assert.That(key, Is.Not.Null);
            }

            // Clean
            Registry.CurrentUser.DeleteSubKey(newRecordPath);
        }

        #endregion

        #region Account Record Items Managing Tests

        [Test]
        public void RecordFieldIsAdded_RegistryKeyValueIsNotNull()
        {
            // Assign
            string recordName = "TEST_ACC";
            string recordFieldName = "TEST_ACC_FIELD";

            // Act
            _recordService.AddRecord(recordName);
            _recordService.AddRecordField(recordName, recordFieldName);

            // Assert
            using var key = RegistryRecordService.RegistryScope.OpenSubKey(RegistryRecordService.GetRecordPath(recordName));
            Assert.That(key.GetValue(recordFieldName), Is.Not.Null);

            // Clean
            _recordService.DeleteRecord(recordName);
        }

        [Test]
        public void RecordFieldIsDeleted_RegistryKeyValueIsNull()
        {
            // Assign
            string recordName = "TEST_ACC";
            string recordFieldName = "TEST_ACC_FIELD";

            // Act
            _recordService.AddRecord(recordName);
            _recordService.AddRecordField(recordName, recordFieldName);
            _recordService.DeleteRecordField(recordName, recordFieldName);

            // Assert
            using var key = RegistryRecordService.RegistryScope.OpenSubKey(RegistryRecordService.GetRecordPath(recordName));
            Assert.That(key.GetValue(recordFieldName), Is.Null);

            // Clean
            _recordService.DeleteRecord(recordName);
        }

        [Test]
        public void RecordFieldLabelIsUpdated_RegistryKeyValueNameIsNEWLABEL()
        {
            // Assign
            string recordName = "TEST_ACC";
            string recordFieldName = "TEST_ACC_FIELD";
            string newLabel = "NEWLABEL";

            // Act
            _recordService.AddRecord(recordName);
            _recordService.AddRecordField(recordName, recordFieldName);
            _recordService.UpdateRecordFieldLabel(recordName, recordFieldName, newLabel);

            // Assert
            using var key = RegistryRecordService.RegistryScope.OpenSubKey(RegistryRecordService.GetRecordPath(recordName));
            Assert.That(key.GetValue(newLabel), Is.Not.Null);

            // Clean
            _recordService.DeleteRecord(recordName);
        }

        [Test]
        public void RecordFieldValueIsUpdated_RegistryKeyValueNameIsNEWVALUE()
        {
            // Assign
            string recordName = "TEST_ACC";
            string recordFieldName = "TEST_ACC_FIELD";
            string newValue = "NEWVALUE";

            // Act
            _recordService.AddRecord(recordName);
            _recordService.AddRecordField(recordName, recordFieldName);
            _recordService.UpdateRecordFieldValue(recordName, recordFieldName, newValue);

            // Assert
            using var key = RegistryRecordService.RegistryScope.OpenSubKey(RegistryRecordService.GetRecordPath(recordName));
            Assert.That(key.GetValue(recordFieldName), Is.EqualTo(newValue));

            // Clean
            _recordService.DeleteRecord(recordName);
        }

        #endregion

        #region All Account Records Managing Tests

        [Test]
        public void AccountRecordsUpdated_CreatedKeysEqualExpected()
        {
            // Assign
            var backup = _recordService.GetAllRecords();
            string record1 = "Account1";
            string record2 = "Account2";
            List<AccountRecordViewModel> records = new()
            {
                new AccountRecordViewModel
                {
                    Label = record1,
                    Fields = new()
                    {
                        new AccountRecordFieldViewModel { Label = "Username", Value = "Username1" },
                        new AccountRecordFieldViewModel { Label = "Email", Value = "Email1" },
                        new AccountRecordFieldViewModel { Label = "Password", Value = "Password1" },
                    }
                },
                new AccountRecordViewModel
                {
                    Label = record2,
                    Fields = new()
                    {
                        new AccountRecordFieldViewModel { Label = "Email", Value = "Email2" },
                        new AccountRecordFieldViewModel { Label = "Password", Value = "Password2" },
                    }
                }
            };

            // Act
            _recordService.UpdateAllRecords(records);

            // Assert
            using (RegistryKey?
                    acc1Key = RegistryRecordService.RegistryScope.OpenSubKey(RegistryRecordService.GetRecordPath(record1)),
                    acc2Key = RegistryRecordService.RegistryScope.OpenSubKey(RegistryRecordService.GetRecordPath(record2)))
            {
                Assert.Multiple(() =>
                {
                    // Assert account1 record
                    Assert.That(acc1Key?.GetValue("Username"), Is.EqualTo("Username1"));
                    Assert.That(acc1Key?.GetValue("Email"), Is.EqualTo("Email1"));
                    Assert.That(acc1Key?.GetValue("Password"), Is.EqualTo("Password1"));

                    // Assert account2 record
                    Assert.That(acc2Key?.GetValue("Email"), Is.EqualTo("Email2"));
                    Assert.That(acc2Key?.GetValue("Password"), Is.EqualTo("Password2"));
                });
            }

            // Backup
            _recordService.UpdateAllRecords(backup);
        }

        #endregion

    }
}