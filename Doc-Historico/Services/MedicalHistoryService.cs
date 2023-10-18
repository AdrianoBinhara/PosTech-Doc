using System;
using Doc_Historico.Helpers;
using Doc_Historico.Interfaces;
using Doc_Historico.Models;

namespace Doc_Historico.Services
{
    public class MedicalHistoryService : IMedicalHistory
	{
        private IRequestProvider _requestProvider;
		public MedicalHistoryService(IRequestProvider requestProvider)
		{
            _requestProvider = requestProvider;
		}

        public async Task<Historico>AddHistoricoPatient(string idUsuario, Historico historico)
        {
            UriBuilder builder = new UriBuilder(BaseUri.Instance.GetMedicalHistoryUri);
            builder.Path += "/idPaciente";
            builder.Query = $"idPaciente={idUsuario}";
            string uri = builder.ToString();
            var result = await _requestProvider.PostAsync<Historico>(uri, historico);
            return result;
        }

        public async Task<bool> DeleteHistorico(string idHistorico, string idUsuario)
        {
            UriBuilder builder = new UriBuilder(BaseUri.Instance.GetMedicalHistoryUri);
            builder.Path += $"/{idUsuario}/{idHistorico}";
            string uri = builder.ToString();
            var result = await _requestProvider.DeleteAsync(uri);
            return result;
        }

        public async Task<Historico> GetSingleHistoricoPatient(string idHistorico, string idUsuario)
        {
            UriBuilder builder = new UriBuilder(BaseUri.Instance.GetMedicalHistoryUri);
            builder.Path += $"/{idUsuario}/{idHistorico}";
            string uri = builder.ToString();
            var list = await _requestProvider.GetAsync<Historico>(uri);
            return list;
        }

        public async Task<List<Historico>> GetAllPatientMedicalHistory(string idUsuario)
        {
            UriBuilder builder = new UriBuilder(BaseUri.Instance.GetMedicalHistoryUri);
            builder.Path += $"/patient/{idUsuario}";
            string uri = builder.ToString();
            var list = await _requestProvider.GetAsync<List<Historico>>(uri);
            return list;
        }
    }
}

