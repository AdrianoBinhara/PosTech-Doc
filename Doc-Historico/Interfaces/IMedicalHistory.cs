using System;
using Doc_Historico.Models;

namespace Doc_Historico.Interfaces
{
	public interface IMedicalHistory
	{
		Task<List<Historico>> GetAllPatientMedicalHistory(string idUsuario);
		Task<Historico> GetSingleHistoricoPatient(string idHistorico, string idUsuario);
		Task DeleteHistorico(string idHistorico, string idUsuario);
		Task<Historico> AddHistoricoPatient(string idUsuario, Historico historico);
	}
}

