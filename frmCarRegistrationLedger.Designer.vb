<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCarRegistrationLedger
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
        Me.dgView = New System.Windows.Forms.DataGridView()
        Me.colField01 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colField02 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colField03 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colField04 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colField05 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.cmdButton00 = New System.Windows.Forms.Button()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.txtField02 = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtField01 = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtField00 = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        CType(Me.dgView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'dgView
        '
        Me.dgView.AllowUserToAddRows = False
        Me.dgView.AllowUserToDeleteRows = False
        Me.dgView.AllowUserToResizeColumns = False
        Me.dgView.AllowUserToResizeRows = False
        Me.dgView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colField01, Me.colField02, Me.colField03, Me.colField04, Me.colField05})
        Me.dgView.Location = New System.Drawing.Point(3, 116)
        Me.dgView.Name = "dgView"
        Me.dgView.ReadOnly = True
        Me.dgView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.dgView.ShowEditingIcon = False
        Me.dgView.Size = New System.Drawing.Size(580, 320)
        Me.dgView.TabIndex = 5
        '
        'colField01
        '
        Me.colField01.FillWeight = 105.0!
        Me.colField01.HeaderText = "Date"
        Me.colField01.Name = "colField01"
        Me.colField01.ReadOnly = True
        Me.colField01.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.colField01.Width = 105
        '
        'colField02
        '
        Me.colField02.FillWeight = 105.0!
        Me.colField02.HeaderText = "File No"
        Me.colField02.Name = "colField02"
        Me.colField02.ReadOnly = True
        Me.colField02.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.colField02.Width = 105
        '
        'colField03
        '
        Me.colField03.FillWeight = 105.0!
        Me.colField03.HeaderText = "Cr No"
        Me.colField03.Name = "colField03"
        Me.colField03.ReadOnly = True
        Me.colField03.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.colField03.Width = 105
        '
        'colField04
        '
        Me.colField04.FillWeight = 105.0!
        Me.colField04.HeaderText = "OR No"
        Me.colField04.Name = "colField04"
        Me.colField04.ReadOnly = True
        Me.colField04.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.colField04.Width = 105
        '
        'colField05
        '
        Me.colField05.FillWeight = 105.0!
        Me.colField05.HeaderText = "StickerNo"
        Me.colField05.Name = "colField05"
        Me.colField05.ReadOnly = True
        Me.colField05.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.colField05.Width = 105
        '
        'Panel2
        '
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel2.Controls.Add(Me.cmdButton00)
        Me.Panel2.Controls.Add(Me.PictureBox1)
        Me.Panel2.Location = New System.Drawing.Point(464, 6)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(119, 97)
        Me.Panel2.TabIndex = 4
        '
        'cmdButton00
        '
        Me.cmdButton00.Location = New System.Drawing.Point(17, 71)
        Me.cmdButton00.Name = "cmdButton00"
        Me.cmdButton00.Size = New System.Drawing.Size(82, 23)
        Me.cmdButton00.TabIndex = 1
        Me.cmdButton00.Text = "Ok"
        Me.cmdButton00.UseVisualStyleBackColor = True
        '
        'PictureBox1
        '
        Me.PictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PictureBox1.Location = New System.Drawing.Point(17, 3)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(82, 66)
        Me.PictureBox1.TabIndex = 0
        Me.PictureBox1.TabStop = False
        '
        'Panel1
        '
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.txtField02)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.txtField01)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.txtField00)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Location = New System.Drawing.Point(3, 6)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(455, 95)
        Me.Panel1.TabIndex = 3
        '
        'txtField02
        '
        Me.txtField02.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtField02.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtField02.Location = New System.Drawing.Point(103, 59)
        Me.txtField02.Name = "txtField02"
        Me.txtField02.ReadOnly = True
        Me.txtField02.Size = New System.Drawing.Size(176, 22)
        Me.txtField02.TabIndex = 24
        Me.txtField02.TabStop = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(27, 62)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(63, 16)
        Me.Label3.TabIndex = 23
        Me.Label3.Text = "Plate No:"
        '
        'txtField01
        '
        Me.txtField01.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtField01.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtField01.Location = New System.Drawing.Point(103, 31)
        Me.txtField01.Name = "txtField01"
        Me.txtField01.ReadOnly = True
        Me.txtField01.Size = New System.Drawing.Size(176, 22)
        Me.txtField01.TabIndex = 20
        Me.txtField01.TabStop = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(9, 34)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(84, 16)
        Me.Label1.TabIndex = 19
        Me.Label1.Text = "Client Name:"
        '
        'txtField00
        '
        Me.txtField00.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtField00.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtField00.Location = New System.Drawing.Point(103, 4)
        Me.txtField00.Name = "txtField00"
        Me.txtField00.ReadOnly = True
        Me.txtField00.Size = New System.Drawing.Size(176, 22)
        Me.txtField00.TabIndex = 16
        Me.txtField00.TabStop = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(29, 9)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(62, 16)
        Me.Label4.TabIndex = 15
        Me.Label4.Text = "Serial ID:"
        '
        'frmCarRegistrationLedger
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(587, 446)
        Me.Controls.Add(Me.dgView)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "frmCarRegistrationLedger"
        Me.Text = "Car Serial Registration Ledger"
        CType(Me.dgView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents dgView As System.Windows.Forms.DataGridView
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents cmdButton00 As System.Windows.Forms.Button
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents txtField02 As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtField00 As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtField01 As System.Windows.Forms.TextBox
    Friend WithEvents colField01 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colField02 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colField03 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colField04 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colField05 As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
