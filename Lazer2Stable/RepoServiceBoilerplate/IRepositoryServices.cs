using System;
using NHibernate;

namespace Lazer2Stable.RepoServiceBoilerplate
{
	public interface IRepositoryServices : IDisposable
	{
		ISession Session { get; }
	}
}