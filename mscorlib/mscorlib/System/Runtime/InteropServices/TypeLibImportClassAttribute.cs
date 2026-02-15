using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Specifies which <see cref="T:System.Type" /> exclusively uses an interface. This class cannot be inherited.</summary>
	// Token: 0x0200052C RID: 1324
	[AttributeUsage(AttributeTargets.Interface, Inherited = false)]
	[ComVisible(true)]
	public sealed class TypeLibImportClassAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.TypeLibImportClassAttribute" /> class specifying the <see cref="T:System.Type" /> that exclusively uses an interface. </summary>
		/// <param name="importClass">The <see cref="T:System.Type" /> object that exclusively uses an interface.</param>
		// Token: 0x06002916 RID: 10518 RVA: 0x000A7F19 File Offset: 0x000A6119
		public TypeLibImportClassAttribute(Type importClass)
		{
			this._importClassName = importClass.ToString();
		}

		// Token: 0x04001507 RID: 5383
		internal string _importClassName;
	}
}
