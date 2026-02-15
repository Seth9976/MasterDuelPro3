using System;

namespace System
{
	/// <summary>Indicates whether a program element is compliant with the Common Language Specification (CLS). This class cannot be inherited.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020000CD RID: 205
	[AttributeUsage(AttributeTargets.All, Inherited = true, AllowMultiple = false)]
	[Serializable]
	public sealed class CLSCompliantAttribute : Attribute
	{
		/// <summary>Initializes an instance of the <see cref="T:System.CLSCompliantAttribute" /> class with a Boolean value indicating whether the indicated program element is CLS-compliant.</summary>
		/// <param name="isCompliant">true if CLS-compliant; otherwise, false. </param>
		// Token: 0x06000544 RID: 1348 RVA: 0x000197BD File Offset: 0x000179BD
		public CLSCompliantAttribute(bool isCompliant)
		{
			this._compliant = isCompliant;
		}

		// Token: 0x040002E0 RID: 736
		private bool _compliant;
	}
}
