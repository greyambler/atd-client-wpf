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
    
    public partial class NStruna
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NStruna()
        {
            this.NTanks = new HashSet<NTank>();
            this.TankReadings = new HashSet<TankReading>();
        }
    
        public System.Guid Id { get; set; }
        public string Name { get; set; }
        public string IP { get; set; }
        public Nullable<int> Port { get; set; }
        public System.Guid NObjectId { get; set; }
        public string NameBatFile { get; set; }
        public string Type_Level { get; set; }
    
        public virtual NObject NObject { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NTank> NTanks { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TankReading> TankReadings { get; set; }
    }
}