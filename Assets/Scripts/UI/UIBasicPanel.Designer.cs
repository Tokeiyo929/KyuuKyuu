using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace QFramework.Example
{
	// Generate Id:35be8c7c-f13d-4e03-aaf7-339848de6807
	public partial class UIBasicPanel
	{
		public const string Name = "UIBasicPanel";
		
		
		private UIBasicPanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			
			mData = null;
		}
		
		public UIBasicPanelData Data
		{
			get
			{
				return mData;
			}
		}
		
		UIBasicPanelData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new UIBasicPanelData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
