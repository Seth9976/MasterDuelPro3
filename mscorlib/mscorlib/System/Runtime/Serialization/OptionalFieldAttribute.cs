using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Serialization
{
	/// <summary>Specifies that a field can be missing from a serialization stream so that the <see cref="T:System.Runtime.Serialization.Formatters.Binary.BinaryFormatter" /> and the <see cref="T:System.Runtime.Serialization.Formatters.Soap.SoapFormatter" /> does not throw an exception. </summary>
	// Token: 0x020004C7 RID: 1223
	[AttributeUsage(AttributeTargets.Field, Inherited = false)]
	[ComVisible(true)]
	public sealed class OptionalFieldAttribute : Attribute
	{
		/// <summary>This property is unused and is reserved.</summary>
		/// <returns>This property is reserved.</returns>
		// Token: 0x1700052B RID: 1323
		// (set) Token: 0x060026F7 RID: 9975 RVA: 0x0009D473 File Offset: 0x0009B673
		public int VersionAdded
		{
			set
			{
				if (value < 1)
				{
					throw new ArgumentException(Environment.GetResourceString("Version value must be positive."));
				}
				this.versionAdded = value;
			}
		}

		// Token: 0x04001295 RID: 4757
		private int versionAdded = 1;
	}
}
