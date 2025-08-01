using System;
using UnityEditor.U2D.Common;
using UnityEngine.UIElements;

namespace UnityEditor.U2D.Animation
{
#if ENABLE_UXML_SERIALIZED_DATA
    [UxmlElement]
#endif
    internal partial class RigToolbar : Toolbar
    {
        private const string k_UxmlPath = "SkinningModule/RigToolbar.uxml";
        private const string k_UssPath = "SkinningModule/RigToolbarStyle.uss";
        private const string k_ToolbarId = "RigToolbar";

        private const string k_CopyRigId = "CopyRig";
        private const string k_PasteRigId = "PasteRig";

#if ENABLE_UXML_TRAITS
        public class CustomUXMLFactor : UxmlFactory<RigToolbar, UxmlTraits> { }
#endif

        public event Action ActivateCopyTool = () => { };
        public event Action TogglePasteTool = () => { };

        public SkinningCache skinningCache { get; set; }

        private Button m_CopyBtn;
        private Button m_PasteBtn;

        public static RigToolbar GenerateFromUXML()
        {
            RigToolbar clone = GetClone(k_UxmlPath, k_ToolbarId) as RigToolbar;
            clone.BindElements();
            clone.SetupShortcutUtility();
            clone.LocalizeTextInChildren();
            clone.AddShortcutsToToolTips();
            return clone;
        }

        public RigToolbar()
        {
            styleSheets.Add(ResourceLoader.Load<StyleSheet>(k_UssPath));
        }

        private void BindElements()
        {
            m_CopyBtn = this.Q<Button>(k_CopyRigId);
            m_CopyBtn.clickable.clicked += () => { ActivateCopyTool(); };

            m_PasteBtn = this.Q<Button>(k_PasteRigId);
            m_PasteBtn.clickable.clicked += () => { TogglePasteTool(); };
        }

        private void SetupShortcutUtility()
        {
            m_ShortcutUtility = new ShortcutUtility(ShortcutIds.pastePanelWeights);
            m_ShortcutUtility.OnShortcutChanged = () =>
            {
                RestoreButtonTooltips(k_UxmlPath, k_ToolbarId);
                AddShortcutsToToolTips();
            };
        }

        public void UpdatePasteButtonCheckedState()
        {
            SetButtonChecked(m_PasteBtn, skinningCache.GetTool(Tools.CopyPaste).isActive);
        }

        public void UpdatePasteButtonEnabledState()
        {
            CopyTool tool = skinningCache.GetTool(Tools.CopyPaste) as CopyTool;
            m_PasteBtn.SetEnabled(tool.hasValidCopiedData);
        }

        private void AddShortcutsToToolTips()
        {
            m_ShortcutUtility.AddShortcutToButtonTooltip(this, k_PasteRigId, ShortcutIds.pastePanelWeights);
        }
    }
}
