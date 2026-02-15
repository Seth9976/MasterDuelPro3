using System;
using System.Configuration.Internal;
using System.IO;
using System.Xml;

// Token: 0x02000002 RID: 2
internal class ConfigXmlTextReader : XmlTextReader, IConfigErrorInfo
{
	// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
	public ConfigXmlTextReader(Stream s, string fileName)
		: base(s)
	{
		if (fileName == null)
		{
			throw new ArgumentNullException("fileName");
		}
		this.fileName = fileName;
	}

	// Token: 0x06000002 RID: 2 RVA: 0x0000206E File Offset: 0x0000026E
	public ConfigXmlTextReader(TextReader input, string fileName)
		: base(input)
	{
		if (fileName == null)
		{
			throw new ArgumentNullException("fileName");
		}
		this.fileName = fileName;
	}

	// Token: 0x17000001 RID: 1
	// (get) Token: 0x06000003 RID: 3 RVA: 0x0000208C File Offset: 0x0000028C
	public string Filename
	{
		get
		{
			return this.fileName;
		}
	}

	// Token: 0x04000001 RID: 1
	private readonly string fileName;
}
