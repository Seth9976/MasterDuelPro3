using System;

namespace System.Xml.Schema
{
	/// <summary>Provides schema compilation options for the <see cref="T:System.Xml.Schema.XmlSchemaSet" /> class This class cannot be inherited.</summary>
	// Token: 0x020002C2 RID: 706
	public sealed class XmlSchemaCompilationSettings
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Schema.XmlSchemaCompilationSettings" /> class. </summary>
		// Token: 0x06002068 RID: 8296 RVA: 0x000BEB33 File Offset: 0x000BCD33
		public XmlSchemaCompilationSettings()
		{
			this.enableUpaCheck = true;
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Xml.Schema.XmlSchemaSet" /> should check for Unique Particle Attribution (UPA) violations.</summary>
		/// <returns>true if the <see cref="T:System.Xml.Schema.XmlSchemaSet" /> should check for Unique Particle Attribution (UPA) violations; otherwise, false. The default is true.</returns>
		// Token: 0x170007B8 RID: 1976
		// (get) Token: 0x06002069 RID: 8297 RVA: 0x000BEB42 File Offset: 0x000BCD42
		public bool EnableUpaCheck
		{
			get
			{
				return this.enableUpaCheck;
			}
		}

		// Token: 0x04000F23 RID: 3875
		private bool enableUpaCheck;
	}
}
