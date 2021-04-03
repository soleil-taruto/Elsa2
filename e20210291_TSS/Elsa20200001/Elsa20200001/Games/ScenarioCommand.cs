using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Commons;
using Charlotte.GameCommons;
using Charlotte.Games.Surfaces;

namespace Charlotte.Games
{
	public class ScenarioCommand
	{
		private string[] Tokens;

		public ScenarioCommand(string[] tokens)
		{
			this.Tokens = tokens;
		}

		public void Invoke()
		{
			try
			{
				if (this.Tokens[1] == "=")
				{
					string instanceName = this.Tokens[0];
					string typeName = this.Tokens[2];

					Game.I.Status.Surfaces.RemoveAll(v => v.InstanceName == instanceName);

					Surface surface = SurfaceCreator.Create(typeName, instanceName);

					Game.I.Status.Surfaces.Add(surface);
				}
				else
				{
					string instanceName = this.Tokens[0];
					string command = this.Tokens[1];
					string[] arguments = this.Tokens.Skip(2).ToArray();

					Surface surface = Game.I.Status.GetSurface(instanceName);

					surface.Invoke(command, arguments);
				}
			}
			catch (Exception ex)
			{
				ProcMain.WriteLog("\u30b3\u30de\u30f3\u30c9\u306e\u5b9f\u884c\u4e2d\u306b\u30a8\u30e9\u30fc\u304c\u767a\u751f\u3057\u307e\u3057\u305f\u3002\u30a8\u30e9\u30fc\u306b\u306a\u3063\u305f\u30c8\u30fc\u30af\u30f3\u5217\u306f\u4ee5\u4e0b\u306e\u3068\u304a\u308a\u3067\u3059\u3002");

				foreach (string token in this.Tokens)
					ProcMain.WriteLog(token);

				if (DDConfig.LOG_ENABLED)
					throw;

				ProcMain.WriteLog("\u30b2\u30fc\u30e0\u3092\u7d9a\u884c\u3057\u307e\u3059\u3002" + ex);
			}
		}
	}
}
