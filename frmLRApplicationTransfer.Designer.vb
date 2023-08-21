<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmLRApplicationTransfer
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
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.txtSeeks00 = New System.Windows.Forms.TextBox()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.cmdButton03 = New System.Windows.Forms.Button()
        Me.cmdButton00 = New System.Windows.Forms.Button()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtSeeks01 = New System.Windows.Forms.TextBox()
        Me.dgView = New System.Windows.Forms.DataGridView()
        Me.colField01 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colField02 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colField03 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colField04 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colField05 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colField06 = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.Panel2.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.dgView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel2
        '
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel2.Controls.Add(Me.Label11)
        Me.Panel2.Controls.Add(Me.txtSeeks00)
        Me.Panel2.Location = New System.Drawing.Point(12, 12)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(712, 35)
        Me.Panel2.TabIndex = 2
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(19, 10)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(85, 13)
        Me.Label11.TabIndex = 126
        Me.Label11.Text = "Customer Name:"
        '
        'txtSeeks00
        '
        Me.txtSeeks00.BackColor = System.Drawing.SystemColors.Window
        Me.txtSeeks00.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSeeks00.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSeeks00.Location = New System.Drawing.Point(110, 6)
        Me.txtSeeks00.MaxLength = 0
        Me.txtSeeks00.Name = "txtSeeks00"
        Me.txtSeeks00.Size = New System.Drawing.Size(292, 20)
        Me.txtSeeks00.TabIndex = 0
        '
        'Panel4
        '
        Me.Panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel4.Controls.Add(Me.cmdButton03)
        Me.Panel4.Controls.Add(Me.cmdButton00)
        Me.Panel4.Location = New System.Drawing.Point(730, 12)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(114, 295)
        Me.Panel4.TabIndex = 68
        '
        'cmdButton03
        '
        Me.cmdButton03.Image = Global.Lender.My.Resources.Resources.save
        Me.cmdButton03.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdButton03.Location = New System.Drawing.Point(5, 3)
        Me.cmdButton03.Name = "cmdButton03"
        Me.cmdButton03.Size = New System.Drawing.Size(102, 38)
        Me.cmdButton03.TabIndex = 23
        Me.cmdButton03.TabStop = False
        Me.cmdButton03.Text = "   &Save"
        Me.cmdButton03.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.cmdButton03.UseVisualStyleBackColor = True
        '
        'cmdButton00
        '
        Me.cmdButton00.Image = Global.Lender.My.Resources.Resources.cancel_update
        Me.cmdButton00.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdButton00.Location = New System.Drawing.Point(5, 41)
        Me.cmdButton00.Name = "cmdButton00"
        Me.cmdButton00.Size = New System.Drawing.Size(102, 38)
        Me.cmdButton00.TabIndex = 28
        Me.cmdButton00.TabStop = False
        Me.cmdButton00.Text = "   Close"
        Me.cmdButton00.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.cmdButton00.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.txtSeeks01)
        Me.Panel1.Location = New System.Drawing.Point(12, 49)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(712, 35)
        Me.Panel1.TabIndex = 127
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(4, 10)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(100, 13)
        Me.Label1.TabIndex = 126
        Me.Label1.Text = "Destination Branch:"
        '
        'txtSeeks01
        '
        Me.txtSeeks01.BackColor = System.Drawing.SystemColors.Window
        Me.txtSeeks01.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSeeks01.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSeeks01.Location = New System.Drawing.Point(110, 6)
        Me.txtSeeks01.MaxLength = 0
        Me.txtSeeks01.Name = "txtSeeks01"
        Me.txtSeeks01.Size = New System.Drawing.Size(292, 20)
        Me.txtSeeks01.TabIndex = 0
        '
        'dgView
        '
        Me.dgView.AllowUserToAddRows = False
        Me.dgView.AllowUserToDeleteRows = False
        Me.dgView.AllowUserToResizeColumns = False
        Me.dgView.AllowUserToResizeRows = False
        Me.dgView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.dgView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colField01, Me.colField02, Me.colField03, Me.colField04, Me.colField05, Me.colField06})
        Me.dgView.Location = New System.Drawing.Point(12, 87)
        Me.dgView.MultiSelect = False
        Me.dgView.Name = "dgView"
        Me.dgView.RowHeadersVisible = False
        Me.dgView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgView.ShowEditingIcon = False
        Me.dgView.Size = New System.Drawing.Size(712, 220)
        Me.dgView.TabIndex = 128
        '
        'colField01
        '
        Me.colField01.HeaderText = "Branch"
        Me.colField01.Name = "colField01"
        Me.colField01.ReadOnly = True
        Me.colField01.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.colField01.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.colField01.Width = 170
        '
        'colField02
        '
        Me.colField02.FillWeight = 250.0!
        Me.colField02.HeaderText = "Date"
        Me.colField02.Name = "colField02"
        Me.colField02.ReadOnly = True
        Me.colField02.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.colField02.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.colField02.Width = 80
        '
        'colField03
        '
        Me.colField03.FillWeight = 250.0!
        Me.colField03.HeaderText = "Name"
        Me.colField03.Name = "colField03"
        Me.colField03.ReadOnly = True
        Me.colField03.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.colField03.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.colField03.Width = 200
        '
        'colField04
        '
        Me.colField04.FillWeight = 90.0!
        Me.colField04.HeaderText = "QM Number"
        Me.colField04.Name = "colField04"
        Me.colField04.ReadOnly = True
        Me.colField04.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.colField04.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.colField04.Width = 78
        '
        'colField05
        '
        Me.colField05.HeaderText = "GOCAS No."
        Me.colField05.Name = "colField05"
        Me.colField05.ReadOnly = True
        Me.colField05.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.colField05.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.colField05.Width = 130
        '
        'colField06
        '
        Me.colField06.HeaderText = "Select"
        Me.colField06.Name = "colField06"
        Me.colField06.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.colField06.Width = 50
        '
        'frmLRApplicationTransfer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(856, 319)
        Me.Controls.Add(Me.dgView)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Panel2)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmLRApplicationTransfer"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "LR Application - Transfer"
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.Panel4.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.dgView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents txtSeeks00 As System.Windows.Forms.TextBox
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents cmdButton00 As System.Windows.Forms.Button
    Friend WithEvents cmdButton03 As System.Windows.Forms.Button
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtSeeks01 As System.Windows.Forms.TextBox
    Friend WithEvents dgView As System.Windows.Forms.DataGridView
    Friend WithEvents colField01 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colField02 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colField03 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colField04 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colField05 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colField06 As System.Windows.Forms.DataGridViewCheckBoxColumn
End Class
