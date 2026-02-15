using System;

namespace Novell.Directory.Ldap.Extensions
{
	// Token: 0x020000B2 RID: 178
	public class ReplicationConstants
	{
		// Token: 0x040002E6 RID: 742
		public const string CREATE_NAMING_CONTEXT_REQ = "2.16.840.1.113719.1.27.100.3";

		// Token: 0x040002E7 RID: 743
		public const string CREATE_NAMING_CONTEXT_RES = "2.16.840.1.113719.1.27.100.4";

		// Token: 0x040002E8 RID: 744
		public const string MERGE_NAMING_CONTEXT_REQ = "2.16.840.1.113719.1.27.100.5";

		// Token: 0x040002E9 RID: 745
		public const string MERGE_NAMING_CONTEXT_RES = "2.16.840.1.113719.1.27.100.6";

		// Token: 0x040002EA RID: 746
		public const string ADD_REPLICA_REQ = "2.16.840.1.113719.1.27.100.7";

		// Token: 0x040002EB RID: 747
		public const string ADD_REPLICA_RES = "2.16.840.1.113719.1.27.100.8";

		// Token: 0x040002EC RID: 748
		public const string REFRESH_SERVER_REQ = "2.16.840.1.113719.1.27.100.9";

		// Token: 0x040002ED RID: 749
		public const string REFRESH_SERVER_RES = "2.16.840.1.113719.1.27.100.10";

		// Token: 0x040002EE RID: 750
		public const string DELETE_REPLICA_REQ = "2.16.840.1.113719.1.27.100.11";

		// Token: 0x040002EF RID: 751
		public const string DELETE_REPLICA_RES = "2.16.840.1.113719.1.27.100.12";

		// Token: 0x040002F0 RID: 752
		public const string NAMING_CONTEXT_COUNT_REQ = "2.16.840.1.113719.1.27.100.13";

		// Token: 0x040002F1 RID: 753
		public const string NAMING_CONTEXT_COUNT_RES = "2.16.840.1.113719.1.27.100.14";

		// Token: 0x040002F2 RID: 754
		public const string CHANGE_REPLICA_TYPE_REQ = "2.16.840.1.113719.1.27.100.15";

		// Token: 0x040002F3 RID: 755
		public const string CHANGE_REPLICA_TYPE_RES = "2.16.840.1.113719.1.27.100.16";

		// Token: 0x040002F4 RID: 756
		public const string GET_REPLICA_INFO_REQ = "2.16.840.1.113719.1.27.100.17";

		// Token: 0x040002F5 RID: 757
		public const string GET_REPLICA_INFO_RES = "2.16.840.1.113719.1.27.100.18";

		// Token: 0x040002F6 RID: 758
		public const string LIST_REPLICAS_REQ = "2.16.840.1.113719.1.27.100.19";

		// Token: 0x040002F7 RID: 759
		public const string LIST_REPLICAS_RES = "2.16.840.1.113719.1.27.100.20";

		// Token: 0x040002F8 RID: 760
		public const string RECEIVE_ALL_UPDATES_REQ = "2.16.840.1.113719.1.27.100.21";

		// Token: 0x040002F9 RID: 761
		public const string RECEIVE_ALL_UPDATES_RES = "2.16.840.1.113719.1.27.100.22";

		// Token: 0x040002FA RID: 762
		public const string SEND_ALL_UPDATES_REQ = "2.16.840.1.113719.1.27.100.23";

		// Token: 0x040002FB RID: 763
		public const string SEND_ALL_UPDATES_RES = "2.16.840.1.113719.1.27.100.24";

		// Token: 0x040002FC RID: 764
		public const string NAMING_CONTEXT_SYNC_REQ = "2.16.840.1.113719.1.27.100.25";

		// Token: 0x040002FD RID: 765
		public const string NAMING_CONTEXT_SYNC_RES = "2.16.840.1.113719.1.27.100.26";

		// Token: 0x040002FE RID: 766
		public const string SCHEMA_SYNC_REQ = "2.16.840.1.113719.1.27.100.27";

		// Token: 0x040002FF RID: 767
		public const string SCHEMA_SYNC_RES = "2.16.840.1.113719.1.27.100.28";

		// Token: 0x04000300 RID: 768
		public const string ABORT_NAMING_CONTEXT_OP_REQ = "2.16.840.1.113719.1.27.100.29";

		// Token: 0x04000301 RID: 769
		public const string ABORT_NAMING_CONTEXT_OP_RES = "2.16.840.1.113719.1.27.100.30";

		// Token: 0x04000302 RID: 770
		public const string GET_IDENTITY_NAME_REQ = "2.16.840.1.113719.1.27.100.31";

		// Token: 0x04000303 RID: 771
		public const string GET_IDENTITY_NAME_RES = "2.16.840.1.113719.1.27.100.32";

		// Token: 0x04000304 RID: 772
		public const string GET_EFFECTIVE_PRIVILEGES_REQ = "2.16.840.1.113719.1.27.100.33";

		// Token: 0x04000305 RID: 773
		public const string GET_EFFECTIVE_PRIVILEGES_RES = "2.16.840.1.113719.1.27.100.34";

		// Token: 0x04000306 RID: 774
		public const string SET_REPLICATION_FILTER_REQ = "2.16.840.1.113719.1.27.100.35";

