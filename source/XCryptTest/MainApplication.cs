using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Security;
using XCrypt;

namespace XCryptTest
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class MainApplication : System.Windows.Forms.Form
	{
		private System.Windows.Forms.TextBox srcText;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox encText;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.ComboBox encMethod;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Button btnEncrypt;
		private System.Windows.Forms.Button btnDecrypt;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox keyText;
        private System.Windows.Forms.TextBox decText;
        private IContainer components;

		XCryptEngine xe = new XCryptEngine();
        private ToolTip toolTipControl;
        private CheckBox chkClipPaste;
        XCryptEngine.AlgorithmType enumChosenAlgorithm = XCryptEngine.AlgorithmType.None;

		public MainApplication()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

            srcText.Text = Environment.UserName.Length > 0 ? Environment.UserName : "Hello World";
            ToggleStateOfButtons();

            toolTipControl.ShowAlways = true;
            toolTipControl.InitialDelay = 1000;

            LoadEnumMembers();
		}

        private void LoadEnumMembers()
        {
            string[] arrEnumTypes = System.Enum.GetNames(enumChosenAlgorithm.GetType()); ;

            foreach (string t in arrEnumTypes)
            {
                if (!encMethod.Items.Contains(t))
                    encMethod.Items.Add(t);
            }
        }

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            this.srcText = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.encText = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.decText = new System.Windows.Forms.TextBox();
            this.encMethod = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnEncrypt = new System.Windows.Forms.Button();
            this.btnDecrypt = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.keyText = new System.Windows.Forms.TextBox();
            this.toolTipControl = new System.Windows.Forms.ToolTip(this.components);
            this.chkClipPaste = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // srcText
            // 
            this.srcText.Location = new System.Drawing.Point(120, 16);
            this.srcText.Name = "srcText";
            this.srcText.Size = new System.Drawing.Size(312, 20);
            this.srcText.TabIndex = 0;
            this.srcText.TextChanged += new System.EventHandler(this.srcText_TextChanged);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(16, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 23);
            this.label1.TabIndex = 1;
            this.label1.Text = "Source Text: ";
            // 
            // encText
            // 
            this.encText.BackColor = System.Drawing.SystemColors.Window;
            this.encText.ForeColor = System.Drawing.SystemColors.WindowText;
            this.encText.Location = new System.Drawing.Point(120, 56);
            this.encText.Multiline = true;
            this.encText.Name = "encText";
            this.encText.ReadOnly = true;
            this.encText.Size = new System.Drawing.Size(312, 64);
            this.encText.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(16, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 56);
            this.label2.TabIndex = 3;
            this.label2.Text = "Encrypted Text: (The encrypted text is in Base64 format)";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(16, 144);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 23);
            this.label3.TabIndex = 4;
            this.label3.Text = "Decrypted Text: ";
            // 
            // decText
            // 
            this.decText.BackColor = System.Drawing.SystemColors.Window;
            this.decText.Location = new System.Drawing.Point(120, 144);
            this.decText.Name = "decText";
            this.decText.ReadOnly = true;
            this.decText.Size = new System.Drawing.Size(312, 20);
            this.decText.TabIndex = 5;
            this.decText.TextChanged += new System.EventHandler(this.decText_TextChanged);
            // 
            // encMethod
            // 
            this.encMethod.Items.AddRange(new object[] {
            "3DES",
            "Blake256",
            "Blake512",
            "BlowFish",
            "DES",
            "MD5",
            "RC2",
            "Rijndael",
            "SHA",
            "SHA256",
            "SHA384",
            "SHA512",
            "TwoFish"});
            this.encMethod.Location = new System.Drawing.Point(120, 184);
            this.encMethod.Name = "encMethod";
            this.encMethod.Size = new System.Drawing.Size(104, 21);
            this.encMethod.Sorted = true;
            this.encMethod.TabIndex = 6;
            this.encMethod.SelectedIndexChanged += new System.EventHandler(this.encMethod_SelectedIndexChanged);
            this.encMethod.TextChanged += new System.EventHandler(this.encMethod_TextChanged);
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(16, 184);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(88, 23);
            this.label5.TabIndex = 7;
            this.label5.Text = "Algorithm: ";
            // 
            // btnEncrypt
            // 
            this.btnEncrypt.Enabled = false;
            this.btnEncrypt.Location = new System.Drawing.Point(19, 224);
            this.btnEncrypt.Name = "btnEncrypt";
            this.btnEncrypt.Size = new System.Drawing.Size(75, 23);
            this.btnEncrypt.TabIndex = 8;
            this.btnEncrypt.Text = "Encrypt";
            this.btnEncrypt.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnDecrypt
            // 
            this.btnDecrypt.Enabled = false;
            this.btnDecrypt.Location = new System.Drawing.Point(120, 224);
            this.btnDecrypt.Name = "btnDecrypt";
            this.btnDecrypt.Size = new System.Drawing.Size(75, 23);
            this.btnDecrypt.TabIndex = 9;
            this.btnDecrypt.Text = "Decrypt";
            this.btnDecrypt.Click += new System.EventHandler(this.button2_Click);
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(248, 184);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(56, 23);
            this.label6.TabIndex = 10;
            this.label6.Text = "Key: ";
            // 
            // keyText
            // 
            this.keyText.Location = new System.Drawing.Point(304, 184);
            this.keyText.Name = "keyText";
            this.keyText.Size = new System.Drawing.Size(128, 20);
            this.keyText.TabIndex = 11;
            // 
            // chkClipPaste
            // 
            this.chkClipPaste.AutoSize = true;
            this.chkClipPaste.Checked = true;
            this.chkClipPaste.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkClipPaste.Location = new System.Drawing.Point(213, 224);
            this.chkClipPaste.Name = "chkClipPaste";
            this.chkClipPaste.Size = new System.Drawing.Size(142, 17);
            this.chkClipPaste.TabIndex = 12;
            this.chkClipPaste.Text = "&Copy output to Clipboard";
            this.chkClipPaste.UseVisualStyleBackColor = true;
            this.chkClipPaste.CheckedChanged += new System.EventHandler(this.chkClipPaste_CheckedChanged);
            // 
            // MainApplication
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(464, 261);
            this.Controls.Add(this.chkClipPaste);
            this.Controls.Add(this.keyText);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btnDecrypt);
            this.Controls.Add(this.btnEncrypt);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.encMethod);
            this.Controls.Add(this.decText);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.encText);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.srcText);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainApplication";
            this.Text = "XCrypt Test Application";
            this.Load += new System.EventHandler(this.MainApplication_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

        
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new MainApplication());
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			this.decText.Text = "";

            InitializeEncryptionEngine();

			if( xe.IsHashAlgorithm )
				this.btnDecrypt.Enabled = false;
			else if( xe.IsSymmetricAlgorithm )
				this.btnDecrypt.Enabled = true;

			xe.Key = this.keyText.Text;
			this.encText.Text = xe.Encrypt(this.srcText.Text);

            ToggleStateOfButtons();

            if (chkClipPaste.Checked)
            {
                Clipboard.SetData(DataFormats.Text, this.encText.Text);
            }
		}

		private void button2_Click(object sender, System.EventArgs e)
		{
			if( this.encText.Text == null || this.encText.Text.Length == 0 ) 
			{
				MessageBox.Show("No Encrypted Text Found","Error");
				return;
			}

			this.decText.Text = "";

            InitializeEncryptionEngine();

			xe.Key = this.keyText.Text; 
			this.decText.Text = xe.Decrypt(this.encText.Text);

            if (chkClipPaste.Checked)
            {
                Clipboard.SetData(DataFormats.Text, this.encText.Text);
            }
		}

        private void InitializeEncryptionEngine()
        {
            switch (this.encMethod.Text)
            {
                case "3DES":
                    enumChosenAlgorithm = XCryptEngine.AlgorithmType.TripleDES;
                    xe.InitializeEngine(XCryptEngine.AlgorithmType.TripleDES);
                    break;
                case "BlowFish":
                    enumChosenAlgorithm = XCryptEngine.AlgorithmType.BlowFish;
                    xe.InitializeEngine(XCryptEngine.AlgorithmType.BlowFish);
                    break;
                case "TwoFish":
                    enumChosenAlgorithm = XCryptEngine.AlgorithmType.Twofish;
                    xe.InitializeEngine(XCryptEngine.AlgorithmType.Twofish);
                    break;
                case "DES":
                    enumChosenAlgorithm = XCryptEngine.AlgorithmType.DES;
                    xe.InitializeEngine(XCryptEngine.AlgorithmType.DES);
                    break;
                case "MD5":
                    enumChosenAlgorithm = XCryptEngine.AlgorithmType.MD5;
                    xe.InitializeEngine(XCryptEngine.AlgorithmType.MD5);
                    break;
                case "RC2":
                    enumChosenAlgorithm = XCryptEngine.AlgorithmType.RC2;
                    xe.InitializeEngine(XCryptEngine.AlgorithmType.RC2);
                    break;
                case "Rijndael":
                    enumChosenAlgorithm = XCryptEngine.AlgorithmType.Rijndael;
                    xe.InitializeEngine(XCryptEngine.AlgorithmType.Rijndael);
                    break;
                case "SHA":
                    enumChosenAlgorithm = XCryptEngine.AlgorithmType.SHA;
                    xe.InitializeEngine(XCryptEngine.AlgorithmType.SHA);
                    break;
                case "SHA256":
                    enumChosenAlgorithm = XCryptEngine.AlgorithmType.SHA256;
                    xe.InitializeEngine(XCryptEngine.AlgorithmType.SHA256);
                    break;
                case "SHA384":
                    enumChosenAlgorithm = XCryptEngine.AlgorithmType.SHA384;
                    xe.InitializeEngine(XCryptEngine.AlgorithmType.SHA384);
                    break;
                case "SHA512":
                    enumChosenAlgorithm = XCryptEngine.AlgorithmType.SHA512;
                    xe.InitializeEngine(XCryptEngine.AlgorithmType.SHA512);
                    break;
                case "Blake256":
                    enumChosenAlgorithm = XCryptEngine.AlgorithmType.Blake256;
                    xe.InitializeEngine(XCryptEngine.AlgorithmType.Blake256);
                    break;
                case "Blake512":
                    enumChosenAlgorithm = XCryptEngine.AlgorithmType.Blake512;
                    xe.InitializeEngine(XCryptEngine.AlgorithmType.Blake512);
                    break;
            }
        }

		private void encMethod_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			this.encText.Text = "";
			this.decText.Text = "";
			this.keyText.Text = "";

            ToggleStateOfButtons();
		}

        void encMethod_TextChanged(object sender, EventArgs e)
        {
            encMethod_SelectedIndexChanged(sender, e);
        }

        private void srcText_TextChanged(object sender, EventArgs e)
        {
            ToggleStateOfButtons();
        }

        private void decText_TextChanged(object sender, EventArgs e)
        {
            ToggleStateOfButtons();
        }

        private void ToggleStateOfButtons()
        {
            btnEncrypt.Enabled = (srcText.Text.Trim().Length > 0) && (encMethod.Text.Trim().Length > 0);

            if (encText.Text.Trim().Length < 1)
            {
                btnDecrypt.Enabled = false;
                toolTipControl.SetToolTip(btnDecrypt, "There is nothing to decrypt.");
            }
            else if (encMethod.Text.Trim().Length < 1)
            {
                btnDecrypt.Enabled = false;
                toolTipControl.SetToolTip(btnDecrypt, "No valid decryption algorithm chosen to decrypt.");
            }
            else if (!xe.IsSymmetric(enumChosenAlgorithm))
            {
                btnDecrypt.Enabled = false;
                toolTipControl.SetToolTip(btnDecrypt, "The chosen algorithm is not symmetric and hence decryption is infeasible.");
            }
            else
            {
                btnDecrypt.Enabled = true;
                toolTipControl.SetToolTip(btnDecrypt, string.Empty);
            }
        }

        private void MainApplication_Load(object sender, EventArgs e)
        {

        }

        private void chkClipPaste_CheckedChanged(object sender, EventArgs e)
        {

        }
	}
}
