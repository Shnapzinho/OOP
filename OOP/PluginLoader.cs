using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace OOP
{
	public static class PluginLoader
	{
		public static IEnumerable<IDataProcessorPlugin> LoadPlugins()
		{
			var plugins = new List<IDataProcessorPlugin>();
			string pluginsDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Plugins");

			if (!Directory.Exists(pluginsDirectory))
			{
				MessageBox.Show($"Plugins directory not found at: {pluginsDirectory}", "Error",
							  MessageBoxButtons.OK, MessageBoxIcon.Error);
				return plugins;
			}

			foreach (string dll in Directory.GetFiles(pluginsDirectory, "*.dll"))
			{
				try
				{
					Assembly assembly = Assembly.LoadFrom(dll);
					foreach (Type type in assembly.GetTypes())
					{
						if (typeof(IDataProcessorPlugin).IsAssignableFrom(type) && !type.IsInterface && !type.IsAbstract)
						{
							IDataProcessorPlugin plugin = (IDataProcessorPlugin)Activator.CreateInstance(type);
							plugins.Add(plugin);
							MessageBox.Show($"Successfully loaded plugin: {plugin.Name} from {Path.GetFileName(dll)}",
										  "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
						}
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show($"Failed to load plugin from {Path.GetFileName(dll)}: {ex.Message}",
								  "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}

			return plugins;
		}
	}
}