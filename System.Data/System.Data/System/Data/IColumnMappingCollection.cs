using System;
using System.Collections;
using System.Reflection;

namespace System.Data
{
	/// <summary>Contains a collection of DataColumnMapping objects, and is implemented by the <see cref="T:System.Data.Common.DataColumnMappingCollection" />, which is used in common by .NET Framework data providers.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000079 RID: 121
	[DefaultMember("Item")]
	public interface IColumnMappingCollection : IList, ICollection, IEnumerable
	{
	}
}
