﻿using Microsoft.EntityFrameworkCore.Storage.Internal;

namespace FinShark.Model
{
    public class Comment
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public int StockId { get; set; }
    }
}
