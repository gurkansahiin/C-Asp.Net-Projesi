//------------------------------------------------------------------------------
// <auto-generated>
//    Bu kod bir şablondan oluşturuldu.
//
//    Bu dosyada el ile yapılan değişiklikler uygulamanızda beklenmedik davranışa neden olabilir.
//    Kod yeniden oluşturulursa, bu dosyada el ile yapılan değişikliklerin üzerine yazılacak.
// </auto-generated>
//------------------------------------------------------------------------------

namespace takimdeneme
{
    using System;
    using System.Collections.Generic;
    
    public partial class takim
    {
        public takim()
        {
            this.futbolcular = new HashSet<futbolcular>();
            this.transfer = new HashSet<transfer>();
            this.transfer1 = new HashSet<transfer>();
        }
    
        public double takim_id { get; set; }
        public Nullable<double> lig_id { get; set; }
        public string Takım { get; set; }
        public string ulke { get; set; }
    
        public virtual ICollection<futbolcular> futbolcular { get; set; }
        public virtual lig lig { get; set; }
        public virtual ICollection<transfer> transfer { get; set; }
        public virtual ICollection<transfer> transfer1 { get; set; }
    }
}