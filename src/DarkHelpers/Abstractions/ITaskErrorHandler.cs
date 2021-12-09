using System;

namespace DarkHelpers.Abstractions
{
    public interface ITaskErrorHandler
	{
		void Handle(Exception ex);
	}
}
