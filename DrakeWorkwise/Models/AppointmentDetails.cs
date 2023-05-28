using DrakeWorkwise.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrakeWorkwise.Models
{
    public class AppointmentDetails : BindableBase
    {
        private string _name = "";
        public string Name
        {
            get { return _name; }
            set { SetPropertyValue(ref _name, value); }
        }
        private string _email = "";
        public string Email
        {
            get { return _email; }
            set { SetPropertyValue(ref _email, value); }
        }

        private string _contactNumber = "";
        public string ContactNumber
        {
            get { return _contactNumber; }
            set { SetPropertyValue(ref _contactNumber, value); }
        }

        private string _employer = "";
        public string Employer
        {
            get { return _employer; }
            set { SetPropertyValue(ref _employer, value); }
        }

        private string _location = "";
        public string Location
        {
            get { return _location; }
            set { SetPropertyValue(ref _location, value); }
        }

        private PreferredDay _preferredDay = PreferredDay.None;
        public PreferredDay PreferredDay
        {
            get { return _preferredDay; }
            set { SetPropertyValue(ref _preferredDay, value); }
        }

        private PreferredTime _preferredTime = PreferredTime.None;
        public PreferredTime PreferredTime
        {
            get { return _preferredTime; }
            set { SetPropertyValue(ref _preferredTime, value); }
        }

        private string _reason = "";
        public string Reason
        {
            get { return _reason; }
            set { SetPropertyValue(ref _reason, value); }
        }
    }

    public enum PreferredDay
    {
        None, Monday, Tuesday, Wednesday, Thursday, Friday, Weekend, Anytime
    }

    public enum PreferredTime
    {
        None, DuringBusinessHours, AfterBusinessHours
    }
}
