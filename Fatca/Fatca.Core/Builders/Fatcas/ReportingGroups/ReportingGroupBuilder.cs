using Fatca.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fatca.Core.Builders.Fatcas.ReportingGroups
{
    public class ReportingGroupBuilder<T> : ReportingFIDocSpecBuilder<ReportingGroupBuilder<T>> where T : ReportingGroupBuilder<T>
    {
        private ReportingGroup _reports;

        public List<T> ForEach
        {
            get { return new List<T>(); }
        }
        private T ApplyChanges()
        {
            _reports = new ReportingGroup();
            _fatca.ReportingGroup = _reports;
            return (T)this;
        }
        public T ReportingGroup { get { return ApplyChanges(); } }
    }
}
