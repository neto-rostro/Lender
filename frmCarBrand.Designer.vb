<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCarBrand
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
        Me.chbActive = New System.Windows.Forms.CheckBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblBrandName = New System.Windows.Forms.Label()
        Me.txtField00 = New System.Windows.Forms.TextBox()
        Me.lblBrandID = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
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
        Me.GroupBox1.Controls.Add(Me.chbActive)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.lblBrandName)
        Me.GroupBox1.Controls.Add(Me.txtField00)
        Me.GroupBox1.Controls.Add(Me.lblBrandID)
        Me.GroupBox1.Controls.Add(Me.Panel1)
        Me.GroupBox1.Location = New System.Drawing.Point(7, 2)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(449, 145)
        Me.GroupBox1.TabIndex = 6
        Me.GroupBox1.TabStop = False
        '
        'txtField01
        '
        Me.txtField01.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtField01.Location = New System.Drawing.Point(94, 67)
        Me.txtField01.Name = "txtField01"
        Me.txtField01.Size = New System.Drawing.Size(233, 22)
        Me.txtField01.TabIndex = 6
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(328, 63)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(18, 24)
        Me.Label2.TabIndex = 9
        Me.Label2.Text = "*"
        '
        'chbActive
        '
        Me.chbActive.AutoSize = True
        Me.chbActive.Checked = True
        Me.chbActive.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chbActive.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chbActive.Location = New System.Drawing.Point(103, 110)
        Me.chbActive.Name = "chbActive"
        Me.chbActive.Size = New System.Drawing.Size(64, 20)
        Me.chbActive.TabIndex = 11
        Me.chbActive.TabStop = False
        Me.chbActive.Text = "Active"
        Me.chbActive.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(1, 111)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(96, 16)
        Me.Label1.TabIndex = 7
        Me.Label1.Text = "Record Status:"
        '
        'lblBrandName
        '
        Me.lblBrandName.AutoSize = True
        Me.lblBrandName.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBrandName.Location = New System.Drawing.Point(1, 69)
        Me.lblBrandName.Name = "lblBrandName"
        Me.lblBrandName.Size = New System.Drawing.Size(87, 16)
        Me.lblBrandName.TabIndex = 5
        Me.lblBrandName.Text = "Brand Name:"
        '
        'txtField00
        '
        Me.txtField00.Cursor = System.Windows.Forms.Cursors.No
        Me.txtField00.Enabled = False
        Me.txtField00.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtField00.Location = New System.Drawing.Point(98, 13)
        Me.txtField00.Name = "txtField00"
        Me.txtField00.ReadOnly = True
        Me.txtField00.Size = New System.Drawing.Size(131, 22)
        Me.txtField00.TabIndex = 1
        Me.txtField00.TabStop = False
        '
        'lblBrandID
        '
        Me.lblBrandID.AutoSize = True
        Me.lblBrandID.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBrandID.Location = New System.Drawing.Point(6, 16)
        Me.lblBrandID.Name = "lblBrandID"
        Me.lblBrandID.Size = New System.Drawing.Size(63, 16)
        Me.lblBrandID.TabIndex = 0
        Me.lblBrandID.Text = "Brand ID:"
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.GrayText
        Me.Panel1.Location = New System.Drawing.Point(102, 18)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(131, 22)
        Me.Panel1.TabIndex = 2
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
        Me.GroupBox2.Location = New System.Drawing.Point(7, 142)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(449, 69)
        Me.GroupBox2.TabIndex = 16
        Me.GroupBox2.TabStop = False
        '
        'cmdButton04
        '
        Me.cmdButton04.Image = Global.Lender.My.Resources.Resources.browse
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
        Me.cmdButton07.Image = Global.Lender.My.Resources.Resources.cancel_update
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
        Me.cmdButton08.Image = Global.Lender.My.Resources.Resources._exit
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
        Me.cmdButton05.Image = Global.Lender.My.Resources.Resources.cancel
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
        Me.cmdButton02.Image = Global.Lender.My.Resources.Resources.save
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
        Me.cmdButton06.Image = Global.Lender.My.Resources.Resources.update
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
        Me.cmdButton01.Image = Global.Lender.My.Resources.Resources.add_item
        Me.cmdButton01.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdButton01.Location = New System.Drawing.Point(159, 12)
        Me.cmdButton01.Name = "cmdButton01"
        Me.cmdButton01.Size = New System.Drawing.Size(53, 53)
        Me.cmdButton01.TabIndex = 0
        Me.cmdButton01.Text = "New"
        Me.cmdButton01.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdButton01.UseVisualStyleBackColor = True
        '
        'frmCarBrand
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(462, 217)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "frmCarBrand"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Car Brand"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents txtField01 As System.Windows.Forms.TextBox
    Friend WithEvents lblBrandName As System.Windows.Forms.Label
    Friend WithEvents txtField00 As System.Windows.Forms.TextBox
    Friend WithEvents lblBrandID As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents chbActive As System.Windows.Forms.CheckBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents cmdButton04 As System.Windows.Forms.Button
    Friend WithEvents cmdButton07 As System.Windows.Forms.Button
    Friend WithEvents cmdButton08 As System.Windows.Forms.Button
    Friend WithEvents cmdButton05 As System.Windows.Forms.Button
    Friend WithEvents cmdButton02 As System.Windows.Forms.Button
    Friend WithEvents cmdButton06 As System.Windows.Forms.Button
    Friend WithEvents cmdButton01 As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
End Class
