using System.ComponentModel.DataAnnotations;

namespace NetCoreWebApiMasterDetaileCRUD.Model
{
    public class student
    {
        [Key]
        public string ID { get; set; }
        public string studentName { get; set; }
        public virtual ICollection<Details> details { get; set; }

    }
}
