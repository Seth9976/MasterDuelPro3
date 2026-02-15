using System;

namespace System.Runtime.CompilerServices
{
	/// <summary>Specifies whether to wrap exceptions that do not derive from the <see cref="T:System.Exception" /> class with a <see cref="T:System.Runtime.CompilerServices.RuntimeWrappedException" /> object. This class cannot be inherited.</summary>
	// Token: 0x02000593 RID: 1427
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false, AllowMultiple = false)]
	[Serializable]
	public sealed class RuntimeCompatibilityAttribute : Attribute
	{
		/// <summary>Gets or sets a value that indicates whether to wrap exceptions that do not derive from the <see cref="T:System.Exception" /> class with a <see cref="T:System.Runtime.CompilerServices.RuntimeWrappedException" /> object.</summary>
		/// <returns>true if exceptions that do not derive from the <see cref="T:System.Exception" /> class should appear wrapped with a <see cref="T:System.Runtime.CompilerServices.RuntimeWrappedException" /> object; otherwise, false.</returns>
		// Token: 0x1700057B RID: 1403
		// (set) Token: 0x06002B04 RID: 11012 RVA: 0x000AABEA File Offset: 0x000A8DEA
		public bool WrapNonExceptionThrows
		{
			[CompilerGenerated]
			set
			{
				this.<WrapNonExceptionThrows>k__BackingField = value;
			}
		}
	}
}
