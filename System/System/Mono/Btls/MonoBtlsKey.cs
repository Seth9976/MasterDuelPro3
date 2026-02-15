using System;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using Mono.Security.Cryptography;

namespace Mono.Btls
{
	// Token: 0x020000A3 RID: 163
	internal class MonoBtlsKey : MonoBtlsObject
	{
		// Token: 0x060002A6 RID: 678
		[DllImport("libmono-btls-shared")]
		private static extern IntPtr mono_btls_key_new();

		// Token: 0x060002A7 RID: 679
		[DllImport("libmono-btls-shared")]
		private static extern void mono_btls_key_free(IntPtr handle);

		// Token: 0x060002A8 RID: 680
		[DllImport("libmono-btls-shared")]
		private static extern IntPtr mono_btls_key_up_ref(IntPtr handle);

		// Token: 0x060002A9 RID: 681
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_key_get_bytes(IntPtr handle, out IntPtr data, out int size, int include_private_bits);

		// Token: 0x060002AA RID: 682
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_key_assign_rsa_private_key(IntPtr handle, byte[] der, int der_length);

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060002AB RID: 683 RVA: 0x0000A7E7 File Offset: 0x000089E7
		internal new MonoBtlsKey.BoringKeyHandle Handle
		{
			get
			{
				return (MonoBtlsKey.BoringKeyHandle)base.Handle;
			}
		}

		// Token: 0x060002AC RID: 684 RVA: 0x00009A2B File Offset: 0x00007C2B
		internal MonoBtlsKey(MonoBtlsKey.BoringKeyHandle handle)
			: base(handle)
		{
		}

		// Token: 0x060002AD RID: 685 RVA: 0x0000A7F4 File Offset: 0x000089F4
		public byte[] GetBytes(bool include_private_bits)
		{
			IntPtr intPtr;
			int num2;
			int num = MonoBtlsKey.mono_btls_key_get_bytes(this.Handle.DangerousGetHandle(), out intPtr, out num2, include_private_bits ? 1 : 0);
			base.CheckError(num, "GetBytes");
			byte[] array = new byte[num2];
			Marshal.Copy(intPtr, array, 0, num2);
			base.FreeDataPtr(intPtr);
			return array;
		}

		// Token: 0x060002AE RID: 686 RVA: 0x0000A844 File Offset: 0x00008A44
		public MonoBtlsKey Copy()
		{
			base.CheckThrow();
			IntPtr intPtr = MonoBtlsKey.mono_btls_key_up_ref(this.Handle.DangerousGetHandle());
			base.CheckError(intPtr != IntPtr.Zero, "Copy");
			return new MonoBtlsKey(new MonoBtlsKey.BoringKeyHandle(intPtr));
		}

		// Token: 0x060002AF RID: 687 RVA: 0x0000A88C File Offset: 0x00008A8C
		public static MonoBtlsKey CreateFromRSAPrivateKey(RSA privateKey)
		{
			byte[] array = PKCS8.PrivateKeyInfo.Encode(privateKey);
			MonoBtlsKey monoBtlsKey = new MonoBtlsKey(new MonoBtlsKey.BoringKeyHandle(MonoBtlsKey.mono_btls_key_new()));
			if (MonoBtlsKey.mono_btls_key_assign_rsa_private_key(monoBtlsKey.Handle.DangerousGetHandle(), array, array.Length) == 0)
			{
				throw new MonoBtlsException("Assigning private key failed.");
			}
			return monoBtlsKey;
		}

		// Token: 0x020000A4 RID: 164
		internal class BoringKeyHandle : MonoBtlsObject.MonoBtlsHandle
		{
			// Token: 0x060002B0 RID: 688 RVA: 0x00009A41 File Offset: 0x00007C41
			internal BoringKeyHandle(IntPtr handle)
				: base(handle, true)
			{
			}

			// Token: 0x060002B1 RID: 689 RVA: 0x0000A8D0 File Offset: 0x00008AD0
			protected override bool ReleaseHandle()
			{
				MonoBtlsKey.mono_btls_key_free(this.handle);
				return true;
			}
		}
	}
}
