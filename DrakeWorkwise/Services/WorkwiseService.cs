using DrakeWorkwise.Interfaces;
using DrakeWorkwise.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrakeWorkwise.Services
{
    public class WorkwiseService : IWorkwiseService
    {
        private readonly IApiManager _apiManager;

        public WorkwiseService(IApiManager apiManager)
        {
            _apiManager = apiManager;
        }
        public async Task<RequestResponse> SendAppointment(Appointment appointment)
        {
            RequestResponse ret = null;
            try
            {

                Dictionary<string, string> parameters = new Dictionary<string, string>();
                parameters.Add("Name", appointment.Name);
                parameters.Add("Email", appointment.Email);
                parameters.Add("ContactNumber", appointment.ContactNumber);
                parameters.Add("Employer", appointment.Employer);
                parameters.Add("Country", appointment.Country);
                parameters.Add("City", appointment.City);
                parameters.Add("Message", appointment.Message);
                parameters.Add("PreferredDays", appointment.PreferredDays);
                parameters.Add("preferred_time", appointment.PreferredTime);
                parameters.Add("request_type", appointment.RequestType);

                var result = await _apiManager.PostForResponseAsync<string>("umbraco/surface/ContactSurface/SaveConsultation/", parameters);
                bool parse = int.TryParse(result, out int res);
                ret = new RequestResponse();

                if (!parse || res <= 0)
                {

                    throw new Exception();
                }
                else
                {
                    ret.Code = res;
                }
            }

            catch (NoInternetException nie)
            {
                Console.WriteLine(nie);
                ret = new RequestResponse()
                {
                    Code = 0000,
                    Feedback = false,
                    Message = "Please check your internet connection."
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                ret = new RequestResponse()
                {
                    Code = 0000,
                    Feedback = false,
                    Message = "Unable to send an appointment request, please try again later."
                };
            }
            return ret;// ?? new RequestResponse() { Code = "0000", Feedback = false, Message = "Failed" };

        }

        public async Task<RequestResponse> SendContactUs(ContactUs contactUs, bool IsContact)
        {
            RequestResponse ret = null;
            try
            {
                Dictionary<string, string> parameters = new Dictionary<string, string>();
                parameters.Add("name", contactUs.Name);
                parameters.Add("email", contactUs.Email);
                parameters.Add("phone", contactUs.Phone);
                parameters.Add("position", contactUs.Position);
                parameters.Add("comments",$"{(IsContact ? "" : "Feedback:")}{contactUs.Comments}");

                var result = await _apiManager.PostForResponseAsync<string>("umbraco/surface/ContactSurface/Save", parameters);
                bool parse = int.TryParse(result, out int res);

                ret = new RequestResponse();

                if(!parse || res <= 0)
                {
                    throw new Exception();
                }
                else
                {
                    ret.Code = res;
                }

            }
            catch (NoInternetException nie)
            {
                Console.WriteLine(nie);
                ret = new RequestResponse()
                {
                    Code = 0000,
                    Feedback = false,
                    Message = "Please check your internet connection."
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                ret = new RequestResponse()
                {
                    Code = 0000,
                    Feedback = false,
                    Message = "Unable to send the request, please try again later."
                };
            }
            return ret;// ?? new RequestResponse() { Code = "0000", Feedback = false, Message = "Failed" };
        }
    }
}
