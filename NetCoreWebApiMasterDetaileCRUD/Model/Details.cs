using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Metrics;

namespace NetCoreWebApiMasterDetaileCRUD.Model
{
    public class Details
    {
        [Key]
        public string DetailsID { get; set; }
        [ForeignKey("student")]
        public string ID { get; set; }
        public string Class { get; set; }
        public string phone { get; set; }
        public virtual student? student { get; set; }
    }
}