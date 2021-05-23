using System;
using NHibernate;
using NHibernate.Cfg;
using Environment = NHibernate.Cfg.Environment;

namespace Lazer2Stable.RepoServiceBoilerplate
{
	/// <summary>
	/// This should be configured as a singleton class so that the nHibernate configuration is called once.
	/// It's purpose is to create database sessions.
	/// </summary>
	public class NHibernateSessionManager : INHibernateSessionManager
	{
		private readonly ISessionFactory _sessionFactory;

		public NHibernateSessionManager(string dbLocation)
		{
			try
			{
				var dialect               = "NHibernate.Dialect.SQLiteDialect";
				var connectionProvider    = "NHibernate.Connection.DriverConnectionProvider";
				var connectionDriverClass = "NHibernate.Driver.SQLite20Driver";
				var connectionString      = $"Data Source={dbLocation};Version=3";
				var showSql               = "true";

				// add mapping files
				var assembly      = GetType().Assembly;
				var configuration = new Configuration();
				configuration.AddAssembly(assembly);

				configuration.SetProperty(Environment.Dialect,            dialect);
				configuration.SetProperty(Environment.ConnectionProvider, connectionProvider);
				configuration.SetProperty(Environment.ConnectionString,   connectionString);
				configuration.SetProperty(Environment.ConnectionDriver,   connectionDriverClass);
				configuration.SetProperty(Environment.ShowSql,            showSql);

				// specify connection details
				_sessionFactory = configuration.BuildSessionFactory();
			}
			catch (Exception e)
			{
				throw new RepositoryException("Cannot connect to the database", e);
			}
		}

		/// <summary>
		/// Opens a new database session.
		/// </summary>
		/// <returns>Returns the session</returns>
		public ISession OpenSession() { return _sessionFactory.OpenSession(); }

		/// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
		public void Dispose() { _sessionFactory?.Dispose(); }
	}
}