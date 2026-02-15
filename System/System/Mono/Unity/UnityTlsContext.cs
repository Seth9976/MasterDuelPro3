using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Mono.Net.Security;
using Mono.Security.Cryptography;
using Mono.Security.Interface;
using Mono.Util;

namespace Mono.Unity
{
	// Token: 0x0200004E RID: 78
	internal class UnityTlsContext : MobileTlsContext
	{
		// Token: 0x060000A8 RID: 168 RVA: 0x00002BAC File Offset: 0x00000DAC
		public unsafe UnityTlsContext(MobileAuthenticatedStream parent, MonoSslAuthenticationOptions options)
			: base(parent, options)
		{
			this.handle = GCHandle.Alloc(this);
			UnityTls.unitytls_errorstate unitytls_errorstate = UnityTls.NativeInterface.unitytls_errorstate_create();
			UnityTls.unitytls_tlsctx_protocolrange unitytls_tlsctx_protocolrange = new UnityTls.unitytls_tlsctx_protocolrange
			{
				min = UnityTlsConversions.GetMinProtocol(options.EnabledSslProtocols),
				max = UnityTlsConversions.GetMaxProtocol(options.EnabledSslProtocols)
			};
			this.readCallback = new UnityTls.unitytls_tlsctx_read_callback(UnityTlsContext.ReadCallback);
			this.writeCallback = new UnityTls.unitytls_tlsctx_write_callback(UnityTlsContext.WriteCallback);
			UnityTls.unitytls_tlsctx_callbacks unitytls_tlsctx_callbacks = new UnityTls.unitytls_tlsctx_callbacks
			{
				write = this.writeCallback,
				read = this.readCallback,
				data = (void*)((IntPtr)this.handle)
			};
			if (options.ServerMode)
			{
				UnityTls.unitytls_x509list* ptr;
				UnityTls.unitytls_key* ptr2;
				UnityTlsContext.ExtractNativeKeyAndChainFromManagedCertificate(options.ServerCertificate, &unitytls_errorstate, out ptr, out ptr2);
				try
				{
					UnityTls.unitytls_x509list_ref unitytls_x509list_ref = UnityTls.NativeInterface.unitytls_x509list_get_ref(ptr, &unitytls_errorstate);
					UnityTls.unitytls_key_ref unitytls_key_ref = UnityTls.NativeInterface.unitytls_key_get_ref(ptr2, &unitytls_errorstate);
					Debug.CheckAndThrow(unitytls_errorstate, "Failed to parse server key/certificate", AlertDescription.InternalError);
					this.tlsContext = UnityTls.NativeInterface.unitytls_tlsctx_create_server(unitytls_tlsctx_protocolrange, unitytls_tlsctx_callbacks, unitytls_x509list_ref.handle, unitytls_key_ref.handle, &unitytls_errorstate);
					if (base.AskForClientCertificate)
					{
						UnityTls.unitytls_x509list* ptr3 = null;
						try
						{
							ptr3 = UnityTls.NativeInterface.unitytls_x509list_create(&unitytls_errorstate);
							UnityTls.unitytls_x509list_ref unitytls_x509list_ref2 = UnityTls.NativeInterface.unitytls_x509list_get_ref(ptr3, &unitytls_errorstate);
							UnityTls.NativeInterface.unitytls_tlsctx_server_require_client_authentication(this.tlsContext, unitytls_x509list_ref2, &unitytls_errorstate);
						}
						finally
						{
							UnityTls.NativeInterface.unitytls_x509list_free(ptr3);
						}
					}
					goto IL_026F;
				}
				finally
				{
					UnityTls.NativeInterface.unitytls_x509list_free(ptr);
					UnityTls.NativeInterface.unitytls_key_free(ptr2);
				}
			}
			byte[] bytes = Encoding.UTF8.GetBytes(options.TargetHost);
			byte[] array;
			byte* ptr4;
			if ((array = bytes) == null || array.Length == 0)
			{
				ptr4 = null;
			}
			else
			{
				ptr4 = &array[0];
			}
			this.tlsContext = UnityTls.NativeInterface.unitytls_tlsctx_create_client(unitytls_tlsctx_protocolrange, unitytls_tlsctx_callbacks, ptr4, (IntPtr)bytes.Length, &unitytls_errorstate);
			array = null;
			this.certificateCallback = new UnityTls.unitytls_tlsctx_certificate_callback(UnityTlsContext.CertificateCallback);
			UnityTls.NativeInterface.unitytls_tlsctx_set_certificate_callback(this.tlsContext, this.certificateCallback, (void*)((IntPtr)this.handle), &unitytls_errorstate);
			IL_026F:
			this.verifyCallback = new UnityTls.unitytls_tlsctx_x509verify_callback(UnityTlsContext.VerifyCallback);
			UnityTls.NativeInterface.unitytls_tlsctx_set_x509verify_callback(this.tlsContext, this.verifyCallback, (void*)((IntPtr)this.handle), &unitytls_errorstate);
			Debug.CheckAndThrow(unitytls_errorstate, "Failed to create UnityTls context", AlertDescription.InternalError);
			this.hasContext = true;
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00002E98 File Offset: 0x00001098
		private unsafe static void ExtractNativeKeyAndChainFromManagedCertificate(X509Certificate cert, UnityTls.unitytls_errorstate* errorState, out UnityTls.unitytls_x509list* nativeCertChain, out UnityTls.unitytls_key* nativeKey)
		{
			if (cert == null)
			{
				throw new ArgumentNullException("cert");
			}
			X509Certificate2 x509Certificate = cert as X509Certificate2;
			if (x509Certificate == null || x509Certificate.PrivateKey == null)
			{
				throw new ArgumentException("Certificate does not have a private key", "cert");
			}
			nativeCertChain = (IntPtr)((UIntPtr)0);
			nativeKey = (IntPtr)((UIntPtr)0);
			try
			{
				nativeCertChain = UnityTls.NativeInterface.unitytls_x509list_create(errorState);
				CertHelper.AddCertificateToNativeChain(nativeCertChain, cert, errorState);
				byte[] array = PKCS8.PrivateKeyInfo.Encode(x509Certificate.PrivateKey);
				try
				{
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
					nativeKey = UnityTls.NativeInterface.unitytls_key_parse_der(ptr, (IntPtr)array.Length, null, (IntPtr)0, errorState);
				}
				finally
				{
					byte[] array2 = null;
				}
			}
			catch
			{
				UnityTls.NativeInterface.unitytls_x509list_free(nativeCertChain);
				UnityTls.NativeInterface.unitytls_key_free(nativeKey);
				throw;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x060000AA RID: 170 RVA: 0x00002F88 File Offset: 0x00001188
		public override bool IsAuthenticated
		{
			get
			{
				return this.isAuthenticated;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x060000AB RID: 171 RVA: 0x00002F90 File Offset: 0x00001190
		internal override X509Certificate LocalClientCertificate
		{
			get
			{
				return this.localClientCertificate;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x060000AC RID: 172 RVA: 0x00002F98 File Offset: 0x00001198
		public override X509Certificate2 RemoteCertificate
		{
			get
			{
				return this.remoteCertificate;
			}
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00002FA0 File Offset: 0x000011A0
		public override void Flush()
		{
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00002FA4 File Offset: 0x000011A4
		[return: TupleElementNames(new string[] { "ret", "wantMore" })]
		public unsafe override ValueTuple<int, bool> Read(byte[] buffer, int offset, int count)
		{
			this.lastException = null;
			UnityTls.unitytls_errorstate unitytls_errorstate = UnityTls.NativeInterface.unitytls_errorstate_create();
			int num;
			fixed (byte[] array = buffer)
			{
				byte* ptr;
				if (buffer == null || array.Length == 0)
				{
					ptr = null;
				}
				else
				{
					ptr = &array[0];
				}
				num = (int)UnityTls.NativeInterface.unitytls_tlsctx_read(this.tlsContext, ptr + offset, (IntPtr)count, &unitytls_errorstate);
			}
			if (this.lastException != null)
			{
				throw this.lastException;
			}
			UnityTls.unitytls_error_code code = unitytls_errorstate.code;
			if (code == UnityTls.unitytls_error_code.UNITYTLS_SUCCESS)
			{
				return new ValueTuple<int, bool>(num, num < count);
			}
			if (code == UnityTls.unitytls_error_code.UNITYTLS_STREAM_CLOSED)
			{
				return new ValueTuple<int, bool>(0, false);
			}
			if (code != UnityTls.unitytls_error_code.UNITYTLS_USER_WOULD_BLOCK)
			{
				if (!this.closedGraceful)
				{
					Debug.CheckAndThrow(unitytls_errorstate, "Failed to read data to TLS context", AlertDescription.InternalError);
				}
				return new ValueTuple<int, bool>(0, false);
			}
			return new ValueTuple<int, bool>(num, true);
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00003070 File Offset: 0x00001270
		[return: TupleElementNames(new string[] { "ret", "wantMore" })]
		public unsafe override ValueTuple<int, bool> Write(byte[] buffer, int offset, int count)
		{
			this.lastException = null;
			UnityTls.unitytls_errorstate unitytls_errorstate = UnityTls.NativeInterface.unitytls_errorstate_create();
			int num;
			fixed (byte[] array = buffer)
			{
				byte* ptr;
				if (buffer == null || array.Length == 0)
				{
					ptr = null;
				}
				else
				{
					ptr = &array[0];
				}
				num = (int)UnityTls.NativeInterface.unitytls_tlsctx_write(this.tlsContext, ptr + offset, (IntPtr)count, &unitytls_errorstate);
			}
			if (this.lastException != null)
			{
				throw this.lastException;
			}
			UnityTls.unitytls_error_code code = unitytls_errorstate.code;
			if (code == UnityTls.unitytls_error_code.UNITYTLS_SUCCESS)
			{
				return new ValueTuple<int, bool>(num, num < count);
			}
			if (code == UnityTls.unitytls_error_code.UNITYTLS_STREAM_CLOSED)
			{
				return new ValueTuple<int, bool>(0, false);
			}
			if (code != UnityTls.unitytls_error_code.UNITYTLS_USER_WOULD_BLOCK)
			{
				Debug.CheckAndThrow(unitytls_errorstate, "Failed to write data to TLS context", AlertDescription.InternalError);
				return new ValueTuple<int, bool>(0, false);
			}
			return new ValueTuple<int, bool>(num, true);
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00003132 File Offset: 0x00001332
		public override void Renegotiate()
		{
			throw new NotSupportedException();
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x000028AE File Offset: 0x00000AAE
		public override bool PendingRenegotiation()
		{
			return false;
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x0000313C File Offset: 0x0000133C
		public unsafe override void Shutdown()
		{
			if (base.Settings != null && base.Settings.SendCloseNotify)
			{
				UnityTls.unitytls_errorstate unitytls_errorstate = UnityTls.NativeInterface.unitytls_errorstate_create();
				UnityTls.NativeInterface.unitytls_tlsctx_notify_close(this.tlsContext, &unitytls_errorstate);
			}
			UnityTls.NativeInterface.unitytls_x509list_free(this.requestedClientCertChain);
			UnityTls.NativeInterface.unitytls_key_free(this.requestedClientKey);
			UnityTls.NativeInterface.unitytls_tlsctx_free(this.tlsContext);
			this.tlsContext = null;
			this.hasContext = false;
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x000031D4 File Offset: 0x000013D4
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing)
				{
					this.Shutdown();
					this.localClientCertificate = null;
					this.remoteCertificate = null;
					if (this.localClientCertificate != null)
					{
						this.localClientCertificate.Dispose();
						this.localClientCertificate = null;
					}
					if (this.remoteCertificate != null)
					{
						this.remoteCertificate.Dispose();
						this.remoteCertificate = null;
					}
					this.connectioninfo = null;
					this.isAuthenticated = false;
					this.hasContext = false;
				}
				this.handle.Free();
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00003268 File Offset: 0x00001468
		public unsafe override void StartHandshake()
		{
			if (base.Settings != null && base.Settings.EnabledCiphers != null)
			{
				UnityTls.unitytls_ciphersuite[] array = new UnityTls.unitytls_ciphersuite[base.Settings.EnabledCiphers.Length];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = (UnityTls.unitytls_ciphersuite)base.Settings.EnabledCiphers[i];
				}
				UnityTls.unitytls_errorstate unitytls_errorstate = UnityTls.NativeInterface.unitytls_errorstate_create();
				UnityTls.unitytls_ciphersuite[] array2;
				UnityTls.unitytls_ciphersuite* ptr;
				if ((array2 = array) == null || array2.Length == 0)
				{
					ptr = null;
				}
				else
				{
					ptr = &array2[0];
				}
				UnityTls.NativeInterface.unitytls_tlsctx_set_supported_ciphersuites(this.tlsContext, ptr, (IntPtr)array.Length, &unitytls_errorstate);
				array2 = null;
				Debug.CheckAndThrow(unitytls_errorstate, "Failed to set list of supported ciphers", AlertDescription.HandshakeFailure);
			}
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00003320 File Offset: 0x00001520
		public unsafe override bool ProcessHandshake()
		{
			this.lastException = null;
			UnityTls.unitytls_errorstate unitytls_errorstate = UnityTls.NativeInterface.unitytls_errorstate_create();
			UnityTls.unitytls_x509verify_result unitytls_x509verify_result = UnityTls.NativeInterface.unitytls_tlsctx_process_handshake(this.tlsContext, &unitytls_errorstate);
			if (unitytls_errorstate.code == UnityTls.unitytls_error_code.UNITYTLS_USER_WOULD_BLOCK)
			{
				return false;
			}
			if (this.lastException != null)
			{
				throw this.lastException;
			}
			if (base.IsServer && unitytls_x509verify_result == (UnityTls.unitytls_x509verify_result)2147483648U)
			{
				Debug.CheckAndThrow(unitytls_errorstate, "Handshake failed", AlertDescription.HandshakeFailure);
				if (!base.ValidateCertificate(null, null))
				{
					throw new TlsException(AlertDescription.HandshakeFailure, "Verification failure during handshake");
				}
			}
			else
			{
				Debug.CheckAndThrow(unitytls_errorstate, unitytls_x509verify_result, "Handshake failed", AlertDescription.HandshakeFailure);
			}
			return true;
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x000033C0 File Offset: 0x000015C0
		public unsafe override void FinishHandshake()
		{
			UnityTls.unitytls_errorstate unitytls_errorstate = UnityTls.NativeInterface.unitytls_errorstate_create();
			UnityTls.unitytls_ciphersuite unitytls_ciphersuite = UnityTls.NativeInterface.unitytls_tlsctx_get_ciphersuite(this.tlsContext, &unitytls_errorstate);
			UnityTls.unitytls_protocol unitytls_protocol = UnityTls.NativeInterface.unitytls_tlsctx_get_protocol(this.tlsContext, &unitytls_errorstate);
			this.connectioninfo = new MonoTlsConnectionInfo
			{
				CipherSuiteCode = (CipherSuiteCode)unitytls_ciphersuite,
				ProtocolVersion = UnityTlsConversions.ConvertProtocolVersion(unitytls_protocol),
				PeerDomainName = base.ServerName
			};
			this.isAuthenticated = true;
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00003444 File Offset: 0x00001644
		[MonoPInvokeCallback(typeof(UnityTls.unitytls_tlsctx_write_callback))]
		private unsafe static IntPtr WriteCallback(void* userData, byte* data, IntPtr bufferLen, UnityTls.unitytls_errorstate* errorState)
		{
			return ((UnityTlsContext)((GCHandle)((IntPtr)userData)).Target).WriteCallback(data, bufferLen, errorState);
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00003474 File Offset: 0x00001674
		private unsafe IntPtr WriteCallback(byte* data, IntPtr bufferLen, UnityTls.unitytls_errorstate* errorState)
		{
			IntPtr intPtr;
			try
			{
				if (this.writeBuffer == null || this.writeBuffer.Length < (int)bufferLen)
				{
					this.writeBuffer = new byte[(int)bufferLen];
				}
				Marshal.Copy((IntPtr)((void*)data), this.writeBuffer, 0, (int)bufferLen);
				if (!base.Parent.InternalWrite(this.writeBuffer, 0, (int)bufferLen))
				{
					UnityTls.NativeInterface.unitytls_errorstate_raise_error(errorState, UnityTls.unitytls_error_code.UNITYTLS_USER_WRITE_FAILED);
					intPtr = (IntPtr)0;
				}
				else
				{
					intPtr = bufferLen;
				}
			}
			catch (Exception ex)
			{
				UnityTls.NativeInterface.unitytls_errorstate_raise_error(errorState, UnityTls.unitytls_error_code.UNITYTLS_USER_UNKNOWN_ERROR);
				if (this.lastException == null)
				{
					this.lastException = ex;
				}
				intPtr = (IntPtr)0;
			}
			return intPtr;
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00003540 File Offset: 0x00001740
		[MonoPInvokeCallback(typeof(UnityTls.unitytls_tlsctx_read_callback))]
		private unsafe static IntPtr ReadCallback(void* userData, byte* buffer, IntPtr bufferLen, UnityTls.unitytls_errorstate* errorState)
		{
			return ((UnityTlsContext)((GCHandle)((IntPtr)userData)).Target).ReadCallback(buffer, bufferLen, errorState);
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00003570 File Offset: 0x00001770
		private unsafe IntPtr ReadCallback(byte* buffer, IntPtr bufferLen, UnityTls.unitytls_errorstate* errorState)
		{
			IntPtr intPtr;
			try
			{
				if (this.readBuffer == null || this.readBuffer.Length < (int)bufferLen)
				{
					this.readBuffer = new byte[(int)bufferLen];
				}
				bool flag;
				int num = base.Parent.InternalRead(this.readBuffer, 0, (int)bufferLen, out flag);
				if (num < 0)
				{
					UnityTls.NativeInterface.unitytls_errorstate_raise_error(errorState, UnityTls.unitytls_error_code.UNITYTLS_USER_READ_FAILED);
				}
				else if (num > 0)
				{
					Marshal.Copy(this.readBuffer, 0, (IntPtr)((void*)buffer), (int)bufferLen);
				}
				else if (flag)
				{
					UnityTls.NativeInterface.unitytls_errorstate_raise_error(errorState, UnityTls.unitytls_error_code.UNITYTLS_USER_WOULD_BLOCK);
				}
				else
				{
					this.closedGraceful = true;
					UnityTls.NativeInterface.unitytls_errorstate_raise_error(errorState, UnityTls.unitytls_error_code.UNITYTLS_USER_READ_FAILED);
				}
				intPtr = (IntPtr)num;
			}
			catch (Exception ex)
			{
				UnityTls.NativeInterface.unitytls_errorstate_raise_error(errorState, UnityTls.unitytls_error_code.UNITYTLS_USER_UNKNOWN_ERROR);
				if (this.lastException == null)
				{
					this.lastException = ex;
				}
				intPtr = (IntPtr)0;
			}
			return intPtr;
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00003678 File Offset: 0x00001878
		[MonoPInvokeCallback(typeof(UnityTls.unitytls_tlsctx_x509verify_callback))]
		private unsafe static UnityTls.unitytls_x509verify_result VerifyCallback(void* userData, UnityTls.unitytls_x509list_ref chain, UnityTls.unitytls_errorstate* errorState)
		{
			return ((UnityTlsContext)((GCHandle)((IntPtr)userData)).Target).VerifyCallback(chain, errorState);
		}

		// Token: 0x060000BC RID: 188 RVA: 0x000036A4 File Offset: 0x000018A4
		private unsafe UnityTls.unitytls_x509verify_result VerifyCallback(UnityTls.unitytls_x509list_ref chain, UnityTls.unitytls_errorstate* errorState)
		{
			UnityTls.unitytls_x509verify_result unitytls_x509verify_result;
			try
			{
				using (X509ChainImplUnityTls x509ChainImplUnityTls = new X509ChainImplUnityTls(chain, false))
				{
					using (X509Chain x509Chain = new X509Chain(x509ChainImplUnityTls))
					{
						this.remoteCertificate = x509Chain.ChainElements[0].Certificate;
						if (base.ValidateCertificate(this.remoteCertificate, x509Chain))
						{
							unitytls_x509verify_result = UnityTls.unitytls_x509verify_result.UNITYTLS_X509VERIFY_SUCCESS;
						}
						else
						{
							unitytls_x509verify_result = UnityTls.unitytls_x509verify_result.UNITYTLS_X509VERIFY_FLAG_NOT_TRUSTED;
						}
					}
				}
			}
			catch (Exception ex)
			{
				if (this.lastException == null)
				{
					this.lastException = ex;
				}
				unitytls_x509verify_result = (UnityTls.unitytls_x509verify_result)4294967295U;
			}
			return unitytls_x509verify_result;
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00003740 File Offset: 0x00001940
		[MonoPInvokeCallback(typeof(UnityTls.unitytls_tlsctx_certificate_callback))]
		private unsafe static void CertificateCallback(void* userData, UnityTls.unitytls_tlsctx* ctx, byte* cn, IntPtr cnLen, UnityTls.unitytls_x509name* caList, IntPtr caListLen, UnityTls.unitytls_x509list_ref* chain, UnityTls.unitytls_key_ref* key, UnityTls.unitytls_errorstate* errorState)
		{
			((UnityTlsContext)((GCHandle)((IntPtr)userData)).Target).CertificateCallback(ctx, cn, cnLen, caList, caListLen, chain, key, errorState);
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00003778 File Offset: 0x00001978
		private unsafe void CertificateCallback(UnityTls.unitytls_tlsctx* ctx, byte* cn, IntPtr cnLen, UnityTls.unitytls_x509name* caList, IntPtr caListLen, UnityTls.unitytls_x509list_ref* chain, UnityTls.unitytls_key_ref* key, UnityTls.unitytls_errorstate* errorState)
		{
			try
			{
				if (this.remoteCertificate == null)
				{
					throw new TlsException(AlertDescription.InternalError, "Cannot request client certificate before receiving one from the server.");
				}
				this.localClientCertificate = base.SelectClientCertificate(null);
				if (this.localClientCertificate == null)
				{
					*chain = new UnityTls.unitytls_x509list_ref
					{
						handle = UnityTls.NativeInterface.UNITYTLS_INVALID_HANDLE
					};
					*key = new UnityTls.unitytls_key_ref
					{
						handle = UnityTls.NativeInterface.UNITYTLS_INVALID_HANDLE
					};
				}
				else
				{
					UnityTls.NativeInterface.unitytls_x509list_free(this.requestedClientCertChain);
					UnityTls.NativeInterface.unitytls_key_free(this.requestedClientKey);
					UnityTlsContext.ExtractNativeKeyAndChainFromManagedCertificate(this.localClientCertificate, errorState, out this.requestedClientCertChain, out this.requestedClientKey);
					*chain = UnityTls.NativeInterface.unitytls_x509list_get_ref(this.requestedClientCertChain, errorState);
					*key = UnityTls.NativeInterface.unitytls_key_get_ref(this.requestedClientKey, errorState);
				}
				Debug.CheckAndThrow(*errorState, "Failed to retrieve certificates on request.", AlertDescription.HandshakeFailure);
			}
			catch (Exception ex)
			{
				UnityTls.NativeInterface.unitytls_errorstate_raise_error(errorState, UnityTls.unitytls_error_code.UNITYTLS_USER_UNKNOWN_ERROR);
				if (this.lastException == null)
				{
					this.lastException = ex;
				}
			}
		}

		// Token: 0x0400009D RID: 157
		private unsafe UnityTls.unitytls_tlsctx* tlsContext = null;

		// Token: 0x0400009E RID: 158
		private unsafe UnityTls.unitytls_x509list* requestedClientCertChain = null;

		// Token: 0x0400009F RID: 159
		private unsafe UnityTls.unitytls_key* requestedClientKey = null;

		// Token: 0x040000A0 RID: 160
		private UnityTls.unitytls_tlsctx_read_callback readCallback;

		// Token: 0x040000A1 RID: 161
		private UnityTls.unitytls_tlsctx_write_callback writeCallback;

		// Token: 0x040000A2 RID: 162
		private UnityTls.unitytls_tlsctx_certificate_callback certificateCallback;

		// Token: 0x040000A3 RID: 163
		private UnityTls.unitytls_tlsctx_x509verify_callback verifyCallback;

		// Token: 0x040000A4 RID: 164
		private X509Certificate localClientCertificate;

		// Token: 0x040000A5 RID: 165
		private X509Certificate2 remoteCertificate;

		// Token: 0x040000A6 RID: 166
		private MonoTlsConnectionInfo connectioninfo;

		// Token: 0x040000A7 RID: 167
		private bool isAuthenticated;

		// Token: 0x040000A8 RID: 168
		private bool hasContext;

		// Token: 0x040000A9 RID: 169
		private bool closedGraceful;

		// Token: 0x040000AA RID: 170
		private byte[] writeBuffer;

		// Token: 0x040000AB RID: 171
		private byte[] readBuffer;

		// Token: 0x040000AC RID: 172
		private GCHandle handle;

		// Token: 0x040000AD RID: 173
		private Exception lastException;
	}
}
