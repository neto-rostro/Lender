<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCarSerialRegistration
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
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.dgView = New System.Windows.Forms.DataGridView()
        Me.colField01 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colField02 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colField03 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colField04 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colField05 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.txtField08 = New System.Windows.Forms.TextBox()
        Me.txtField13 = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.txtField12 = New System.Windows.Forms.TextBox()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.txtField09 = New System.Windows.Forms.TextBox()
        Me.txtField11 = New System.Windows.Forms.TextBox()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.txtField10 = New System.Windows.Forms.TextBox()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtField02 = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.txtField07 = New System.Windows.Forms.TextBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.txtField06 = New System.Windows.Forms.TextBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.txtField05 = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtField04 = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtField03 = New System.Windows.Forms.TextBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtField01 = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtField00 = New System.Windows.Forms.TextBox()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.cmdButton00 = New System.Windows.Forms.Button()
        Me.cmdbutton5 = New System.Windows.Forms.Button()
        Me.cmdButton01 = New System.Windows.Forms.Button()
        Me.GroupBox1.SuspendLayout()
        Me.Panel5.SuspendLayout()
        CType(Me.dgView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel3.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Panel5)
        Me.GroupBox1.Controls.Add(Me.Panel3)
        Me.GroupBox1.Controls.Add(Me.Panel1)
        Me.GroupBox1.Location = New System.Drawing.Point(112, -4)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(578, 457)
        Me.GroupBox1.TabIndex = 63
        Me.GroupBox1.TabStop = False
        '
        'Panel5
        '
        Me.Panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel5.Controls.Add(Me.dgView)
        Me.Panel5.Location = New System.Drawing.Point(10, 227)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(564, 224)
        Me.Panel5.TabIndex = 11
        '
        'dgView
        '
        Me.dgView.AllowUserToAddRows = False
        Me.dgView.AllowUserToDeleteRows = False
        Me.dgView.AllowUserToResizeColumns = False
        Me.dgView.AllowUserToResizeRows = False
        Me.dgView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colField01, Me.colField02, Me.colField03, Me.colField04, Me.colField05})
        Me.dgView.Location = New System.Drawing.Point(4, 6)
        Me.dgView.Name = "dgView"
        Me.dgView.ReadOnly = True
        Me.dgView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.dgView.ShowEditingIcon = False
        Me.dgView.Size = New System.Drawing.Size(553, 213)
        Me.dgView.TabIndex = 6
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
        'Panel3
        '
        Me.Panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel3.Controls.Add(Me.Label16)
        Me.Panel3.Controls.Add(Me.txtField08)
        Me.Panel3.Controls.Add(Me.txtField13)
        Me.Panel3.Controls.Add(Me.Label12)
        Me.Panel3.Controls.Add(Me.Label21)
        Me.Panel3.Controls.Add(Me.txtField12)
        Me.Panel3.Controls.Add(Me.Label17)
        Me.Panel3.Controls.Add(Me.txtField09)
        Me.Panel3.Controls.Add(Me.txtField11)
        Me.Panel3.Controls.Add(Me.Label24)
        Me.Panel3.Controls.Add(Me.txtField10)
        Me.Panel3.Controls.Add(Me.Label23)
        Me.Panel3.Controls.Add(Me.Label4)
        Me.Panel3.Controls.Add(Me.txtField02)
        Me.Panel3.Controls.Add(Me.Label11)
        Me.Panel3.Controls.Add(Me.txtField07)
        Me.Panel3.Controls.Add(Me.Label15)
        Me.Panel3.Controls.Add(Me.txtField06)
        Me.Panel3.Controls.Add(Me.Label14)
        Me.Panel3.Controls.Add(Me.txtField05)
        Me.Panel3.Controls.Add(Me.Label8)
        Me.Panel3.Controls.Add(Me.txtField04)
        Me.Panel3.Controls.Add(Me.Label9)
        Me.Panel3.Controls.Add(Me.txtField03)
        Me.Panel3.Location = New System.Drawing.Point(10, 51)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(562, 172)
        Me.Panel3.TabIndex = 10
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.Location = New System.Drawing.Point(367, 4)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(54, 16)
        Me.Label16.TabIndex = 103
        Me.Label16.Text = "File No:"
        '
        'txtField08
        '
        Me.txtField08.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtField08.Location = New System.Drawing.Point(435, 2)
        Me.txtField08.Name = "txtField08"
        Me.txtField08.Size = New System.Drawing.Size(108, 20)
        Me.txtField08.TabIndex = 92
        '
        'txtField13
        '
        Me.txtField13.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtField13.Location = New System.Drawing.Point(435, 133)
        Me.txtField13.MaxLength = 8
        Me.txtField13.Name = "txtField13"
        Me.txtField13.Size = New System.Drawing.Size(108, 20)
        Me.txtField13.TabIndex = 97
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(348, 136)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(73, 16)
        Me.Label12.TabIndex = 102
        Me.Label12.Text = "Sticker No:"
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.Location = New System.Drawing.Point(313, 107)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(108, 16)
        Me.Label21.TabIndex = 101
        Me.Label21.Text = "Registered OR#:"
        '
        'txtField12
        '
        Me.txtField12.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtField12.Location = New System.Drawing.Point(435, 105)
        Me.txtField12.MaxLength = 15
        Me.txtField12.Name = "txtField12"
        Me.txtField12.Size = New System.Drawing.Size(108, 20)
        Me.txtField12.TabIndex = 96
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.Location = New System.Drawing.Point(361, 27)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(60, 16)
        Me.Label17.TabIndex = 100
        Me.Label17.Text = "CRE No:"
        '
        'txtField09
        '
        Me.txtField09.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtField09.Location = New System.Drawing.Point(435, 26)
        Me.txtField09.MaxLength = 10
        Me.txtField09.Name = "txtField09"
        Me.txtField09.Size = New System.Drawing.Size(108, 20)
        Me.txtField09.TabIndex = 93
        '
        'txtField11
        '
        Me.txtField11.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtField11.Location = New System.Drawing.Point(435, 78)
        Me.txtField11.MaxLength = 8
        Me.txtField11.Name = "txtField11"
        Me.txtField11.Size = New System.Drawing.Size(108, 20)
        Me.txtField11.TabIndex = 95
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.Location = New System.Drawing.Point(347, 55)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(74, 16)
        Me.Label24.TabIndex = 98
        Me.Label24.Text = "Control No:"
        '
        'txtField10
        '
        Me.txtField10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtField10.Location = New System.Drawing.Point(435, 52)
        Me.txtField10.MaxLength = 10
        Me.txtField10.Name = "txtField10"
        Me.txtField10.Size = New System.Drawing.Size(108, 20)
        Me.txtField10.TabIndex = 94
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.Location = New System.Drawing.Point(358, 80)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(63, 16)
        Me.Label23.TabIndex = 99
        Me.Label23.Text = "Plate No:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(3, 3)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(108, 16)
        Me.Label4.TabIndex = 72
        Me.Label4.Text = "Customer Name:"
        '
        'txtField02
        '
        Me.txtField02.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtField02.Location = New System.Drawing.Point(128, 2)
        Me.txtField02.MaxLength = 12
        Me.txtField02.Name = "txtField02"
        Me.txtField02.Size = New System.Drawing.Size(135, 20)
        Me.txtField02.TabIndex = 3
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(1, 135)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(108, 16)
        Me.Label11.TabIndex = 71
        Me.Label11.Text = "Registered ID#3:"
        '
        'txtField07
        '
        Me.txtField07.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtField07.Location = New System.Drawing.Point(128, 132)
        Me.txtField07.MaxLength = 12
        Me.txtField07.Name = "txtField07"
        Me.txtField07.Size = New System.Drawing.Size(135, 20)
        Me.txtField07.TabIndex = 8
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(1, 107)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(108, 16)
        Me.Label15.TabIndex = 70
        Me.Label15.Text = "Registered ID#2:"
        '
        'txtField06
        '
        Me.txtField06.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtField06.Location = New System.Drawing.Point(128, 104)
        Me.txtField06.MaxLength = 12
        Me.txtField06.Name = "txtField06"
        Me.txtField06.Size = New System.Drawing.Size(135, 20)
        Me.txtField06.TabIndex = 7
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(2, 79)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(108, 16)
        Me.Label14.TabIndex = 69
        Me.Label14.Text = "Registered ID#1:"
        '
        'txtField05
        '
        Me.txtField05.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtField05.Location = New System.Drawing.Point(128, 78)
        Me.txtField05.MaxLength = 12
        Me.txtField05.Name = "txtField05"
        Me.txtField05.Size = New System.Drawing.Size(135, 20)
        Me.txtField05.TabIndex = 6
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(26, 53)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(80, 16)
        Me.Label8.TabIndex = 68
        Me.Label8.Text = "Co Buyer#2:"
        '
        'txtField04
        '
        Me.txtField04.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtField04.Location = New System.Drawing.Point(128, 51)
        Me.txtField04.MaxLength = 12
        Me.txtField04.Name = "txtField04"
        Me.txtField04.Size = New System.Drawing.Size(135, 20)
        Me.txtField04.TabIndex = 5
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(32, 27)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(80, 16)
        Me.Label9.TabIndex = 67
        Me.Label9.Text = "Co Buyer#1:"
        '
        'txtField03
        '
        Me.txtField03.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtField03.Location = New System.Drawing.Point(128, 26)
        Me.txtField03.MaxLength = 12
        Me.txtField03.Name = "txtField03"
        Me.txtField03.Size = New System.Drawing.Size(135, 20)
        Me.txtField03.TabIndex = 4
        '
        'Panel1
        '
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Controls.Add(Me.txtField01)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.txtField00)
        Me.Panel1.Controls.Add(Me.Panel2)
        Me.Panel1.Location = New System.Drawing.Point(10, 11)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(560, 35)
        Me.Panel1.TabIndex = 0
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(292, 7)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(129, 16)
        Me.Label5.TabIndex = 61
        Me.Label5.Text = "Date of Registration:"
        '
        'txtField01
        '
        Me.txtField01.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtField01.Location = New System.Drawing.Point(435, 6)
        Me.txtField01.Name = "txtField01"
        Me.txtField01.Size = New System.Drawing.Size(108, 20)
        Me.txtField01.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(46, 8)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(62, 16)
        Me.Label1.TabIndex = 6
        Me.Label1.Text = "Serial ID:"
        '
        'txtField00
        '
        Me.txtField00.BackColor = System.Drawing.SystemColors.Control
        Me.txtField00.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtField00.Enabled = False
        Me.txtField00.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtField00.Location = New System.Drawing.Point(128, 4)
        Me.txtField00.MaxLength = 12
        Me.txtField00.Name = "txtField00"
        Me.txtField00.ReadOnly = True
        Me.txtField00.Size = New System.Drawing.Size(135, 22)
        Me.txtField00.TabIndex = 0
        Me.txtField00.TabStop = False
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.SystemColors.ControlText
        Me.Panel2.Location = New System.Drawing.Point(128, 9)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(139, 20)
        Me.Panel2.TabIndex = 7
        '
        'Panel4
        '
        Me.Panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel4.Controls.Add(Me.cmdButton00)
        Me.Panel4.Controls.Add(Me.cmdbutton5)
        Me.Panel4.Controls.Add(Me.cmdButton01)
        Me.Panel4.Location = New System.Drawing.Point(6, 7)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(101, 446)
        Me.Panel4.TabIndex = 64
        '
        'cmdButton00
        '
        Me.cmdButton00.Image = Global.Lender.My.Resources.Resources._exit
        Me.cmdButton00.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdButton00.Location = New System.Drawing.Point(3, 85)
        Me.cmdButton00.Name = "cmdButton00"
        Me.cmdButton00.Size = New System.Drawing.Size(91, 38)
        Me.cmdButton00.TabIndex = 24
        Me.cmdButton00.Text = "   Close"
        Me.cmdButton00.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.cmdButton00.UseVisualStyleBackColor = True
        '
        'cmdbutton5
        '
        Me.cmdbutton5.Image = Global.Lender.My.Resources.Resources.void
        Me.cmdbutton5.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdbutton5.Location = New System.Drawing.Point(3, 44)
        Me.cmdbutton5.Name = "cmdbutton5"
        Me.cmdbutton5.Size = New System.Drawing.Size(90, 38)
        Me.cmdbutton5.TabIndex = 65
        Me.cmdbutton5.Text = "Delete"
        Me.cmdbutton5.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.cmdbutton5.UseVisualStyleBackColor = True
        '
        'cmdButton01
        '
        Me.cmdButton01.Image = Global.Lender.My.Resources.Resources._new
        Me.cmdButton01.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdButton01.Location = New System.Drawing.Point(3, 5)
        Me.cmdButton01.Name = "cmdButton01"
        Me.cmdButton01.Size = New System.Drawing.Size(90, 38)
        Me.cmdButton01.TabIndex = 23
        Me.cmdButton01.Text = "New"
        Me.cmdButton01.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.cmdButton01.UseVisualStyleBackColor = True
        '
        'frmCarSerialRegistration
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(692, 453)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Panel4)
        Me.Name = "frmCarSerialRegistration"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Car Serial Registration"
        Me.GroupBox1.ResumeLayout(False)
        Me.Panel5.ResumeLayout(False)
        CType(Me.dgView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel4.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtField00 As System.Windows.Forms.TextBox
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents cmdButton00 As System.Windows.Forms.Button
    Friend WithEvents cmdbutton5 As System.Windows.Forms.Button
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtField02 As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents txtField07 As System.Windows.Forms.TextBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents txtField06 As System.Windows.Forms.TextBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents txtField05 As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtField04 As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtField03 As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtField01 As System.Windows.Forms.TextBox
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents txtField08 As System.Windows.Forms.TextBox
    Friend WithEvents txtField13 As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents txtField12 As System.Windows.Forms.TextBox
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents txtField09 As System.Windows.Forms.TextBox
    Friend WithEvents txtField11 As System.Windows.Forms.TextBox
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents txtField10 As System.Windows.Forms.TextBox
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents cmdButton01 As System.Windows.Forms.Button
    Friend WithEvents dgView As System.Windows.Forms.DataGridView
    Friend WithEvents colField01 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colField02 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colField03 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colField04 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colField05 As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
