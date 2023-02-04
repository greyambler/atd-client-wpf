#define _IMPORTLOCAL // импорт из локальной строки адрес не нужен
//#define _IPLOCAL       // импорт с нужного IP и Port без привязко к CassaTH
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


namespace ShMI.BaseMain
{
    public class ModuleBaseMain : INotifyPropertyChanged
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
                catch (Exception er)
                {
                    _ = GetMessageError(er);
                }
            }
            get => "";
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged(string propertyName)
        {
            Console.WriteLine($"propertyName \t\t\t = {propertyName}");
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public string GetMessageError(Exception er)
        {
            return string.Format("\r\nMessage\r\n{0}\r\nGetType\r\n{1}\r\nStackTrace\r\n{2}\r\nInnerException\r\n{3}", er.Message, er.GetType(), er.StackTrace, er.InnerException);
        }

        #endregion

        #region UcBottomPanel uc_HeadrPanel

        public static string NumberBuild_Stat => Assembly.GetExecutingAssembly().GetName().Version.ToString();
        public string NameSolution
        {
            get
            {
                Assembly assembly = Assembly.GetExecutingAssembly();
                object[] attributes =
                    assembly.GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
                if (attributes.Length > 0)
                {
                    AssemblyDescriptionAttribute descriptionAttribute =
                        (AssemblyDescriptionAttribute)attributes[0];
                    return descriptionAttribute.Description;
                }
                else
                {
                    return "";
                }
            }
        }
        public string NameTerminal => Dns.GetHostName();
        public string NumberVersion => NumberBuild.Substring(0, NumberBuild.IndexOf('.'));
        public string NumberBuild => Assembly.GetExecutingAssembly().GetName().Version.ToString();
        public string ServerName_Curent
        {
            get; set;
        }
        public string DataBaseName_Curent
        {
            get; set;
        }

        public static string Base_Dir => AppDomain.CurrentDomain.BaseDirectory;

        #endregion UcBottomPanel uc_HeadrPanel

        #region Работа с сервисом
        public async void ReStartService()
        {
            if (IsExist_Servise())
            {
                string scCommand = "sc.exe";
                string stop = $"stop {Properties.Resources.ServiceName}";
                Process rStop = Process.Start(scCommand, stop);
                rStop.WaitForExit();

                await Task.Delay(30000);

                string start = $"start {Properties.Resources.ServiceName}";
                Process rStart = Process.Start(scCommand, start);
                rStart.WaitForExit();
            }

        }

        public bool IsExist_Servise()
        {
            Microsoft.Win32.RegistryKey myRegKey = Microsoft.Win32.Registry.LocalMachine;
            myRegKey = myRegKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services");
            return myRegKey.GetSubKeyNames().Contains(Properties.Resources.ServiceName);// ("AATD_SSClient");
        }
        #endregion Работа с сервисом

        #region Db
        public static string File_DB => "DbATD.sdf";
        public static string Path_DB =>
#if _DEBUG // D:\MyProjects\C#\DB 
        @"D:\MyProjects\C#\DB";
#else
            Base_Dir;
#endif

        public static string PathToDb => Path.Combine(Path_DB, File_DB);
        private static string NameDB => "ModelDb";
        private static string Get_ConnectionString(string db_Server, string userPassword, string Model = "Model1")
        {
            return string.Format(
                "metadata=res://*/{0}.csdl|res://*/{0}.ssdl|res://*/{0}.msl;provider=System.Data.SqlServerCe.4.0;provider connection string=\"Data Source={2};Password={1}\""
                , Model, userPassword, db_Server);
        }
        private static string MGetLocalMy => Utils.ShMI_Decrypt(Properties.Resources.GetLocalMy);

        public static EntitiesDb GetDb
        {
            get
            {
                string con = Get_ConnectionString(
                    db_Server: PathToDb,
                    userPassword: MGetLocalMy
                    , Model: NameDB
                    );
                return new EntitiesDb(con);
            }
        }

        #endregion Db

        #region проверка существования
        //public bool IsCassaExist(NCassa _Cassa_Test)
        //{
        //    if (IsPingEnter(_Cassa_Test?.IP))//проверка PING
        //    {
        //        if (IsPingPortEnter(_Cassa_Test.IP, _Cassa_Test.Port))//проверка Port
        //        {
        //            return true;
        //        }
        //    }
        //    return false;
        //}

        public bool IsCassaExist(NCassa nCassa)
        {
            try
            {
                IsPingEnter(nCassa.IP);
                IsPingPortEnter(nCassa.IP, nCassa.Port);
                return true;
            }
            catch
            {
                return false;
            }
        }
        private void IsPingEnter(string ip)
        {
            using (System.Net.NetworkInformation.Ping ping = new System.Net.NetworkInformation.Ping())
            {
                if (!string.IsNullOrEmpty(ip))
                {
                    System.Net.NetworkInformation.PingReply reply = ping.Send(ip, 1);
                    //// если нет ответа
                    if (reply.Status != System.Net.NetworkInformation.IPStatus.Success)
                    {
                        throw new Exception($"PING по адресу {ip} не прошел!");
                    }
                }
                else
                {
                    throw new Exception($"Адрес устройства пуст!");
                }
            }
        }
        private void IsPingPortEnter(string ip, int port)
        {
            using (System.Net.Sockets.TcpClient tcpClient = new System.Net.Sockets.TcpClient())
            {
                try
                {
                    tcpClient.Connect(ip, port);
                }
                catch (Exception er)
                {
                    throw new Exception(er.Message);
                }
            }
        }

        #endregion проверка существования

        #region Получить данные из внешних источников
        /*
         27.01.2023 14:23:53 0109
        [{"Name_Object":"АЗС 1","SiteID":"1","Address":"077"}]

        27.01.2023 14:23:53 7928
        [{"GlobalCode":"100001","LocalCode":"3"},
        { "GlobalCode":"100002","LocalCode":"2"},
        { "GlobalCode":"100005","LocalCode":"10"},
        { "GlobalCode":"100012","LocalCode":"18"},
        { "GlobalCode":"107305","LocalCode":"9"},
        { "GlobalCode":"107306","LocalCode":"12"},
        { "GlobalCode":"107307","LocalCode":"7"}]

        27.01.2023 14:23:54 1611
        [{ "IP":"172.23.16.238:8000","Name":"49","NObjectId_Name":"1","cash_type":"0"},
        { "IP":"172.23.16.239:8000","Name":"0","NObjectId_Name":"1","cash_type":"0"},
        { "IP":"192.23.16.237:8000","Name":"16","NObjectId_Name":"1","cash_type":"2"},
        { "IP":"192.23.16.238:8000","Name":"43","NObjectId_Name":"1","cash_type":"0"}]

        27.01.2023 14:23:56 0300
        [{ "TankNumber":"1","Grade":"10","Grade_Name":"ДТ","Max_volume":"10000","WaterLevel_Allow":"100","water_alarm":"enable","level_alarm":"enable"},
        { "TankNumber":"2","Grade":"12","Grade_Name":"АИ-95","Max_volume":"4000","WaterLevel_Allow":"100","water_alarm":"enable","level_alarm":"enable"},
        { "TankNumber":"3","Grade":"7","Grade_Name":"АИ-98","Max_volume":"10000","WaterLevel_Allow":"100","water_alarm":"enable","level_alarm":"enable"},
        { "TankNumber":"4","Grade":"3","Grade_Name":"АИ-92 П","Max_volume":"10000","WaterLevel_Allow":"100","water_alarm":"enable","level_alarm":"enable"},
        { "TankNumber":"5","Grade":"3","Grade_Name":"АИ-92 П","Max_volume":"10000","WaterLevel_Allow":"100","water_alarm":"enable","level_alarm":"enable"},
        { "TankNumber":"7","Grade":"18","Grade_Name":"СУГ","Max_volume":"100000","WaterLevel_Allow":"100","water_alarm":"enable","level_alarm":"enable"}]
        
         */

        public void GetSaveDataFromTH(NCassa nCassa)
        {
            //По IP адресу(поле в объекте кассы) порту и типу запроса получить (строковая константа) получить строку (Json) с данными.
            bool isCassaExist = false;

#if _IMPORTLOCAL
            // импорт из локальной строки адрес не нужен
            isCassaExist = true;
            //IsPingEnter("172.23.16.106");
            //IsPingPortEnter("172.23.16.106", 8080);
#else
#if _IPLOCAL
            // импорт с нужного IP и Port без привязко к CassaTH
            isCassaExist = true;
            IsPingEnter("172.23.16.106");
            IsPingPortEnter("172.23.16.106", 8086);

#else
            if (nCassa.ThisNotNull())
            {
                isCassaExist = IsCassaExist(nCassa);
            }
#endif
#endif

            if (isCassaExist)
            {
                //Объекты
                ///// Save_GetObj(Get_Http_Write_TH(_NCassa_Test, NameTable: "atdclient?GetObj"));
#if _IMPORTLOCAL
                string dataGetObj = "[{\"Name_Object\":\"АЗС 1\",\"SiteID\":\"1\",\"Address\":\"077\"}]";
#else
#if _IPLOCAL
                string dataGetObj = GetDataTHService(string.Format("http://{0}:{1}/", "172.23.16.106", "8086"), NameTable: "atdclient?GetObj");
#else
                string dataGetObj = GetDataTHService(nCassa.AddressDevice, NameTable: "atdclient?GetObj");
#endif
#endif
                SaveNObjects(dataGetObj);

                //Коды товаров
                /////Save_GetFuel(Get_Http_Write_TH(_NCassa_Test, NameTable: "atdclient?GetFuel"));
#if _IMPORTLOCAL
                string dataGetFuel = "[{ \"GlobalCode\":\"100001\",\"LocalCode\":\"3\"},{ \"GlobalCode\":\"100002\",\"LocalCode\":\"2\"},{ \"GlobalCode\":\"100005\",\"LocalCode\":\"10\"},{ \"GlobalCode\":\"100012\",\"LocalCode\":\"18\"},{ \"GlobalCode\":\"107305\",\"LocalCode\":\"9\"},{ \"GlobalCode\":\"107306\",\"LocalCode\":\"12\"},{ \"GlobalCode\":\"107307\",\"LocalCode\":\"7\"}]";
#else
#if _IPLOCAL
                string dataGetFuel = GetDataTHService(string.Format("http://{0}:{1}/", "172.23.16.106", "8086"), NameTable: "atdclient?GetFuel");
#else
                string dataGetFuel = GetDataTHService(nCassa.AddressDevice, NameTable: "atdclient?GetFuel");
#endif
#endif
                SaveReCodesTables(dataGetFuel);

                //Кассы
                ///// Save_GetCashDesk(Get_Http_Write_TH(_NCassa_Test, NameTable: "atdclient?GetCashDesk"));
#if _IMPORTLOCAL
                string dataGetCashDesk = "[{ \"IP\":\"172.23.16.238:8000\",\"Name\":\"49\",\"NObjectId_Name\":\"1\",\"cash_type\":\"0\"},{ \"IP\":\"172.23.16.239:8000\",\"Name\":\"0\",\"NObjectId_Name\":\"1\",\"cash_type\":\"0\"},{ \"IP\":\"192.23.16.237:8000\",\"Name\":\"16\",\"NObjectId_Name\":\"1\",\"cash_type\":\"2\"},{ \"IP\":\"192.23.16.238:8000\",\"Name\":\"43\",\"NObjectId_Name\":\"1\",\"cash_type\":\"0\"}]";
#else
#if _IPLOCAL
                string dataGetCashDesk = GetDataTHService(string.Format("http://{0}:{1}/", "172.23.16.106", "8086"), NameTable: "atdclient?GetCashDesk");
#else
                string dataGetCashDesk = GetDataTHService(nCassa.AddressDevice, NameTable: "atdclient?GetCashDesk");
#endif
#endif
                SaveNCassa(dataGetCashDesk);

                //Резервуары
                ///// Save_GetTank(Get_Http_Write_TH(_NCassa_Test, NameTable: "atdclient?GetTank"));
#if _IMPORTLOCAL
                string dataGetTank = "[{ \"TankNumber\":\"1\",\"Grade\":\"10\",\"Grade_Name\":\"ДТ\",\"Max_volume\":\"10000\",\"WaterLevel_Allow\":\"100\",\"water_alarm\":\"enable\",\"level_alarm\":\"enable\"},{ \"TankNumber\":\"2\",\"Grade\":\"12\",\"Grade_Name\":\"АИ-95\",\"Max_volume\":\"4000\",\"WaterLevel_Allow\":\"100\",\"water_alarm\":\"enable\",\"level_alarm\":\"enable\"},{ \"TankNumber\":\"3\",\"Grade\":\"7\",\"Grade_Name\":\"АИ-98\",\"Max_volume\":\"10000\",\"WaterLevel_Allow\":\"100\",\"water_alarm\":\"enable\",\"level_alarm\":\"enable\"},{ \"TankNumber\":\"4\",\"Grade\":\"3\",\"Grade_Name\":\"АИ-92 П\",\"Max_volume\":\"10000\",\"WaterLevel_Allow\":\"100\",\"water_alarm\":\"enable\",\"level_alarm\":\"enable\"},{ \"TankNumber\":\"5\",\"Grade\":\"3\",\"Grade_Name\":\"АИ-92 П\",\"Max_volume\":\"10000\",\"WaterLevel_Allow\":\"100\",\"water_alarm\":\"enable\",\"level_alarm\":\"enable\"},{ \"TankNumber\":\"7\",\"Grade\":\"18\",\"Grade_Name\":\"СУГ\",\"Max_volume\":\"100000\",\"WaterLevel_Allow\":\"100\",\"water_alarm\":\"enable\",\"level_alarm\":\"enable\"}]";
#else
#if _IPLOCAL
                string dataGetTank = GetDataTHService(string.Format("http://{0}:{1}/", "172.23.16.106", "8086"), NameTable: "atdclient?GetTank");
#else
                string dataGetTank = GetDataTHService(nCassa.AddressDevice, NameTable: "atdclient?GetTank");
#endif
#endif
                SaveNTank(dataGetTank);

                //Задачи
                ///// Save_GetTask(Get_Http_Write_TH(_NCassa_Test, NameTable: "atdclient?GetSched"));
                //string dataGetSched = "[{\"Name_Task\":\"TASK1\",\"Period_Task\":120,\"Period_Ping\":null,\"Type_Device\":\"th\",\"Zip_Dir\":\"C:\\\\ATD\\\\ATD_DATA\",\"CountDaySave\":0,\"NObjectId_Name\":\"АЗС 1\"}]";
#if _IMPORTLOCAL
                string dataGetSched = "[{\"Name_Task\":\"TASK1\",\"Period_Task\":120,\"Period_Ping\":null,\"Type_Device\":\"th\",\"Zip_Dir\":\"C:\\\\ATD\\\\ATD_DATA\",\"CountDaySave\":0,\"NObjectId_Name\":\"АЗС 1\"}," +
                    "{\"Name_Task\":\"TASK2\",\"Period_Task\":10,\"Period_Ping\":null,\"Type_Device\":\"cassa\",\"Zip_Dir\":\"C:\\\\ATD\\\\ATD_DATA\",\"CountDaySave\":2,\"NObjectId_Name\":\"АЗС 1\"}," +
                    "{\"Name_Task\":\"TASK3\",\"Period_Task\":120,\"Period_Ping\":null,\"Type_Device\":\"level\",\"Zip_Dir\":\"C:\\\\ATD\\\\ATD_DATA\",\"CountDaySave\":50,\"NObjectId_Name\":\"АЗС 1\"}," +
                    "{\"Name_Task\":\"TASK4\",\"Period_Task\":120,\"Period_Ping\":null,\"Type_Device\":\"zip\",\"Zip_Dir\":\"C:\\\\ATD\\\\ATD_DATA\",\"CountDaySave\":0,\"NObjectId_Name\":\"АЗС 1\"}," +
                    "{\"Name_Task\":\"TASK5\",\"Period_Task\":120,\"Period_Ping\":null,\"Type_Device\":\"auditarch\",\"Zip_Dir\":\"C:\\\\ATD\\\\ATD_DATA\",\"CountDaySave\":1,\"NObjectId_Name\":\"АЗС 1\"}," +
                    "{\"Name_Task\":\"TASK6\",\"Period_Task\":3600,\"Period_Ping\":600,\"Type_Device\":\"MSG_Water\",\"Zip_Dir\":\"C:\\\\ATD\\\\ATD_DATA\",\"CountDaySave\":0,\"NObjectId_Name\":\"АЗС 1\"}," +
                    "{\"Name_Task\":\"TASK7\",\"Period_Task\":3600,\"Period_Ping\":600,\"Type_Device\":\"MSG_Fuel\",\"Zip_Dir\":\"C:\\\\ATD\\\\ATD_DATA\",\"CountDaySave\":0,\"NObjectId_Name\":\"АЗС 1\"}]";
#else
#if _IPLOCAL
                string dataGetSched = GetDataTHService(string.Format("http://{0}:{1}/", "172.23.16.106", "8086"), NameTable: "atdclient?GetSched");
#else
                string dataGetSched = GetDataTHService(nCassa.AddressDevice, NameTable: "atdclient?GetSched");
#endif
#endif
                SaveTask_Device(dataGetSched);
            }
            else
            {
                throw new Exception($"Подключение не установлено, т.к.конечный компьютер отверг запрос на подключение {nCassa?.IP}:{nCassa?.Port}");
            }
        }

        private string GetDataTHService(string AddressDevice, string NameTable = "")
        {
            //throw new Exception("Error GetDataTHService");

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(AddressDevice + NameTable);
            request.Method = "POST";
            string answerDataStr = "";
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                using (StreamReader streamReader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding(1251)))
                {
                    answerDataStr = streamReader.ReadToEnd();
                    streamReader.Close();
                }
                response.Close();
            }
            return answerDataStr.Replace("},", "},\n");
        }

        private void SaveNObjects(string answerDataStr)
        {
            List<NObject> ListItems;
            try
            {
                ListItems = JsonConvert.DeserializeObject<List<NObject>>(answerDataStr);
            }
            catch
            {
                throw new Exception("Ошибка DeserializeObject NObject");
            }
            if (ListItems.ThisNotNull())
            {
                using (EntitiesDb db = GetDb)
                {
                    foreach (NObject item in ListItems)
                    {
                        db.SaveNObject(item);
                    }
                }
            }
        }

        private void SaveReCodesTables(string answerDataStr)
        {
            List<ReCodesTable> ListItems;
            try
            {
                ListItems = JsonConvert.DeserializeObject<List<ReCodesTable>>(answerDataStr);
            }
            catch
            {
                throw new Exception("Ошибка DeserializeObject ReCodesTable");
            }
            if (ListItems.ThisNotNull())
            {
                using (EntitiesDb db = GetDb)
                {
                    foreach (ReCodesTable item in ListItems)
                    {
                        db.SaveReCodesTable(item);
                    }
                }
            }
        }

        private void SaveNCassa(string answerDataStr)
        {
            List<NCassa> ListItems;
            try
            {
                ListItems = JsonConvert.DeserializeObject<List<NCassa>>(answerDataStr);
            }
            catch
            {
                throw new Exception("Ошибка DeserializeObject NCassa");
            }
            if (ListItems.ThisNotNull())
            {
                using (EntitiesDb db = GetDb)
                {
                    foreach (NCassa item in ListItems)
                    {
                        NObject nObject = db.NObjects.FirstOrDefault(s => s.SiteID == item.NObjectId_Name);
                        if (nObject.ThisNotNull())
                        {
                            item.Name = item.Name.ToUpper();
                            item.Name = item.Name.ToUpper().StartsWith("CASSA") ? item.Name.Substring("CASSA".Length) : item.Name;
                            item.Name = $"CASSA{item.Name}";
                            item.NObjectId = nObject.Id;

                            //Разделить адрес на IP и Port
                            string[] IpPort = item.IP.Split(':');
                            if (IpPort.Length >= 2)
                            {
                                item.IP = IpPort[0];
                                if (int.TryParse(IpPort[1], out int port))
                                {
                                    item.Port = port;
                                }
                            }

                            db.SaveNCassa(item);
                        }
                    }
                }
            }
        }

        private void SaveNTank(string answerDataStr)
        {
            List<NTank> ListItems;
            try
            {
                ListItems = JsonConvert.DeserializeObject<List<NTank>>(answerDataStr);
            }
            catch
            {
                throw new Exception("Ошибка DeserializeObject NTank");
            }
            if (ListItems.ThisNotNull())
            {
                using (EntitiesDb db = GetDb)
                {
                    foreach (NTank item in ListItems)
                    {
                        NStruna nStruna = db.NStrunas.FirstOrDefault();
                        if (!nStruna.ThisNotNull())
                        {
                            //Струны нет.
                            FirstStruna(db);
                        }
                        nStruna = db.NStrunas.FirstOrDefault();

                        /*****************/

                        if (nStruna.ThisNotNull())
                        {
                            NTank nTank = db.NTanks.FirstOrDefault(s => s.TankNumber == item.TankNumber && s.NStrunaId == nStruna.Id);
                            if (nTank.ThisNotNull())
                            {
                                item.NStrunaId = nStruna.Id;
                                db.SaveNTank(item);
                            }
                            else
                            {
                                item.NStrunaId = nStruna.Id;
                                db.SaveNTank(item);
                            }
                        }
                    }
                }
            }
        }

        private void FirstStruna(EntitiesDb db)
        {
            NObject _NObject = db.NObjects.FirstOrDefault();
            if (_NObject != null)
            {
                NStruna nStruna = new NStruna()
                {
                    Name = "STRN1",
                    NObjectId = _NObject.Id,
                    NameBatFile = @"c:\struna\stc.bat",
                    Type_Level = "struna",// "DOMS";
                };
                db.SaveNStruna(nStruna);
            }
        }

        private void SaveTask_Device(string answerDataStr)
        {
            List<Task_Device> ListItems;
            try
            {
                ListItems = JsonConvert.DeserializeObject<List<Task_Device>>(answerDataStr);
            }
            catch
            {
                throw new Exception("Ошибка DeserializeObject Task_Device");
            }
            if (ListItems.ThisNotNull())
            {
                using (EntitiesDb db = GetDb)
                {
                    foreach (Task_Device item in ListItems)
                    {
                        //NObject nObject = db.NObjects.FirstOrDefault(s => s.Name_Object == item.NObjectId_Name);
                        //if (nObject != null)
                        //{
                        //    item.NObjectId = nObject.Id;
                        //    db.SaveTask_Device(item);
                        //}
                    }
                };
            }
        }

        #endregion Получить данные из внешних источников
    }
}
