using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;

namespace ShMI.BaseMain
{
    public partial class EntitiesDb : DbContext
    {
        public EntitiesDb(string nameOrConnectionString) : base(nameOrConnectionString) { }

        public void SaveTestTable(TestTable Item)
        {
            try
            {
                if (Item.Id == 0)
                {
                    _ = TestTables.Add(Item);
                    _ = SaveChanges();
                }
                else
                {
                    TestTable T = TestTables.FirstOrDefault(s => s.Id == Item.Id);
                    if (T.ThisNotNull())
                    {
                        T.name = Item.name;
                        _ = SaveChanges();
                    }
                }
            }
            catch (Exception er)
            {
                string.Format("\r\nMessage\r\n{0}\r\nGetType\r\n{1}\r\nStackTrace\r\n{2}\r\nInnerException\r\n{3}", er.Message, er.GetType(), er.StackTrace, er.InnerException);
            }
        }
        public void DeleteTestTable(TestTable Item)
        {
            if (Item.ThisNotNull() && Item.Id != 0)
            {
                TestTable T = TestTables.FirstOrDefault(s => s.Id == Item.Id);
                if (T != null)
                {
                    _ = TestTables.Remove(T);
                }
                _ = SaveChanges();
            }
        }

        public void SaveNObject(NObject Item)
        {
            try
            {
                NObject NObjectStart = NObjects.FirstOrDefault(s => s.SiteID == "-1" && s.Name_Object == "-1");
                //Первый раз. Ничего нет.
                if (NObjectStart.ThisNotNull())
                {
                    NObjectStart.ChangeValueItem(Item);
                }
                else
                {
                    if (Item.Id == Guid.Empty)
                    {
                        //Проверка по "Код объекта"
                        NObject itemOld = NObjects.FirstOrDefault(s => s.SiteID == Item.SiteID);
                        if (itemOld.ThisNotNull())
                        {
                            itemOld.ChangeValueItem(Item);
                        }
                        else
                        {
                            _ = NObjects.Add(new NObject(Item));
                        }
                    }
                    else
                    {
                        NObjects.FirstOrDefault(s => s.Id == Item.Id).ChangeValueItem(Item);
                    }
                }
                _ = SaveChanges();
            }
            catch (Exception er)
            {
                string.Format("\r\nMessage\r\n{0}\r\nGetType\r\n{1}\r\nStackTrace\r\n{2}\r\nInnerException\r\n{3}", er.Message, er.GetType(), er.StackTrace, er.InnerException);
            }
        }
        public void DeleteNObject(NObject Item)
        {
            if (Item.ThisNotNull() && Item.Id != Guid.Empty)
            {
                NObject T = NObjects.FirstOrDefault(s => s.Id == Item.Id);
                if (T != null)
                {
                    _ = NObjects.Remove(T);
                }
                _ = SaveChanges();
            }
        }

        public void SaveReCodesTable(ReCodesTable Item)
        {
            try
            {
                if (Item.Id == Guid.Empty)
                {
                    ReCodesTable itemOld = ReCodesTables.FirstOrDefault(s => s.GlobalCode == Item.GlobalCode);
                    if (itemOld.ThisNotNull())
                    {
                        itemOld.ChangeValueItem(Item);
                    }
                    else
                    {
                        _ = ReCodesTables.Add(new ReCodesTable(Item));
                    }
                }
                else
                {
                    ReCodesTables.FirstOrDefault(s => s.Id == Item.Id).ChangeValueItem(Item);
                }
                _ = SaveChanges();
            }
            catch (Exception er)
            {
                string.Format("\r\nMessage\r\n{0}\r\nGetType\r\n{1}\r\nStackTrace\r\n{2}\r\nInnerException\r\n{3}", er.Message, er.GetType(), er.StackTrace, er.InnerException);
            }
        }
        public void DeleteReCodesTable(ReCodesTable Item)
        {
            if (Item.ThisNotNull() && Item.Id != Guid.Empty)
            {
                ReCodesTable T = ReCodesTables.FirstOrDefault(s => s.Id == Item.Id);
                if (T != null)
                {
                    _ = ReCodesTables.Remove(T);
                }
                _ = SaveChanges();
            }
        }

