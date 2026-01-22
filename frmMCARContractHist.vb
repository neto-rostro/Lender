Imports ggcAppDriver

'========================================================
' MC AR CONTRACT UI CONTROLLER
'========================================================
Public Class frmMCARContractHist

    '==================== PRIVATE VARIABLES ====================
    Private lnMsg As String
    Private poControl As Control
    Private pnLoadx As Integer


    Private psBranch As String = ""
    Private psClient As String = ""
    Private psGocas As String = ""

    Private psGocasTransNo As String = ""
    Private psTransStat As String = ""

    Private pxeModuleName As String = "MC AR Contract"

    ' Transaction controller
    Private WithEvents poTrans As ggcLRTransaction.MCARContract

    '==================== FORM EVENTS ====================
    Private Sub frmMCARContract_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        If pnLoadx = 1 Then
            poTrans = New ggcLRTransaction.MCARContract(p_oAppDriver, 0)


            initFields(poTrans.EditMode)
            ClearTextBoxes(Me)
            pnLoadx = 2
        End If
    End Sub

    Private Sub frmMCARContract_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If pnLoadx = 0 Then
            'poTrans.resetEditmode()
            initFields(poTrans.EditMode.MODE_UNKNOWN)

            ' Register dynamic event handlers
            Call grpEventHandler(Me, GetType(Button), "cmdButton", "Click", AddressOf cmdButton_Click)
            Call grpEventHandler(Me, GetType(TextBox), "txtField", "GotFocus", AddressOf txtField_GotFocus)
            Call grpEventHandler(Me, GetType(TextBox), "txtField", "LostFocus", AddressOf txtField_LostFocus)
            Call grpCancelHandler(Me, GetType(TextBox), "txtField", "Validating", AddressOf txtField_Validating)

            Call grpEventHandler(Me, GetType(TextBox), "txtSeeks", "GotFocus", AddressOf txtSeeks_GotFocus)
            Call grpEventHandler(Me, GetType(TextBox), "txtSeeks", "LostFocus", AddressOf txtSeeks_LostFocus)

            pnLoadx = 1
        End If
    End Sub

    '==================== BUTTON HANDLER ====================
    Private Sub cmdButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim loChk As Button = CType(sender, Button)
        Dim lnIndex As Integer = Val(Mid(loChk.Name, 10))
        Dim transaction As String = ""
        Select Case lnIndex

            Case 9   ' Close
                Me.Close()

            Case 2  ' browse
                If (poTrans.SearchTransaction(fsFilter, psBranch, txtSeeks02.Text, txtSeeks03.Text, False)) Then

                    initFields(poTrans.EditMode)
                    LoadRecord(Me.Panel3)
                Else
                    ClearTextBoxes(Me)
                End If

            Case 6 'post
                transaction = txtField00.Text
                If poTrans.ApproveTransaction(transaction, psGocasTransNo) Then

                    initFields(poTrans.EditMode)
                    ClearTextBoxes(Me)
                End If

            Case 10
                transaction = txtField00.Text
                If Not String.IsNullOrEmpty(txtField00.Text) Then
                    If poTrans.ExportTransaction(transaction, psGocasTransNo, psTransStat) Then
                        initFields(poTrans.EditMode)
                        ClearTextBoxes(Me)
                    End If
                Else
                    MsgBox("No transaction is currently loaded. 
                            Please load a transaction to proceed.", MsgBoxStyle.OkOnly + MsgBoxStyle.Critical, "")
                End If

        End Select
    End Sub

    '==================== LOAD RECORD ====================
    Private Sub LoadRecord(ByVal loControl As Control)
        For Each loTxt As Control In loControl.Controls
            If loTxt.HasChildren Then
                LoadRecord(loTxt)

            ElseIf TypeOf loTxt Is TextBox Then
                If LCase(Mid(loTxt.Name, 1, 8)) = "txtfield" Then
                    Dim loIndex As Integer = Val(Mid(loTxt.Name, 9))

                    With poTrans
                        Select Case loIndex
                            Case 0 : loTxt.Text = .Master("sTransNox")
                            Case 1
                                If Not IsDate(.Master("dTransact")) Then _
                                    .Master("dTransact") = p_oAppDriver.getSysDate
                                loTxt.Text = Format(.Master("dTransact"), xsDATE_MEDIUM)

                            Case 2 : loTxt.Text = .GetBranchName(.Master("sBranchCd"))

                            Case 3 : loTxt.Text = .Master("sReferNox")

                            Case 4 : loTxt.Text = .Master("sAcctNmbr")

                            Case 5 : loTxt.Text = .GetClientName(.Master("sClientID"))

                            Case 6 : loTxt.Text = .Master("sSerialID")

                            Case 7 : loTxt.Text = .GetSerialInfo(.Master("sSerialID"), .Master("sBranchCd")).Item1

                            Case 8 : loTxt.Text = .GetSerialInfo(.Master("sSerialID"), .Master("sBranchCd")).Item2

                            'Case 9 : loTxt.Text = If(IsDBNull(.Master("dFirstPay")), "", .Master("dFirstPay").ToString())

                            Case 10 : loTxt.Text = .Master("nAcctTerm")

                            Case 11 : loTxt.Text = Format(.Master("nDownPaym"), xsDECIMAL)

                            'Case 12 : loTxt.Text = Format(.Master("nPenaltyx"), xsDECIMAL)

                            Case 13 : loTxt.Text = Format(.Master("nMonAmort"), xsDECIMAL)

                            Case 14 : loTxt.Text = Format(.Master("nRebatesx"), xsDECIMAL)

                            Case 15 : loTxt.Text = Format(.Master("nPenaltyx"), xsDECIMAL)

                            Case 16 : loTxt.Text = If(IsDBNull(.Master("sRemarksx")), "", .Master("sRemarksx").ToString())
                        End Select

                        setTranStat(.Master("cTranStat"), lblStatus)
                        psTransStat = .Master("cTranStat")
                    End With
                End If
            End If
        Next
    End Sub




    Public Sub initFields(ByVal pnEditMode As Integer)

        Dim enableFields As Boolean = Not (pnEditMode = poTrans.EditMode.MODE_READY Or pnEditMode = poTrans.EditMode.MODE_UNKNOWN)
        Dim alwaysDisabledFields As Integer() = {0, 6, 7, 8, 9, 16}

        For i As Integer = 0 To 16
            Dim txt = Me.Controls.Find("txtField" & i.ToString("00"), True) _
                                 .OfType(Of TextBox)() _
                                 .FirstOrDefault()

            If txt Is Nothing Then Continue For

            txt.ReadOnly = Not enableFields
            txt.TabStop = enableFields

            If alwaysDisabledFields.Contains(i) Then
                txt.ReadOnly = True
                txt.TabStop = False
                txt.BackColor = Color.Gainsboro
            Else
                txt.BackColor = Color.White
            End If
        Next
    End Sub

    '==================== TEXTBOX EVENTS ====================
    Private Sub txtField_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim loTxt As TextBox = CType(sender, TextBox)
        poControl = loTxt
        loTxt.BackColor = Color.Azure
        loTxt.SelectAll()
    End Sub

    Private Sub txtField_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim loTxt As TextBox = CType(sender, TextBox)
        loTxt.BackColor = If(loTxt.ReadOnly, SystemColors.Control, SystemColors.Window)
    End Sub

    Private Sub txtField_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs)
        Dim loTxt As TextBox = CType(sender, TextBox)
        Dim loIndex As Integer = Val(Mid(loTxt.Name, 9))

        If Mid(loTxt.Name, 1, 8) = "txtField" AndAlso Not loTxt.ReadOnly Then
            poTrans.Master(loIndex) = loTxt.Text

            Select Case loIndex
                Case 13
                    If Not IsNumeric(loTxt.Text) Then
                        MessageBox.Show("Invalid input", "Information",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information)
                        HighlightInvalidInput(loTxt)
                        e.Cancel = True
                    Else
                        loTxt.Text = Format(CDec(loTxt.Text), xsDECIMAL)
                        poTrans.Master("nMonAmort") = loTxt.Text
                    End If
                Case 14
                    If Not IsNumeric(loTxt.Text) Then
                        MessageBox.Show("Invalid input", "Information",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information)
                        HighlightInvalidInput(loTxt)
                        e.Cancel = True
                    Else
                        loTxt.Text = Format(CDec(loTxt.Text), xsDECIMAL)
                        poTrans.Master("nRebatesx") = loTxt.Text
                    End If
                Case 15
                    If Not IsNumeric(loTxt.Text) Then
                        MessageBox.Show("Invalid input", "Information",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information)
                        HighlightInvalidInput(loTxt)
                        e.Cancel = True
                    Else
                        loTxt.Text = Format(CDec(loTxt.Text), xsDECIMAL)
                        poTrans.Master("nPenaltyx") = loTxt.Text
                    End If
            End Select
        End If
    End Sub

    Private Sub txtSeeks_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim loTxt As TextBox = CType(sender, TextBox)
        poControl = loTxt
        loTxt.BackColor = Color.Azure
        loTxt.SelectAll()
    End Sub

    Private Sub txtSeeks_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim loTxt As TextBox = CType(sender, TextBox)
        loTxt.BackColor = If(loTxt.ReadOnly, SystemColors.Control, SystemColors.Window)
    End Sub



    Private Sub ArrowKeys_Keydown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Keys.Up, Keys.Down
                Select Case e.KeyCode
                    Case Keys.Down
                        SetNextFocus()
                    Case Keys.Up
                        SetPreviousFocus()
                End Select
        End Select
    End Sub

    Private Sub frmLRPaymentEntry_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.F3 Or e.KeyCode = Keys.Enter Then
            Dim loTxt As Control

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

            If Mid(loTxt.Name, 1, 8) = "txtSeeks" Then
                Select Case loIndex
                    Case 1
                        Call poTrans.SearchBranch(loTxt.Text, False, True)
                        txtSeeks01.Text = poTrans.BranchName
                        If Not String.IsNullOrEmpty(txtField00.Text) Then
                            psBranch = poTrans.BranchCode
                        Else
                            psBranch = ""
                        End If

                End Select

            End If

            '*********************
            If TypeOf poControl Is TextBox Then
                SelectNextControl(loTxt, True, True, True, True)
            End If
        End If
    End Sub
    '==================== INPUT HIGHLIGHT ====================
    Public Sub HighlightInvalidInput(ByVal txt As TextBox, Optional ByVal duration As Integer = 1000)
        If txt Is Nothing Then Exit Sub

        Dim originalBack As Color = txt.BackColor
        Dim originalFore As Color = txt.ForeColor

        txt.BackColor = Color.Yellow
        txt.ForeColor = Color.Red
        txt.Focus()

        Dim t As New Timer()
        t.Interval = duration
        AddHandler t.Tick,
            Sub()
                txt.BackColor = originalBack
                txt.ForeColor = originalFore
                t.Stop()
                t.Dispose()
            End Sub
        t.Start()
    End Sub


    Public Sub ClearTextBoxes(ByVal parentCtrl As Control)

        For Each ctrl As Control In parentCtrl.Controls

            If TypeOf ctrl Is TextBox Then
                CType(ctrl, TextBox).Clear()
            End If

            ' Handle nested containers
            If ctrl.HasChildren Then
                ClearTextBoxes(ctrl)
            End If

        Next
        lblStatus.Text = "UNKNOWN"
    End Sub
End Class
