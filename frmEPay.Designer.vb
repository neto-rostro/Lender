<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmEPay
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
        Me.Label555 = New System.Windows.Forms.Label()
        Me.cmdButton00 = New System.Windows.Forms.Button()
        Me.cmdButton01 = New System.Windows.Forms.Button()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.txtField05 = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtField04 = New System.Windows.Forms.TextBox()
        Me.txtField03 = New System.Windows.Forms.TextBox()
        Me.txtField02 = New System.Windows.Forms.TextBox()
        Me.txtField01 = New System.Windows.Forms.TextBox()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.lb1Field00 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtField00 = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Panel1.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label555
        '
        Me.Label555.AutoSize = True
        Me.Label555.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label555.ForeColor = System.Drawing.Color.Coral
        Me.Label555.Location = New System.Drawing.Point(157, 28)
        Me.Label555.Name = "Label555"
        Me.Label555.Size = New System.Drawing.Size(53, 13)
        Me.Label555.TabIndex = 273
        '
        'cmdButton00
        '
        Me.cmdButton00.Image = Global.Lender.My.Resources.Resources.cancel
        Me.cmdButton00.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdButton00.Location = New System.Drawing.Point(7, 79)
        Me.cmdButton00.Name = "cmdButton00"
        Me.cmdButton00.Size = New System.Drawing.Size(78, 41)
        Me.cmdButton00.TabIndex = 67
        Me.cmdButton00.TabStop = False
        Me.cmdButton00.Text = "     &Cancel"
        Me.cmdButton00.UseVisualStyleBackColor = True
        '
        'cmdButton01
        '
        Me.cmdButton01.Image = Global.Lender.My.Resources.Resources.post
        Me.cmdButton01.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdButton01.Location = New System.Drawing.Point(7, 32)
        Me.cmdButton01.Name = "cmdButton01"
        Me.cmdButton01.Size = New System.Drawing.Size(78, 41)
        Me.cmdButton01.TabIndex = 70
        Me.cmdButton01.TabStop = False
        Me.cmdButton01.Text = "     &OK"
        Me.cmdButton01.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.PictureBox1)
        Me.Panel1.Location = New System.Drawing.Point(-1, -1)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(557, 26)
        Me.Panel1.TabIndex = 71
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(32, 6)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(40, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "E-Pay"
        '
        'PictureBox1
        '
        Me.PictureBox1.BackgroundImage = Global.Lender.My.Resources.Resources.GLogoy
        Me.PictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.PictureBox1.Location = New System.Drawing.Point(0, 0)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(25, 26)
        Me.PictureBox1.TabIndex = 0
        Me.PictureBox1.TabStop = False
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.txtField05)
        Me.Panel2.Controls.Add(Me.Label10)
        Me.Panel2.Controls.Add(Me.Panel3)
        Me.Panel2.Controls.Add(Me.Panel4)
        Me.Panel2.Location = New System.Drawing.Point(91, 32)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(465, 247)
        Me.Panel2.TabIndex = 72
        '
        'txtField05
        '
        Me.txtField05.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtField05.ForeColor = System.Drawing.Color.Red
        Me.txtField05.Location = New System.Drawing.Point(257, 209)
        Me.txtField05.Margin = New System.Windows.Forms.Padding(10, 3, 10, 3)
        Me.txtField05.Name = "txtField05"
        Me.txtField05.Size = New System.Drawing.Size(190, 31)
        Me.txtField05.TabIndex = 80
        Me.txtField05.Text = "5,000.00"
        Me.txtField05.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(158, 212)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(93, 13)
        Me.Label10.TabIndex = 79
        Me.Label10.Text = "AMOUNT PAID"
        '
        'Panel3
        '
        Me.Panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel3.Controls.Add(Me.Label6)
        Me.Panel3.Controls.Add(Me.Label5)
        Me.Panel3.Controls.Add(Me.Label4)
        Me.Panel3.Controls.Add(Me.Label3)
        Me.Panel3.Controls.Add(Me.txtField04)
        Me.Panel3.Controls.Add(Me.txtField03)
        Me.Panel3.Controls.Add(Me.txtField02)
        Me.Panel3.Controls.Add(Me.txtField01)
        Me.Panel3.Location = New System.Drawing.Point(5, 49)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(442, 150)
        Me.Panel3.TabIndex = 78
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(5, 95)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(60, 13)
        Me.Label6.TabIndex = 9
        Me.Label6.Text = "REMARKS"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(5, 69)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(38, 13)
        Me.Label5.TabIndex = 8
        Me.Label5.Text = "TERM"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(5, 43)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(94, 13)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "REFERENCE NO."
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(5, 17)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(60, 13)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "COMPANY"
        '
        'txtField04
        '
        Me.txtField04.Location = New System.Drawing.Point(141, 92)
        Me.txtField04.Multiline = True
        Me.txtField04.Name = "txtField04"
        Me.txtField04.Size = New System.Drawing.Size(292, 50)
        Me.txtField04.TabIndex = 5
        '
        'txtField03
        '
        Me.txtField03.Location = New System.Drawing.Point(141, 66)
        Me.txtField03.Name = "txtField03"
        Me.txtField03.Size = New System.Drawing.Size(111, 20)
        Me.txtField03.TabIndex = 4
        '
        'txtField02
        '
        Me.txtField02.Location = New System.Drawing.Point(141, 40)
        Me.txtField02.Name = "txtField02"
        Me.txtField02.Size = New System.Drawing.Size(111, 20)
        Me.txtField02.TabIndex = 3
        '
        'txtField01
        '
        Me.txtField01.Location = New System.Drawing.Point(141, 14)
        Me.txtField01.Name = "txtField01"
        Me.txtField01.Size = New System.Drawing.Size(292, 20)
        Me.txtField01.TabIndex = 2
        '
        'Panel4
        '
        Me.Panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel4.Controls.Add(Me.lb1Field00)
        Me.Panel4.Controls.Add(Me.Label7)
        Me.Panel4.Controls.Add(Me.txtField00)
        Me.Panel4.Controls.Add(Me.Label2)
        Me.Panel4.Location = New System.Drawing.Point(5, 2)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(442, 43)
        Me.Panel4.TabIndex = 77
        '
        'lb1Field00
        '
        Me.lb1Field00.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lb1Field00.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lb1Field00.Location = New System.Drawing.Point(287, 4)
        Me.lb1Field00.Name = "lb1Field00"
        Me.lb1Field00.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.lb1Field00.Size = New System.Drawing.Size(146, 35)
        Me.lb1Field00.TabIndex = 3
        Me.lb1Field00.Text = "5,000.00"
        Me.lb1Field00.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(200, 9)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(81, 13)
        Me.Label7.TabIndex = 2
        Me.Label7.Text = "TOTAL AMT."
        '
        'txtField00
        '
        Me.txtField00.Enabled = False
        Me.txtField00.Location = New System.Drawing.Point(75, 9)
        Me.txtField00.Name = "txtField00"
        Me.txtField00.Size = New System.Drawing.Size(119, 20)
        Me.txtField00.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(5, 13)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(67, 13)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Trans. No."
        '
        'frmEPay
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(557, 280)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.cmdButton01)
        Me.Controls.Add(Me.cmdButton00)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmEPay"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "MC Credit Approval"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Label555 As System.Windows.Forms.Label
    Friend WithEvents cmdButton00 As System.Windows.Forms.Button
    Friend WithEvents cmdButton01 As System.Windows.Forms.Button
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents txtField05 As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtField04 As System.Windows.Forms.TextBox
    Friend WithEvents txtField03 As System.Windows.Forms.TextBox
    Friend WithEvents txtField02 As System.Windows.Forms.TextBox
    Friend WithEvents txtField01 As System.Windows.Forms.TextBox
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents lb1Field00 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtField00 As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
End Class
