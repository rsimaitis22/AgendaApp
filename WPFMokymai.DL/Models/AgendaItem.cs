using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaApp.DL.Models
{
    public class AgendaItem
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime FinishDate { get; set; }
        public bool IsCompleted { get; set; }
        public bool IsRepeatable { get; set; }
        public int RepeatableInterval { get; set; }
    }
}
