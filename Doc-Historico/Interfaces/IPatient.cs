using System;
using Doc_Historico.Models;

namespace Doc_Historico.Interfaces
{
	public interface IPatient
	{
		Task<List<Patient>> GetPatientList();
		Task<Patient> ChangePatientInfo(Patient patient);
		Task<Patient> CreatePatient(Patient patient);
		Task<Patient> GetPatientById(string id);
		Task DeletePatient(string id);
	}
}

