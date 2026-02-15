using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Mono.Unity
{
	// Token: 0x02000017 RID: 23
	internal static class UnityTls
	{
		// Token: 0x06000058 RID: 88
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetUnityTlsInterface();

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000059 RID: 89 RVA: 0x00002B63 File Offset: 0x00000D63
		public static bool IsSupported
		{
			get
			{
				return UnityTls.NativeInterface != null;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600005A RID: 90 RVA: 0x00002B70 File Offset: 0x00000D70
		public static UnityTls.unitytls_interface_struct NativeInterface
		{
			get
			{
				if (UnityTls.marshalledInterface == null)
				{
					IntPtr unityTlsInterface = UnityTls.GetUnityTlsInterface();
					if (unityTlsInterface == IntPtr.Zero)
					{
						return null;
					}
					UnityTls.marshalledInterface = Marshal.PtrToStructure<UnityTls.unitytls_interface_struct>(unityTlsInterface);
				}
				return UnityTls.marshalledInterface;
			}
		}

		// Token: 0x04000024 RID: 36
		private static UnityTls.unitytls_interface_struct marshalledInterface;

		// Token: 0x02000018 RID: 24
		public enum unitytls_error_code : uint
		{
			// Token: 0x04000026 RID: 38
			UNITYTLS_SUCCESS,
			// Token: 0x04000027 RID: 39
			UNITYTLS_INVALID_ARGUMENT,
			// Token: 0x04000028 RID: 40
			UNITYTLS_INVALID_FORMAT,
			// Token: 0x04000029 RID: 41
			UNITYTLS_INVALID_PASSWORD,
			// Token: 0x0400002A RID: 42
			UNITYTLS_INVALID_STATE,
			// Token: 0x0400002B RID: 43
			UNITYTLS_BUFFER_OVERFLOW,
			// Token: 0x0400002C RID: 44
			UNITYTLS_OUT_OF_MEMORY,
			// Token: 0x0400002D RID: 45
			UNITYTLS_INTERNAL_ERROR,
			// Token: 0x0400002E RID: 46
			UNITYTLS_NOT_SUPPORTED,
			// Token: 0x0400002F RID: 47
			UNITYTLS_ENTROPY_SOURCE_FAILED,
			// Token: 0x04000030 RID: 48
			UNITYTLS_STREAM_CLOSED,
			// Token: 0x04000031 RID: 49
			UNITYTLS_DER_PARSE_ERROR,
			// Token: 0x04000032 RID: 50
			UNITYTLS_KEY_PARSE_ERROR,
			// Token: 0x04000033 RID: 51
			UNITYTLS_SSL_ERROR,
			// Token: 0x04000034 RID: 52
			UNITYTLS_USER_CUSTOM_ERROR_START = 1048576U,
			// Token: 0x04000035 RID: 53
			UNITYTLS_USER_WOULD_BLOCK,
			// Token: 0x04000036 RID: 54
			UNITYTLS_USER_WOULD_BLOCK_READ,
			// Token: 0x04000037 RID: 55
			UNITYTLS_USER_WOULD_BLOCK_WRITE,
			// Token: 0x04000038 RID: 56
			UNITYTLS_USER_READ_FAILED,
			// Token: 0x04000039 RID: 57
			UNITYTLS_USER_WRITE_FAILED,
			// Token: 0x0400003A RID: 58
			UNITYTLS_USER_UNKNOWN_ERROR,
			// Token: 0x0400003B RID: 59
			UNITYTLS_SSL_NEEDS_VERIFY,
			// Token: 0x0400003C RID: 60
			UNITYTLS_HANDSHAKE_STEP,
			// Token: 0x0400003D RID: 61
			UNITYTLS_USER_CUSTOM_ERROR_END = 2097152U
		}

		// Token: 0x02000019 RID: 25
		public enum unitytls_log_level : uint
		{
			// Token: 0x0400003F RID: 63
			UNITYTLS_LOGLEVEL_MIN,
			// Token: 0x04000040 RID: 64
			UNITYTLS_LOGLEVEL_FATAL = 0U,
			// Token: 0x04000041 RID: 65
			UNITYTLS_LOGLEVEL_ERROR,
			// Token: 0x04000042 RID: 66
			UNITYTLS_LOGLEVEL_WARN,
			// Token: 0x04000043 RID: 67
			UNITYTLS_LOGLEVEL_INFO,
			// Token: 0x04000044 RID: 68
			UNITYTLS_LOGLEVEL_DEBUG,
			// Token: 0x04000045 RID: 69
			UNITYTLS_LOGLEVEL_TRACE,
			// Token: 0x04000046 RID: 70
			UNITYTLS_LOGLEVEL_MAX = 5U
		}

		// Token: 0x0200001A RID: 26
		public struct unitytls_errorstate
		{
			// Token: 0x04000047 RID: 71
			private uint magic;

			// Token: 0x04000048 RID: 72
			public UnityTls.unitytls_error_code code;

			// Token: 0x04000049 RID: 73
			private ulong reserved;
		}

		// Token: 0x0200001B RID: 27
		public struct unitytls_key
		{
		}

		// Token: 0x0200001C RID: 28
		public struct unitytls_key_ref
		{
			// Token: 0x0400004A RID: 74
			public ulong handle;
		}

		// Token: 0x0200001D RID: 29
		public struct unitytls_x509_ref
		{
			// Token: 0x0400004B RID: 75
			public ulong handle;
		}

		// Token: 0x0200001E RID: 30
		public struct unitytls_x509list
		{
		}

		// Token: 0x0200001F RID: 31
		public struct unitytls_x509list_ref
		{
			// Token: 0x0400004C RID: 76
			public ulong handle;
		}

		// Token: 0x02000020 RID: 32
		[Flags]
		public enum unitytls_x509verify_result : uint
		{
			// Token: 0x0400004E RID: 78
			UNITYTLS_X509VERIFY_SUCCESS = 0U,
			// Token: 0x0400004F RID: 79
			UNITYTLS_X509VERIFY_NOT_DONE = 2147483648U,
			// Token: 0x04000050 RID: 80
			UNITYTLS_X509VERIFY_FATAL_ERROR = 4294967295U,
			// Token: 0x04000051 RID: 81
			UNITYTLS_X509VERIFY_FLAG_EXPIRED = 1U,
			// Token: 0x04000052 RID: 82
			UNITYTLS_X509VERIFY_FLAG_REVOKED = 2U,
			// Token: 0x04000053 RID: 83
			UNITYTLS_X509VERIFY_FLAG_CN_MISMATCH = 4U,
			// Token: 0x04000054 RID: 84
			UNITYTLS_X509VERIFY_FLAG_NOT_TRUSTED = 8U,
			// Token: 0x04000055 RID: 85
			UNITYTLS_X509VERIFY_FLAG_BADCRL_NOT_TRUSTED = 16U,
			// Token: 0x04000056 RID: 86
			UNITYTLS_X509VERIFY_FLAG_BADCRL_EXPIRED = 32U,
			// Token: 0x04000057 RID: 87
			UNITYTLS_X509VERIFY_FLAG_BADCERT_MISSING = 64U,
			// Token: 0x04000058 RID: 88
			UNITYTLS_X509VERIFY_FLAG_BADCERT_SKIP_VERIFY = 128U,
			// Token: 0x04000059 RID: 89
			UNITYTLS_X509VERIFY_FLAG_BADCERT_OTHER = 256U,
			// Token: 0x0400005A RID: 90
			UNITYTLS_X509VERIFY_FLAG_BADCERT_FUTURE = 512U,
			// Token: 0x0400005B RID: 91
			UNITYTLS_X509VERIFY_FLAG_BADCRL_FUTURE = 1024U,
			// Token: 0x0400005C RID: 92
			UNITYTLS_X509VERIFY_FLAG_BADCERT_KEY_USAGE = 2048U,
			// Token: 0x0400005D RID: 93
			UNITYTLS_X509VERIFY_FLAG_BADCERT_EXT_KEY_USAGE = 4096U,
			// Token: 0x0400005E RID: 94
			UNITYTLS_X509VERIFY_FLAG_BADCERT_NS_CERT_TYPE = 8192U,
			// Token: 0x0400005F RID: 95
			UNITYTLS_X509VERIFY_FLAG_BADCERT_BAD_MD = 16384U,
			// Token: 0x04000060 RID: 96
			UNITYTLS_X509VERIFY_FLAG_BADCERT_BAD_PK = 32768U,
			// Token: 0x04000061 RID: 97
			UNITYTLS_X509VERIFY_FLAG_BADCERT_BAD_KEY = 65536U,
			// Token: 0x04000062 RID: 98
			UNITYTLS_X509VERIFY_FLAG_BADCRL_BAD_MD = 131072U,
			// Token: 0x04000063 RID: 99
			UNITYTLS_X509VERIFY_FLAG_BADCRL_BAD_PK = 262144U,
			// Token: 0x04000064 RID: 100
			UNITYTLS_X509VERIFY_FLAG_BADCRL_BAD_KEY = 524288U,
			// Token: 0x04000065 RID: 101
			UNITYTLS_X509VERIFY_FLAG_USER_ERROR1 = 65536U,
			// Token: 0x04000066 RID: 102
			UNITYTLS_X509VERIFY_FLAG_USER_ERROR2 = 131072U,
			// Token: 0x04000067 RID: 103
			UNITYTLS_X509VERIFY_FLAG_USER_ERROR3 = 262144U,
			// Token: 0x04000068 RID: 104
			UNITYTLS_X509VERIFY_FLAG_USER_ERROR4 = 524288U,
			// Token: 0x04000069 RID: 105
			UNITYTLS_X509VERIFY_FLAG_USER_ERROR5 = 1048576U,
			// Token: 0x0400006A RID: 106
			UNITYTLS_X509VERIFY_FLAG_USER_ERROR6 = 2097152U,
			// Token: 0x0400006B RID: 107
			UNITYTLS_X509VERIFY_FLAG_USER_ERROR7 = 4194304U,
			// Token: 0x0400006C RID: 108
			UNITYTLS_X509VERIFY_FLAG_USER_ERROR8 = 8388608U,
			// Token: 0x0400006D RID: 109
			UNITYTLS_X509VERIFY_FLAG_UNKNOWN_ERROR = 134217728U
		}

		// Token: 0x02000021 RID: 33
		// (Invoke) Token: 0x0600005C RID: 92
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public unsafe delegate UnityTls.unitytls_x509verify_result unitytls_x509verify_callback(void* userData, UnityTls.unitytls_x509_ref cert, UnityTls.unitytls_x509verify_result result, UnityTls.unitytls_errorstate* errorState);

		// Token: 0x02000022 RID: 34
		public struct unitytls_tlsctx
		{
		}

		// Token: 0x02000023 RID: 35
		public struct unitytls_x509name
		{
		}

		// Token: 0x02000024 RID: 36
		public enum unitytls_ciphersuite : uint
		{
			// Token: 0x0400006F RID: 111
			UNITYTLS_CIPHERSUITE_INVALID = 16777215U
		}

		// Token: 0x02000025 RID: 37
		public enum unitytls_protocol : uint
		{
			// Token: 0x04000071 RID: 113
			UNITYTLS_PROTOCOL_TLS_1_0,
			// Token: 0x04000072 RID: 114
			UNITYTLS_PROTOCOL_TLS_1_1,
			// Token: 0x04000073 RID: 115
			UNITYTLS_PROTOCOL_TLS_1_2,
			// Token: 0x04000074 RID: 116
			UNITYTLS_PROTOCOL_INVALID
		}

		// Token: 0x02000026 RID: 38
		public struct unitytls_tlsctx_protocolrange
		{
			// Token: 0x04000075 RID: 117
			public UnityTls.unitytls_protocol min;

			// Token: 0x04000076 RID: 118
			public UnityTls.unitytls_protocol max;
		}

		// Token: 0x02000027 RID: 39
		// (Invoke) Token: 0x0600005E RID: 94
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public unsafe delegate IntPtr unitytls_tlsctx_write_callback(void* userData, byte* data, IntPtr bufferLen, UnityTls.unitytls_errorstate* errorState);

		// Token: 0x02000028 RID: 40
		// (Invoke) Token: 0x06000060 RID: 96
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public unsafe delegate IntPtr unitytls_tlsctx_read_callback(void* userData, byte* buffer, IntPtr bufferLen, UnityTls.unitytls_errorstate* errorState);

		// Token: 0x02000029 RID: 41
		// (Invoke) Token: 0x06000062 RID: 98
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public unsafe delegate void unitytls_tlsctx_trace_callback(void* userData, UnityTls.unitytls_tlsctx* ctx, byte* traceMessage, IntPtr traceMessageLen);

		// Token: 0x0200002A RID: 42
		// (Invoke) Token: 0x06000064 RID: 100
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public unsafe delegate void unitytls_tlsctx_certificate_callback(void* userData, UnityTls.unitytls_tlsctx* ctx, byte* cn, IntPtr cnLen, UnityTls.unitytls_x509name* caList, IntPtr caListLen, UnityTls.unitytls_x509list_ref* chain, UnityTls.unitytls_key_ref* key, UnityTls.unitytls_errorstate* errorState);

		// Token: 0x0200002B RID: 43
		// (Invoke) Token: 0x06000066 RID: 102
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public unsafe delegate UnityTls.unitytls_x509verify_result unitytls_tlsctx_x509verify_callback(void* userData, UnityTls.unitytls_x509list_ref chain, UnityTls.unitytls_errorstate* errorState);

		// Token: 0x0200002C RID: 44
		public struct unitytls_tlsctx_callbacks
		{
			// Token: 0x04000077 RID: 119
			public UnityTls.unitytls_tlsctx_read_callback read;

			// Token: 0x04000078 RID: 120
			public UnityTls.unitytls_tlsctx_write_callback write;

			// Token: 0x04000079 RID: 121
			public unsafe void* data;
		}

		// Token: 0x0200002D RID: 45
		[StructLayout(LayoutKind.Sequential)]
		public class unitytls_interface_struct
		{
			// Token: 0x0400007A RID: 122
			public readonly ulong UNITYTLS_INVALID_HANDLE;

			// Token: 0x0400007B RID: 123
			public readonly UnityTls.unitytls_tlsctx_protocolrange UNITYTLS_TLSCTX_PROTOCOLRANGE_DEFAULT;

			// Token: 0x0400007C RID: 124
			public UnityTls.unitytls_interface_struct.unitytls_errorstate_create_t unitytls_errorstate_create;

			// Token: 0x0400007D RID: 125
			public UnityTls.unitytls_interface_struct.unitytls_errorstate_raise_error_t unitytls_errorstate_raise_error;

			// Token: 0x0400007E RID: 126
			public UnityTls.unitytls_interface_struct.unitytls_key_get_ref_t unitytls_key_get_ref;

			// Token: 0x0400007F RID: 127
			public UnityTls.unitytls_interface_struct.unitytls_key_parse_der_t unitytls_key_parse_der;

			// Token: 0x04000080 RID: 128
			public UnityTls.unitytls_interface_struct.unitytls_key_parse_pem_t unitytls_key_parse_pem;

			// Token: 0x04000081 RID: 129
			public UnityTls.unitytls_interface_struct.unitytls_key_free_t unitytls_key_free;

			// Token: 0x04000082 RID: 130
			public UnityTls.unitytls_interface_struct.unitytls_x509_export_der_t unitytls_x509_export_der;

			// Token: 0x04000083 RID: 131
			public UnityTls.unitytls_interface_struct.unitytls_x509list_get_ref_t unitytls_x509list_get_ref;

			// Token: 0x04000084 RID: 132
			public UnityTls.unitytls_interface_struct.unitytls_x509list_get_x509_t unitytls_x509list_get_x509;

			// Token: 0x04000085 RID: 133
			public UnityTls.unitytls_interface_struct.unitytls_x509list_create_t unitytls_x509list_create;

			// Token: 0x04000086 RID: 134
			public UnityTls.unitytls_interface_struct.unitytls_x509list_append_t unitytls_x509list_append;

			// Token: 0x04000087 RID: 135
			public UnityTls.unitytls_interface_struct.unitytls_x509list_append_der_t unitytls_x509list_append_der;

			// Token: 0x04000088 RID: 136
			public UnityTls.unitytls_interface_struct.unitytls_x509list_append_der_t unitytls_x509list_append_pem;

			// Token: 0x04000089 RID: 137
			public UnityTls.unitytls_interface_struct.unitytls_x509list_free_t unitytls_x509list_free;

			// Token: 0x0400008A RID: 138
			public UnityTls.unitytls_interface_struct.unitytls_x509verify_default_ca_t unitytls_x509verify_default_ca;

			// Token: 0x0400008B RID: 139
			public UnityTls.unitytls_interface_struct.unitytls_x509verify_explicit_ca_t unitytls_x509verify_explicit_ca;

			// Token: 0x0400008C RID: 140
			public UnityTls.unitytls_interface_struct.unitytls_tlsctx_create_server_t unitytls_tlsctx_create_server;

			// Token: 0x0400008D RID: 141
			public UnityTls.unitytls_interface_struct.unitytls_tlsctx_create_client_t unitytls_tlsctx_create_client;

			// Token: 0x0400008E RID: 142
			public UnityTls.unitytls_interface_struct.unitytls_tlsctx_server_require_client_authentication_t unitytls_tlsctx_server_require_client_authentication;

			// Token: 0x0400008F RID: 143
			public UnityTls.unitytls_interface_struct.unitytls_tlsctx_set_certificate_callback_t unitytls_tlsctx_set_certificate_callback;

			// Token: 0x04000090 RID: 144
			public UnityTls.unitytls_interface_struct.unitytls_tlsctx_set_trace_callback_t unitytls_tlsctx_set_trace_callback;

			// Token: 0x04000091 RID: 145
			public UnityTls.unitytls_interface_struct.unitytls_tlsctx_set_x509verify_callback_t unitytls_tlsctx_set_x509verify_callback;

			// Token: 0x04000092 RID: 146
			public UnityTls.unitytls_interface_struct.unitytls_tlsctx_set_supported_ciphersuites_t unitytls_tlsctx_set_supported_ciphersuites;

			// Token: 0x04000093 RID: 147
			public UnityTls.unitytls_interface_struct.unitytls_tlsctx_get_ciphersuite_t unitytls_tlsctx_get_ciphersuite;

			// Token: 0x04000094 RID: 148
			public UnityTls.unitytls_interface_struct.unitytls_tlsctx_get_protocol_t unitytls_tlsctx_get_protocol;

			// Token: 0x04000095 RID: 149
			public UnityTls.unitytls_interface_struct.unitytls_tlsctx_process_handshake_t unitytls_tlsctx_process_handshake;

			// Token: 0x04000096 RID: 150
			public UnityTls.unitytls_interface_struct.unitytls_tlsctx_read_t unitytls_tlsctx_read;

			// Token: 0x04000097 RID: 151
			public UnityTls.unitytls_interface_struct.unitytls_tlsctx_write_t unitytls_tlsctx_write;

			// Token: 0x04000098 RID: 152
			public UnityTls.unitytls_interface_struct.unitytls_tlsctx_notify_close_t unitytls_tlsctx_notify_close;

			// Token: 0x04000099 RID: 153
			public UnityTls.unitytls_interface_struct.unitytls_tlsctx_free_t unitytls_tlsctx_free;

			// Token: 0x0400009A RID: 154
			public UnityTls.unitytls_interface_struct.unitytls_random_generate_bytes_t unitytls_random_generate_bytes;

			// Token: 0x0400009B RID: 155
			public UnityTls.unitytls_interface_struct.unitytls_x509verify_result_to_string_t unitytls_x509verify_result_to_string;

			// Token: 0x0400009C RID: 156
			public UnityTls.unitytls_interface_struct.unitytls_tlsctx_set_trace_level_t unitytls_tlsctx_set_trace_level;

			// Token: 0x0200002E RID: 46
			// (Invoke) Token: 0x06000069 RID: 105
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public delegate UnityTls.unitytls_errorstate unitytls_errorstate_create_t();

			// Token: 0x0200002F RID: 47
			// (Invoke) Token: 0x0600006B RID: 107
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public unsafe delegate void unitytls_errorstate_raise_error_t(UnityTls.unitytls_errorstate* errorState, UnityTls.unitytls_error_code errorCode);

			// Token: 0x02000030 RID: 48
			// (Invoke) Token: 0x0600006D RID: 109
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public unsafe delegate UnityTls.unitytls_key_ref unitytls_key_get_ref_t(UnityTls.unitytls_key* key, UnityTls.unitytls_errorstate* errorState);

			// Token: 0x02000031 RID: 49
			// (Invoke) Token: 0x0600006F RID: 111
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public unsafe delegate UnityTls.unitytls_key* unitytls_key_parse_der_t(byte* buffer, IntPtr bufferLen, byte* password, IntPtr passwordLen, UnityTls.unitytls_errorstate* errorState);

			// Token: 0x02000032 RID: 50
			// (Invoke) Token: 0x06000071 RID: 113
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public unsafe delegate UnityTls.unitytls_key* unitytls_key_parse_pem_t(byte* buffer, IntPtr bufferLen, byte* password, IntPtr passwordLen, UnityTls.unitytls_errorstate* errorState);

			// Token: 0x02000033 RID: 51
			// (Invoke) Token: 0x06000073 RID: 115
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public unsafe delegate void unitytls_key_free_t(UnityTls.unitytls_key* key);

			// Token: 0x02000034 RID: 52
			// (Invoke) Token: 0x06000075 RID: 117
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public unsafe delegate IntPtr unitytls_x509_export_der_t(UnityTls.unitytls_x509_ref cert, byte* buffer, IntPtr bufferLen, UnityTls.unitytls_errorstate* errorState);

			// Token: 0x02000035 RID: 53
			// (Invoke) Token: 0x06000077 RID: 119
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public unsafe delegate UnityTls.unitytls_x509list_ref unitytls_x509list_get_ref_t(UnityTls.unitytls_x509list* list, UnityTls.unitytls_errorstate* errorState);

			// Token: 0x02000036 RID: 54
			// (Invoke) Token: 0x06000079 RID: 121
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public unsafe delegate UnityTls.unitytls_x509_ref unitytls_x509list_get_x509_t(UnityTls.unitytls_x509list_ref list, IntPtr index, UnityTls.unitytls_errorstate* errorState);

			// Token: 0x02000037 RID: 55
			// (Invoke) Token: 0x0600007B RID: 123
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public unsafe delegate UnityTls.unitytls_x509list* unitytls_x509list_create_t(UnityTls.unitytls_errorstate* errorState);

			// Token: 0x02000038 RID: 56
			// (Invoke) Token: 0x0600007D RID: 125
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public unsafe delegate void unitytls_x509list_append_t(UnityTls.unitytls_x509list* list, UnityTls.unitytls_x509_ref cert, UnityTls.unitytls_errorstate* errorState);

			// Token: 0x02000039 RID: 57
			// (Invoke) Token: 0x0600007F RID: 127
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public unsafe delegate void unitytls_x509list_append_der_t(UnityTls.unitytls_x509list* list, byte* buffer, IntPtr bufferLen, UnityTls.unitytls_errorstate* errorState);

			// Token: 0x0200003A RID: 58
			// (Invoke) Token: 0x06000081 RID: 129
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public unsafe delegate void unitytls_x509list_free_t(UnityTls.unitytls_x509list* list);

			// Token: 0x0200003B RID: 59
			// (Invoke) Token: 0x06000083 RID: 131
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public unsafe delegate UnityTls.unitytls_x509verify_result unitytls_x509verify_default_ca_t(UnityTls.unitytls_x509list_ref chain, byte* cn, IntPtr cnLen, UnityTls.unitytls_x509verify_callback cb, void* userData, UnityTls.unitytls_errorstate* errorState);

			// Token: 0x0200003C RID: 60
			// (Invoke) Token: 0x06000085 RID: 133
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public unsafe delegate UnityTls.unitytls_x509verify_result unitytls_x509verify_explicit_ca_t(UnityTls.unitytls_x509list_ref chain, UnityTls.unitytls_x509list_ref trustCA, byte* cn, IntPtr cnLen, UnityTls.unitytls_x509verify_callback cb, void* userData, UnityTls.unitytls_errorstate* errorState);

			// Token: 0x0200003D RID: 61
			// (Invoke) Token: 0x06000087 RID: 135
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public unsafe delegate UnityTls.unitytls_tlsctx* unitytls_tlsctx_create_server_t(UnityTls.unitytls_tlsctx_protocolrange supportedProtocols, UnityTls.unitytls_tlsctx_callbacks callbacks, ulong certChain, ulong leafCertificateKey, UnityTls.unitytls_errorstate* errorState);

			// Token: 0x0200003E RID: 62
			// (Invoke) Token: 0x06000089 RID: 137
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public unsafe delegate UnityTls.unitytls_tlsctx* unitytls_tlsctx_create_client_t(UnityTls.unitytls_tlsctx_protocolrange supportedProtocols, UnityTls.unitytls_tlsctx_callbacks callbacks, byte* cn, IntPtr cnLen, UnityTls.unitytls_errorstate* errorState);

			// Token: 0x0200003F RID: 63
			// (Invoke) Token: 0x0600008B RID: 139
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public unsafe delegate void unitytls_tlsctx_server_require_client_authentication_t(UnityTls.unitytls_tlsctx* ctx, UnityTls.unitytls_x509list_ref clientAuthCAList, UnityTls.unitytls_errorstate* errorState);

			// Token: 0x02000040 RID: 64
			// (Invoke) Token: 0x0600008D RID: 141
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public unsafe delegate void unitytls_tlsctx_set_certificate_callback_t(UnityTls.unitytls_tlsctx* ctx, UnityTls.unitytls_tlsctx_certificate_callback cb, void* userData, UnityTls.unitytls_errorstate* errorState);

			// Token: 0x02000041 RID: 65
			// (Invoke) Token: 0x0600008F RID: 143
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public unsafe delegate void unitytls_tlsctx_set_trace_callback_t(UnityTls.unitytls_tlsctx* ctx, UnityTls.unitytls_tlsctx_trace_callback cb, void* userData, UnityTls.unitytls_errorstate* errorState);

			// Token: 0x02000042 RID: 66
			// (Invoke) Token: 0x06000091 RID: 145
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public unsafe delegate void unitytls_tlsctx_set_x509verify_callback_t(UnityTls.unitytls_tlsctx* ctx, UnityTls.unitytls_tlsctx_x509verify_callback cb, void* userData, UnityTls.unitytls_errorstate* errorState);

			// Token: 0x02000043 RID: 67
			// (Invoke) Token: 0x06000093 RID: 147
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public unsafe delegate void unitytls_tlsctx_set_supported_ciphersuites_t(UnityTls.unitytls_tlsctx* ctx, UnityTls.unitytls_ciphersuite* supportedCiphersuites, IntPtr supportedCiphersuitesLen, UnityTls.unitytls_errorstate* errorState);

			// Token: 0x02000044 RID: 68
			// (Invoke) Token: 0x06000095 RID: 149
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public unsafe delegate UnityTls.unitytls_ciphersuite unitytls_tlsctx_get_ciphersuite_t(UnityTls.unitytls_tlsctx* ctx, UnityTls.unitytls_errorstate* errorState);

			// Token: 0x02000045 RID: 69
			// (Invoke) Token: 0x06000097 RID: 151
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public unsafe delegate UnityTls.unitytls_protocol unitytls_tlsctx_get_protocol_t(UnityTls.unitytls_tlsctx* ctx, UnityTls.unitytls_errorstate* errorState);

			// Token: 0x02000046 RID: 70
			// (Invoke) Token: 0x06000099 RID: 153
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public unsafe delegate UnityTls.unitytls_x509verify_result unitytls_tlsctx_process_handshake_t(UnityTls.unitytls_tlsctx* ctx, UnityTls.unitytls_errorstate* errorState);

			// Token: 0x02000047 RID: 71
			// (Invoke) Token: 0x0600009B RID: 155
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public unsafe delegate IntPtr unitytls_tlsctx_read_t(UnityTls.unitytls_tlsctx* ctx, byte* buffer, IntPtr bufferLen, UnityTls.unitytls_errorstate* errorState);

			// Token: 0x02000048 RID: 72
			// (Invoke) Token: 0x0600009D RID: 157
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public unsafe delegate IntPtr unitytls_tlsctx_write_t(UnityTls.unitytls_tlsctx* ctx, byte* data, IntPtr bufferLen, UnityTls.unitytls_errorstate* errorState);

			// Token: 0x02000049 RID: 73
			// (Invoke) Token: 0x0600009F RID: 159
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public unsafe delegate void unitytls_tlsctx_notify_close_t(UnityTls.unitytls_tlsctx* ctx, UnityTls.unitytls_errorstate* errorState);

			// Token: 0x0200004A RID: 74
			// (Invoke) Token: 0x060000A1 RID: 161
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public unsafe delegate void unitytls_tlsctx_free_t(UnityTls.unitytls_tlsctx* ctx);

			// Token: 0x0200004B RID: 75
			// (Invoke) Token: 0x060000A3 RID: 163
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public unsafe delegate void unitytls_random_generate_bytes_t(byte* buffer, IntPtr bufferLen, UnityTls.unitytls_errorstate* errorState);

			// Token: 0x0200004C RID: 76
			// (Invoke) Token: 0x060000A5 RID: 165
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public unsafe delegate char* unitytls_x509verify_result_to_string_t(UnityTls.unitytls_x509verify_result v);

			// Token: 0x0200004D RID: 77
			// (Invoke) Token: 0x060000A7 RID: 167
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public unsafe delegate void unitytls_tlsctx_set_trace_level_t(UnityTls.unitytls_tlsctx* ctx, UnityTls.unitytls_log_level level);
		}
	}
}