        public void SaveNCassa(NCassa Item)
        {
            try
            {
                if (Item.Id == Guid.Empty)
                {
                    NCassa itemOld = NCassas.FirstOrDefault(s => s.Name == Item.Name);
                    if (itemOld.ThisNotNull())
                    {
                        itemOld.ChangeValueItem(Item);
                    }
                    else
                    {
                        //Первый раз. Ничего нет.
                        if (Item.NObjectId == Guid.Empty)
                        {
                            SaveNObject(new NObject()
                            {
                                Name_Object = "-1",
                                SiteID = "-1",
                                Address = "000",
                            });
                            Item.NObjectId = NObjects.FirstOrDefault(s => s.SiteID == "-1" && s.Name_Object == "-1").Id;
                            Item.IP = "127.0.0.1";
                            Item.Port = 8080;
                        }
                        _ = NCassas.Add(new NCassa(Item));
                    }
                }
                else
                {
                    NCassas.FirstOrDefault(s => s.Id == Item.Id).ChangeValueItem(Item);
                }
                _ = SaveChanges();
            }
            catch (Exception er)
            {
                string.Format("\r\nMessage\r\n{0}\r\nGetType\r\n{1}\r\nStackTrace\r\n{2}\r\nInnerException\r\n{3}", er.Message, er.GetType(), er.StackTrace, er.InnerException);
            }
        }
        public void DeleteNCassa(NCassa Item)
        {
            if (Item.ThisNotNull() && Item.Id != Guid.Empty)
            {
                NCassa T = NCassas.FirstOrDefault(s => s.Id == Item.Id);
                if (T != null)
                {
                    _ = NCassas.Remove(T);
                }
                _ = SaveChanges();
            }
        }

        public void SaveNStruna(NStruna Item)
        {
            try
            {
                if (Item.Id == Guid.Empty)
                {
                    NStruna itemOld = NStrunas.FirstOrDefault(s => s.Name == Item.Name);
                    if (itemOld.ThisNotNull())
                    {
                        itemOld.ChangeValueItem(Item);
                    }
                    else
                    {
                        _ = NStrunas.Add(new NStruna(Item));
                    }
                }
                else
                {
                    NStrunas.FirstOrDefault(s => s.Id == Item.Id).ChangeValueItem(Item);
                }
                _ = SaveChanges();
            }
            catch (Exception er)
            {
                string.Format("\r\nMessage\r\n{0}\r\nGetType\r\n{1}\r\nStackTrace\r\n{2}\r\nInnerException\r\n{3}", er.Message, er.GetType(), er.StackTrace, er.InnerException);
            }
        }
        public void DeleteNStruna(NStruna Item)
        {
            if (Item.ThisNotNull() && Item.Id != Guid.Empty)
            {
                NStruna T = NStrunas.FirstOrDefault(s => s.Id == Item.Id);
                if (T != null)
                {
                    _ = NStrunas.Remove(T);
                }
                _ = SaveChanges();
            }
        }

        public void SaveNTank(NTank Item)
        {
            try
            {
                if (Item.Id == Guid.Empty)
                {
                    NTank itemOld = NTanks.FirstOrDefault(s => s.TankNumber == Item.TankNumber && s.NStrunaId == Item.NStrunaId);
                    if (itemOld.ThisNotNull())
                    {
                        itemOld.ChangeValueItem(Item);
                    }
                    else
                    {
                        var t = new NTank(Item);
                        _ = NTanks.Add(new NTank(Item));
                    }
                }
                else
                {
                    NTanks.FirstOrDefault(s => s.Id == Item.Id).ChangeValueItem(Item);
                }
                _ = SaveChanges();
            }
            catch (Exception er)
            {
                string.Format("\r\nMessage\r\n{0}\r\nGetType\r\n{1}\r\nStackTrace\r\n{2}\r\nInnerException\r\n{3}", er.Message, er.GetType(), er.StackTrace, er.InnerException);
            }
        }
        public void DeleteNTank(NTank Item)
        {
            if (Item.ThisNotNull() && Item.Id != Guid.Empty)
            {
                NTank T = NTanks.FirstOrDefault(s => s.Id == Item.Id);
                if (T != null)
                {
                    _ = NTanks.Remove(T);
                }
                _ = SaveChanges();
            }
        }

