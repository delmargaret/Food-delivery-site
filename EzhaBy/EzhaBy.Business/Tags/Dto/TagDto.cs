using System;

namespace EzhaBy.Business.Tags.Dto
{
    public class TagDto
    {
        public Guid Id { get; set; }

        public string TagName { get; set; }

        public byte[] TagIcon { get; set; }

        public bool isAssigned { get; set; }
    }
}
