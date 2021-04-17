using System;

namespace EzhaBy.Business.Tags.Dto
{
    public class TagDto
    {
        public Guid Id { get; set; }

        public string TagName { get; set; }

        public bool IsAssigned { get; set; }
    }
}
