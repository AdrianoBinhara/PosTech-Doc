using System;
using Doc_Historico.Helpers;
using Doc_Historico.Interfaces;
using Doc_Historico.Models;
using Xamarin.Google.Crypto.Tink.Shaded.Protobuf;

namespace Doc_Historico.Services
{
    public class PatientService : IPatient
	{
        private IRequestProvider _requestProvider;
		public PatientService(IRequestProvider requestProvider)
		{
            _requestProvider = requestProvider;
		}

        public Task<Patient> ChangePatientInfo(Patient patient)
        {
            throw new NotImplementedException();
        }

        public Task<Patient> CreatePatient(Patient patient)
        {
            throw new NotImplementedException();
        }

        public Task DeletePatient(string id)
        {
            throw new NotImplementedException();
        }

        public Task<Patient> GetPatientById(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Patient>> GetPatientList()
        {
            try
            {
                UriBuilder builder = new UriBuilder(BaseUri.Instance.Uri);
                builder.Path = "/Patient";
                string uri = builder.ToString();
                var list = await _requestProvider.GetAsync<List<Patient>>(uri);
                return list;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
}

