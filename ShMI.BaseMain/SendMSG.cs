//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ShMI.BaseMain
{
    using System;
    using System.Collections.Generic;
    
    public partial class SendMSG
    {
        public System.Guid Id { get; set; }
        public System.DateTime DateSendLast { get; set; }
        public string Status { get; set; }
        public string StatusMsg { get; set; }
        public string TypeWarn { get; set; }
        public string WaterLevel { get; set; }
        public System.Guid NTankId { get; set; }
        public string TankNumber { get; set; }
        public string Grade { get; set; }
        public string Grade_Name { get; set; }
    
        public virtual NTank NTank { get; set; }
    }
}