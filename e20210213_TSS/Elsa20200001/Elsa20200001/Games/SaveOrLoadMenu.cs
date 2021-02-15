using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Games
{
	public class SaveOrLoadMenu : IDisposable
	{
		public static SaveOrLoadMenu I;

		public SaveOrLoadMenu()
		{
			I = this;
		}

		public void Dispose()
		{
			I = null;
		}
	}
}
