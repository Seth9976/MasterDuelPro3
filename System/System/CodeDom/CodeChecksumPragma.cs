using System;

namespace System.CodeDom
{
	/// <summary>Represents a code checksum pragma code entity.  </summary>
	// Token: 0x020001E8 RID: 488
	[Serializable]
	public class CodeChecksumPragma : CodeDirective
	{
		/// <summary>Gets or sets the path to the checksum file.</summary>
		/// <returns>The path to the checksum file.</returns>
		// Token: 0x17000244 RID: 580
		// (get) Token: 0x06000BD1 RID: 3025 RVA: 0x0003A8E9 File Offset: 0x00038AE9
		public string FileName
		{
			get
			{
				return this._fileName ?? string.Empty;
			}
		}

		// Token: 0x0400089B RID: 2203
		private string _fileName;
	}
}
