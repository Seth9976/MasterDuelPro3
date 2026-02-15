using System;
using System.Collections;
using System.Configuration;
using System.Xml;

namespace System.Diagnostics
{
	// Token: 0x02000154 RID: 340
	internal class ListenerElement : TypedElement
	{
		// Token: 0x060007DB RID: 2011 RVA: 0x0002B7E0 File Offset: 0x000299E0
		public ListenerElement(bool allowReferences)
			: base(typeof(TraceListener))
		{
			this._allowReferences = allowReferences;
			ConfigurationPropertyOptions configurationPropertyOptions = ConfigurationPropertyOptions.None;
			if (!this._allowReferences)
			{
				configurationPropertyOptions |= ConfigurationPropertyOptions.IsRequired;
			}
			this._propListenerTypeName = new ConfigurationProperty("type", typeof(string), null, configurationPropertyOptions);
			this._properties.Remove("type");
			this._properties.Add(this._propListenerTypeName);
			this._properties.Add(ListenerElement._propFilter);
			this._properties.Add(ListenerElement._propName);
			this._properties.Add(ListenerElement._propOutputOpts);
		}

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x060007DC RID: 2012 RVA: 0x0002B880 File Offset: 0x00029A80
		public Hashtable Attributes
		{
			get
			{
				if (this._attributes == null)
				{
					this._attributes = new Hashtable(StringComparer.OrdinalIgnoreCase);
				}
				return this._attributes;
			}
		}

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x060007DD RID: 2013 RVA: 0x0002B8A0 File Offset: 0x00029AA0
		[ConfigurationProperty("filter")]
		public FilterElement Filter
		{
			get
			{
				return (FilterElement)base[ListenerElement._propFilter];
			}
		}

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x060007DE RID: 2014 RVA: 0x0002B8B2 File Offset: 0x00029AB2
		// (set) Token: 0x060007DF RID: 2015 RVA: 0x0002B8C4 File Offset: 0x00029AC4
		[ConfigurationProperty("name", IsRequired = true, IsKey = true)]
		public string Name
		{
			get
			{
				return (string)base[ListenerElement._propName];
			}
			set
			{
				base[ListenerElement._propName] = value;
			}
		}

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x060007E0 RID: 2016 RVA: 0x0002B8D2 File Offset: 0x00029AD2
		[ConfigurationProperty("traceOutputOptions", DefaultValue = TraceOptions.None)]
		public TraceOptions TraceOutputOptions
		{
			get
			{
				return (TraceOptions)base[ListenerElement._propOutputOpts];
			}
		}

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x060007E1 RID: 2017 RVA: 0x0002B8E4 File Offset: 0x00029AE4
		// (set) Token: 0x060007E2 RID: 2018 RVA: 0x0002B8F7 File Offset: 0x00029AF7
		[ConfigurationProperty("type")]
		public override string TypeName
		{
			get
			{
				return (string)base[this._propListenerTypeName];
			}
			set
			{
				base[this._propListenerTypeName] = value;
			}
		}

		// Token: 0x060007E3 RID: 2019 RVA: 0x0002B908 File Offset: 0x00029B08
		public override bool Equals(object compareTo)
		{
			if (this.Name.Equals("Default") && this.TypeName.Equals(typeof(DefaultTraceListener).FullName))
			{
				ListenerElement listenerElement = compareTo as ListenerElement;
				return listenerElement != null && listenerElement.Name.Equals("Default") && listenerElement.TypeName.Equals(typeof(DefaultTraceListener).FullName);
			}
			return base.Equals(compareTo);
		}

