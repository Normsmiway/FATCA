using Fatca.Core.Models;
using Fatca.Core.Extensions;
using System;

namespace Fatca.Core.Builders.Fatcas
{
    public class ReportingFIDocSpecBuilder<T> : ReportingFIAddressFixBuilder<ReportingFIDocSpecBuilder<T>> where T : ReportingFIDocSpecBuilder<T>
    {
        private DocSpec _docSpec;

        public T AlsoAddDocSpec()
        {
            _docSpec = new DocSpec();
            return ApplyChanges();
        }

        private T ApplyChanges()
        {
            _fatca.ReportingFI.DocSpec = _docSpec;
            return (T)this;
        }

        public T OfDocTypeIndic(string docTypeIndic)
        {
            _docSpec.DocTypeIndic = docTypeIndic;
            return ApplyChanges();
        }

        public T WithDocRefId(string docRefId)
        {
            int fi = new Random().Next(0034001, 9999999);
            _docSpec.DocRefId = $"{_fatca?.ReportingFI?.TIN}.FI".ToTimedId() + $"{fi}";// docRefId;
            return ApplyChanges();
        }
    }
}
