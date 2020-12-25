using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgendaApp.DL.Models
{
    [Table("AgendaItems")]
    public class AgendaItem 
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        public string Title { get; set; }
        [Required]
        [MaxLength(100)]
        public string Description { get; set; }
        public DateTime? StartDate { get; set; }
        [Required]
        public DateTime FinishDate { get; set; }
        public bool IsCompleted { get; set; }
        public bool IsRepeatable { get; set; }
        public int RepeatableInterval { get; set; }
        [Required]
        public int Priority { get; set; }
    }
}
