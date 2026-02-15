using System;

namespace Mono.Security.Interface
{
	// Token: 0x02000039 RID: 57
	public class Alert
	{
		// Token: 0x17000051 RID: 81
		// (get) Token: 0x0600012E RID: 302 RVA: 0x0000926A File Offset: 0x0000746A
		public AlertLevel Level
		{
			get
			{
				return this.level;
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x0600012F RID: 303 RVA: 0x00009272 File Offset: 0x00007472
		public AlertDescription Description
		{
			get
			{
				return this.description;
			}
		}

		// Token: 0x06000130 RID: 304 RVA: 0x0000927A File Offset: 0x0000747A
		public Alert(AlertDescription description)
		{
			this.description = description;
			this.inferAlertLevel();
		}

		// Token: 0x06000131 RID: 305 RVA: 0x00009290 File Offset: 0x00007490
		private void inferAlertLevel()
		{
			AlertDescription alertDescription = this.description;
			if (alertDescription <= AlertDescription.ExportRestriction)
			{
				if (alertDescription <= AlertDescription.UnexpectedMessage)
				{
					if (alertDescription != AlertDescription.CloseNotify)
					{
						if (alertDescription != AlertDescription.UnexpectedMessage)
						{
							goto IL_00C5;
						}
						goto IL_00C5;
					}
				}
				else
				{
					if (alertDescription - AlertDescription.BadRecordMAC <= 2)
					{
						goto IL_00C5;
					}
					switch (alertDescription)
					{
					case AlertDescription.DecompressionFailure:
					case (AlertDescription)31:
					case (AlertDescription)32:
					case (AlertDescription)33:
					case (AlertDescription)34:
					case (AlertDescription)35:
					case (AlertDescription)36:
					case (AlertDescription)37:
					case (AlertDescription)38:
					case (AlertDescription)39:
					case AlertDescription.HandshakeFailure:
					case AlertDescription.NoCertificate_RESERVED:
					case AlertDescription.BadCertificate:
					case AlertDescription.UnsupportedCertificate:
					case AlertDescription.CertificateRevoked:
					case AlertDescription.CertificateExpired:
					case AlertDescription.CertificateUnknown:
					case AlertDescription.IlegalParameter:
					case AlertDescription.UnknownCA:
					case AlertDescription.AccessDenied:
					case AlertDescription.DecodeError:
					case AlertDescription.DecryptError:
						goto IL_00C5;
					default:
						if (alertDescription != AlertDescription.ExportRestriction)
						{
							goto IL_00C5;
						}
						goto IL_00C5;
					}
				}
			}
			else if (alertDescription <= AlertDescription.InternalError)
			{
				if (alertDescription - AlertDescription.ProtocolVersion > 1 && alertDescription != AlertDescription.InternalError)
				{
					goto IL_00C5;
				}
				goto IL_00C5;
			}
			else if (alertDescription != AlertDescription.UserCancelled && alertDescription != AlertDescription.NoRenegotiation)
			{
				if (alertDescription != AlertDescription.UnsupportedExtension)
				{
					goto IL_00C5;
				}
				goto IL_00C5;
			}
			this.level = AlertLevel.Warning;
			return;
			IL_00C5:
			this.level = AlertLevel.Fatal;
		}

		// Token: 0x06000132 RID: 306 RVA: 0x00009369 File Offset: 0x00007569
		public override string ToString()
		{
			return string.Format("[Alert: {0}:{1}]", this.Level, this.Description);
		}

		// Token: 0x040000B6 RID: 182
		private AlertLevel level;

		// Token: 0x040000B7 RID: 183
		private AlertDescription description;
	}
}
