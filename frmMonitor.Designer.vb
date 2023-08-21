<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMonitor
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.tbcMon = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.dgvCreditApp = New System.Windows.Forms.DataGridView()
        Me.Column4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column7 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column5 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column8 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column6 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.tbcMon.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        CType(Me.dgvCreditApp, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'tbcMon
        '
        Me.tbcMon.Controls.Add(Me.TabPage1)
        Me.tbcMon.Location = New System.Drawing.Point(6, 40)
        Me.tbcMon.Name = "tbcMon"
        Me.tbcMon.SelectedIndex = 0
        Me.tbcMon.Size = New System.Drawing.Size(445, 316)
        Me.tbcMon.TabIndex = 2
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.dgvCreditApp)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(437, 290)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Credit Online Application"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'dgvCreditApp
        '
        Me.dgvCreditApp.AllowUserToAddRows = False
        Me.dgvCreditApp.AllowUserToDeleteRows = False
        Me.dgvCreditApp.AllowUserToResizeColumns = False
        Me.dgvCreditApp.AllowUserToResizeRows = False
        Me.dgvCreditApp.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.dgvCreditApp.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column4, Me.Column7, Me.Column5, Me.Column1, Me.Column8, Me.Column2, Me.Column3, Me.Column6})
        Me.dgvCreditApp.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.dgvCreditApp.Location = New System.Drawing.Point(0, 6)
        Me.dgvCreditApp.Name = "dgvCreditApp"
        Me.dgvCreditApp.ReadOnly = True
        Me.dgvCreditApp.RowHeadersVisible = False
        Me.dgvCreditApp.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvCreditApp.ShowEditingIcon = False
        Me.dgvCreditApp.Size = New System.Drawing.Size(436, 278)
        Me.dgvCreditApp.TabIndex = 1
        '
        'Column4
        '
        Me.Column4.HeaderText = "No."
        Me.Column4.Name = "Column4"
        Me.Column4.ReadOnly = True
        Me.Column4.Width = 40
        '
        'Column7
        '
        Me.Column7.HeaderText = "Area"
        Me.Column7.Name = "Column7"
        Me.Column7.ReadOnly = True
        '
        'Column5
        '
        Me.Column5.HeaderText = "Branch"
        Me.Column5.Name = "Column5"
        Me.Column5.ReadOnly = True
        '
        'Column1
        '
        Me.Column1.HeaderText = "QM Result"
        Me.Column1.Name = "Column1"
        Me.Column1.ReadOnly = True
        Me.Column1.Width = 90
        '
        'Column8
        '
        Me.Column8.HeaderText = "Date Received"
        Me.Column8.Name = "Column8"
        Me.Column8.ReadOnly = True
        '
        'Column2
        '
        Me.Column2.HeaderText = "Client Name"
        Me.Column2.Name = "Column2"
        Me.Column2.ReadOnly = True
        Me.Column2.Width = 270
        '
        'Column3
        '
        Me.Column3.HeaderText = "Date"
        Me.Column3.Name = "Column3"
        Me.Column3.ReadOnly = True
        '
        'Column6
        '
        Me.Column6.HeaderText = "Transaction No"
        Me.Column6.Name = "Column6"
        Me.Column6.ReadOnly = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(2, 8)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(120, 20)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Reqt Transfer"
        '
        'frmMonitor
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.ClientSize = New System.Drawing.Size(457, 361)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.tbcMon)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "frmMonitor"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.tbcMon.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        CType(Me.dgvCreditApp, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents tbcMon As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents dgvCreditApp As System.Windows.Forms.DataGridView
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Column4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column7 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column5 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column8 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column6 As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
