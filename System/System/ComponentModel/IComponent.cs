using System;
using System.ComponentModel.Design;
using System.ComponentModel.Design.Serialization;
using System.Runtime.InteropServices;

namespace System.ComponentModel
{
	/// <summary>Provides functionality required by all components. </summary>
	// Token: 0x020002BF RID: 703
	[Designer("System.Windows.Forms.Design.ComponentDocumentDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(IRootDesigner))]
	[TypeConverter(typeof(ComponentConverter))]
	[ComVisible(true)]
	[RootDesignerSerializer("System.ComponentModel.Design.Serialization.RootCodeDomSerializer, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.Serialization.CodeDomSerializer, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", true)]
	[Designer("System.ComponentModel.Design.ComponentDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(IDesigner))]
	public interface IComponent : IDisposable
	{
		/// <summary>Gets or sets the <see cref="T:System.ComponentModel.ISite" /> associated with the <see cref="T:System.ComponentModel.IComponent" />.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.ISite" /> object associated with the component; or null, if the component does not have a site.</returns>
		// Token: 0x17000389 RID: 905
		// (get) Token: 0x060010A8 RID: 4264
		// (set) Token: 0x060010A9 RID: 4265
		ISite Site { get; set; }
	}
}
