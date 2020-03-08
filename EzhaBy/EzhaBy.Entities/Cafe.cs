using System.Collections.Generic;

namespace EzhaBy.Entities
{
    public class Cafe
    {
        public string Id { get; set; }
        public string CafeName { get; set; }
        public byte[] MainImage { get; set; }
        public byte[] BackgroundImage { get; set; }
        public string DeliveryTime { get; set; }
        public decimal DeliveryPrice { get; set; }
        public CafeTypes CafeType { get; set; }
        public string WorkingHours { get; set; }
        public float Rating { get; set; }
        public string AddressId { get; set; }
        public virtual Address Address { get; set; }
        public virtual List<CafeDescription> KeyWords { get; set; }
        public virtual List<CafeSection> Sections { get; set; }
    }
}
