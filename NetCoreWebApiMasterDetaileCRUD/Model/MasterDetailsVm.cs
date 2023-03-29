using System.Diagnostics.Metrics;

namespace NetCoreWebApiMasterDetaileCRUD.Model
{
    public class MasterDetailsVm
    {
        public MasterDetailsVm()
        {
            this.student = new student();
            this.details = new List<Details>();
            /* do nothing */
        }
        public student student { get; set; }
        public List<Details> details
        {
            get; set;
        }
    }
}