        public void SaveTask_Device(Task_Device Item)
        {
            Task_Device CurrentItem = Item;
            try
            {
                if (Item.Id == Guid.Empty)
                {
                    Task_Device itemOld = Task_Device.FirstOrDefault(s => s.Name_Task == Item.Name_Task && s.NObjectId == Item.NObjectId && s.Type_Device == Item.Type_Device);
                    if (itemOld.ThisNotNull())
                    {
                        itemOld.ChangeValueItem(Item);
                    }
                    else
                    {
                        //var t = new Task_Device(Item);
                        _ = Task_Device.Add(new Task_Device(Item));
                    }
                }
                else
                {
                    Task_Device.FirstOrDefault(s => s.Id == Item.Id).ChangeValueItem(CurrentItem);
                }
                _ = SaveChanges();
            }
            catch (Exception er)
            {
                string mes = string.Format("\r\nMessage\r\n{0}\r\nGetType\r\n{1}\r\nStackTrace\r\n{2}\r\nInnerException\r\n{3}", er.Message, er.GetType(), er.StackTrace, er.InnerException);
                throw new Exception(mes);
            }
        }
        public void DeleteTask_Device(Task_Device Item)
        {
            if (Item.ThisNotNull() && Item.Id != Guid.Empty)
            {
                Task_Device T = Task_Device.FirstOrDefault(s => s.Id == Item.Id);
                if (T != null)
                {
                    _ = Task_Device.Remove(T);
                }
                _ = SaveChanges();
            }
        }
        public void ValidTask_Device(Task_Device Item)
        {
            //ecть раписание на этом объекте

            Task_Device itemOld = Task_Device.FirstOrDefault(s => s.NObjectId == Item.NObjectId && s.Type_Device == Item.Type_Device);
            if (Item.Id == Guid.Empty && itemOld.ThisNotNull())
            {
                string mes = string.Format("\r\nРасписание \"{0}\" для объекта \"{1}\" уже существует.", Item.Type_Device, Item.NObject?.Name_Object);
                throw new Exception(mes);
            }

            if (Item.Id != Guid.Empty && itemOld.ThisNotNull() && Item.Id != itemOld.Id)
            {
                string mes = string.Format("\r\nРасписание \"{0}\" для объекта \"{1}\" уже существует.", Item.Type_Device, Item.NObject?.Name_Object);
                throw new Exception(mes);
            }

        }


    }

