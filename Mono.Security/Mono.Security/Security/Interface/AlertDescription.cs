using System;

namespace Mono.Security.Interface
{
	// Token: 0x02000038 RID: 56
	public enum AlertDescription : byte
	{
		// Token: 0x0400009D RID: 157
		CloseNotify,
		// Token: 0x0400009E RID: 158
		UnexpectedMessage = 10,
		// Token: 0x0400009F RID: 159
		BadRecordMAC = 20,
		// Token: 0x040000A0 RID: 160
		DecryptionFailed_RESERVED,
		// Token: 0x040000A1 RID: 161
		RecordOverflow,
		// Token: 0x040000A2 RID: 162
		DecompressionFailure = 30,
		// Token: 0x040000A3 RID: 163
		HandshakeFailure = 40,
		// Token: 0x040000A4 RID: 164
		NoCertificate_RESERVED,
		// Token: 0x040000A5 RID: 165
		BadCertificate,
		// Token: 0x040000A6 RID: 166
		UnsupportedCertificate,
		// Token: 0x040000A7 RID: 167
		CertificateRevoked,
		// Token: 0x040000A8 RID: 168
		CertificateExpired,
		// Token: 0x040000A9 RID: 169
		CertificateUnknown,
		// Token: 0x040000AA RID: 170
		IlegalParameter,
		// Token: 0x040000AB RID: 171
		UnknownCA,
		// Token: 0x040000AC RID: 172
		AccessDenied,
		// Token: 0x040000AD RID: 173
		DecodeError,
		// Token: 0x040000AE RID: 174
		DecryptError,
		// Token: 0x040000AF RID: 175
		ExportRestriction = 60,
		// Token: 0x040000B0 RID: 176
		ProtocolVersion = 70,
		// Token: 0x040000B1 RID: 177
		InsuficientSecurity,
		// Token: 0x040000B2 RID: 178
		InternalError = 80,
		// Token: 0x040000B3 RID: 179
		UserCancelled = 90,
		// Token: 0x040000B4 RID: 180
		NoRenegotiation = 100,
		// Token: 0x040000B5 RID: 181
		UnsupportedExtension = 110
	}
}
