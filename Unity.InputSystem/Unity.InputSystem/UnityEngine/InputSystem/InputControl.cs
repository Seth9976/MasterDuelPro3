using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem
{
	// Token: 0x0200006F RID: 111
	[DebuggerDisplay("{DebuggerDisplay(),nq}")]
	public abstract class InputControl
	{
		// Token: 0x17000172 RID: 370
		// (get) Token: 0x06000502 RID: 1282 RVA: 0x0001498A File Offset: 0x00012B8A
		public string name
		{
			get
			{
				return this.m_Name;
			}
		}

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x06000503 RID: 1283 RVA: 0x00014997 File Offset: 0x00012B97
		// (set) Token: 0x06000504 RID: 1284 RVA: 0x000149C8 File Offset: 0x00012BC8
		public string displayName
		{
			get
			{
				this.RefreshConfigurationIfNeeded();
				if (this.m_DisplayName != null)
				{
					return this.m_DisplayName;
				}
				if (this.m_DisplayNameFromLayout != null)
				{
					return this.m_DisplayNameFromLayout;
				}
				return this.m_Name;
			}
			protected set
			{
				this.m_DisplayName = value;
			}
		}

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x06000505 RID: 1285 RVA: 0x000149D1 File Offset: 0x00012BD1
		// (set) Token: 0x06000506 RID: 1286 RVA: 0x000149F8 File Offset: 0x00012BF8
		public string shortDisplayName
		{
			get
			{
				this.RefreshConfigurationIfNeeded();
				if (this.m_ShortDisplayName != null)
				{
					return this.m_ShortDisplayName;
				}
				if (this.m_ShortDisplayNameFromLayout != null)
				{
					return this.m_ShortDisplayNameFromLayout;
				}
				return null;
			}
			protected set
			{
				this.m_ShortDisplayName = value;
			}
		}

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x06000507 RID: 1287 RVA: 0x00014A01 File Offset: 0x00012C01
		public string path
		{
			get
			{
				if (this.m_Path == null)
				{
					this.m_Path = InputControlPath.Combine(this.m_Parent, this.m_Name);
				}
				return this.m_Path;
			}
		}

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x06000508 RID: 1288 RVA: 0x00014A2D File Offset: 0x00012C2D
		public string layout
		{
			get
			{
				return this.m_Layout;
			}
		}

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x06000509 RID: 1289 RVA: 0x00014A3A File Offset: 0x00012C3A
		public string variants
		{
			get
			{
				return this.m_Variants;
			}
		}

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x0600050A RID: 1290 RVA: 0x00014A47 File Offset: 0x00012C47
		public InputDevice device
		{
			get
			{
				return this.m_Device;
			}
		}

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x0600050B RID: 1291 RVA: 0x00014A4F File Offset: 0x00012C4F
		public InputControl parent
		{
			get
			{
				return this.m_Parent;
			}
		}

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x0600050C RID: 1292 RVA: 0x00014A57 File Offset: 0x00012C57
		public ReadOnlyArray<InputControl> children
		{
			get
			{
				return new ReadOnlyArray<InputControl>(this.m_Device.m_ChildrenForEachControl, this.m_ChildStartIndex, this.m_ChildCount);
			}
		}

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x0600050D RID: 1293 RVA: 0x00014A75 File Offset: 0x00012C75
		public ReadOnlyArray<InternedString> usages
		{
			get
			{
				return new ReadOnlyArray<InternedString>(this.m_Device.m_UsagesForEachControl, this.m_UsageStartIndex, this.m_UsageCount);
			}
		}

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x0600050E RID: 1294 RVA: 0x00014A93 File Offset: 0x00012C93
		public ReadOnlyArray<InternedString> aliases
		{
			get
			{
				return new ReadOnlyArray<InternedString>(this.m_Device.m_AliasesForEachControl, this.m_AliasStartIndex, this.m_AliasCount);
			}
		}

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x0600050F RID: 1295 RVA: 0x00014AB1 File Offset: 0x00012CB1
		public InputStateBlock stateBlock
		{
			get
			{
				return this.m_StateBlock;
			}
		}

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x06000510 RID: 1296 RVA: 0x00014AB9 File Offset: 0x00012CB9
		// (set) Token: 0x06000511 RID: 1297 RVA: 0x00014AC8 File Offset: 0x00012CC8
		public bool noisy
		{
			get
			{
				return (this.m_ControlFlags & InputControl.ControlFlags.IsNoisy) > (InputControl.ControlFlags)0;
			}
			internal set
			{
				if (value)
				{
					this.m_ControlFlags |= InputControl.ControlFlags.IsNoisy;
					ReadOnlyArray<InputControl> list = this.children;
					for (int i = 0; i < list.Count; i++)
					{
						if (list[i] != null)
						{
							list[i].noisy = true;
						}
					}
					return;
				}
				this.m_ControlFlags &= ~InputControl.ControlFlags.IsNoisy;
			}
		}

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x06000512 RID: 1298 RVA: 0x00014B27 File Offset: 0x00012D27
		// (set) Token: 0x06000513 RID: 1299 RVA: 0x00014B34 File Offset: 0x00012D34
		public bool synthetic
		{
			get
			{
				return (this.m_ControlFlags & InputControl.ControlFlags.IsSynthetic) > (InputControl.ControlFlags)0;
			}
			internal set
			{
				if (value)
				{
					this.m_ControlFlags |= InputControl.ControlFlags.IsSynthetic;
					return;
				}
				this.m_ControlFlags &= ~InputControl.ControlFlags.IsSynthetic;
			}
		}

		// Token: 0x17000180 RID: 384
		public InputControl this[string path]
		{
			get
			{
				InputControl inputControl = InputControlPath.TryFindChild(this, path, 0);
				if (inputControl == null)
				{
					throw new KeyNotFoundException(string.Format("Cannot find control '{0}' as child of '{1}'", path, this));
				}
				return inputControl;
			}
		}

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x06000515 RID: 1301
		public abstract Type valueType { get; }

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x06000516 RID: 1302
		public abstract int valueSizeInBytes { get; }

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x06000517 RID: 1303 RVA: 0x00014B76 File Offset: 0x00012D76
		public float magnitude
		{
			get
			{
				return this.EvaluateMagnitude();
			}
		}

		// Token: 0x06000518 RID: 1304 RVA: 0x00014B7E File Offset: 0x00012D7E
		public override string ToString()
		{
			return this.layout + ":" + this.path;
		}

		// Token: 0x06000519 RID: 1305 RVA: 0x00014B98 File Offset: 0x00012D98
		private string DebuggerDisplay()
		{
			if (!this.device.added)
			{
				return this.ToString();
			}
			string text;
			try
			{
				text = string.Format("{0}:{1}={2}", this.layout, this.path, this.ReadValueAsObject());
			}
			catch (Exception)
			{
				text = this.ToString();
			}
			return text;
		}

		// Token: 0x0600051A RID: 1306 RVA: 0x00014BF4 File Offset: 0x00012DF4
		public float EvaluateMagnitude()
		{
			return this.EvaluateMagnitude(this.currentStatePtr);
		}

		// Token: 0x0600051B RID: 1307 RVA: 0x00010C8F File Offset: 0x0000EE8F
		public unsafe virtual float EvaluateMagnitude(void* statePtr)
		{
			return -1f;
		}

		// Token: 0x0600051C RID: 1308
		public unsafe abstract object ReadValueFromBufferAsObject(void* buffer, int bufferSize);

		// Token: 0x0600051D RID: 1309
		public unsafe abstract object ReadValueFromStateAsObject(void* statePtr);

		// Token: 0x0600051E RID: 1310
		public unsafe abstract void ReadValueFromStateIntoBuffer(void* statePtr, void* bufferPtr, int bufferSize);

		// Token: 0x0600051F RID: 1311 RVA: 0x00014C02 File Offset: 0x00012E02
		public unsafe virtual void WriteValueFromBufferIntoState(void* bufferPtr, int bufferSize, void* statePtr)
		{
			throw new NotSupportedException(string.Format("Control '{0}' does not support writing", this));
		}

		// Token: 0x06000520 RID: 1312 RVA: 0x00014C02 File Offset: 0x00012E02
		public unsafe virtual void WriteValueFromObjectIntoState(object value, void* statePtr)
		{
			throw new NotSupportedException(string.Format("Control '{0}' does not support writing", this));
		}

		// Token: 0x06000521 RID: 1313
		public unsafe abstract bool CompareValue(void* firstStatePtr, void* secondStatePtr);

		// Token: 0x06000522 RID: 1314 RVA: 0x00014C14 File Offset: 0x00012E14
		public InputControl TryGetChildControl(string path)
		{
			if (string.IsNullOrEmpty(path))
			{
				throw new ArgumentNullException("path");
			}
			return InputControlPath.TryFindChild(this, path, 0);
		}

		// Token: 0x06000523 RID: 1315 RVA: 0x00014C34 File Offset: 0x00012E34
		public TControl TryGetChildControl<TControl>(string path) where TControl : InputControl
		{
			if (string.IsNullOrEmpty(path))
			{
				throw new ArgumentNullException("path");
			}
			InputControl control = this.TryGetChildControl(path);
			if (control == null)
			{
				return default(TControl);
			}
			TControl controlOfType = control as TControl;
			if (controlOfType == null)
			{
				throw new InvalidOperationException(string.Concat(new string[]
				{
					"Expected control '",
					path,
					"' to be of type '",
					typeof(TControl).Name,
					"' but is of type '",
					control.GetType().Name,
					"' instead!"
				}));
			}
			return controlOfType;
		}

		// Token: 0x06000524 RID: 1316 RVA: 0x00014CD3 File Offset: 0x00012ED3
		public InputControl GetChildControl(string path)
		{
			if (string.IsNullOrEmpty(path))
			{
				throw new ArgumentNullException("path");
			}
			InputControl inputControl = this.TryGetChildControl(path);
			if (inputControl == null)
			{
				throw new ArgumentException("Cannot find input control '" + this.MakeChildPath(path) + "'", "path");
			}
			return inputControl;
		}

		// Token: 0x06000525 RID: 1317 RVA: 0x00014D14 File Offset: 0x00012F14
		public TControl GetChildControl<TControl>(string path) where TControl : InputControl
		{
			InputControl control = this.GetChildControl(path);
			TControl controlOfType = control as TControl;
			if (controlOfType == null)
			{
				throw new ArgumentException(string.Concat(new string[]
				{
					"Expected control '",
					path,
					"' to be of type '",
					typeof(TControl).Name,
					"' but is of type '",
					control.GetType().Name,
					"' instead!"
				}), "path");
			}
			return controlOfType;
		}

		// Token: 0x06000526 RID: 1318 RVA: 0x00014D98 File Offset: 0x00012F98
		protected InputControl()
		{
			this.m_StateBlock.byteOffset = 4294967294U;
		}

		// Token: 0x06000527 RID: 1319 RVA: 0x000049FE File Offset: 0x00002BFE
		protected virtual void FinishSetup()
		{
		}

		// Token: 0x06000528 RID: 1320 RVA: 0x00014DBB File Offset: 0x00012FBB
		protected void RefreshConfigurationIfNeeded()
		{
			if (!this.isConfigUpToDate)
			{
				this.RefreshConfiguration();
				this.isConfigUpToDate = true;
			}
		}

		// Token: 0x06000529 RID: 1321 RVA: 0x000049FE File Offset: 0x00002BFE
		protected virtual void RefreshConfiguration()
		{
		}

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x0600052A RID: 1322 RVA: 0x00014DD2 File Offset: 0x00012FD2
		protected internal unsafe void* currentStatePtr
		{
			get
			{
				return InputStateBuffers.GetFrontBufferForDevice(this.GetDeviceIndex());
			}
		}

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x0600052B RID: 1323 RVA: 0x00014DDF File Offset: 0x00012FDF
		protected internal unsafe void* previousFrameStatePtr
		{
			get
			{
				return InputStateBuffers.GetBackBufferForDevice(this.GetDeviceIndex());
			}
		}

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x0600052C RID: 1324 RVA: 0x00014DEC File Offset: 0x00012FEC
		protected internal unsafe void* defaultStatePtr
		{
			get
			{
				return InputStateBuffers.s_DefaultStateBuffer;
			}
		}

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x0600052D RID: 1325 RVA: 0x00014DF3 File Offset: 0x00012FF3
		protected internal unsafe void* noiseMaskPtr
		{
			get
			{
				return InputStateBuffers.s_NoiseMaskBuffer;
			}
		}

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x0600052E RID: 1326 RVA: 0x00014DFC File Offset: 0x00012FFC
		protected internal uint stateOffsetRelativeToDeviceRoot
		{
			get
			{
				uint deviceStateOffset = this.device.m_StateBlock.byteOffset;
				return this.m_StateBlock.byteOffset - deviceStateOffset;
			}
		}

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x0600052F RID: 1327 RVA: 0x00014E27 File Offset: 0x00013027
		public FourCC optimizedControlDataType
		{
			get
			{
				return this.m_OptimizedControlDataType;
			}
		}

		// Token: 0x06000530 RID: 1328 RVA: 0x00014E2F File Offset: 0x0001302F
		protected virtual FourCC CalculateOptimizedControlDataType()
		{
			return 0;
		}

		// Token: 0x06000531 RID: 1329 RVA: 0x00014E38 File Offset: 0x00013038
		public void ApplyParameterChanges()
		{
			this.SetOptimizedControlDataTypeRecursively();
			for (InputControl currentParent = this.parent; currentParent != null; currentParent = currentParent.parent)
			{
				currentParent.SetOptimizedControlDataType();
			}
			this.MarkAsStaleRecursively();
		}

		// Token: 0x06000532 RID: 1330 RVA: 0x00014E6A File Offset: 0x0001306A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetOptimizedControlDataType()
		{
			this.m_OptimizedControlDataType = (InputSystem.s_Manager.optimizedControlsFeatureEnabled ? this.CalculateOptimizedControlDataType() : 0);
		}

		// Token: 0x06000533 RID: 1331 RVA: 0x00014E8C File Offset: 0x0001308C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal void SetOptimizedControlDataTypeRecursively()
		{
			if (this.m_ChildCount > 0)
			{
				foreach (InputControl inputControl in this.children)
				{
					inputControl.SetOptimizedControlDataTypeRecursively();
				}
			}
			this.SetOptimizedControlDataType();
		}

		// Token: 0x06000534 RID: 1332 RVA: 0x00014EF0 File Offset: 0x000130F0
		[Conditional("UNITY_EDITOR")]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal void EnsureOptimizationTypeHasNotChanged()
		{
			if (!InputSystem.s_Manager.optimizedControlsFeatureEnabled)
			{
				return;
			}
			FourCC currentOptimizedControlDataType = this.CalculateOptimizedControlDataType();
			if (currentOptimizedControlDataType != this.optimizedControlDataType)
			{
				Debug.LogError(string.Concat(new string[]
				{
					"Control '",
					this.name,
					"' / '",
					this.path,
					"' suddenly changed optimization state due to either format ",
					string.Format("change or control parameters change (was '{0}' but became '{1}'), ", this.optimizedControlDataType, currentOptimizedControlDataType),
					"this hinders control hot path optimization, please call control.ApplyParameterChanges() after the changes to the control to fix this error."
				}));
				this.m_OptimizedControlDataType = currentOptimizedControlDataType;
			}
			if (this.m_ChildCount > 0)
			{
				foreach (InputControl inputControl in this.children)
				{
				}
			}
		}

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x06000535 RID: 1333 RVA: 0x00014FD4 File Offset: 0x000131D4
		// (set) Token: 0x06000536 RID: 1334 RVA: 0x00014FE3 File Offset: 0x000131E3
		internal bool isSetupFinished
		{
			get
			{
				return (this.m_ControlFlags & InputControl.ControlFlags.SetupFinished) == InputControl.ControlFlags.SetupFinished;
			}
			set
			{
				if (value)
				{
					this.m_ControlFlags |= InputControl.ControlFlags.SetupFinished;
					return;
				}
				this.m_ControlFlags &= ~InputControl.ControlFlags.SetupFinished;
			}
		}

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x06000537 RID: 1335 RVA: 0x00015007 File Offset: 0x00013207
		// (set) Token: 0x06000538 RID: 1336 RVA: 0x00015014 File Offset: 0x00013214
		internal bool isButton
		{
			get
			{
				return (this.m_ControlFlags & InputControl.ControlFlags.IsButton) == InputControl.ControlFlags.IsButton;
			}
			set
			{
				if (value)
				{
					this.m_ControlFlags |= InputControl.ControlFlags.IsButton;
					return;
				}
				this.m_ControlFlags &= ~InputControl.ControlFlags.IsButton;
			}
		}

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x06000539 RID: 1337 RVA: 0x00015037 File Offset: 0x00013237
		// (set) Token: 0x0600053A RID: 1338 RVA: 0x00015044 File Offset: 0x00013244
		internal bool isConfigUpToDate
		{
			get
			{
				return (this.m_ControlFlags & InputControl.ControlFlags.ConfigUpToDate) == InputControl.ControlFlags.ConfigUpToDate;
			}
			set
			{
				if (value)
				{
					this.m_ControlFlags |= InputControl.ControlFlags.ConfigUpToDate;
					return;
				}
				this.m_ControlFlags &= ~InputControl.ControlFlags.ConfigUpToDate;
			}
		}

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x0600053B RID: 1339 RVA: 0x00015067 File Offset: 0x00013267
		// (set) Token: 0x0600053C RID: 1340 RVA: 0x00015076 File Offset: 0x00013276
		internal bool dontReset
		{
			get
			{
				return (this.m_ControlFlags & InputControl.ControlFlags.DontReset) == InputControl.ControlFlags.DontReset;
			}
			set
			{
				if (value)
				{
					this.m_ControlFlags |= InputControl.ControlFlags.DontReset;
					return;
				}
				this.m_ControlFlags &= ~InputControl.ControlFlags.DontReset;
			}
		}

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x0600053D RID: 1341 RVA: 0x0001509A File Offset: 0x0001329A
		// (set) Token: 0x0600053E RID: 1342 RVA: 0x000150A9 File Offset: 0x000132A9
		internal bool usesStateFromOtherControl
		{
			get
			{
				return (this.m_ControlFlags & InputControl.ControlFlags.UsesStateFromOtherControl) == InputControl.ControlFlags.UsesStateFromOtherControl;
			}
			set
			{
				if (value)
				{
					this.m_ControlFlags |= InputControl.ControlFlags.UsesStateFromOtherControl;
					return;
				}
				this.m_ControlFlags &= ~InputControl.ControlFlags.UsesStateFromOtherControl;
			}
		}

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x0600053F RID: 1343 RVA: 0x000150CD File Offset: 0x000132CD
		internal bool hasDefaultState
		{
			get
			{
				return !this.m_DefaultState.isEmpty;
			}
		}

		// Token: 0x06000540 RID: 1344 RVA: 0x000150E0 File Offset: 0x000132E0
		internal void CallFinishSetupRecursive()
		{
			ReadOnlyArray<InputControl> list = this.children;
			for (int i = 0; i < list.Count; i++)
			{
				list[i].CallFinishSetupRecursive();
			}
			this.FinishSetup();
			this.SetOptimizedControlDataTypeRecursively();
		}

		// Token: 0x06000541 RID: 1345 RVA: 0x0001511F File Offset: 0x0001331F
		internal string MakeChildPath(string path)
		{
			if (this is InputDevice)
			{
				return path;
			}
			return this.path + "/" + path;
		}

		// Token: 0x06000542 RID: 1346 RVA: 0x0001513C File Offset: 0x0001333C
		internal void BakeOffsetIntoStateBlockRecursive(uint offset)
		{
			this.m_StateBlock.byteOffset = this.m_StateBlock.byteOffset + offset;
			ReadOnlyArray<InputControl> list = this.children;
			for (int i = 0; i < list.Count; i++)
			{
				list[i].BakeOffsetIntoStateBlockRecursive(offset);
			}
		}

		// Token: 0x06000543 RID: 1347 RVA: 0x00015184 File Offset: 0x00013384
		internal int GetDeviceIndex()
		{
			int deviceIndex = this.m_Device.m_DeviceIndex;
			if (deviceIndex == -1)
			{
				throw new InvalidOperationException(string.Concat(new string[]
				{
					"Cannot query value of control '",
					this.path,
					"' before '",
					this.device.name,
					"' has been added to system!"
				}));
			}
			return deviceIndex;
		}

		// Token: 0x06000544 RID: 1348 RVA: 0x000151E2 File Offset: 0x000133E2
		internal bool IsValueConsideredPressed(float value)
		{
			if (this.isButton)
			{
				return ((ButtonControl)this).IsValueConsideredPressed(value);
			}
			return value >= ButtonControl.s_GlobalDefaultButtonPressPoint;
		}

		// Token: 0x06000545 RID: 1349 RVA: 0x000049FE File Offset: 0x00002BFE
		internal virtual void AddProcessor(object first)
		{
		}

		// Token: 0x06000546 RID: 1350 RVA: 0x00015204 File Offset: 0x00013404
		internal void MarkAsStale()
		{
			this.m_CachedValueIsStale = true;
			this.m_UnprocessedCachedValueIsStale = true;
		}

		// Token: 0x06000547 RID: 1351 RVA: 0x00015214 File Offset: 0x00013414
		internal void MarkAsStaleRecursively()
		{
			this.MarkAsStale();
			foreach (InputControl inputControl in this.children)
			{
				inputControl.MarkAsStale();
				ButtonControl buttonControl = inputControl as ButtonControl;
				if (buttonControl != null)
				{
					buttonControl.UpdateWasPressed();
				}
			}
		}

		// Token: 0x04000271 RID: 625
		protected internal InputStateBlock m_StateBlock;

		// Token: 0x04000272 RID: 626
		internal InternedString m_Name;

		// Token: 0x04000273 RID: 627
		internal string m_Path;

		// Token: 0x04000274 RID: 628
		internal string m_DisplayName;

		// Token: 0x04000275 RID: 629
		internal string m_DisplayNameFromLayout;

		// Token: 0x04000276 RID: 630
		internal string m_ShortDisplayName;

		// Token: 0x04000277 RID: 631
		internal string m_ShortDisplayNameFromLayout;

		// Token: 0x04000278 RID: 632
		internal InternedString m_Layout;

		// Token: 0x04000279 RID: 633
		internal InternedString m_Variants;

		// Token: 0x0400027A RID: 634
		internal InputDevice m_Device;

		// Token: 0x0400027B RID: 635
		internal InputControl m_Parent;

		// Token: 0x0400027C RID: 636
		internal int m_UsageCount;

		// Token: 0x0400027D RID: 637
		internal int m_UsageStartIndex;

		// Token: 0x0400027E RID: 638
		internal int m_AliasCount;

		// Token: 0x0400027F RID: 639
		internal int m_AliasStartIndex;

		// Token: 0x04000280 RID: 640
		internal int m_ChildCount;

		// Token: 0x04000281 RID: 641
		internal int m_ChildStartIndex;

		// Token: 0x04000282 RID: 642
		internal InputControl.ControlFlags m_ControlFlags;

		// Token: 0x04000283 RID: 643
		internal bool m_CachedValueIsStale = true;

		// Token: 0x04000284 RID: 644
		internal bool m_UnprocessedCachedValueIsStale = true;

		// Token: 0x04000285 RID: 645
		internal PrimitiveValue m_DefaultState;

		// Token: 0x04000286 RID: 646
		internal PrimitiveValue m_MinValue;

		// Token: 0x04000287 RID: 647
		internal PrimitiveValue m_MaxValue;

		// Token: 0x04000288 RID: 648
		internal FourCC m_OptimizedControlDataType;

		// Token: 0x02000070 RID: 112
		[Flags]
		internal enum ControlFlags
		{
			// Token: 0x0400028A RID: 650
			ConfigUpToDate = 1,
			// Token: 0x0400028B RID: 651
			IsNoisy = 2,
			// Token: 0x0400028C RID: 652
			IsSynthetic = 4,
			// Token: 0x0400028D RID: 653
			IsButton = 8,
			// Token: 0x0400028E RID: 654
			DontReset = 16,
			// Token: 0x0400028F RID: 655
			SetupFinished = 32,
			// Token: 0x04000290 RID: 656
			UsesStateFromOtherControl = 64
		}
	}
}