    public class MainTable : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged Members
        public string MChangeProperty
        {
            set
            {
                try
                {
                    NotifyPropertyChanged(value);
                }
                catch
                {
                }
            }
            get => "";
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged(string propertyName)
        {
            Console.WriteLine($"MainN {propertyName}");
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        public string GetNameItem(string typeCassa)
        {
            int i = 1;
            string nameItem = typeCassa.ToUpper();
            using (EntitiesDb db = ModuleBaseMain.GetDb)
            {
                switch (nameItem)
                {
                    case "TASK":
                        {
                            foreach (Task_Device item in db.Task_Device.ToList())
                            {
                                if (int.TryParse(item.Name_Task.Replace(nameItem, ""), out int t))
                                {
                                    if (i < t)
                                    {
                                        i = t;
                                        i++;
                                    }
                                    else if (i == t)
                                    {
                                        i++;
                                    }
                                }
                            }
                            break;
                        }
                    default: break;
                }
                foreach (NCassa item in db.NCassas.ToList())
                {
                    if (int.TryParse(item.Name.Replace(nameItem, ""), out int t))
                    {
                        if (i < t)
                        {
                            i = t;
                            i++;
                        }
                        else if (i == t)
                        {
                            i++;
                        }
                    }
                }
            }
            return string.Format("{0}{1}", nameItem, i);
        }

        public string zip_Dir_Def = @"C:\ATD\ATD_DATA";
        public string Zip_Dir_Def
        {
            get => zip_Dir_Def;
            set
            {
                zip_Dir_Def = value;
                MChangeProperty = "Zip_Dir_Def";
            }
        }
    }

    public partial class TestTable : MainTable
    {
        public TestTable()
        {

        }
    }

    public partial class NObject : MainTable, IDataErrorInfo
    {
        public NObject(NObject item)
        {
            Id = item.Id == Guid.Empty ? Guid.NewGuid() : item.Id;
            //Имя объекта
            Name_Object = item.Name_Object;
            //Код объекта
            SiteID = item.SiteID;
            //Адрес объекта
            Address = item.Address;
        }

        public void ChangeValueItem(NObject item)
        {
            Name_Object = item.Name_Object;
            SiteID = item.SiteID;
            Address = item.Address;
        }

        public string this[string columnName]
        {
            get
            {
                string error = string.Empty;
                switch (columnName)
                {
                    case "SiteID":
                        if (string.IsNullOrEmpty(SiteID))
                        {
                            error = "\"Код объекта\" - обязательное поле.";
                        }
                        break;
                    case "Name_Object":
                        if (string.IsNullOrEmpty(Name_Object))

                        {
                            error = "\"Имя объекта\" - обязательное поле.";
                        }
                        break;
                    default:
                        break;
                }
                return error;
            }
        }
        public string Error => null;
    }

    public partial class ReCodesTable : MainTable, IDataErrorInfo
    {
        public ReCodesTable() { }
        public ReCodesTable(ReCodesTable item)
        {
            Id = item.Id == Guid.Empty ? Guid.NewGuid() : item.Id;
            GlobalCode = item.GlobalCode;
            LocalCode = item.LocalCode;
        }

        public void ChangeValueItem(ReCodesTable item)
        {
            GlobalCode = item.GlobalCode;
            LocalCode = item.LocalCode;
        }

        public string this[string columnName]
        {
            get
            {
                string error = string.Empty;
                switch (columnName)
                {
                    case "LocalCode":
                        if (string.IsNullOrEmpty(LocalCode) || !int.TryParse(LocalCode, out int r))
                        {
                            error = "\"Локальный код\" - обязательное поле.";
                        }
                        break;
                    case "GlobalCode":
                        if (string.IsNullOrEmpty(GlobalCode) || !int.TryParse(GlobalCode, out int t))
                        {
                            error = "\"Глобальный код\" - обязательное поле.";
                        }
                        break;
                    default:
                        break;
                }
                return error;
            }
        }
        //
        public string Error => null;

    }

    public partial class NCassa : MainTable, IDataErrorInfo
    {
        public NCassa(NCassa item)
        {
            Id = item.Id == Guid.Empty ? Guid.NewGuid() : item.Id;
            IP = item.IP;
            Port = item.Port;
            Name = item.Name;
            DateStart = item.DateStart;
            NObjectId = item.NObjectId;
            LastID = item.LastID;
            CountReadLine = item.CountReadLine == 0 ? 200 : item.CountReadLine;
            NCassa_Active = item.NCassa_Active;
            Cash_type = item.Cash_type;
            NObjectId_Name = item.NObjectId_Name;
        }

        public NCassa(string TypeName)
        {
            Name = GetNameItem(TypeName);
            //NObjectId = CurrentNObject == null ? Guid.Empty : CurrentNObject.Id;
        }

        public NObject InitObject()
        {
            using (EntitiesDb db = ModuleBaseMain.GetDb)
            {
                return NObjectId == Guid.Empty ? db.NObjects.FirstOrDefault() : db.NObjects.FirstOrDefault(s => s.Id == NObjectId);
            }
        }

        public NObject CurrentNObject => InitObject();

        public void ChangeValueItem(NCassa item)
        {
            IP = item.IP;
            Port = item.Port;
            Name = item.Name;
            DateStart = item.DateStart;
            NObjectId = item.NObjectId;
            LastID = item.LastID;
            CountReadLine = item.CountReadLine == 0 ? 200 : item.CountReadLine;
            NCassa_Active = item.NCassa_Active;
            Cash_type = item.Cash_type;
            NObjectId_Name = item.NObjectId_Name;
        }

        public string AddressDevice => string.Format("http://{0}:{1}/", IP, Port);

        public string this[string columnName]
        {
            get
            {
                string error = string.Empty;
                switch (columnName)
                {
                    case "IP":
                        if (string.IsNullOrEmpty(IP))
                        {
                            error = "\"IP\" - обязательное поле.";
                        }
                        break;
                    case "Port":
                        if (Port < 0)
                        {
                            error = "\"Port\" - обязательное поле.";
                        }
                        break;
                    default:
                        break;
                }
                return error;
            }
        }
        public string Error => null;

    }

    public partial class NStruna : MainTable
    {
        public NStruna(NStruna item)
        {
            Id = item.Id == Guid.Empty ? Guid.NewGuid() : item.Id;
            Name = item.Name;
            NObjectId = item.NObjectId;
            NameBatFile = item.NameBatFile;
            Type_Level = item.Type_Level;
        }

        public NStruna(string TypeName)
        {
            Name = GetNameItem(TypeName);
            NObjectId = CurrentNObject == null ? Guid.Empty : CurrentNObject.Id;
        }

        public string GetNameItem(string typeCassa)
        {
            int i = 1;
            string nameItem = typeCassa.ToUpper();
            using (EntitiesDb db = ModuleBaseMain.GetDb)
            {
                foreach (NStruna item in db.NStrunas.ToList())
                {
                    if (int.TryParse(item.Name.Replace(nameItem, ""), out int t))
                    {
                        if (i < t)
                        {
                            i = t;
                            i++;
                        }
                        else if (i == t)
                        {
                            i++;
                        }
                    }
                }
            }
            return string.Format("{0}{1}", nameItem, i);
        }

        public NObject InitObject()
        {
            using (EntitiesDb db = ModuleBaseMain.GetDb)
            {
                return NObjectId == Guid.Empty ? db.NObjects.FirstOrDefault() : db.NObjects.FirstOrDefault(s => s.Id == NObjectId);
            }
        }

        public NObject CurrentNObject => InitObject();

        public void ChangeValueItem(NStruna item)
        {
            Name = item.Name;
            NObjectId = item.NObjectId;
            NameBatFile = item.NameBatFile;
            Type_Level = item.Type_Level;
        }

        public static NStruna Deserialize(string strJson)
        {
            return JsonConvert.DeserializeObject<NStruna>(strJson);
        }

        public static List<NStruna> DeserializeList(string strJson)
        {
            return JsonConvert.DeserializeObject<List<NStruna>>(strJson);
        }
    }

    public partial class NTank : MainTable
    {
        public NTank(NTank item)
        {
            Id = item.Id == Guid.Empty ? Guid.NewGuid() : item.Id;

            TankNumber = item.TankNumber;
            Grade = item.Grade;
            Grade_Name = item.Grade_Name;
            NStrunaId = item.NStrunaId;
            Max_Volume = item.Max_Volume;
            WaterLevel_Allow = item.WaterLevel_Allow;
            Level_alarm = item.Level_alarm;
            Water_alarm = item.Water_alarm;
        }

        public NStruna InitStruna()
        {
            using (EntitiesDb db = ModuleBaseMain.GetDb)
            {
                return db.NStrunas.FirstOrDefault(s => s.Id == NStrunaId);
            }
        }

        public NStruna CurrentNStruna => InitStruna();

        public NObject InitObject()
        {
            using (EntitiesDb db = ModuleBaseMain.GetDb)
            {
                return db.NObjects.FirstOrDefault(s => s.Id == CurrentNStruna.NObjectId);
            }
        }

        public NObject CurrentNObject => InitObject();

        public void ChangeValueItem(NTank item)
        {
            TankNumber = item.TankNumber;
            Grade = item.Grade;
            Grade_Name = item.Grade_Name;
            NStrunaId = item.NStrunaId;
            Max_Volume = item.Max_Volume;
            WaterLevel_Allow = item.WaterLevel_Allow;
            Level_alarm = item.Level_alarm;
            Water_alarm = item.Water_alarm;
        }
    }

    public partial class Task_Device : MainTable, IDataErrorInfo
    {
        public Task_Device() { }

        public Task_Device(Task_Device item)
        {
            Id = item.Id == Guid.Empty ? Guid.NewGuid() : item.Id;
            Date_LastLine_Struna = item.Date_LastLine_Struna == new DateTime() ? null : item.Date_LastLine_Struna;
            Date_LastLine_Cassa = item.Date_LastLine_Cassa == new DateTime() ? null : item.Date_LastLine_Cassa;
            Name_Task = item.Name_Task;
            Period_Task = item.Period_Task;
            Type_Device = item.Type_Device;
            Zip_Dir = item.Zip_Dir;
            NObjectId = item.NObjectId;
            CountDaySave = Type_Device == "zip" || Type_Device == "MSG_Water" || Type_Device == "MSG_Fuel"
                ? 0
                : (item.CountDaySave > 10) ? 10 : item.CountDaySave;


            Period_Ping = item.Period_Ping;
            Is_TaskOk = item.Is_TaskOk;
            Status = item.Status;
            Type_Task = item.Type_Task;

            //StatusMsg = item.StatusMsg;
            //NObject = item.NObject;
        }

        public Task_Device(string TypeName)
        {
            Name_Task = GetNameItem(TypeName);
            //NObjectId = CurrentNObject == null ? Guid.Empty : CurrentNObject.Id;

            InitListType_Device();
            Type_Device = "zip";
            Zip_Dir = Zip_Dir_Def;
        }
        public void InitListType_Device()
        {
            ListType_Device = new List<string>{
                "def",
                "zip",
                "auditarch",
                "level",
                "cassa",
                "th",
                "MSG_Water",
                "MSG_Fuel",
            };
            //Status = "GetObj, GetFuel, GetCashDesk, GetTank, GetSched";
        }

        public List<string> ListType_Device
        {
            get; set;
        }


        public void ChangeValueItem(Task_Device item)
        {
            Date_LastLine_Struna = item.Date_LastLine_Struna == new DateTime() ? null : item.Date_LastLine_Struna;
            Date_LastLine_Cassa = item.Date_LastLine_Cassa == new DateTime() ? null : item.Date_LastLine_Cassa;
            Name_Task = item.Name_Task;
            Period_Task = item.Period_Task;
            Type_Device = item.Type_Device;
            Zip_Dir = item.Zip_Dir;
            NObjectId = item.NObjectId;
            CountDaySave = item.CountDaySave;
            Period_Ping = item.Period_Ping;
            Is_TaskOk = item.Is_TaskOk;
            Status = item.Status;
            Type_Task = item.Type_Task;
        }


        public TimeSpan TimeSpan_EquipmentPeriodicity
        {
            get
            {
                long tik = Period_Task;
                tik *= 10000000;
                return new TimeSpan(tik);
            }
            set => MChangeProperty = "Period";
        }
        public string String_EquipmentPeriodicity
        {
            get => string.Format("{0} {1}:{2}:{3}",
                    TimeSpan_EquipmentPeriodicity.Days, TimeSpan_EquipmentPeriodicity.Hours,
                    TimeSpan_EquipmentPeriodicity.Minutes, TimeSpan_EquipmentPeriodicity.Seconds);
            set { }
        }

        public string this[string columnName]
        {
            get
            {
                string error = string.Empty;
                //switch (columnName)
                //{
                //    case "LocalCode":
                //        if (string.IsNullOrEmpty(LocalCode) || !int.TryParse(LocalCode, out int r))
                //        {
                //            error = "\"Локальный код\" - обязательное поле.";
                //        }
                //        break;
                //    case "GlobalCode":
                //        if (string.IsNullOrEmpty(GlobalCode) || !int.TryParse(GlobalCode, out int t))
                //        {
                //            error = "\"Глобальный код\" - обязательное поле.";
                //        }
                //        break;
                //    default:
                //        break;
                //}
                return error;
            }
        }
        //
        public string Error => null;
    }
}
