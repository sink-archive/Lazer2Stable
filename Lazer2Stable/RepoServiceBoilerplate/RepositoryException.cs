using System;

namespace Lazer2Stable.RepoServiceBoilerplate
{
	public class RepositoryException : Exception
	{
		public RepositoryException(string message) : base(message) {}
		public RepositoryException(string message, Exception e) : base(message, e){}
	}
}