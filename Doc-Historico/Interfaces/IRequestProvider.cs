using System;
using Doc_Historico.Models;

namespace Doc_Historico.Interfaces
{
	public interface IRequestProvider
	{
        Task<TResult> GetAsync<TResult>(string uri);
        Task DeleteAsync(string uri);
        Task<TResult> PostAsync<TResult>(string uri, Patient patient);
        Task<TResult> PutAsync<TResult>(string uri, Patient patient);

    }
}

