using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DxLibDLL;
using Charlotte.Commons;
using Charlotte.GameCommons;

namespace Charlotte.Tests
{
	public class Test0001
	{
		public void Test01()
		{
			for (; ; )
			{
				DDDraw.DrawSimple(DDCCResource.GetPicture(@"dat\Novel\背景.png"), 0, 0);

				DDEngine.EachFrame();
			}
		}
	}
}
