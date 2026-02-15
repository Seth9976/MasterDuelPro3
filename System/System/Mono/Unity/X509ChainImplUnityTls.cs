using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace Mono.Unity
{
	// Token: 0x02000052 RID: 82
	internal class X509ChainImplUnityTls : X509ChainImpl
	{
		// Token: 0x060000D2 RID: 210 RVA: 0x00003F30 File Offset: 0x00002130
		internal X509ChainImplUnityTls(UnityTls.unitytls_x509list_ref nativeCertificateChain, bool reverseOrder = false)
		{
			this.elements = null;
			this.ownedList = null;
			this.nativeCertificateChain = nativeCertificateChain;
			this.reverseOrder = reverseOrder;
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00003F60 File Offset: 0x00002160
		internal unsafe X509ChainImplUnityTls(UnityTls.unitytls_x509list* ownedList, UnityTls.unitytls_errorstate* errorState, bool reverseOrder = false)
		{
			this.elements = null;
			this.ownedList = ownedList;
			this.nativeCertificateChain = UnityTls.NativeInterface.unitytls_x509list_get_ref(ownedList, errorState);
			this.reverseOrder = reverseOrder;
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x060000D4 RID: 212 RVA: 0x00003F9F File Offset: 0x0000219F
		public override bool IsValid
		{
			get
			{
				return this.nativeCertificateChain.handle != UnityTls.NativeInterface.UNITYTLS_INVALID_HANDLE;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060000D5 RID: 213 RVA: 0x00003FBB File Offset: 0x000021BB
		internal UnityTls.unitytls_x509list_ref NativeCertificateChain
		{
			get
			{
				return this.nativeCertificateChain;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060000D6 RID: 214 RVA: 0x00003FC4 File Offset: 0x000021C4
		public unsafe override X509ChainElementCollection ChainElements
		{
			get
			{
				base.ThrowIfContextInvalid();
				if (this.elements != null)
				{
					return this.elements;
				}
				this.elements = new X509ChainElementCollection();
				UnityTls.unitytls_errorstate unitytls_errorstate = UnityTls.NativeInterface.unitytls_errorstate_create();
				UnityTls.unitytls_x509_ref unitytls_x509_ref = UnityTls.NativeInterface.unitytls_x509list_get_x509(this.nativeCertificateChain, (IntPtr)0, &unitytls_errorstate);
				int num = 1;
				while (unitytls_x509_ref.handle != UnityTls.NativeInterface.UNITYTLS_INVALID_HANDLE)
				{
					IntPtr intPtr = UnityTls.NativeInterface.unitytls_x509_export_der(unitytls_x509_ref, null, (IntPtr)0, &unitytls_errorstate);
					byte[] array = new byte[(int)intPtr];
					byte[] array2;
					byte* ptr;
					if ((array2 = array) == null || array2.Length == 0)
					{
						ptr = null;
					}
					else
					{
						ptr = &array2[0];
					}
					UnityTls.NativeInterface.unitytls_x509_export_der(unitytls_x509_ref, ptr, intPtr, &unitytls_errorstate);
					array2 = null;
					this.elements.Add(new X509Certificate2(array));
					unitytls_x509_ref = UnityTls.NativeInterface.unitytls_x509list_get_x509(this.nativeCertificateChain, (IntPtr)num, &unitytls_errorstate);
					num++;
				}
				if (this.reverseOrder)
				{
					X509ChainElementCollection x509ChainElementCollection = new X509ChainElementCollection();
					for (int i = this.elements.Count - 1; i >= 0; i--)
					{
						x509ChainElementCollection.Add(this.elements[i].Certificate);
					}
					this.elements = x509ChainElementCollection;
				}
				return this.elements;
			}
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00004124 File Offset: 0x00002324
		public override void AddStatus(X509ChainStatusFlags error)
		{
			if (this.chainStatusList == null)
			{
				this.chainStatusList = new List<X509ChainStatus>();
			}
			this.chainStatusList.Add(new X509ChainStatus(error));
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060000D8 RID: 216 RVA: 0x0000414A File Offset: 0x0000234A
		public override X509ChainPolicy ChainPolicy
		{
			get
			{
				return this.policy;
			}
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x000028AE File Offset: 0x00000AAE
		public override bool Build(X509Certificate2 certificate)
		{
			return false;
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00004154 File Offset: 0x00002354
		public override void Reset()
		{
			if (this.elements != null)
			{
				this.nativeCertificateChain.handle = UnityTls.NativeInterface.UNITYTLS_INVALID_HANDLE;
				this.elements.Clear();
				this.elements = null;
			}
			if (this.ownedList != null)
			{
				UnityTls.NativeInterface.unitytls_x509list_free(this.ownedList);
				this.ownedList = null;
			}
		}

		// Token: 0x060000DB RID: 219 RVA: 0x000041B7 File Offset: 0x000023B7
		protected override void Dispose(bool disposing)
		{
			this.Reset();
			base.Dispose(disposing);
		}

		// Token: 0x040000AE RID: 174
		private X509ChainElementCollection elements;

		// Token: 0x040000AF RID: 175
		private unsafe UnityTls.unitytls_x509list* ownedList;

		// Token: 0x040000B0 RID: 176
		private UnityTls.unitytls_x509list_ref nativeCertificateChain;

		// Token: 0x040000B1 RID: 177
		private X509ChainPolicy policy = new X509ChainPolicy();

		// Token: 0x040000B2 RID: 178
		private List<X509ChainStatus> chainStatusList;

		// Token: 0x040000B3 RID: 179
		private bool reverseOrder;
	}
}
