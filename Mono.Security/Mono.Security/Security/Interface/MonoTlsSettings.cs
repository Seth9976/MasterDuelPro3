using System;
using System.Security.Cryptography.X509Certificates;
using System.Threading;

namespace Mono.Security.Interface
{
	// Token: 0x02000044 RID: 68
	public sealed class MonoTlsSettings
	{
		// Token: 0x17000060 RID: 96
		// (get) Token: 0x0600014C RID: 332 RVA: 0x0000944A File Offset: 0x0000764A
		// (set) Token: 0x0600014D RID: 333 RVA: 0x00009452 File Offset: 0x00007652
		public MonoRemoteCertificateValidationCallback RemoteCertificateValidationCallback { get; set; }

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x0600014E RID: 334 RVA: 0x0000945B File Offset: 0x0000765B
		// (set) Token: 0x0600014F RID: 335 RVA: 0x00009463 File Offset: 0x00007663
		public MonoLocalCertificateSelectionCallback ClientCertificateSelectionCallback { get; set; }

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000150 RID: 336 RVA: 0x0000946C File Offset: 0x0000766C
		// (set) Token: 0x06000151 RID: 337 RVA: 0x00009474 File Offset: 0x00007674
		public bool? UseServicePointManagerCallback
		{
			get
			{
				return this.useServicePointManagerCallback;
			}
			set
			{
				this.useServicePointManagerCallback = value;
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000152 RID: 338 RVA: 0x0000947D File Offset: 0x0000767D
		public bool SkipSystemValidators
		{
			get
			{
				return this.skipSystemValidators;
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000153 RID: 339 RVA: 0x00009485 File Offset: 0x00007685
		public bool CallbackNeedsCertificateChain
		{
			get
			{
				return this.callbackNeedsChain;
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000154 RID: 340 RVA: 0x0000948D File Offset: 0x0000768D
		// (set) Token: 0x06000155 RID: 341 RVA: 0x00009495 File Offset: 0x00007695
		public DateTime? CertificateValidationTime { get; set; }

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000156 RID: 342 RVA: 0x0000949E File Offset: 0x0000769E
		// (set) Token: 0x06000157 RID: 343 RVA: 0x000094A6 File Offset: 0x000076A6
		public X509CertificateCollection TrustAnchors { get; set; }

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000158 RID: 344 RVA: 0x000094AF File Offset: 0x000076AF
		// (set) Token: 0x06000159 RID: 345 RVA: 0x000094B7 File Offset: 0x000076B7
		public object UserSettings { get; set; }

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x0600015A RID: 346 RVA: 0x000094C0 File Offset: 0x000076C0
		// (set) Token: 0x0600015B RID: 347 RVA: 0x000094C8 File Offset: 0x000076C8
		internal string[] CertificateSearchPaths { get; set; }

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x0600015C RID: 348 RVA: 0x000094D1 File Offset: 0x000076D1
		// (set) Token: 0x0600015D RID: 349 RVA: 0x000094D9 File Offset: 0x000076D9
		internal bool SendCloseNotify { get; set; }

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x0600015E RID: 350 RVA: 0x000094E2 File Offset: 0x000076E2
		// (set) Token: 0x0600015F RID: 351 RVA: 0x000094EA File Offset: 0x000076EA
		public string[] ClientCertificateIssuers { get; set; }

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000160 RID: 352 RVA: 0x000094F3 File Offset: 0x000076F3
		// (set) Token: 0x06000161 RID: 353 RVA: 0x000094FB File Offset: 0x000076FB
		public bool DisallowUnauthenticatedCertificateRequest { get; set; }

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000162 RID: 354 RVA: 0x00009504 File Offset: 0x00007704
		// (set) Token: 0x06000163 RID: 355 RVA: 0x0000950C File Offset: 0x0000770C
		public TlsProtocols? EnabledProtocols { get; set; }

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000164 RID: 356 RVA: 0x00009515 File Offset: 0x00007715
		// (set) Token: 0x06000165 RID: 357 RVA: 0x0000951D File Offset: 0x0000771D
		[CLSCompliant(false)]
		public CipherSuiteCode[] EnabledCiphers { get; set; }

		// Token: 0x06000166 RID: 358 RVA: 0x00009526 File Offset: 0x00007726
		public MonoTlsSettings()
		{
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000167 RID: 359 RVA: 0x0000953C File Offset: 0x0000773C
		public static MonoTlsSettings DefaultSettings
		{
			get
			{
				if (MonoTlsSettings.defaultSettings == null)
				{
					Interlocked.CompareExchange<MonoTlsSettings>(ref MonoTlsSettings.defaultSettings, new MonoTlsSettings(), null);
				}
				return MonoTlsSettings.defaultSettings;
			}
		}

		// Token: 0x06000168 RID: 360 RVA: 0x0000955B File Offset: 0x0000775B
		public static MonoTlsSettings CopyDefaultSettings()
		{
			return MonoTlsSettings.DefaultSettings.Clone();
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000169 RID: 361 RVA: 0x00009567 File Offset: 0x00007767
		[Obsolete("Do not use outside System.dll!")]
		public ICertificateValidator CertificateValidator
		{
			get
			{
				return this.certificateValidator;
			}
		}

		// Token: 0x0600016A RID: 362 RVA: 0x0000956F File Offset: 0x0000776F
		[Obsolete("Do not use outside System.dll!")]
		public MonoTlsSettings CloneWithValidator(ICertificateValidator validator)
		{
			if (this.cloned)
			{
				this.certificateValidator = validator;
				return this;
			}
			return new MonoTlsSettings(this)
			{
				certificateValidator = validator
			};
		}

		// Token: 0x0600016B RID: 363 RVA: 0x0000958F File Offset: 0x0000778F
		public MonoTlsSettings Clone()
		{
			return new MonoTlsSettings(this);
		}

		// Token: 0x0600016C RID: 364 RVA: 0x00009598 File Offset: 0x00007798
		private MonoTlsSettings(MonoTlsSettings other)
		{
			this.RemoteCertificateValidationCallback = other.RemoteCertificateValidationCallback;
			this.ClientCertificateSelectionCallback = other.ClientCertificateSelectionCallback;
			this.checkCertName = other.checkCertName;
			this.checkCertRevocationStatus = other.checkCertRevocationStatus;
			this.UseServicePointManagerCallback = other.useServicePointManagerCallback;
			this.skipSystemValidators = other.skipSystemValidators;
			this.callbackNeedsChain = other.callbackNeedsChain;
			this.UserSettings = other.UserSettings;
			this.EnabledProtocols = other.EnabledProtocols;
			this.EnabledCiphers = other.EnabledCiphers;
			this.CertificateValidationTime = other.CertificateValidationTime;
			this.SendCloseNotify = other.SendCloseNotify;
			this.ClientCertificateIssuers = other.ClientCertificateIssuers;
			this.DisallowUnauthenticatedCertificateRequest = other.DisallowUnauthenticatedCertificateRequest;
			if (other.TrustAnchors != null)
			{
				this.TrustAnchors = new X509CertificateCollection(other.TrustAnchors);
			}
			if (other.CertificateSearchPaths != null)
			{
				this.CertificateSearchPaths = new string[other.CertificateSearchPaths.Length];
				other.CertificateSearchPaths.CopyTo(this.CertificateSearchPaths, 0);
			}
			this.cloned = true;
		}

		// Token: 0x040001DC RID: 476
		private bool cloned;

		// Token: 0x040001DD RID: 477
		private bool checkCertName = true;

		// Token: 0x040001DE RID: 478
		private bool checkCertRevocationStatus;

		// Token: 0x040001DF RID: 479
		private bool? useServicePointManagerCallback;

		// Token: 0x040001E0 RID: 480
		private bool skipSystemValidators;

		// Token: 0x040001E1 RID: 481
		private bool callbackNeedsChain = true;

		// Token: 0x040001E2 RID: 482
		private ICertificateValidator certificateValidator;

		// Token: 0x040001E3 RID: 483
		private static MonoTlsSettings defaultSettings;
	}
}