		// Token: 0x060007E4 RID: 2020 RVA: 0x0002B983 File Offset: 0x00029B83
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x060007E5 RID: 2021 RVA: 0x0002B98C File Offset: 0x00029B8C
		public TraceListener GetRuntimeObject()
		{
			if (this._runtimeObject != null)
			{
				return (TraceListener)this._runtimeObject;
			}
			TraceListener traceListener;
			try
			{
				if (string.IsNullOrEmpty(this.TypeName))
				{
					if (this._attributes != null || base.ElementInformation.Properties[ListenerElement._propFilter.Name].ValueOrigin == PropertyValueOrigin.SetHere || this.TraceOutputOptions != TraceOptions.None || !string.IsNullOrEmpty(base.InitData))
					{
						throw new ConfigurationErrorsException(SR.GetString("A listener with no type name specified references the sharedListeners section and cannot have any attributes other than 'Name'.  Listener: '{0}'.", new object[] { this.Name }));
					}
					if (DiagnosticsConfiguration.SharedListeners == null)
					{
						throw new ConfigurationErrorsException(SR.GetString("Listener '{0}' does not exist in the sharedListeners section.", new object[] { this.Name }));
					}
					ListenerElement listenerElement = DiagnosticsConfiguration.SharedListeners[this.Name];
					if (listenerElement == null)
					{
						throw new ConfigurationErrorsException(SR.GetString("Listener '{0}' does not exist in the sharedListeners section.", new object[] { this.Name }));
					}
					this._runtimeObject = listenerElement.GetRuntimeObject();
					traceListener = (TraceListener)this._runtimeObject;
				}
				else
				{
					TraceListener traceListener2 = (TraceListener)base.BaseGetRuntimeObject();
					traceListener2.initializeData = base.InitData;
					traceListener2.Name = this.Name;
					traceListener2.SetAttributes(this.Attributes);
					traceListener2.TraceOutputOptions = this.TraceOutputOptions;
					if (this.Filter != null && this.Filter.TypeName != null && this.Filter.TypeName.Length != 0)
					{
						traceListener2.Filter = this.Filter.GetRuntimeObject();
						XmlWriterTraceListener xmlWriterTraceListener = traceListener2 as XmlWriterTraceListener;
						if (xmlWriterTraceListener != null)
						{
							xmlWriterTraceListener.shouldRespectFilterOnTraceTransfer = true;
						}
					}
					this._runtimeObject = traceListener2;
					traceListener = traceListener2;
				}
			}
			catch (ArgumentException ex)
			{
				throw new ConfigurationErrorsException(SR.GetString("Couldn't create listener '{0}'.", new object[] { this.Name }), ex);
			}
			return traceListener;
		}

		// Token: 0x060007E6 RID: 2022 RVA: 0x0002BB64 File Offset: 0x00029D64
		protected override bool OnDeserializeUnrecognizedAttribute(string name, string value)
		{
			this.Attributes.Add(name, value);
			return true;
		}

		// Token: 0x060007E7 RID: 2023 RVA: 0x0002BB74 File Offset: 0x00029D74
		protected override void PreSerialize(XmlWriter writer)
		{
			if (this._attributes != null)
			{
				IDictionaryEnumerator enumerator = this._attributes.GetEnumerator();
				while (enumerator.MoveNext())
				{
					string text = (string)enumerator.Value;
					string text2 = (string)enumerator.Key;
					if (text != null && writer != null)
					{
						writer.WriteAttributeString(text2, text);
					}
				}
			}
		}

		// Token: 0x060007E8 RID: 2024 RVA: 0x0002BBC5 File Offset: 0x00029DC5
		protected override bool SerializeElement(XmlWriter writer, bool serializeCollectionKey)
		{
			return base.SerializeElement(writer, serializeCollectionKey) || (this._attributes != null && this._attributes.Count > 0);
		}

		// Token: 0x060007E9 RID: 2025 RVA: 0x0002BBEC File Offset: 0x00029DEC
		protected override void Unmerge(ConfigurationElement sourceElement, ConfigurationElement parentElement, ConfigurationSaveMode saveMode)
		{
			base.Unmerge(sourceElement, parentElement, saveMode);
			ListenerElement listenerElement = sourceElement as ListenerElement;
			if (listenerElement != null && listenerElement._attributes != null)
			{
				this._attributes = listenerElement._attributes;
			}
		}

		// Token: 0x04000610 RID: 1552
		private static readonly ConfigurationProperty _propFilter = new ConfigurationProperty("filter", typeof(FilterElement), null, ConfigurationPropertyOptions.None);

		// Token: 0x04000611 RID: 1553
		private static readonly ConfigurationProperty _propName = new ConfigurationProperty("name", typeof(string), null, ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey);

		// Token: 0x04000612 RID: 1554
		private static readonly ConfigurationProperty _propOutputOpts = new ConfigurationProperty("traceOutputOptions", typeof(TraceOptions), TraceOptions.None, ConfigurationPropertyOptions.None);

		// Token: 0x04000613 RID: 1555
		private ConfigurationProperty _propListenerTypeName;

		// Token: 0x04000614 RID: 1556
		private bool _allowReferences;

		// Token: 0x04000615 RID: 1557
		private Hashtable _attributes;

		// Token: 0x04000616 RID: 1558
		internal bool _isAddedByDefault;
	}
}
