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
    
    public partial class lig
    {
        public lig()
        {
            this.takim = new HashSet<takim>();
        }
    
        public double lig_id { get; set; }
        public string adi { get; set; }
        public string ulke { get; set; }
    
        public virtual ICollection<takim> takim { get; set; }
    }
}
