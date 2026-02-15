using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace System.Diagnostics
{
	/// <summary>Provides a set of methods and properties that enable applications to trace the execution of code and associate trace messages with their source. </summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x0200016A RID: 362
	public class TraceSource
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.TraceSource" /> class, using the specified name for the source. </summary>
		/// <param name="name">The name of the source (typically, the name of the application).</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> is an empty string ("").</exception>
		// Token: 0x06000884 RID: 2180 RVA: 0x0002D67D File Offset: 0x0002B87D
		public TraceSource(string name)
			: this(name, SourceLevels.Off)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.TraceSource" /> class, using the specified name for the source and the default source level at which tracing is to occur.  </summary>
		/// <param name="name">The name of the source, typically the name of the application.</param>
		/// <param name="defaultLevel">A bitwise combination of the enumeration values that specifies the default source level at which to trace.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> is an empty string ("").</exception>
		// Token: 0x06000885 RID: 2181 RVA: 0x0002D688 File Offset: 0x0002B888
		public TraceSource(string name, SourceLevels defaultLevel)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (name.Length == 0)
			{
				throw new ArgumentException("name");
			}
			this.sourceName = name;
			this.switchLevel = defaultLevel;
			List<WeakReference> list = TraceSource.tracesources;
			lock (list)
			{
				TraceSource._pruneCachedTraceSources();
				TraceSource.tracesources.Add(new WeakReference(this));
			}
		}

		// Token: 0x06000886 RID: 2182 RVA: 0x0002D710 File Offset: 0x0002B910
		private static void _pruneCachedTraceSources()
		{
			List<WeakReference> list = TraceSource.tracesources;
			lock (list)
			{
				if (TraceSource.s_LastCollectionCount != GC.CollectionCount(2))
				{
					List<WeakReference> list2 = new List<WeakReference>(TraceSource.tracesources.Count);
					for (int i = 0; i < TraceSource.tracesources.Count; i++)
					{
						if ((TraceSource)TraceSource.tracesources[i].Target != null)
						{
							list2.Add(TraceSource.tracesources[i]);
						}
					}
					if (list2.Count < TraceSource.tracesources.Count)
					{
						TraceSource.tracesources.Clear();
						TraceSource.tracesources.AddRange(list2);
						TraceSource.tracesources.TrimExcess();
					}
					TraceSource.s_LastCollectionCount = GC.CollectionCount(2);
				}
			}
		}

		// Token: 0x06000887 RID: 2183 RVA: 0x0002D7E4 File Offset: 0x0002B9E4
		private void Initialize()
		{
			if (!this._initCalled)
			{
				lock (this)
				{
					if (!this._initCalled)
					{
						SourceElementsCollection sources = DiagnosticsConfiguration.Sources;
						if (sources != null)
						{
							SourceElement sourceElement = sources[this.sourceName];
							if (sourceElement != null)
							{
								if (!string.IsNullOrEmpty(sourceElement.SwitchName))
								{
									this.CreateSwitch(sourceElement.SwitchType, sourceElement.SwitchName);
								}
								else
								{
									this.CreateSwitch(sourceElement.SwitchType, this.sourceName);
									if (!string.IsNullOrEmpty(sourceElement.SwitchValue))
									{
										this.internalSwitch.Level = (SourceLevels)Enum.Parse(typeof(SourceLevels), sourceElement.SwitchValue);
									}
								}
								this.listeners = sourceElement.Listeners.GetRuntimeObject();
								this.attributes = new StringDictionary();
								TraceUtils.VerifyAttributes(sourceElement.Attributes, this.GetSupportedAttributes(), this);
								this.attributes.ReplaceHashtable(sourceElement.Attributes);
							}
							else
							{
								this.NoConfigInit();
							}
						}
						else
						{
							this.NoConfigInit();
						}
						this._initCalled = true;
					}
				}
			}
		}

		// Token: 0x06000888 RID: 2184 RVA: 0x0002D91C File Offset: 0x0002BB1C
		private void NoConfigInit()
		{
			this.internalSwitch = new SourceSwitch(this.sourceName, this.switchLevel.ToString());
			this.listeners = new TraceListenerCollection();
			this.listeners.Add(new DefaultTraceListener());
			this.attributes = null;
		}

		/// <summary>Flushes all the trace listeners in the trace listener collection.</summary>
		/// <exception cref="T:System.ObjectDisposedException">An attempt was made to trace an event during finalization.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000889 RID: 2185 RVA: 0x0002D978 File Offset: 0x0002BB78
		public void Flush()
		{
			if (this.listeners != null)
			{
				if (TraceInternal.UseGlobalLock)
				{
					object critSec = TraceInternal.critSec;
					lock (critSec)
					{
						using (IEnumerator enumerator = this.listeners.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								object obj = enumerator.Current;
								((TraceListener)obj).Flush();
							}
							return;
						}
					}
				}
				foreach (object obj2 in this.listeners)
				{
					TraceListener traceListener = (TraceListener)obj2;
					if (!traceListener.IsThreadSafe)
					{
						TraceListener traceListener2 = traceListener;
						lock (traceListener2)
						{
							traceListener.Flush();
							continue;
						}
					}
					traceListener.Flush();
				}
			}
		}

		/// <summary>Gets the custom attributes supported by the trace source.</summary>
		/// <returns>A string array naming the custom attributes supported by the trace source, or null if there are no custom attributes.</returns>
		// Token: 0x0600088A RID: 2186 RVA: 0x000027B6 File Offset: 0x000009B6
		protected internal virtual string[] GetSupportedAttributes()
		{
			return null;
		}

		// Token: 0x0600088B RID: 2187 RVA: 0x0002DA90 File Offset: 0x0002BC90
		private void CreateSwitch(string typename, string name)
		{
			if (!string.IsNullOrEmpty(typename))
			{
				this.internalSwitch = (SourceSwitch)TraceUtils.GetRuntimeObject(typename, typeof(SourceSwitch), name);
				return;
			}
			this.internalSwitch = new SourceSwitch(name, this.switchLevel.ToString());
		}

		/// <summary>Gets the collection of trace listeners for the trace source.</summary>
		/// <returns>A <see cref="T:System.Diagnostics.TraceListenerCollection" /> that contains the active trace listeners associated with the source. </returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x17000173 RID: 371
		// (get) Token: 0x0600088C RID: 2188 RVA: 0x0002DAE3 File Offset: 0x0002BCE3
		public TraceListenerCollection Listeners
		{
			get
			{
				this.Initialize();
				return this.listeners;
			}
		}

		/// <summary>Gets or sets the source switch value.</summary>
		/// <returns>A <see cref="T:System.Diagnostics.SourceSwitch" /> object representing the source switch value.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <see cref="P:System.Diagnostics.TraceSource.Switch" /> is set to null.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x17000174 RID: 372
		// (get) Token: 0x0600088D RID: 2189 RVA: 0x0002DAF3 File Offset: 0x0002BCF3
		public SourceSwitch Switch
		{
			get
			{
				this.Initialize();
				return this.internalSwitch;
			}
		}

		// Token: 0x0400067A RID: 1658
		private static List<WeakReference> tracesources = new List<WeakReference>();

		// Token: 0x0400067B RID: 1659
		private static int s_LastCollectionCount;

		// Token: 0x0400067C RID: 1660
		private volatile SourceSwitch internalSwitch;

		// Token: 0x0400067D RID: 1661
		private volatile TraceListenerCollection listeners;

		// Token: 0x0400067E RID: 1662
		private StringDictionary attributes;

		// Token: 0x0400067F RID: 1663
		private SourceLevels switchLevel;

		// Token: 0x04000680 RID: 1664
		private volatile string sourceName;

		// Token: 0x04000681 RID: 1665
		internal volatile bool _initCalled;
	}
}
