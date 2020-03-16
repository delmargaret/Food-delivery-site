using System;
using System.Linq;

namespace EzhaBy.Entities
{
    public class Tag
    {
        public Guid Id { get; set; }

        public string TagName { get; set; }

        public byte[] TagIcon { get; set; }

        public virtual IQueryable<CateringFacilityTag> CateringFacilityTags { get; set; }

    }
}
