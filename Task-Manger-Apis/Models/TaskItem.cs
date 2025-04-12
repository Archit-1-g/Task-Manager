namespace OritsoTaskManager.Models
{
    public class TaskItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public string Status { get; set; }
        public string Remarks { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime LastUpdatedOn { get; set; }
        public string CreatedByName { get; set; }
        public int CreatedById { get; set; }
        public string UpdatedByName { get; set; }
        public int UpdatedById { get; set; }
    }
}
