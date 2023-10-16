using System;
using Doc_Historico.Helpers;
using Doc_Historico.Interfaces;
using Doc_Historico.Models;

namespace Doc_Historico.Services
{
    public class PatientService : IPatient
	{
        private IRequestProvider _requestProvider;
		public PatientService(IRequestProvider requestProvider)
		{
            _requestProvider = requestProvider;
		}

        public async Task<Patient> ChangePatientInfo(Patient patient)
        {
            UriBuilder builder = new UriBuilder(BaseUri.Instance.GetPatientsUri);
            string uri = builder.ToString();
            var result = await _requestProvider.PutAsync<Patient>(uri, patient);
            return result;
        }

        public async Task<Patient> CreatePatient(Patient patient)
        {
            UriBuilder builder = new UriBuilder(BaseUri.Instance.GetPatientsUri);
            string uri = builder.ToString();
            var result = await _requestProvider.PostAsync<Patient>(uri, patient);
            return result;

        }

        public async Task DeletePatient(string id)
        {
            UriBuilder builder = new(BaseUri.Instance.GetPatientsUri);
            builder.Path += $"/{id}";
            string uri = builder.ToString();
            await _requestProvider.DeleteAsync(uri);
        }

        public async Task<Patient> GetPatientById(string id)
        {
            try
            {
                UriBuilder builder = new UriBuilder(BaseUri.Instance.GetPatientsUri);
                builder.Path += $"/{id}";
                string uri = builder.ToString();
                var list = await _requestProvider.GetAsync<Patient>(uri);
                return list;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<List<Patient>> GetPatientList()
        {
            try
            {
                UriBuilder builder = new UriBuilder(BaseUri.Instance.GetPatientsUri);
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

