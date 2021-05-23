using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NHibernate;
using NHibernate.Linq;

namespace Lazer2Stable.RepoServiceBoilerplate
{
    /// <summary>
    /// The base class for the repository services. 
    /// </summary>
    public abstract class RepositoryServicesBase : IRepositoryServices
    {
        private bool _disposed;

        /// <summary>
        /// If this is true, then when this instance is disposed, the session is also disposed
        /// </summary>
        private readonly bool _ownerOfSession;

        /// <summary>
        /// An active database session
        /// </summary>
        public ISession Session { get; private set; }

        protected readonly ITransaction Transaction;

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="sessionManager">An instance of the session manager that provides the database connections</param>
		protected RepositoryServicesBase(INHibernateSessionManager sessionManager)
        {
            try
            {

                Session = sessionManager.OpenSession();

                Transaction = Session.BeginTransaction();
                _ownerOfSession = true;
            }
            catch (Exception)
            {
                throw new RepositoryException("Unable to connect to the database");
            }
        }

		/// <summary>
		/// This version of the constructor takes in a session. In this case, this class doesn't own
		/// the session so will not be responsible for disposing of it.
		/// </summary>
		/// <param name="repositoryService">An existing session, in which case this instance does not dispose of the session. If this value
		/// is null then this instance does take ownership</param>
		protected RepositoryServicesBase(IRepositoryServices repositoryService)
        {
            var session = repositoryService?.Session;

            Session = session ?? throw new RepositoryException("Cannot reuse a session that doesn't exist");
#pragma warning disable 618
            Transaction = session.Transaction;
#pragma warning restore 618
            _ownerOfSession = false;
        }

        /*
        /// <summary>
        /// Returns a list of entities
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IList<T> GetList<T>() where T : IEntity
        {
            var list = Session.Query<T>().ToList();
            return list;
        }

        /// <summary>
        /// Returns a list of entities asynchronously
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected async Task<IList<T>> GetListAsync<T>() where T : IEntity
        {
            var list = await Session.Query<T>().ToListAsync();
            return list;
        }

        /// <summary>
        /// Gets as single item
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        protected T GetItemById<T>(int id) where T : class, IEntity
        {
            var item = Session.Query<T>().SingleOrDefault(s => s.Id == id);
            return item;
        }

        /// <summary>
        /// Gets a single item asynchronously
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        protected async Task<T> GetItemByIdAsync<T>(int id) where T : class, IEntity
        {
            var item = await Session.Query<T>().SingleOrDefaultAsync(s => s.Id == id);
            return item;
        }
        */

        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        public void Dispose()
        {
            if (_disposed)
				return;

			_disposed = true;

            if (_ownerOfSession)
            {
                if (Transaction.IsActive) Transaction.Commit();

				Transaction?.Dispose();
                Session?.Close();
                Session?.Dispose();
            }
        }

    }
}