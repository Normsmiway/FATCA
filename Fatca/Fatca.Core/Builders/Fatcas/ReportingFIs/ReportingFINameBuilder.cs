using Fatca.Core.Models;

namespace Fatca.Core.Builders.Fatcas
{
    public class ReportingFINameBuilder<T> : ReportingFIBuilder<ReportingFINameBuilder<T>> where T : ReportingFINameBuilder<T>
    {
        private Name _name;

        public T IncludeName()
        {
            _name = new Name();
            return (T)this;
        }

        public T WithFirstNameAs(string firstname)
        {
            //  _fatca.ReportingFI.Name.FirstName = firstname;
            _name.FirstName = firstname;
            return ApplyChanges();

        }
        public T AndMiddleName(string middleName)
        {
            _name.MiddleName = middleName;
            return ApplyChanges();

        }
        public T ThenLastName(string lastName)
        {
            _name.LastName = lastName;
            return ApplyChanges();

        }

        public T WithParams(string[] param)
        {
            _name.Text = param;
            return ApplyChanges();

        }

        private T ApplyChanges()
        {
            _fatca.ReportingFI.Name = _name;
            return (T)this;
        }
    }
}
