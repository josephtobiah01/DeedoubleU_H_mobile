using DrakeWorkwise.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrakeWorkwise.Interfaces
{
    public interface IWorkwiseService
    {
        Task<RequestResponse> SendAppointment(Appointment appointment);
        Task<RequestResponse> SendContactUs(ContactUs contactUs, bool IsContact);
    }
}
