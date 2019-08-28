namespace Excel2Conf
{
    partial class Main
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
            this.exportSelect = new System.Windows.Forms.Button();
            this.selectall = new System.Windows.Forms.LinkLabel();
            this.unselectall = new System.Windows.Forms.LinkLabel();
            this.designIn = new System.Windows.Forms.TextBox();
            this.serverOut = new System.Windows.Forms.TextBox();
            this.clientOut = new System.Windows.Forms.TextBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.selectDesignIn = new System.Windows.Forms.Button();
            this.selectServerOut = new System.Windows.Forms.Button();
            this.selectClientOut = new System.Windows.Forms.Button();
            this.searchText = new System.Windows.Forms.TextBox();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.SuspendLayout();
            // 
            // exportSelect
            // 
            this.exportSelect.Location = new System.Drawing.Point(638, 108);
            this.exportSelect.Name = "exportSelect";
            this.exportSelect.Size = new System.Drawing.Size(75, 23);
            this.exportSelect.TabIndex = 2;
            this.exportSelect.Text = "导出";
            this.exportSelect.UseVisualStyleBackColor = true;
            this.exportSelect.Click += new System.EventHandler(this.exportSelect_Click);
            // 
            // selectall
            // 
            this.selectall.AutoSize = true;
            this.selectall.Location = new System.Drawing.Point(14, 117);
            this.selectall.Name = "selectall";
            this.selectall.Size = new System.Drawing.Size(23, 12);
            this.selectall.TabIndex = 4;
            this.selectall.TabStop = true;
            this.selectall.Text = "all";
            this.selectall.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.selectall_LinkClicked);
            // 
            // unselectall
            // 
            this.unselectall.AutoSize = true;
            this.unselectall.Location = new System.Drawing.Point(44, 117);
            this.unselectall.Name = "unselectall";
            this.unselectall.Size = new System.Drawing.Size(29, 12);
            this.unselectall.TabIndex = 5;
            this.unselectall.TabStop = true;
            this.unselectall.Text = "none";
            this.unselectall.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.unselectall_LinkClicked);
            // 
            // designIn
            // 
            this.designIn.Location = new System.Drawing.Point(108, 12);
            this.designIn.Name = "designIn";
            this.designIn.Size = new System.Drawing.Size(512, 21);
            this.designIn.TabIndex = 6;
            this.designIn.TextChanged += new System.EventHandler(this.designIn_TextChanged);
            // 
            // serverOut
            // 
            this.serverOut.Location = new System.Drawing.Point(108, 39);
            this.serverOut.Name = "serverOut";
            this.serverOut.Size = new System.Drawing.Size(512, 21);
            this.serverOut.TabIndex = 7;
            // 
            // clientOut
            // 
            this.clientOut.Location = new System.Drawing.Point(108, 66);
            this.clientOut.Name = "clientOut";
            this.clientOut.Size = new System.Drawing.Size(512, 21);
            this.clientOut.TabIndex = 8;
            // 
            // listBox1
            // 
            this.listBox1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 12;
            this.listBox1.Location = new System.Drawing.Point(740, 41);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(535, 532);
            this.listBox1.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 10;
            this.label1.Text = "策划目录：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 11;
            this.label2.Text = "服务端目录：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 75);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 12;
            this.label3.Text = "客户端目录";
            // 
            // selectDesignIn
            // 
            this.selectDesignIn.Location = new System.Drawing.Point(636, 10);
            this.selectDesignIn.Name = "selectDesignIn";
            this.selectDesignIn.Size = new System.Drawing.Size(75, 23);
            this.selectDesignIn.TabIndex = 13;
            this.selectDesignIn.Text = "选择目录";
            this.selectDesignIn.UseVisualStyleBackColor = true;
            this.selectDesignIn.Click += new System.EventHandler(this.selectDesignIn_Click);
            // 
            // selectServerOut
            // 
            this.selectServerOut.Location = new System.Drawing.Point(636, 37);
            this.selectServerOut.Name = "selectServerOut";
            this.selectServerOut.Size = new System.Drawing.Size(75, 23);
            this.selectServerOut.TabIndex = 14;
            this.selectServerOut.Text = "选择目录";
            this.selectServerOut.UseVisualStyleBackColor = true;
            this.selectServerOut.Click += new System.EventHandler(this.selectServerOut_Click);
            // 
            // selectClientOut
            // 
            this.selectClientOut.Location = new System.Drawing.Point(636, 64);
            this.selectClientOut.Name = "selectClientOut";
            this.selectClientOut.Size = new System.Drawing.Size(75, 23);
            this.selectClientOut.TabIndex = 15;
            this.selectClientOut.Text = "选择目录";
            this.selectClientOut.UseVisualStyleBackColor = true;
            this.selectClientOut.Click += new System.EventHandler(this.selectClientOut_Click);
            // 
            // searchText
            // 
            this.searchText.Location = new System.Drawing.Point(108, 108);
            this.searchText.Name = "searchText";
            this.searchText.Size = new System.Drawing.Size(512, 21);
            this.searchText.TabIndex = 16;
            this.searchText.TextChanged += new System.EventHandler(this.searchText_TextChanged);
            // 
            // treeView1
            // 
            this.treeView1.CheckBoxes = true;
            this.treeView1.Location = new System.Drawing.Point(17, 138);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(696, 436);
            this.treeView1.TabIndex = 17;
            this.treeView1.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterCheck);
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1287, 586);
            this.Controls.Add(this.treeView1);
            this.Controls.Add(this.searchText);
            this.Controls.Add(this.selectClientOut);
            this.Controls.Add(this.selectServerOut);
            this.Controls.Add(this.selectDesignIn);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.clientOut);
            this.Controls.Add(this.serverOut);
            this.Controls.Add(this.designIn);
            this.Controls.Add(this.unselectall);
            this.Controls.Add(this.selectall);
            this.Controls.Add(this.exportSelect);
            this.Name = "Main";
            this.Text = "Main";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button exportSelect;
        private System.Windows.Forms.LinkLabel selectall;
        private System.Windows.Forms.LinkLabel unselectall;
        private System.Windows.Forms.TextBox designIn;
        private System.Windows.Forms.TextBox serverOut;
        private System.Windows.Forms.TextBox clientOut;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button selectDesignIn;
        private System.Windows.Forms.Button selectServerOut;
        private System.Windows.Forms.Button selectClientOut;
        private System.Windows.Forms.TextBox searchText;
        private System.Windows.Forms.TreeView treeView1;
    }
}

