using System;
using System.Collections.Generic;
using System.Text;

namespace DisMon
{
	interface IDisMon
	{
		//public void Init();

		int Count();
		//void Revert();
		void Reset();
		void MarkAsPrimary(int newPrimaryIndex);
		void MarkAllSecondaryAsDisabled();
		//bool IsDisabled(int monitorIndex);
		void MarkAsDisabled(int monitorIndex);
		//void MarkAsEnabled(int enableIndex);
		bool ApplyChanges();
		void Restore();
	}
}
