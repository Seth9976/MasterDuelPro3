using System;
using System.Globalization;
using Novell.Directory.Ldap.Utilclass;

namespace Novell.Directory.Ldap
{
	// Token: 0x02000030 RID: 48
	public class LdapException : Exception
	{
		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060001EA RID: 490 RVA: 0x00008B2F File Offset: 0x00006D2F
		public virtual string LdapErrorMessage
		{
			get
			{
				if (this.serverMessage != null && this.serverMessage.Length == 0)
				{
					return null;
				}
				return this.serverMessage;
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060001EB RID: 491 RVA: 0x00008B4E File Offset: 0x00006D4E
		public virtual Exception Cause
		{
			get
			{
				return this.rootException;
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060001EC RID: 492 RVA: 0x00008B56 File Offset: 0x00006D56
		public virtual int ResultCode
		{
			get
			{
				return this.resultCode;
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060001ED RID: 493 RVA: 0x00008B5E File Offset: 0x00006D5E
		public virtual string MatchedDN
		{
			get
			{
				return this.matchedDN;
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060001EE RID: 494 RVA: 0x00008B66 File Offset: 0x00006D66
		public override string Message
		{
			get
			{
				return this.resultCodeToString();
			}
		}

		// Token: 0x060001EF RID: 495 RVA: 0x00008B6E File Offset: 0x00006D6E
		public LdapException()
		{
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x00008B76 File Offset: 0x00006D76
		public LdapException(string messageOrKey, int resultCode, string serverMsg)
			: this(messageOrKey, null, resultCode, serverMsg, null, null)
		{
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x00008B84 File Offset: 0x00006D84
		public LdapException(string messageOrKey, object[] arguments, int resultCode, string serverMsg)
			: this(messageOrKey, arguments, resultCode, serverMsg, null, null)
		{
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x00008B93 File Offset: 0x00006D93
		public LdapException(string messageOrKey, int resultCode, string serverMsg, Exception rootException)
			: this(messageOrKey, null, resultCode, serverMsg, null, rootException)
		{
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x00008BA2 File Offset: 0x00006DA2
		public LdapException(string messageOrKey, object[] arguments, int resultCode, string serverMsg, Exception rootException)
			: this(messageOrKey, arguments, resultCode, serverMsg, null, rootException)
		{
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x00008BB2 File Offset: 0x00006DB2
		public LdapException(string messageOrKey, int resultCode, string serverMsg, string matchedDN)
			: this(messageOrKey, null, resultCode, serverMsg, matchedDN, null)
		{
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x00008BC1 File Offset: 0x00006DC1
		public LdapException(string messageOrKey, object[] arguments, int resultCode, string serverMsg, string matchedDN)
			: this(messageOrKey, arguments, resultCode, serverMsg, matchedDN, null)
		{
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x00008BD1 File Offset: 0x00006DD1
		internal LdapException(string messageOrKey, object[] arguments, int resultCode, string serverMsg, string matchedDN, Exception rootException)
		{
			this.messageOrKey = messageOrKey;
			this.arguments = arguments;
			this.resultCode = resultCode;
			this.rootException = rootException;
			this.matchedDN = matchedDN;
			this.serverMessage = serverMsg;
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x00008C06 File Offset: 0x00006E06
		public virtual string resultCodeToString()
		{
			return ResourcesHandler.getResultString(this.resultCode);
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x00008C13 File Offset: 0x00006E13
		public static string resultCodeToString(int code)
		{
			return ResourcesHandler.getResultString(code);
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x00008C1B File Offset: 0x00006E1B
		public virtual string resultCodeToString(CultureInfo locale)
		{
			return ResourcesHandler.getResultString(this.resultCode, locale);
		}

		// Token: 0x060001FA RID: 506 RVA: 0x00008C29 File Offset: 0x00006E29
		public static string resultCodeToString(int code, CultureInfo locale)
		{
			return ResourcesHandler.getResultString(code, locale);
		}

		// Token: 0x060001FB RID: 507 RVA: 0x00008C32 File Offset: 0x00006E32
		public override string ToString()
		{
			return this.getExceptionString("LdapException");
		}

		// Token: 0x060001FC RID: 508 RVA: 0x00008C40 File Offset: 0x00006E40
		internal virtual string getExceptionString(string exception)
		{
			string text = ResourcesHandler.getMessage("TOSTRING", new object[]
			{
				exception,
				base.Message,
				this.resultCode,
				this.resultCodeToString()
			});
			if (text.ToUpper().Equals("TOSTRING".ToUpper()))
			{
				text = string.Concat(new string[]
				{
					exception,
					": (",
					this.resultCode.ToString(),
					") ",
					this.resultCodeToString()
				});
			}
			if (this.serverMessage != null && this.serverMessage.Length != 0)
			{
				string text2 = ResourcesHandler.getMessage("SERVER_MSG", new object[] { exception, this.serverMessage });
				if (text2.ToUpper().Equals("SERVER_MSG".ToUpper()))
				{
					text2 = exception + ": Server Message: " + this.serverMessage;
				}
				text = text + "\n" + text2;
			}
			if (this.matchedDN != null)
			{
				string text2 = ResourcesHandler.getMessage("MATCHED_DN", new object[] { exception, this.matchedDN });
				if (text2.ToUpper().Equals("MATCHED_DN".ToUpper()))
				{
					text2 = exception + ": Matched DN: " + this.matchedDN;
				}
				text = text + "\n" + text2;
			}
			if (this.rootException != null)
			{
				text = text + "\n" + this.rootException.ToString();
			}
			return text;
		}

		// Token: 0x040000DC RID: 220
		private int resultCode;

		// Token: 0x040000DD RID: 221
		private string messageOrKey;

		// Token: 0x040000DE RID: 222
		private object[] arguments;

		// Token: 0x040000DF RID: 223
		private string matchedDN;

		// Token: 0x040000E0 RID: 224
		private Exception rootException;

		// Token: 0x040000E1 RID: 225
		private string serverMessage;

		// Token: 0x040000E2 RID: 226
		public const int SUCCESS = 0;

		// Token: 0x040000E3 RID: 227
		public const int OPERATIONS_ERROR = 1;

		// Token: 0x040000E4 RID: 228
		public const int PROTOCOL_ERROR = 2;

		// Token: 0x040000E5 RID: 229
		public const int TIME_LIMIT_EXCEEDED = 3;

		// Token: 0x040000E6 RID: 230
		public const int SIZE_LIMIT_EXCEEDED = 4;

		// Token: 0x040000E7 RID: 231
		public const int COMPARE_FALSE = 5;

		// Token: 0x040000E8 RID: 232
		public const int COMPARE_TRUE = 6;

		// Token: 0x040000E9 RID: 233
		public const int AUTH_METHOD_NOT_SUPPORTED = 7;

		// Token: 0x040000EA RID: 234
		public const int STRONG_AUTH_REQUIRED = 8;

		// Token: 0x040000EB RID: 235
		public const int Ldap_PARTIAL_RESULTS = 9;

		// Token: 0x040000EC RID: 236
		public const int REFERRAL = 10;

		// Token: 0x040000ED RID: 237
		public const int ADMIN_LIMIT_EXCEEDED = 11;

		// Token: 0x040000EE RID: 238
		public const int UNAVAILABLE_CRITICAL_EXTENSION = 12;

		// Token: 0x040000EF RID: 239
		public const int CONFIDENTIALITY_REQUIRED = 13;

		// Token: 0x040000F0 RID: 240
		public const int SASL_BIND_IN_PROGRESS = 14;

		// Token: 0x040000F1 RID: 241
		public const int NO_SUCH_ATTRIBUTE = 16;

		// Token: 0x040000F2 RID: 242
		public const int UNDEFINED_ATTRIBUTE_TYPE = 17;

		// Token: 0x040000F3 RID: 243
		public const int INAPPROPRIATE_MATCHING = 18;

		// Token: 0x040000F4 RID: 244
		public const int CONSTRAINT_VIOLATION = 19;

		// Token: 0x040000F5 RID: 245
		public const int ATTRIBUTE_OR_VALUE_EXISTS = 20;

		// Token: 0x040000F6 RID: 246
		public const int INVALID_ATTRIBUTE_SYNTAX = 21;

		// Token: 0x040000F7 RID: 247
		public const int NO_SUCH_OBJECT = 32;

		// Token: 0x040000F8 RID: 248
		public const int ALIAS_PROBLEM = 33;

		// Token: 0x040000F9 RID: 249
		public const int INVALID_DN_SYNTAX = 34;

		// Token: 0x040000FA RID: 250
		public const int IS_LEAF = 35;

		// Token: 0x040000FB RID: 251
		public const int ALIAS_DEREFERENCING_PROBLEM = 36;

		// Token: 0x040000FC RID: 252
		public const int INAPPROPRIATE_AUTHENTICATION = 48;

		// Token: 0x040000FD RID: 253
		public const int INVALID_CREDENTIALS = 49;

		// Token: 0x040000FE RID: 254
		public const int INSUFFICIENT_ACCESS_RIGHTS = 50;

		// Token: 0x040000FF RID: 255
		public const int BUSY = 51;

		// Token: 0x04000100 RID: 256
		public const int UNAVAILABLE = 52;

		// Token: 0x04000101 RID: 257
		public const int UNWILLING_TO_PERFORM = 53;

		// Token: 0x04000102 RID: 258
		public const int LOOP_DETECT = 54;

		// Token: 0x04000103 RID: 259
		public const int NAMING_VIOLATION = 64;

		// Token: 0x04000104 RID: 260
		public const int OBJECT_CLASS_VIOLATION = 65;

		// Token: 0x04000105 RID: 261
		public const int NOT_ALLOWED_ON_NONLEAF = 66;

		// Token: 0x04000106 RID: 262
		public const int NOT_ALLOWED_ON_RDN = 67;

		// Token: 0x04000107 RID: 263
		public const int ENTRY_ALREADY_EXISTS = 68;

		// Token: 0x04000108 RID: 264
		public const int OBJECT_CLASS_MODS_PROHIBITED = 69;

		// Token: 0x04000109 RID: 265
		public const int AFFECTS_MULTIPLE_DSAS = 71;

		// Token: 0x0400010A RID: 266
		public const int OTHER = 80;

		// Token: 0x0400010B RID: 267
		public const int SERVER_DOWN = 81;

		// Token: 0x0400010C RID: 268
		public const int LOCAL_ERROR = 82;

		// Token: 0x0400010D RID: 269
		public const int ENCODING_ERROR = 83;

		// Token: 0x0400010E RID: 270
		public const int DECODING_ERROR = 84;

		// Token: 0x0400010F RID: 271
		public const int Ldap_TIMEOUT = 85;

		// Token: 0x04000110 RID: 272
		public const int AUTH_UNKNOWN = 86;

		// Token: 0x04000111 RID: 273
		public const int FILTER_ERROR = 87;

		// Token: 0x04000112 RID: 274
		public const int USER_CANCELLED = 88;

		// Token: 0x04000113 RID: 275
		public const int NO_MEMORY = 90;

		// Token: 0x04000114 RID: 276
		public const int CONNECT_ERROR = 91;

		// Token: 0x04000115 RID: 277
		public const int Ldap_NOT_SUPPORTED = 92;

		// Token: 0x04000116 RID: 278
		public const int CONTROL_NOT_FOUND = 93;

		// Token: 0x04000117 RID: 279
		public const int NO_RESULTS_RETURNED = 94;

		// Token: 0x04000118 RID: 280
		public const int MORE_RESULTS_TO_RETURN = 95;

		// Token: 0x04000119 RID: 281
		public const int CLIENT_LOOP = 96;

		// Token: 0x0400011A RID: 282
		public const int REFERRAL_LIMIT_EXCEEDED = 97;

		// Token: 0x0400011B RID: 283
		public const int INVALID_RESPONSE = 100;

		// Token: 0x0400011C RID: 284
		public const int AMBIGUOUS_RESPONSE = 101;

		// Token: 0x0400011D RID: 285
		public const int TLS_NOT_SUPPORTED = 112;

		// Token: 0x0400011E RID: 286
		public const int SSL_HANDSHAKE_FAILED = 113;

		// Token: 0x0400011F RID: 287
		public const int SSL_PROVIDER_NOT_FOUND = 114;
	}
}
