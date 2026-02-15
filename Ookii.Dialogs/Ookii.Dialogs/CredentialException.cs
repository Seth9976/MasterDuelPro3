using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using Ookii.Dialogs.Properties;

namespace Ookii.Dialogs
{
	// Token: 0x02000009 RID: 9
	[Serializable]
	public class CredentialException : Win32Exception
	{
		// Token: 0x06000044 RID: 68 RVA: 0x00003202 File Offset: 0x00001402
		public CredentialException()
			: base(Resources.CredentialError)
		{
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00003211 File Offset: 0x00001411
		public CredentialException(int error)
			: base(error)
		{
		}

		// Token: 0x06000046 RID: 70 RVA: 0x0000321C File Offset: 0x0000141C
		public CredentialException(string message)
			: base(message)
		{
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00003227 File Offset: 0x00001427
		public CredentialException(int error, string message)
			: base(error, message)
		{
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00003233 File Offset: 0x00001433
		public CredentialException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x06000049 RID: 73 RVA: 0x0000323F File Offset: 0x0000143F
		protected CredentialException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
