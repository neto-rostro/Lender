Option Explicit On
Imports ggcAppDriver
Imports ggcLRTransaction

Public Class frmEPay
    Private p_sTransNox As String
    Private p_nMonthly As Decimal
    Private WithEvents poTrans As ARPayment
    Private pnLoadx As Integer
    Private poControl As Control
    Private p_cTranType As String

    Public WriteOnly Property sTransNox As String
        Set(ByVal value As String)
            p_sTransNox = value
        End Set
    End Property
    Public WriteOnly Property nAmountxx As Decimal
        Set(ByVal value As Decimal)
            p_nMonthly = value
        End Set
    End Property

    Private Sub frmEPay_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        

    End Sub
    Private Sub frmEPay_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Debug.Print("frmEPay_Load")
        Call grpEventHandler(Me, GetType(Button), "cmdButton", "Click", AddressOf cmdButton_Click)
        Call grpEventHandler(Me, GetType(TextBox), "txtField", "GotFocus", AddressOf txtField_GotFocus)
        Call grpEventHandler(Me, GetType(TextBox), "txtField", "LostFocus", AddressOf txtField_LostFocus)
        Call grpCancelHandler(Me, GetType(TextBox), "txtField", "Validating", AddressOf txtField_Validating)
        Call grpKeyHandler(Me, GetType(TextBox), "txtField", "KeyDown", AddressOf txtField_KeyDown)
        Call grpKeyHandler(Me, GetType(TextBox), "txtField", "KeyDown", AddressOf ArrowKeys_Keydown)

    End Sub
    Private Sub ArrowKeys_Keydown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Keys.Up, Keys.Down, Keys.Left, Keys.Right
                Select Case e.KeyCode
                    Case Keys.Down, Keys.Right
                        SetNextFocus()
                    Case Keys.Up, Keys.Left
                        SetPreviousFocus()
                End Select
        End Select
    End Sub
    Private Sub txtField_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim loTxt As TextBox
        loTxt = CType(sender, System.Windows.Forms.TextBox)

        poControl = loTxt

        loTxt.BackColor = Color.Azure
        loTxt.SelectAll()
    End Sub
    Private Sub txtField_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim loTxt As TextBox
        loTxt = CType(sender, System.Windows.Forms.TextBox)

        If loTxt.ReadOnly Then
            loTxt.BackColor = SystemColors.Control
        Else
            loTxt.BackColor = SystemColors.Window
        End If
    End Sub
    Private Sub validation()
        If txtField01.Text = Nothing Then
            MsgBox("Company must not be empty!", MsgBoxStyle.Information, "EPayment Entry")
            txtField01.Focus()
        ElseIf txtField02.Text = Nothing Then
            MsgBox("Reference must not be empty!", MsgBoxStyle.Information, "EPayment Entry")
            txtField02.Focus()
        End If
    End Sub
    Private Sub cmdButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim loChk As Button
        loChk = CType(sender, System.Windows.Forms.Button)

        Dim lnIndex As Integer
        lnIndex = Val(Mid(loChk.Name, 10))

        Select Case lnIndex
            Case 0 ' Exit
                Me.Dispose()
            Case 1 'Save confirmation
                validation()
        End Select
    End Sub
    Private Sub txtField_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs)
        Dim loTxt As TextBox
        loTxt = CType(sender, System.Windows.Forms.TextBox)
        Dim loIndex As Integer
        loIndex = Val(Mid(loTxt.Name, 9))
        If Mid(loTxt.Name, 1, 8) = "txtEmplo" And loTxt.ReadOnly = False Then
            Select Case loIndex
                Case 1
                    If txtField01.Text = Nothing Then
                        MsgBox("Company must not be empty!", MsgBoxStyle.Information, "EPayment Entry")
                        txtField01.Focus()
                    End If
                Case 2
                    If txtField02.Text = Nothing Then
                        MsgBox("Reference must not be empty!", MsgBoxStyle.Information, "EPayment Entry")
                        txtField02.Focus()
                    End If
                Case 5
                    If Not IsNumeric(loTxt.Text) Then
                        loTxt.Text = Format(0, xsDECIMAL)
                    Else
                        loTxt.Text = Format(CDbl(loTxt.Text), xsDECIMAL)
                    End If
                    loTxt.Text = CDbl(loTxt.Text)
            End Select
        End If
    End Sub

    Private Sub loadMaster(ByVal loControl As Control)
        Dim loTxt As Control
        For Each loTxt In loControl.Controls
           
                If (TypeOf loTxt Is TextBox) Then
                    Dim loIndex As Integer
                    loIndex = Val(Mid(loTxt.Name, 9))
                    If LCase(Mid(loTxt.Name, 1, 8)) = "txtField" Then
                        Select Case loIndex
                            Case 0
                                loTxt.Text = p_sTransNox
                            Case 5
                            loTxt.Text = Format(CDbl(poTrans.Master("nAmountxx")), xsDECIMAL)
                        End Select
                    End If
                Else
                    If (TypeOf loTxt Is Label) Then
                        Dim loIndex As Integer
                        loIndex = Val(Mid(loTxt.Name, 9))
                        If LCase(Mid(loTxt.Name, 1, 8)) = "lb1Field" Then
                            Select Case loIndex
                                Case 0
                                loTxt.Text = Format(CDbl(p_nMonthly), xsDECIMAL)
                            End Select

                        End If
                    End If
                End If
        Next
    End Sub
    Private Sub txtField_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        If e.KeyCode = Keys.F3 Or e.KeyCode = Keys.Enter Then
            Dim loTxt As Control
            loTxt = CType(sender, System.Windows.Forms.TextBox)

            loTxt = Nothing
            If TypeOf poControl Is TextBox Then
                loTxt = CType(poControl, System.Windows.Forms.TextBox)
            ElseIf TypeOf poControl Is CheckBox Then
                loTxt = CType(poControl, System.Windows.Forms.CheckBox)
            ElseIf TypeOf poControl Is ComboBox Then
                loTxt = CType(poControl, System.Windows.Forms.ComboBox)
            End If

            '*********************
            Dim loIndex As Integer
            loIndex = Val(Mid(loTxt.Name, 9))

            If Mid(loTxt.Name, 1, 8) = "txtField" Then
                Select Case loIndex
                    Case 5

                End Select
            End If

            '*********************
            If TypeOf poControl Is TextBox Then
                SelectNextControl(loTxt, True, True, True, True)
            End If
        End If
    End Sub
End Class