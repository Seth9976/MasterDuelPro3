using System;
using System.CodeDom.Compiler;
using System.Configuration;
using System.Xml.Serialization.Configuration;

namespace System.Xml.Serialization
{
	// Token: 0x02000154 RID: 340
	internal sealed class XmlSerializerCompilerParameters
	{
		// Token: 0x060010C4 RID: 4292 RVA: 0x00052681 File Offset: 0x00050881
		private XmlSerializerCompilerParameters(CompilerParameters parameters, bool needTempDirAccess)
		{
			this.needTempDirAccess = needTempDirAccess;
			this.parameters = parameters;
		}

		// Token: 0x170003BB RID: 955
		// (get) Token: 0x060010C5 RID: 4293 RVA: 0x00052697 File Offset: 0x00050897
		internal bool IsNeedTempDirAccess
		{
			get
			{
				return this.needTempDirAccess;
			}
		}

		// Token: 0x170003BC RID: 956
		// (get) Token: 0x060010C6 RID: 4294 RVA: 0x0005269F File Offset: 0x0005089F
		internal CompilerParameters CodeDomParameters
		{
			get
			{
				return this.parameters;
			}
		}

		// Token: 0x060010C7 RID: 4295 RVA: 0x000526A8 File Offset: 0x000508A8
		internal static XmlSerializerCompilerParameters Create(string location)
		{
			CompilerParameters compilerParameters = new CompilerParameters();
			compilerParameters.GenerateInMemory = true;
			if (string.IsNullOrEmpty(location))
			{
				XmlSerializerSection xmlSerializerSection = ConfigurationManager.GetSection(ConfigurationStrings.XmlSerializerSectionPath) as XmlSerializerSection;
				location = ((xmlSerializerSection == null) ? location : xmlSerializerSection.TempFilesLocation);
				if (!string.IsNullOrEmpty(location))
				{
					location = location.Trim();
				}
			}
			compilerParameters.TempFiles = new TempFileCollection(location);
			return new XmlSerializerCompilerParameters(compilerParameters, string.IsNullOrEmpty(location));
		}

		// Token: 0x060010C8 RID: 4296 RVA: 0x0005270E File Offset: 0x0005090E
		internal static XmlSerializerCompilerParameters Create(CompilerParameters parameters, bool needTempDirAccess)
		{
			return new XmlSerializerCompilerParameters(parameters, needTempDirAccess);
		}

		// Token: 0x04000817 RID: 2071
		private bool needTempDirAccess;

		// Token: 0x04000818 RID: 2072
		private CompilerParameters parameters;
	}
}
