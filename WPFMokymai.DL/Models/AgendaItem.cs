using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgendaApp.DL.Models
{
    [Table("AgendaItems")]
    public class AgendaItem : INotifyPropertyChanged
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        private string title;

        public string Title
        {
            get { return title; }
            set 
            {
                if (title != value)
                {
                    title = value;
                    NotifyPropertyChanged("Title");
                }
            }
        }

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

        public AgendaItemPriority AgendaItemPriority { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }
    }
    public enum AgendaItemPriority
    {
        Low,
        Medium,
        High
    }
}
