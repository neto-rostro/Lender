<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCarColor
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
        Me.txtField01 = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtField00 = New System.Windows.Forms.TextBox()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.chbactive = New System.Windows.Forms.CheckBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lblColorName = New System.Windows.Forms.Label()
        Me.lblColorId = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.cmdButton04 = New System.Windows.Forms.Button()
        Me.cmdButton07 = New System.Windows.Forms.Button()
        Me.cmdButton08 = New System.Windows.Forms.Button()
        Me.cmdButton05 = New System.Windows.Forms.Button()
        Me.cmdButton02 = New System.Windows.Forms.Button()
        Me.cmdButton06 = New System.Windows.Forms.Button()
        Me.cmdButton01 = New System.Windows.Forms.Button()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.txtField01)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.txtField00)
        Me.GroupBox1.Controls.Add(Me.Panel3)
        Me.GroupBox1.Controls.Add(Me.chbactive)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.lblColorName)
        Me.GroupBox1.Controls.Add(Me.lblColorId)
        Me.GroupBox1.Location = New System.Drawing.Point(9, 1)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(449, 147)
        Me.GroupBox1.TabIndex = 9
        Me.GroupBox1.TabStop = False
        '
        'txtField01
        '
        Me.txtField01.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtField01.Location = New System.Drawing.Point(102, 64)
        Me.txtField01.Name = "txtField01"
        Me.txtField01.Size = New System.Drawing.Size(224, 22)
        Me.txtField01.TabIndex = 13
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(327, 61)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(18, 24)
        Me.Label2.TabIndex = 15
        Me.Label2.Text = "*"
        '
        'txtField00
        '
        Me.txtField00.BackColor = System.Drawing.SystemColors.Control
        Me.txtField00.Cursor = System.Windows.Forms.Cursors.No
        Me.txtField00.Enabled = False
        Me.txtField00.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtField00.Location = New System.Drawing.Point(102, 19)
        Me.txtField00.Name = "txtField00"
        Me.txtField00.ReadOnly = True
        Me.txtField00.Size = New System.Drawing.Size(138, 22)
        Me.txtField00.TabIndex = 12
        Me.txtField00.TabStop = False
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Panel3.Location = New System.Drawing.Point(103, 20)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(142, 24)
        Me.Panel3.TabIndex = 14
        '
        'chbactive
        '
        Me.chbactive.AutoSize = True
        Me.chbactive.Checked = True
        Me.chbactive.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chbactive.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chbactive.Location = New System.Drawing.Point(105, 105)
        Me.chbactive.Name = "chbactive"
        Me.chbactive.Size = New System.Drawing.Size(64, 20)
        Me.chbactive.TabIndex = 11
        Me.chbactive.Text = "Active"
        Me.chbactive.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(9, 106)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(96, 16)
        Me.Label3.TabIndex = 10
        Me.Label3.Text = "Record Status:"
        '
        'lblColorName
        '
        Me.lblColorName.AutoSize = True
        Me.lblColorName.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblColorName.Location = New System.Drawing.Point(6, 65)
        Me.lblColorName.Name = "lblColorName"
        Me.lblColorName.Size = New System.Drawing.Size(83, 16)
        Me.lblColorName.TabIndex = 9
        Me.lblColorName.Text = "Color Name:"
        '
        'lblColorId
        '
        Me.lblColorId.AutoSize = True
        Me.lblColorId.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblColorId.Location = New System.Drawing.Point(6, 19)
        Me.lblColorId.Name = "lblColorId"
        Me.lblColorId.Size = New System.Drawing.Size(59, 16)
        Me.lblColorId.TabIndex = 8
        Me.lblColorId.Text = "Color ID:"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.cmdButton04)
        Me.GroupBox2.Controls.Add(Me.cmdButton07)
        Me.GroupBox2.Controls.Add(Me.cmdButton08)
        Me.GroupBox2.Controls.Add(Me.cmdButton05)
        Me.GroupBox2.Controls.Add(Me.cmdButton02)
        Me.GroupBox2.Controls.Add(Me.cmdButton06)
        Me.GroupBox2.Controls.Add(Me.cmdButton01)
        Me.GroupBox2.Location = New System.Drawing.Point(7, 151)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(449, 69)
        Me.GroupBox2.TabIndex = 15
        Me.GroupBox2.TabStop = False
        '
        'cmdButton04
        '
        Me.cmdButton04.Image = My.Resources.Resources.browse
        Me.cmdButton04.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdButton04.Location = New System.Drawing.Point(333, 12)
        Me.cmdButton04.Name = "cmdButton04"
        Me.cmdButton04.Size = New System.Drawing.Size(53, 53)
        Me.cmdButton04.TabIndex = 3
        Me.cmdButton04.Text = "Browse"
        Me.cmdButton04.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdButton04.UseVisualStyleBackColor = True
        '
        'cmdButton07
        '
        Me.cmdButton07.Enabled = False
        Me.cmdButton07.Image = My.Resources.Resources.cancel_update
        Me.cmdButton07.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdButton07.Location = New System.Drawing.Point(275, 12)
        Me.cmdButton07.Name = "cmdButton07"
        Me.cmdButton07.Size = New System.Drawing.Size(53, 53)
        Me.cmdButton07.TabIndex = 6
        Me.cmdButton07.Text = "Delete"
        Me.cmdButton07.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdButton07.UseVisualStyleBackColor = True
        '
        'cmdButton08
        '
        Me.cmdButton08.Image = My.Resources.Resources._exit
        Me.cmdButton08.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdButton08.Location = New System.Drawing.Point(389, 12)
        Me.cmdButton08.Name = "cmdButton08"
        Me.cmdButton08.Size = New System.Drawing.Size(53, 53)
        Me.cmdButton08.TabIndex = 7
        Me.cmdButton08.Text = "Close"
        Me.cmdButton08.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdButton08.UseVisualStyleBackColor = True
        '
        'cmdButton05
        '
        Me.cmdButton05.Image = My.Resources.Resources.cancel
        Me.cmdButton05.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdButton05.Location = New System.Drawing.Point(390, 12)
        Me.cmdButton05.Name = "cmdButton05"
        Me.cmdButton05.Size = New System.Drawing.Size(53, 53)
        Me.cmdButton05.TabIndex = 4
        Me.cmdButton05.Text = "Cancel"
        Me.cmdButton05.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdButton05.UseVisualStyleBackColor = True
        '
        'cmdButton02
        '
        Me.cmdButton02.Image = My.Resources.Resources.save
        Me.cmdButton02.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdButton02.Location = New System.Drawing.Point(274, 11)
        Me.cmdButton02.Name = "cmdButton02"
        Me.cmdButton02.Size = New System.Drawing.Size(53, 53)
        Me.cmdButton02.TabIndex = 1
        Me.cmdButton02.Text = "Save"
        Me.cmdButton02.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdButton02.UseVisualStyleBackColor = True
        '
        'cmdButton06
        '
        Me.cmdButton06.Enabled = False
        Me.cmdButton06.Image = My.Resources.Resources.update
        Me.cmdButton06.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdButton06.Location = New System.Drawing.Point(218, 12)
        Me.cmdButton06.Name = "cmdButton06"
        Me.cmdButton06.Size = New System.Drawing.Size(53, 53)
        Me.cmdButton06.TabIndex = 5
        Me.cmdButton06.Text = "Update"
        Me.cmdButton06.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdButton06.UseVisualStyleBackColor = True
        '
        'cmdButton01
        '
        Me.cmdButton01.Image = My.Resources.Resources.add_item
        Me.cmdButton01.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdButton01.Location = New System.Drawing.Point(159, 12)
        Me.cmdButton01.Name = "cmdButton01"
        Me.cmdButton01.Size = New System.Drawing.Size(53, 53)
        Me.cmdButton01.TabIndex = 0
        Me.cmdButton01.Text = "New"
        Me.cmdButton01.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdButton01.UseVisualStyleBackColor = True
        '
        'frmCarColor
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(462, 223)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "frmCarColor"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Car Color"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents txtField00 As System.Windows.Forms.TextBox
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents txtField01 As System.Windows.Forms.TextBox
    Friend WithEvents chbactive As System.Windows.Forms.CheckBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents lblColorName As System.Windows.Forms.Label
    Friend WithEvents lblColorId As System.Windows.Forms.Label
    Friend WithEvents cmdButton01 As System.Windows.Forms.Button
    Friend WithEvents cmdButton06 As System.Windows.Forms.Button
    Friend WithEvents cmdButton02 As System.Windows.Forms.Button
    Friend WithEvents cmdButton05 As System.Windows.Forms.Button
    Friend WithEvents cmdButton08 As System.Windows.Forms.Button
    Friend WithEvents cmdButton07 As System.Windows.Forms.Button
    Friend WithEvents cmdButton04 As System.Windows.Forms.Button
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
End Class
