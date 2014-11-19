﻿using System.Drawing;
using System.Windows.Forms;

namespace Bloom.Collection
{
	partial class CollectionSettingsDialog
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CollectionSettingsDialog));
			this._tab = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this._removeLanguage3Link = new System.Windows.Forms.LinkLabel();
			this._changeLanguage3Link = new System.Windows.Forms.LinkLabel();
			this._changeLanguage2Link = new System.Windows.Forms.LinkLabel();
			this._changeLanguage1Link = new System.Windows.Forms.LinkLabel();
			this._language3Name = new System.Windows.Forms.Label();
			this._language3Label = new System.Windows.Forms.Label();
			this._language2Name = new System.Windows.Forms.Label();
			this._language2Label = new System.Windows.Forms.Label();
			this._language1Name = new System.Windows.Forms.Label();
			this._language1Label = new System.Windows.Forms.Label();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this._rtlLanguage3CheckBox = new System.Windows.Forms.CheckBox();
			this._rtlLanguage2CheckBox = new System.Windows.Forms.CheckBox();
			this._rtlLanguage1CheckBox = new System.Windows.Forms.CheckBox();
			this._fontComboLanguage3 = new System.Windows.Forms.ComboBox();
			this._fontComboLanguage2 = new System.Windows.Forms.ComboBox();
			this._fontComboLanguage1 = new System.Windows.Forms.ComboBox();
			this._language3FontLabel = new System.Windows.Forms.Label();
			this._language2FontLabel = new System.Windows.Forms.Label();
			this._language1FontLabel = new System.Windows.Forms.Label();
			this._xmatterPackLabel = new System.Windows.Forms.Label();
			this._xmatterPackCombo = new System.Windows.Forms.ComboBox();
			this.tabPage3 = new System.Windows.Forms.TabPage();
			this._bloomCollectionName = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this._districtText = new System.Windows.Forms.TextBox();
			this._provinceText = new System.Windows.Forms.TextBox();
			this._countryText = new System.Windows.Forms.TextBox();
			this._countryLabel = new System.Windows.Forms.Label();
			this._districtLabel = new System.Windows.Forms.Label();
			this._provinceLabel = new System.Windows.Forms.Label();
			this.tabPage4 = new System.Windows.Forms.TabPage();
			this._showExperimentCommands = new System.Windows.Forms.CheckBox();
			this._showExperimentalTemplates = new System.Windows.Forms.CheckBox();
			this._showSendReceive = new System.Windows.Forms.CheckBox();
			this._useImageServer = new System.Windows.Forms.CheckBox();
			this._okButton = new System.Windows.Forms.Button();
			this._restartReminder = new System.Windows.Forms.Label();
			this._L10NSharpExtender = new L10NSharp.UI.L10NSharpExtender(this.components);
			this._cancelButton = new System.Windows.Forms.Button();
			this.settingsProtectionLauncherButton1 = new Palaso.UI.WindowsForms.SettingProtection.SettingsProtectionLauncherButton();
			this._helpButton = new System.Windows.Forms.Button();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this._tab.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.tabPage2.SuspendLayout();
			this.tabPage3.SuspendLayout();
			this.tabPage4.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this._L10NSharpExtender)).BeginInit();
			this.SuspendLayout();
			// 
			// _tab
			// 
			this._tab.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this._tab.Controls.Add(this.tabPage1);
			this._tab.Controls.Add(this.tabPage2);
			this._tab.Controls.Add(this.tabPage3);
			this._tab.Controls.Add(this.tabPage4);
			this._tab.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this._tab.Location = new System.Drawing.Point(1, 2);
			this._tab.Name = "_tab";
			this._tab.SelectedIndex = 0;
			this._tab.Size = new System.Drawing.Size(618, 341);
			this._tab.TabIndex = 0;
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this._removeLanguage3Link);
			this.tabPage1.Controls.Add(this._changeLanguage3Link);
			this.tabPage1.Controls.Add(this._changeLanguage2Link);
			this.tabPage1.Controls.Add(this._changeLanguage1Link);
			this.tabPage1.Controls.Add(this._language3Name);
			this.tabPage1.Controls.Add(this._language3Label);
			this.tabPage1.Controls.Add(this._language2Name);
			this.tabPage1.Controls.Add(this._language2Label);
			this.tabPage1.Controls.Add(this._language1Name);
			this.tabPage1.Controls.Add(this._language1Label);
			this._L10NSharpExtender.SetLocalizableToolTip(this.tabPage1, null);
			this._L10NSharpExtender.SetLocalizationComment(this.tabPage1, null);
			this._L10NSharpExtender.SetLocalizingId(this.tabPage1, "CollectionSettingsDialog.LanguageTab.LanguageTabLabel");
			this.tabPage1.Location = new System.Drawing.Point(4, 26);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(610, 311);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "Languages";
			this.tabPage1.UseVisualStyleBackColor = true;
			// 
			// _removeLanguage3Link
			// 
			this._removeLanguage3Link.AutoSize = true;
			this._L10NSharpExtender.SetLocalizableToolTip(this._removeLanguage3Link, null);
			this._L10NSharpExtender.SetLocalizationComment(this._removeLanguage3Link, null);
			this._L10NSharpExtender.SetLocalizingId(this._removeLanguage3Link, "CollectionSettingsDialog.LanguageTab._removeLanguageLink");
			this._removeLanguage3Link.Location = new System.Drawing.Point(159, 243);
			this._removeLanguage3Link.Name = "_removeLanguage3Link";
			this._removeLanguage3Link.Size = new System.Drawing.Size(58, 19);
			this._removeLanguage3Link.TabIndex = 18;
			this._removeLanguage3Link.TabStop = true;
			this._removeLanguage3Link.Text = "Remove";
			this._removeLanguage3Link.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this._removeSecondNationalLanguageButton_LinkClicked);
			// 
			// _changeLanguage3Link
			// 
			this._changeLanguage3Link.AutoSize = true;
			this._L10NSharpExtender.SetLocalizableToolTip(this._changeLanguage3Link, null);
			this._L10NSharpExtender.SetLocalizationComment(this._changeLanguage3Link, null);
			this._L10NSharpExtender.SetLocalizingId(this._changeLanguage3Link, "CollectionSettingsDialog.LanguageTab.ChangeLanguageLink");
			this._changeLanguage3Link.Location = new System.Drawing.Point(27, 243);
			this._changeLanguage3Link.Name = "_changeLanguage3Link";
			this._changeLanguage3Link.Size = new System.Drawing.Size(65, 19);
			this._changeLanguage3Link.TabIndex = 17;
			this._changeLanguage3Link.TabStop = true;
			this._changeLanguage3Link.Text = "Change...";
			this._changeLanguage3Link.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this._language3ChangeLink_LinkClicked);
			// 
			// _changeLanguage2Link
			// 
			this._changeLanguage2Link.AutoSize = true;
			this._L10NSharpExtender.SetLocalizableToolTip(this._changeLanguage2Link, null);
			this._L10NSharpExtender.SetLocalizationComment(this._changeLanguage2Link, null);
			this._L10NSharpExtender.SetLocalizingId(this._changeLanguage2Link, "CollectionSettingsDialog.LanguageTab.ChangeLanguageLink");
			this._changeLanguage2Link.Location = new System.Drawing.Point(27, 158);
			this._changeLanguage2Link.Name = "_changeLanguage2Link";
			this._changeLanguage2Link.Size = new System.Drawing.Size(65, 19);
			this._changeLanguage2Link.TabIndex = 16;
			this._changeLanguage2Link.TabStop = true;
			this._changeLanguage2Link.Text = "Change...";
			this._changeLanguage2Link.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this._language2ChangeLink_LinkClicked);
			// 
			// _changeLanguage1Link
			// 
			this._changeLanguage1Link.AutoSize = true;
			this._L10NSharpExtender.SetLocalizableToolTip(this._changeLanguage1Link, null);
			this._L10NSharpExtender.SetLocalizationComment(this._changeLanguage1Link, null);
			this._L10NSharpExtender.SetLocalizingId(this._changeLanguage1Link, "CollectionSettingsDialog.LanguageTab.ChangeLanguageLink");
			this._changeLanguage1Link.Location = new System.Drawing.Point(27, 69);
			this._changeLanguage1Link.Name = "_changeLanguage1Link";
			this._changeLanguage1Link.Size = new System.Drawing.Size(65, 19);
			this._changeLanguage1Link.TabIndex = 15;
			this._changeLanguage1Link.TabStop = true;
			this._changeLanguage1Link.Text = "Change...";
			this._changeLanguage1Link.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this._language1ChangeLink_LinkClicked);
			// 
			// _language3Name
			// 
			this._language3Name.AutoSize = true;
			this._language3Name.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this._L10NSharpExtender.SetLocalizableToolTip(this._language3Name, null);
			this._L10NSharpExtender.SetLocalizationComment(this._language3Name, null);
			this._L10NSharpExtender.SetLocalizationPriority(this._language3Name, L10NSharp.LocalizationPriority.NotLocalizable);
			this._L10NSharpExtender.SetLocalizingId(this._language3Name, "CollectionSettingsDialog._nationalLanguage2Label");
			this._language3Name.Location = new System.Drawing.Point(26, 218);
			this._language3Name.Name = "_language3Name";
			this._language3Name.Size = new System.Drawing.Size(49, 19);
			this._language3Name.TabIndex = 14;
			this._language3Name.Text = "foobar";
			// 
			// _language3Label
			// 
			this._language3Label.AutoSize = true;
			this._language3Label.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this._L10NSharpExtender.SetLocalizableToolTip(this._language3Label, null);
			this._L10NSharpExtender.SetLocalizationComment(this._language3Label, null);
			this._L10NSharpExtender.SetLocalizingId(this._language3Label, "CollectionSettingsDialog.LanguageTab._language3Label");
			this._language3Label.Location = new System.Drawing.Point(26, 198);
			this._language3Label.Name = "_language3Label";
			this._language3Label.Size = new System.Drawing.Size(316, 19);
			this._language3Label.TabIndex = 13;
			this._language3Label.Text = "Language 3 (e.g. Regional Language)   (Optional)";
			// 
			// _language2Name
			// 
			this._language2Name.AutoSize = true;
			this._language2Name.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this._L10NSharpExtender.SetLocalizableToolTip(this._language2Name, null);
			this._L10NSharpExtender.SetLocalizationComment(this._language2Name, null);
			this._L10NSharpExtender.SetLocalizationPriority(this._language2Name, L10NSharp.LocalizationPriority.NotLocalizable);
			this._L10NSharpExtender.SetLocalizingId(this._language2Name, "CollectionSettingsDialog._nationalLanguage1Label");
			this._language2Name.Location = new System.Drawing.Point(26, 133);
			this._language2Name.Name = "_language2Name";
			this._language2Name.Size = new System.Drawing.Size(49, 19);
			this._language2Name.TabIndex = 11;
			this._language2Name.Text = "foobar";
			// 
			// _language2Label
			// 
			this._language2Label.AutoSize = true;
			this._language2Label.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this._L10NSharpExtender.SetLocalizableToolTip(this._language2Label, null);
			this._L10NSharpExtender.SetLocalizationComment(this._language2Label, null);
			this._L10NSharpExtender.SetLocalizingId(this._language2Label, "CollectionSettingsDialog.LanguageTab._language2Label");
			this._language2Label.Location = new System.Drawing.Point(26, 113);
			this._language2Label.Name = "_language2Label";
			this._language2Label.Size = new System.Drawing.Size(238, 19);
			this._language2Label.TabIndex = 10;
			this._language2Label.Text = "Language 2 (e.g. National Language)";
			// 
			// _language1Name
			// 
			this._language1Name.AutoSize = true;
			this._language1Name.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this._L10NSharpExtender.SetLocalizableToolTip(this._language1Name, null);
			this._L10NSharpExtender.SetLocalizationComment(this._language1Name, null);
			this._L10NSharpExtender.SetLocalizationPriority(this._language1Name, L10NSharp.LocalizationPriority.NotLocalizable);
			this._L10NSharpExtender.SetLocalizingId(this._language1Name, "CollectionSettingsDialog._vernacularLanguageName");
			this._language1Name.Location = new System.Drawing.Point(26, 44);
			this._language1Name.Name = "_language1Name";
			this._language1Name.Size = new System.Drawing.Size(49, 19);
			this._language1Name.TabIndex = 8;
			this._language1Name.Text = "foobar";
			// 
			// _language1Label
			// 
			this._language1Label.AutoSize = true;
			this._language1Label.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this._L10NSharpExtender.SetLocalizableToolTip(this._language1Label, null);
			this._L10NSharpExtender.SetLocalizationComment(this._language1Label, null);
			this._L10NSharpExtender.SetLocalizingId(this._language1Label, "CollectionSettingsDialog.LanguageTab._language1Label");
			this._language1Label.Location = new System.Drawing.Point(26, 24);
			this._language1Label.Name = "_language1Label";
			this._language1Label.Size = new System.Drawing.Size(140, 19);
			this._language1Label.TabIndex = 7;
			this._language1Label.Text = "Vernacular Language";
			// 
			// tabPage2
			// 
			this.tabPage2.Controls.Add(this._rtlLanguage3CheckBox);
			this.tabPage2.Controls.Add(this._rtlLanguage2CheckBox);
			this.tabPage2.Controls.Add(this._rtlLanguage1CheckBox);
			this.tabPage2.Controls.Add(this._fontComboLanguage3);
			this.tabPage2.Controls.Add(this._fontComboLanguage2);
			this.tabPage2.Controls.Add(this._fontComboLanguage1);
			this.tabPage2.Controls.Add(this._language3FontLabel);
			this.tabPage2.Controls.Add(this._language2FontLabel);
			this.tabPage2.Controls.Add(this._language1FontLabel);
			this.tabPage2.Controls.Add(this._xmatterPackLabel);
			this.tabPage2.Controls.Add(this._xmatterPackCombo);
			this._L10NSharpExtender.SetLocalizableToolTip(this.tabPage2, null);
			this._L10NSharpExtender.SetLocalizationComment(this.tabPage2, null);
			this._L10NSharpExtender.SetLocalizingId(this.tabPage2, "CollectionSettingsDialog.BookMakingTab.BookMakingTabLabel");
			this.tabPage2.Location = new System.Drawing.Point(4, 26);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage2.Size = new System.Drawing.Size(610, 311);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "Book Making";
			this.tabPage2.UseVisualStyleBackColor = true;
			// 
			// _rtlLanguage3CheckBox
			// 
			this._rtlLanguage3CheckBox.AutoSize = true;
			this._rtlLanguage3CheckBox.Font = new System.Drawing.Font("Segoe UI", 8F);
			this._L10NSharpExtender.SetLocalizableToolTip(this._rtlLanguage3CheckBox, null);
			this._L10NSharpExtender.SetLocalizationComment(this._rtlLanguage3CheckBox, null);
			this._L10NSharpExtender.SetLocalizingId(this._rtlLanguage3CheckBox, "CollectionSettingsDialog.BookMakingTab.RightToLeftWritingSystem");
			this._rtlLanguage3CheckBox.Location = new System.Drawing.Point(32, 247);
			this._rtlLanguage3CheckBox.Name = "_rtlLanguage3CheckBox";
			this._rtlLanguage3CheckBox.Size = new System.Drawing.Size(170, 17);
			this._rtlLanguage3CheckBox.TabIndex = 26;
			this._rtlLanguage3CheckBox.Text = "Right to Left Writing System";
			this._rtlLanguage3CheckBox.UseVisualStyleBackColor = true;
			this._rtlLanguage3CheckBox.CheckedChanged += new System.EventHandler(this._rtlLanguageCheckBox_CheckedChanged);
			// 
			// _rtlLanguage2CheckBox
			// 
			this._rtlLanguage2CheckBox.AutoSize = true;
			this._rtlLanguage2CheckBox.Font = new System.Drawing.Font("Segoe UI", 8F);
			this._L10NSharpExtender.SetLocalizableToolTip(this._rtlLanguage2CheckBox, null);
			this._L10NSharpExtender.SetLocalizationComment(this._rtlLanguage2CheckBox, null);
			this._L10NSharpExtender.SetLocalizingId(this._rtlLanguage2CheckBox, "CollectionSettingsDialog.BookMakingTab.RightToLeftWritingSystem");
			this._rtlLanguage2CheckBox.Location = new System.Drawing.Point(32, 160);
			this._rtlLanguage2CheckBox.Name = "_rtlLanguage2CheckBox";
			this._rtlLanguage2CheckBox.Size = new System.Drawing.Size(170, 17);
			this._rtlLanguage2CheckBox.TabIndex = 24;
			this._rtlLanguage2CheckBox.Text = "Right to Left Writing System";
			this._rtlLanguage2CheckBox.UseVisualStyleBackColor = true;
			this._rtlLanguage2CheckBox.CheckedChanged += new System.EventHandler(this._rtlLanguageCheckBox_CheckedChanged);
			// 
			// _rtlLanguage1CheckBox
			// 
			this._rtlLanguage1CheckBox.AutoSize = true;
			this._rtlLanguage1CheckBox.Font = new System.Drawing.Font("Segoe UI", 8F);
			this._L10NSharpExtender.SetLocalizableToolTip(this._rtlLanguage1CheckBox, null);
			this._L10NSharpExtender.SetLocalizationComment(this._rtlLanguage1CheckBox, null);
			this._L10NSharpExtender.SetLocalizingId(this._rtlLanguage1CheckBox, "CollectionSettingsDialog.BookMakingTab.RightToLeftWritingSystem");
			this._rtlLanguage1CheckBox.Location = new System.Drawing.Point(32, 73);
			this._rtlLanguage1CheckBox.Name = "_rtlLanguage1CheckBox";
			this._rtlLanguage1CheckBox.Size = new System.Drawing.Size(170, 17);
			this._rtlLanguage1CheckBox.TabIndex = 22;
			this._rtlLanguage1CheckBox.Text = "Right to Left Writing System";
			this._rtlLanguage1CheckBox.UseVisualStyleBackColor = true;
			this._rtlLanguage1CheckBox.CheckedChanged += new System.EventHandler(this._rtlLanguageCheckBox_CheckedChanged);
			// 
			// _fontComboLanguage3
			// 
			this._fontComboLanguage3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this._fontComboLanguage3.FormattingEnabled = true;
			this._L10NSharpExtender.SetLocalizableToolTip(this._fontComboLanguage3, null);
			this._L10NSharpExtender.SetLocalizationComment(this._fontComboLanguage3, null);
			this._L10NSharpExtender.SetLocalizingId(this._fontComboLanguage3, "CollectionSettingsDialog._fontComboLanguage3");
			this._fontComboLanguage3.Location = new System.Drawing.Point(31, 220);
			this._fontComboLanguage3.Name = "_fontComboLanguage3";
			this._fontComboLanguage3.Size = new System.Drawing.Size(190, 25);
			this._fontComboLanguage3.TabIndex = 25;
			this._fontComboLanguage3.SelectedIndexChanged += new System.EventHandler(this._fontComboLanguage3_SelectedIndexChanged);
			// 
			// _fontComboLanguage2
			// 
			this._fontComboLanguage2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this._fontComboLanguage2.FormattingEnabled = true;
			this._L10NSharpExtender.SetLocalizableToolTip(this._fontComboLanguage2, null);
			this._L10NSharpExtender.SetLocalizationComment(this._fontComboLanguage2, null);
			this._L10NSharpExtender.SetLocalizingId(this._fontComboLanguage2, "CollectionSettingsDialog._fontComboLanguage2");
			this._fontComboLanguage2.Location = new System.Drawing.Point(31, 133);
			this._fontComboLanguage2.Name = "_fontComboLanguage2";
			this._fontComboLanguage2.Size = new System.Drawing.Size(190, 25);
			this._fontComboLanguage2.TabIndex = 23;
			this._fontComboLanguage2.SelectedIndexChanged += new System.EventHandler(this._fontComboLanguage2_SelectedIndexChanged);
			// 
			// _fontComboLanguage1
			// 
			this._fontComboLanguage1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this._fontComboLanguage1.FormattingEnabled = true;
			this._L10NSharpExtender.SetLocalizableToolTip(this._fontComboLanguage1, null);
			this._L10NSharpExtender.SetLocalizationComment(this._fontComboLanguage1, null);
			this._L10NSharpExtender.SetLocalizingId(this._fontComboLanguage1, "CollectionSettingsDialog._fontComboLanguage1");
			this._fontComboLanguage1.Location = new System.Drawing.Point(31, 46);
			this._fontComboLanguage1.Name = "_fontComboLanguage1";
			this._fontComboLanguage1.Size = new System.Drawing.Size(190, 25);
			this._fontComboLanguage1.TabIndex = 21;
			this._fontComboLanguage1.SelectedIndexChanged += new System.EventHandler(this._fontComboLanguage1_SelectedIndexChanged);
			// 
			// _language3FontLabel
			// 
			this._language3FontLabel.AutoSize = true;
			this._language3FontLabel.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this._L10NSharpExtender.SetLocalizableToolTip(this._language3FontLabel, null);
			this._L10NSharpExtender.SetLocalizationComment(this._language3FontLabel, "{0} is a language name.");
			this._L10NSharpExtender.SetLocalizingId(this._language3FontLabel, "CollectionSettingsDialog.BookMakingTab.DefaultFontFor");
			this._language3FontLabel.Location = new System.Drawing.Point(27, 198);
			this._language3FontLabel.Name = "_language3FontLabel";
			this._language3FontLabel.Size = new System.Drawing.Size(131, 19);
			this._language3FontLabel.TabIndex = 24;
			this._language3FontLabel.Text = "Default Font for {0}";
			// 
			// _language2FontLabel
			// 
			this._language2FontLabel.AutoSize = true;
			this._language2FontLabel.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this._L10NSharpExtender.SetLocalizableToolTip(this._language2FontLabel, null);
			this._L10NSharpExtender.SetLocalizationComment(this._language2FontLabel, "{0} is a language name.");
			this._L10NSharpExtender.SetLocalizingId(this._language2FontLabel, "CollectionSettingsDialog.BookMakingTab.DefaultFontFor");
			this._language2FontLabel.Location = new System.Drawing.Point(27, 111);
			this._language2FontLabel.Name = "_language2FontLabel";
			this._language2FontLabel.Size = new System.Drawing.Size(131, 19);
			this._language2FontLabel.TabIndex = 23;
			this._language2FontLabel.Text = "Default Font for {0}";
			// 
			// _language1FontLabel
			// 
			this._language1FontLabel.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this._L10NSharpExtender.SetLocalizableToolTip(this._language1FontLabel, null);
			this._L10NSharpExtender.SetLocalizationComment(this._language1FontLabel, "{0} is a language name.");
			this._L10NSharpExtender.SetLocalizingId(this._language1FontLabel, "CollectionSettingsDialog.BookMakingTab.DefaultFontFor");
			this._language1FontLabel.Location = new System.Drawing.Point(27, 24);
			this._language1FontLabel.Name = "_language1FontLabel";
			this._language1FontLabel.Size = new System.Drawing.Size(250, 19);
			this._language1FontLabel.TabIndex = 22;
			this._language1FontLabel.Text = "Default Font for {0}";
			// 
			// _xmatterPackLabel
			// 
			this._xmatterPackLabel.AutoSize = true;
			this._xmatterPackLabel.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this._L10NSharpExtender.SetLocalizableToolTip(this._xmatterPackLabel, null);
			this._L10NSharpExtender.SetLocalizationComment(this._xmatterPackLabel, null);
			this._L10NSharpExtender.SetLocalizingId(this._xmatterPackLabel, "CollectionSettingsDialog.BookMakingTab.Front/BackMatterPack");
			this._xmatterPackLabel.Location = new System.Drawing.Point(289, 24);
			this._xmatterPackLabel.Name = "_xmatterPackLabel";
			this._xmatterPackLabel.Size = new System.Drawing.Size(156, 19);
			this._xmatterPackLabel.TabIndex = 1;
			this._xmatterPackLabel.Text = "Front/Back Matter Pack";
			// 
			// _xmatterPackCombo
			// 
			this._xmatterPackCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this._xmatterPackCombo.FormattingEnabled = true;
			this._L10NSharpExtender.SetLocalizableToolTip(this._xmatterPackCombo, null);
			this._L10NSharpExtender.SetLocalizationComment(this._xmatterPackCombo, null);
			this._L10NSharpExtender.SetLocalizingId(this._xmatterPackCombo, "CollectionSettingsDialog._xmatterPackCombo");
			this._xmatterPackCombo.Location = new System.Drawing.Point(293, 46);
			this._xmatterPackCombo.Name = "_xmatterPackCombo";
			this._xmatterPackCombo.Size = new System.Drawing.Size(146, 25);
			this._xmatterPackCombo.TabIndex = 27;
			// 
			// tabPage3
			// 
			this.tabPage3.Controls.Add(this._bloomCollectionName);
			this.tabPage3.Controls.Add(this.label1);
			this.tabPage3.Controls.Add(this._districtText);
			this.tabPage3.Controls.Add(this._provinceText);
			this.tabPage3.Controls.Add(this._countryText);
			this.tabPage3.Controls.Add(this._countryLabel);
			this.tabPage3.Controls.Add(this._districtLabel);
			this.tabPage3.Controls.Add(this._provinceLabel);
			this._L10NSharpExtender.SetLocalizableToolTip(this.tabPage3, null);
			this._L10NSharpExtender.SetLocalizationComment(this.tabPage3, null);
			this._L10NSharpExtender.SetLocalizingId(this.tabPage3, "CollectionSettingsDialog.ProjectInformationTab.ProjectInformationTabLabel");
			this.tabPage3.Location = new System.Drawing.Point(4, 26);
			this.tabPage3.Name = "tabPage3";
			this.tabPage3.Size = new System.Drawing.Size(610, 311);
			this.tabPage3.TabIndex = 2;
			this.tabPage3.Text = "Project Information";
			this.tabPage3.UseVisualStyleBackColor = true;
			// 
			// _bloomCollectionName
			// 
			this._bloomCollectionName.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this._L10NSharpExtender.SetLocalizableToolTip(this._bloomCollectionName, null);
			this._L10NSharpExtender.SetLocalizationComment(this._bloomCollectionName, null);
			this._L10NSharpExtender.SetLocalizingId(this._bloomCollectionName, "CollectionSettingsDialog.BloomProjectName");
			this._bloomCollectionName.Location = new System.Drawing.Point(32, 246);
			this._bloomCollectionName.Name = "_bloomCollectionName";
			this._bloomCollectionName.Size = new System.Drawing.Size(291, 25);
			this._bloomCollectionName.TabIndex = 22;
			this._bloomCollectionName.TextChanged += new System.EventHandler(this._bloomCollectionName_TextChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this._L10NSharpExtender.SetLocalizableToolTip(this.label1, null);
			this._L10NSharpExtender.SetLocalizationComment(this.label1, null);
			this._L10NSharpExtender.SetLocalizingId(this.label1, "CollectionSettingsDialog.ProjectInformationTab.BloomCollectionName");
			this.label1.Location = new System.Drawing.Point(28, 224);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(158, 19);
			this.label1.TabIndex = 21;
			this.label1.Text = "Bloom Collection Name";
			// 
			// _districtText
			// 
			this._districtText.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this._L10NSharpExtender.SetLocalizableToolTip(this._districtText, null);
			this._L10NSharpExtender.SetLocalizationComment(this._districtText, null);
			this._L10NSharpExtender.SetLocalizingId(this._districtText, "CollectionSettingsDialog._districtText");
			this._districtText.Location = new System.Drawing.Point(32, 177);
			this._districtText.Name = "_districtText";
			this._districtText.Size = new System.Drawing.Size(291, 25);
			this._districtText.TabIndex = 5;
			// 
			// _provinceText
			// 
			this._provinceText.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this._L10NSharpExtender.SetLocalizableToolTip(this._provinceText, null);
			this._L10NSharpExtender.SetLocalizationComment(this._provinceText, null);
			this._L10NSharpExtender.SetLocalizingId(this._provinceText, "CollectionSettingsDialog._provinceText");
			this._provinceText.Location = new System.Drawing.Point(32, 112);
			this._provinceText.Name = "_provinceText";
			this._provinceText.Size = new System.Drawing.Size(291, 25);
			this._provinceText.TabIndex = 4;
			// 
			// _countryText
			// 
			this._countryText.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this._L10NSharpExtender.SetLocalizableToolTip(this._countryText, null);
			this._L10NSharpExtender.SetLocalizationComment(this._countryText, null);
			this._L10NSharpExtender.SetLocalizingId(this._countryText, "CollectionSettingsDialog._countryText");
			this._countryText.Location = new System.Drawing.Point(32, 45);
			this._countryText.Name = "_countryText";
			this._countryText.Size = new System.Drawing.Size(291, 25);
			this._countryText.TabIndex = 3;
			// 
			// _countryLabel
			// 
			this._countryLabel.AutoSize = true;
			this._countryLabel.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this._L10NSharpExtender.SetLocalizableToolTip(this._countryLabel, null);
			this._L10NSharpExtender.SetLocalizationComment(this._countryLabel, null);
			this._L10NSharpExtender.SetLocalizingId(this._countryLabel, "CollectionSettingsDialog.ProjectInformationTab.Country");
			this._countryLabel.Location = new System.Drawing.Point(28, 23);
			this._countryLabel.Name = "_countryLabel";
			this._countryLabel.Size = new System.Drawing.Size(59, 19);
			this._countryLabel.TabIndex = 2;
			this._countryLabel.Text = "Country";
			// 
			// _districtLabel
			// 
			this._districtLabel.AutoSize = true;
			this._districtLabel.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this._L10NSharpExtender.SetLocalizableToolTip(this._districtLabel, null);
			this._L10NSharpExtender.SetLocalizationComment(this._districtLabel, null);
			this._L10NSharpExtender.SetLocalizingId(this._districtLabel, "CollectionSettingsDialog.ProjectInformationTab.District");
			this._districtLabel.Location = new System.Drawing.Point(28, 155);
			this._districtLabel.Name = "_districtLabel";
			this._districtLabel.Size = new System.Drawing.Size(55, 19);
			this._districtLabel.TabIndex = 1;
			this._districtLabel.Text = "District";
			// 
			// _provinceLabel
			// 
			this._provinceLabel.AutoSize = true;
			this._provinceLabel.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this._L10NSharpExtender.SetLocalizableToolTip(this._provinceLabel, null);
			this._L10NSharpExtender.SetLocalizationComment(this._provinceLabel, null);
			this._L10NSharpExtender.SetLocalizingId(this._provinceLabel, "CollectionSettingsDialog.ProjectInformationTab.Province");
			this._provinceLabel.Location = new System.Drawing.Point(28, 90);
			this._provinceLabel.Name = "_provinceLabel";
			this._provinceLabel.Size = new System.Drawing.Size(63, 19);
			this._provinceLabel.TabIndex = 0;
			this._provinceLabel.Text = "Province";
			// 
			// tabPage4
			// 
			this.tabPage4.Controls.Add(this._showExperimentCommands);
			this.tabPage4.Controls.Add(this._showExperimentalTemplates);
			this.tabPage4.Controls.Add(this._showSendReceive);
			this.tabPage4.Controls.Add(this._useImageServer);
			this._L10NSharpExtender.SetLocalizableToolTip(this.tabPage4, null);
			this._L10NSharpExtender.SetLocalizationComment(this.tabPage4, null);
			this._L10NSharpExtender.SetLocalizingId(this.tabPage4, "CollectionSettingsDialog.AdvancedTab.AdvancedProgramSettingsTabLabel");
			this.tabPage4.Location = new System.Drawing.Point(4, 26);
			this.tabPage4.Name = "tabPage4";
			this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage4.Size = new System.Drawing.Size(610, 311);
			this.tabPage4.TabIndex = 3;
			this.tabPage4.Text = "Advanced Program Settings";
			this.tabPage4.UseVisualStyleBackColor = true;
			// 
			// _showExperimentCommands
			// 
			this._showExperimentCommands.AutoSize = true;
			this._L10NSharpExtender.SetLocalizableToolTip(this._showExperimentCommands, null);
			this._L10NSharpExtender.SetLocalizationComment(this._showExperimentCommands, null);
			this._L10NSharpExtender.SetLocalizationPriority(this._showExperimentCommands, L10NSharp.LocalizationPriority.Low);
			this._L10NSharpExtender.SetLocalizingId(this._showExperimentCommands, "CollectionSettingsDialog.AdvancedTab.Experimental.ShowExperimentalCommands");
			this._showExperimentCommands.Location = new System.Drawing.Point(50, 165);
			this._showExperimentCommands.Name = "_showExperimentCommands";
			this._showExperimentCommands.Size = new System.Drawing.Size(404, 23);
			this._showExperimentCommands.TabIndex = 4;
			this._showExperimentCommands.Text = "Show Experimental Commands (e.g. Export XML for InDesign)";
			this._showExperimentCommands.UseVisualStyleBackColor = true;
			this._showExperimentCommands.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
			// 
			// _showExperimentalTemplates
			// 
			this._showExperimentalTemplates.AutoSize = true;
			this._L10NSharpExtender.SetLocalizableToolTip(this._showExperimentalTemplates, null);
			this._L10NSharpExtender.SetLocalizationComment(this._showExperimentalTemplates, null);
			this._L10NSharpExtender.SetLocalizationPriority(this._showExperimentalTemplates, L10NSharp.LocalizationPriority.Low);
			this._L10NSharpExtender.SetLocalizingId(this._showExperimentalTemplates, "CollectionSettingsDialog.AdvancedTab.Experimental.ShowExperimentalTemplates");
			this._showExperimentalTemplates.Location = new System.Drawing.Point(50, 120);
			this._showExperimentalTemplates.Name = "_showExperimentalTemplates";
			this._showExperimentalTemplates.Size = new System.Drawing.Size(354, 23);
			this._showExperimentalTemplates.TabIndex = 3;
			this._showExperimentalTemplates.Text = "Show Experimental Templates (e.g. Picture Dictionary)";
			this._showExperimentalTemplates.UseVisualStyleBackColor = true;
			this._showExperimentalTemplates.CheckedChanged += new System.EventHandler(this._showExperimentalTemplates_CheckedChanged);
			// 
			// _showSendReceive
			// 
			this._showSendReceive.AutoSize = true;
			this._showSendReceive.Enabled = false;
			this._L10NSharpExtender.SetLocalizableToolTip(this._showSendReceive, null);
			this._L10NSharpExtender.SetLocalizationComment(this._showSendReceive, null);
			this._L10NSharpExtender.SetLocalizationPriority(this._showSendReceive, L10NSharp.LocalizationPriority.Low);
			this._L10NSharpExtender.SetLocalizingId(this._showSendReceive, "CollectionSettingsDialog.AdvancedTab.Experimental._ShowSendReceive");
			this._showSendReceive.Location = new System.Drawing.Point(50, 78);
			this._showSendReceive.Name = "_showSendReceive";
			this._showSendReceive.Size = new System.Drawing.Size(291, 23);
			this._showSendReceive.TabIndex = 1;
			this._showSendReceive.Text = "(Experimental) Show Send/Receive Controls";
			this._showSendReceive.UseVisualStyleBackColor = true;
			this._showSendReceive.CheckedChanged += new System.EventHandler(this._showSendReceive_CheckedChanged);
			// 
			// _useImageServer
			// 
			this._useImageServer.AutoSize = true;
			this._useImageServer.Enabled = false;
			this._L10NSharpExtender.SetLocalizableToolTip(this._useImageServer, null);
			this._L10NSharpExtender.SetLocalizationComment(this._useImageServer, "This option will probably go away, as any bugs with the Image Server get ironed o" +
        "ut. This has to do with how Bloom works internally; the I.S. makes it work bette" +
        "r on slow computers.");
			this._L10NSharpExtender.SetLocalizingId(this._useImageServer, "CollectionSettingsDialog.AdvancedTab.Experimental.UseImageServer");
			this._useImageServer.Location = new System.Drawing.Point(50, 35);
			this._useImageServer.Name = "_useImageServer";
			this._useImageServer.Size = new System.Drawing.Size(406, 23);
			this._useImageServer.TabIndex = 0;
			this._useImageServer.Text = "Use Image Server to reduce memory usage with large images.";
			this._useImageServer.UseVisualStyleBackColor = true;
			// 
			// _okButton
			// 
			this._okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this._L10NSharpExtender.SetLocalizableToolTip(this._okButton, null);
			this._L10NSharpExtender.SetLocalizationComment(this._okButton, null);
			this._L10NSharpExtender.SetLocalizingId(this._okButton, "Common.OKButton");
			this._okButton.Location = new System.Drawing.Point(422, 393);
			this._okButton.Name = "_okButton";
			this._okButton.Size = new System.Drawing.Size(91, 23);
			this._okButton.TabIndex = 1;
			this._okButton.Text = "&OK";
			this._okButton.UseVisualStyleBackColor = true;
			this._okButton.Click += new System.EventHandler(this._okButton_Click);
			// 
			// _restartReminder
			// 
			this._restartReminder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this._restartReminder.AutoSize = true;
			this._restartReminder.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this._restartReminder.ForeColor = System.Drawing.Color.Firebrick;
			this._L10NSharpExtender.SetLocalizableToolTip(this._restartReminder, null);
			this._L10NSharpExtender.SetLocalizationComment(this._restartReminder, null);
			this._L10NSharpExtender.SetLocalizingId(this._restartReminder, "CollectionSettingsDialog._restartMessage");
			this._restartReminder.Location = new System.Drawing.Point(273, 348);
			this._restartReminder.MaximumSize = new System.Drawing.Size(380, 0);
			this._restartReminder.Name = "_restartReminder";
			this._restartReminder.Size = new System.Drawing.Size(346, 34);
			this._restartReminder.TabIndex = 19;
			this._restartReminder.Text = "Bloom will close and re-open this project with the new settings.";
			this._restartReminder.Visible = false;
			// 
			// _L10NSharpExtender
			// 
			this._L10NSharpExtender.LocalizationManagerId = "Bloom";
			this._L10NSharpExtender.PrefixForNewItems = null;
			// 
			// _cancelButton
			// 
			this._cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this._cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this._L10NSharpExtender.SetLocalizableToolTip(this._cancelButton, null);
			this._L10NSharpExtender.SetLocalizationComment(this._cancelButton, null);
			this._L10NSharpExtender.SetLocalizingId(this._cancelButton, "Common.CancelButton");
			this._cancelButton.Location = new System.Drawing.Point(533, 393);
			this._cancelButton.Name = "_cancelButton";
			this._cancelButton.Size = new System.Drawing.Size(75, 23);
			this._cancelButton.TabIndex = 21;
			this._cancelButton.Text = "&Cancel";
			this._cancelButton.UseVisualStyleBackColor = true;
			this._cancelButton.Click += new System.EventHandler(this._cancelButton_Click);
			// 
			// settingsProtectionLauncherButton1
			// 
			this.settingsProtectionLauncherButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this._L10NSharpExtender.SetLocalizableToolTip(this.settingsProtectionLauncherButton1, null);
			this._L10NSharpExtender.SetLocalizationComment(this.settingsProtectionLauncherButton1, null);
			this._L10NSharpExtender.SetLocalizingId(this.settingsProtectionLauncherButton1, "CollectionSettingsDialog.SettingsProtectionLauncherButton");
			this.settingsProtectionLauncherButton1.Location = new System.Drawing.Point(13, 345);
			this.settingsProtectionLauncherButton1.Margin = new System.Windows.Forms.Padding(0);
			this.settingsProtectionLauncherButton1.Name = "settingsProtectionLauncherButton1";
			this.settingsProtectionLauncherButton1.Size = new System.Drawing.Size(257, 37);
			this.settingsProtectionLauncherButton1.TabIndex = 20;
			// 
			// _helpButton
			// 
			this._helpButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this._L10NSharpExtender.SetLocalizableToolTip(this._helpButton, null);
			this._L10NSharpExtender.SetLocalizationComment(this._helpButton, null);
			this._L10NSharpExtender.SetLocalizingId(this._helpButton, "Common.HelpButton");
			this._helpButton.Location = new System.Drawing.Point(13, 393);
			this._helpButton.Name = "_helpButton";
			this._helpButton.Size = new System.Drawing.Size(75, 23);
			this._helpButton.TabIndex = 22;
			this._helpButton.Text = "&Help";
			this._helpButton.UseVisualStyleBackColor = true;
			this._helpButton.Click += new System.EventHandler(this._helpButton_Click);
			// 
			// CollectionSettingsDialog
			// 
			this.AcceptButton = this._okButton;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this._cancelButton;
			this.ClientSize = new System.Drawing.Size(620, 431);
			this.ControlBox = false;
			this.Controls.Add(this._helpButton);
			this.Controls.Add(this._cancelButton);
			this.Controls.Add(this.settingsProtectionLauncherButton1);
			this.Controls.Add(this._restartReminder);
			this.Controls.Add(this._okButton);
			this.Controls.Add(this._tab);

			this._L10NSharpExtender.SetLocalizableToolTip(this, null);
			this._L10NSharpExtender.SetLocalizationComment(this, null);
			this._L10NSharpExtender.SetLocalizingId(this, "CollectionSettingsDialog.CollectionSettingsWindowTitle");
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "CollectionSettingsDialog";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Settings";
			this.Load += new System.EventHandler(this.OnLoad);
			this._tab.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.tabPage1.PerformLayout();
			this.tabPage2.ResumeLayout(false);
			this.tabPage2.PerformLayout();
			this.tabPage3.ResumeLayout(false);
			this.tabPage3.PerformLayout();
			this.tabPage4.ResumeLayout(false);
			this.tabPage4.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this._L10NSharpExtender)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TabControl _tab;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.TabPage tabPage3;
		private System.Windows.Forms.Button _okButton;
		protected System.Windows.Forms.Label _language1Label;
		private System.Windows.Forms.LinkLabel _changeLanguage3Link;
		private System.Windows.Forms.LinkLabel _changeLanguage2Link;
		private System.Windows.Forms.LinkLabel _changeLanguage1Link;
		protected System.Windows.Forms.Label _language3Name;
		protected System.Windows.Forms.Label _language3Label;
		protected System.Windows.Forms.Label _language2Name;
        protected System.Windows.Forms.Label _language2Label;
		private System.Windows.Forms.Label _xmatterPackLabel;
		private System.Windows.Forms.ComboBox _xmatterPackCombo;
		private System.Windows.Forms.TextBox _districtText;
		private System.Windows.Forms.TextBox _provinceText;
		private System.Windows.Forms.TextBox _countryText;
		private System.Windows.Forms.Label _countryLabel;
		private System.Windows.Forms.Label _districtLabel;
		private System.Windows.Forms.Label _provinceLabel;
		private System.Windows.Forms.LinkLabel _removeLanguage3Link;
		private System.Windows.Forms.Label _restartReminder;
		private Palaso.UI.WindowsForms.SettingProtection.SettingsProtectionLauncherButton settingsProtectionLauncherButton1;
		private L10NSharp.UI.L10NSharpExtender _L10NSharpExtender;
		private System.Windows.Forms.TabPage tabPage4;
		private System.Windows.Forms.CheckBox _useImageServer;
		private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.CheckBox _showSendReceive;
        protected System.Windows.Forms.Label _language1Name;
        private System.Windows.Forms.TextBox _bloomCollectionName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button _cancelButton;
        private System.Windows.Forms.Label _language1FontLabel;
        private System.Windows.Forms.ComboBox _fontComboLanguage1;
		private System.Windows.Forms.Label _language2FontLabel;
		private System.Windows.Forms.ComboBox _fontComboLanguage2;
		private System.Windows.Forms.Label _language3FontLabel;
		private System.Windows.Forms.ComboBox _fontComboLanguage3;
        private System.Windows.Forms.CheckBox _showExperimentalTemplates;
		private System.Windows.Forms.CheckBox _showExperimentCommands;
		private Button _helpButton;
		private CheckBox _rtlLanguage3CheckBox;
		private CheckBox _rtlLanguage2CheckBox;
		private CheckBox _rtlLanguage1CheckBox;
	}
}