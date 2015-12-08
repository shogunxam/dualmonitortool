using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DMT.Library.GuiUtils
{
	static class FileSelectionHelper
	{
		public static void SetInitialFileNameInDialog(OpenFileDialog dlg, string fileName)
		{
			dlg.FileName = fileName;
			if (fileName.Length > 0)
			{
				try
				{
					string dir = Path.GetDirectoryName(fileName);
					dlg.InitialDirectory = dir;
					dlg.FileName = Path.GetFileName(fileName);
				}
				catch (Exception)
				{
				}
			}
		}
	}
}
