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
    
    public partial class TANK_READING
    {
        public System.Guid Id { get; set; }
        public string SiteID { get; set; }
        public System.DateTime ReferenceTime { get; set; }
        public System.DateTime DateCreatedLineCassa { get; set; }
        public int RecordID { get; set; }
        public string TankNumber { get; set; }
        public string Grade { get; set; }
        public string Volume { get; set; }
        public string FuelLevel { get; set; }
        public string Temperature { get; set; }
        public string FuelWeight { get; set; }
        public string Density { get; set; }
        public string WaterLevel { get; set; }
        public string Status { get; set; }
        public string StatusMsg { get; set; }
        public System.Guid NCassaId { get; set; }
        public string Type { get; set; }
    
        public virtual NCassa NCassa { get; set; }
    }
}
