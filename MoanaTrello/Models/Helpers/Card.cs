using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoanaTrello.Models.Helpers
{
    public class Card
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Status { get; set; }
        public int Position { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public string OwnerId { get; set; }
        public string AsigneeId { get; set; }
    }

    public class CardRequest
    {
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
