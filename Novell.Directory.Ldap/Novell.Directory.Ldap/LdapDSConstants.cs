using System;

namespace Novell.Directory.Ldap
{
	// Token: 0x0200002D RID: 45
	public struct LdapDSConstants
	{
		// Token: 0x040000BF RID: 191
		public static readonly long LDAP_DS_ENTRY_BROWSE = 1L;

		// Token: 0x040000C0 RID: 192
		public static readonly long LDAP_DS_ENTRY_ADD = 2L;

		// Token: 0x040000C1 RID: 193
		public static readonly long LDAP_DS_ENTRY_DELETE = 4L;

		// Token: 0x040000C2 RID: 194
		public static readonly long LDAP_DS_ENTRY_RENAME = 8L;

		// Token: 0x040000C3 RID: 195
		public static readonly long LDAP_DS_ENTRY_SUPERVISOR = 16L;

		// Token: 0x040000C4 RID: 196
		public static readonly long LDAP_DS_ENTRY_INHERIT_CTL = 64L;

		// Token: 0x040000C5 RID: 197
		public static readonly long LDAP_DS_ATTR_COMPARE = 1L;

		// Token: 0x040000C6 RID: 198
		public static readonly long LDAP_DS_ATTR_READ = 2L;

		// Token: 0x040000C7 RID: 199
		public static readonly long LDAP_DS_ATTR_WRITE = 4L;

		// Token: 0x040000C8 RID: 200
		public static readonly long LDAP_DS_ATTR_SELF = 8L;

		// Token: 0x040000C9 RID: 201
		public static readonly long LDAP_DS_ATTR_SUPERVISOR = 32L;

		// Token: 0x040000CA RID: 202
		public static readonly long LDAP_DS_ATTR_INHERIT_CTL = 64L;

		// Token: 0x040000CB RID: 203
		public static readonly long LDAP_DS_DYNAMIC_ACL = 1073741824L;

		// Token: 0x040000CC RID: 204
		public static readonly int LDAP_DS_ALIAS_ENTRY = 1;

		// Token: 0x040000CD RID: 205
		public static readonly int LDAP_DS_PARTITION_ROOT = 2;

		// Token: 0x040000CE RID: 206
		public static readonly int LDAP_DS_CONTAINER_ENTRY = 4;

		// Token: 0x040000CF RID: 207
		public static readonly int LDAP_DS_CONTAINER_ALIAS = 8;

		// Token: 0x040000D0 RID: 208
		public static readonly int LDAP_DS_MATCHES_LIST_FILTER = 16;

		// Token: 0x040000D1 RID: 209
		public static readonly int LDAP_DS_REFERENCE_ENTRY = 32;

		// Token: 0x040000D2 RID: 210
		public static readonly int LDAP_DS_40X_REFERENCE_ENTRY = 64;

		// Token: 0x040000D3 RID: 211
		public static readonly int LDAP_DS_BACKLINKED = 128;

		// Token: 0x040000D4 RID: 212
		public static readonly int LDAP_DS_NEW_ENTRY = 256;

		// Token: 0x040000D5 RID: 213
		public static readonly int LDAP_DS_TEMPORARY_REFERENCE = 512;

		// Token: 0x040000D6 RID: 214
		public static readonly int LDAP_DS_AUDITED = 1024;

		// Token: 0x040000D7 RID: 215
		public static readonly int LDAP_DS_ENTRY_NOT_PRESENT = 2048;

		// Token: 0x040000D8 RID: 216
		public static readonly int LDAP_DS_ENTRY_VERIFY_CTS = 4096;

		// Token: 0x040000D9 RID: 217
		public static readonly int LDAP_DS_ENTRY_DAMAGED = 8192;
	}
}
