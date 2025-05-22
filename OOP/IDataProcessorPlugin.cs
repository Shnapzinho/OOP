// IDataProcessorPlugin.cs
using System;

namespace OOP
{
	public interface IDataProcessorPlugin
	{
		string Name { get; }
		string ProcessBeforeSave(string data);
		string ProcessAfterLoad(string data);
	}
}