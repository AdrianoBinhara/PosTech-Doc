using System;
namespace Doc_Historico.Interfaces
{
	public interface IRequestProvider
	{
        Task<TResult> GetAsync<TResult>(string uri);

    }
}

