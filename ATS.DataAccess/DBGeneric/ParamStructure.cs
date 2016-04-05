/*
 * File added by Hiren Dhinoja, Prompt Softech
 * Added On: 01-June-2012
 * Reason: For db connection with oracle. (taken from Sumul Morder Service)
 * */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Diagnostics;

namespace ATS.DataAccess
{
	[Serializable()]
	[DebuggerDisplay("{ParamName}, {Value}, {ParamDirection}, {DataType}")]
	public struct ParamStruct
	{
		public string ParamName;
		public DbType DataType;
		public object Value;
		public ParameterDirection ParamDirection;
		//public string SourceColumn;
		public Int16 Size;

		
	}
}
