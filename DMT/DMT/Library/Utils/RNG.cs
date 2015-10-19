using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DMT.Library.Utils
{
	public static class RNG
	{
		static Random _random = new Random();

		static RNG()
		{
			_random = new Random();
		}

		public static int Next(int maxValue)
		{
			return Next(0, maxValue);
		}

		public static int Next(int minValue, int maxValue)
		{
			int r = _random.Next(minValue, maxValue);

#if DEBUG
			DMT.Library.Logging.Logger logger = new Logging.Logger();
			logger.LogInfo("RNG", "[{0}, {1}) -> {2}", minValue, maxValue, r);
#endif
			return r;
		}
	}
}
