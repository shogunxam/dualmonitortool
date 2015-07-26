using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DMT.Library.Html
{
	public interface IHttpConnectionManager
	{
		HttpConnection GetConnection(Uri uri);
	}
}
