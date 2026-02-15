using System;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	/// <summary>Creates or associates parameter information.</summary>
	// Token: 0x0200067C RID: 1660
	[ComDefaultInterface(typeof(_ParameterBuilder))]
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.None)]
	[StructLayout(LayoutKind.Sequential)]
	public class ParameterBuilder : _ParameterBuilder
	{
		// Token: 0x0600336B RID: 13163 RVA: 0x000C1DD0 File Offset: 0x000BFFD0
		internal ParameterBuilder(MethodBase mb, int pos, ParameterAttributes attributes, string strParamName)
		{
			this.name = strParamName;
			this.position = pos;
			this.attrs = attributes;
			this.methodb = mb;
			if (mb is DynamicMethod)
			{
				this.table_idx = 0;
				return;
			}
			this.table_idx = mb.get_next_table_index(this, 8, 1);
		}

		/// <summary>Retrieves the attributes for this parameter.</summary>
		/// <returns>Read-only. Retrieves the attributes for this parameter.</returns>
		// Token: 0x17000773 RID: 1907
		// (get) Token: 0x0600336C RID: 13164 RVA: 0x000C1E1F File Offset: 0x000C001F
		public virtual int Attributes
		{
			get
			{
				return (int)this.attrs;
			}
		}

		/// <summary>Retrieves the name of this parameter.</summary>
		/// <returns>Read-only. Retrieves the name of this parameter.</returns>
		// Token: 0x17000774 RID: 1908
		// (get) Token: 0x0600336D RID: 13165 RVA: 0x000C1E27 File Offset: 0x000C0027
		public virtual string Name
		{
			get
			{
				return this.name;
			}
		}

		/// <summary>Retrieves the signature position for this parameter.</summary>
		/// <returns>Read-only. Retrieves the signature position for this parameter.</returns>
		// Token: 0x17000775 RID: 1909
		// (get) Token: 0x0600336E RID: 13166 RVA: 0x000C1E2F File Offset: 0x000C002F
		public virtual int Position
		{
			get
			{
				return this.position;
			}
		}

		// Token: 0x04001ADC RID: 6876
		private MethodBase methodb;

		// Token: 0x04001ADD RID: 6877
		private string name;

		// Token: 0x04001ADE RID: 6878
		private CustomAttributeBuilder[] cattrs;

		// Token: 0x04001ADF RID: 6879
		private UnmanagedMarshal marshal_info;

		// Token: 0x04001AE0 RID: 6880
		private ParameterAttributes attrs;

		// Token: 0x04001AE1 RID: 6881
		private int position;

		// Token: 0x04001AE2 RID: 6882
		private int table_idx;

		// Token: 0x04001AE3 RID: 6883
		private object def_value;
	}
}
