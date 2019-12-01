using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Entities
{
    public class HomeWorkAssignment : BaseEntityFull
    {
        public Guid YearClassId { get; set; }
        public YearClass YearClass { get; set; }

        public DateTimeOffset DueDate { get; set; }


        private readonly List<SubmittedHomeWork> _submittedHomeWorks = new List<SubmittedHomeWork>();
        public IReadOnlyCollection<SubmittedHomeWork> SubmittedHomeWorks => _submittedHomeWorks.AsReadOnly();

        private readonly List<HomeWorkAssignmentItem> _homeWorkAssignmentItems = new List<HomeWorkAssignmentItem>();
        public IReadOnlyCollection<HomeWorkAssignmentItem> HomeWorkAssignmentItems => _homeWorkAssignmentItems.AsReadOnly();

        public void AddSubmittedHomeWork(SubmittedHomeWork submittedHomeWork)
        {
            submittedHomeWork.HomeWorkAssignment = this;
            submittedHomeWork.HomeWorkAssignmentId = this.Id;
            this._submittedHomeWorks.Add(submittedHomeWork);
        }

        public void AddHomeWorkAssignmentItem(HomeWorkAssignmentItem homeWorkAssignmentItem)
        {
            homeWorkAssignmentItem.HomeWorkAssignment = this;
            homeWorkAssignmentItem.HomeWorkAssignmentId = this.Id;
            this._homeWorkAssignmentItems.Add(homeWorkAssignmentItem);
        }

    }
}
