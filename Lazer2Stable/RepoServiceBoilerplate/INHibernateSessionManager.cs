using System;
using NHibernate;

namespace Lazer2Stable.RepoServiceBoilerplate
{
	public interface INHibernateSessionManager : IDisposable
	{
		/// <summary>
		/// Opens the database session.
		/// </summary>
		/// <returns>Returns the session</returns>
		ISession OpenSession();
	}
}