		// Token: 0x04000307 RID: 775
		public const string SET_REPLICATION_FILTER_RES = "2.16.840.1.113719.1.27.100.36";

		// Token: 0x04000308 RID: 776
		public const string GET_REPLICATION_FILTER_REQ = "2.16.840.1.113719.1.27.100.37";

		// Token: 0x04000309 RID: 777
		public const string GET_REPLICATION_FILTER_RES = "2.16.840.1.113719.1.27.100.38";

		// Token: 0x0400030A RID: 778
		public const string CREATE_ORPHAN_NAMING_CONTEXT_REQ = "2.16.840.1.113719.1.27.100.39";

		// Token: 0x0400030B RID: 779
		public const string CREATE_ORPHAN_NAMING_CONTEXT_RES = "2.16.840.1.113719.1.27.100.40";

		// Token: 0x0400030C RID: 780
		public const string REMOVE_ORPHAN_NAMING_CONTEXT_REQ = "2.16.840.1.113719.1.27.100.41";

		// Token: 0x0400030D RID: 781
		public const string REMOVE_ORPHAN_NAMING_CONTEXT_RES = "2.16.840.1.113719.1.27.100.42";

		// Token: 0x0400030E RID: 782
		public const string TRIGGER_BKLINKER_REQ = "2.16.840.1.113719.1.27.100.43";

		// Token: 0x0400030F RID: 783
		public const string TRIGGER_BKLINKER_RES = "2.16.840.1.113719.1.27.100.44";

		// Token: 0x04000310 RID: 784
		public const string TRIGGER_JANITOR_REQ = "2.16.840.1.113719.1.27.100.47";

		// Token: 0x04000311 RID: 785
		public const string TRIGGER_JANITOR_RES = "2.16.840.1.113719.1.27.100.48";

		// Token: 0x04000312 RID: 786
		public const string TRIGGER_LIMBER_REQ = "2.16.840.1.113719.1.27.100.49";

		// Token: 0x04000313 RID: 787
		public const string TRIGGER_LIMBER_RES = "2.16.840.1.113719.1.27.100.50";

		// Token: 0x04000314 RID: 788
		public const string TRIGGER_SKULKER_REQ = "2.16.840.1.113719.1.27.100.51";

		// Token: 0x04000315 RID: 789
		public const string TRIGGER_SKULKER_RES = "2.16.840.1.113719.1.27.100.52";

		// Token: 0x04000316 RID: 790
		public const string TRIGGER_SCHEMA_SYNC_REQ = "2.16.840.1.113719.1.27.100.53";

		// Token: 0x04000317 RID: 791
		public const string TRIGGER_SCHEMA_SYNC_RES = "2.16.840.1.113719.1.27.100.54";

		// Token: 0x04000318 RID: 792
		public const string TRIGGER_PART_PURGE_REQ = "2.16.840.1.113719.1.27.100.55";

		// Token: 0x04000319 RID: 793
		public const string TRIGGER_PART_PURGE_RES = "2.16.840.1.113719.1.27.100.56";

		// Token: 0x0400031A RID: 794
		public const int Ldap_ENSURE_SERVERS_UP = 1;

		// Token: 0x0400031B RID: 795
		public const int Ldap_RT_MASTER = 0;

		// Token: 0x0400031C RID: 796
		public const int Ldap_RT_SECONDARY = 1;

		// Token: 0x0400031D RID: 797
		public const int Ldap_RT_READONLY = 2;

		// Token: 0x0400031E RID: 798
		public const int Ldap_RT_SUBREF = 3;

		// Token: 0x0400031F RID: 799
		public const int Ldap_RT_SPARSE_WRITE = 4;

		// Token: 0x04000320 RID: 800
		public const int Ldap_RT_SPARSE_READ = 5;

		// Token: 0x04000321 RID: 801
		public const int Ldap_RS_ON = 0;

		// Token: 0x04000322 RID: 802
		public const int Ldap_RS_NEW_REPLICA = 1;

		// Token: 0x04000323 RID: 803
		public const int Ldap_RS_DYING_REPLICA = 2;

		// Token: 0x04000324 RID: 804
		public const int Ldap_RS_LOCKED = 3;

		// Token: 0x04000325 RID: 805
		public const int Ldap_RS_TRANSITION_ON = 6;

		// Token: 0x04000326 RID: 806
		public const int Ldap_RS_DEAD_REPLICA = 7;

		// Token: 0x04000327 RID: 807
		public const int Ldap_RS_BEGIN_ADD = 8;

		// Token: 0x04000328 RID: 808
		public const int Ldap_RS_MASTER_START = 11;

		// Token: 0x04000329 RID: 809
		public const int Ldap_RS_MASTER_DONE = 12;

		// Token: 0x0400032A RID: 810
		public const int Ldap_RS_SS_0 = 48;

		// Token: 0x0400032B RID: 811
		public const int Ldap_RS_SS_1 = 49;

		// Token: 0x0400032C RID: 812
		public const int Ldap_RS_JS_0 = 64;

		// Token: 0x0400032D RID: 813
		public const int Ldap_RS_JS_1 = 65;

		// Token: 0x0400032E RID: 814
		public const int Ldap_RS_JS_2 = 66;

		// Token: 0x0400032F RID: 815
		public const int Ldap_DS_FLAG_BUSY = 1;

		// Token: 0x04000330 RID: 816
		public const int Ldap_DS_FLAG_BOUNDARY = 2;
	}
}
