Option Explicit On

Imports ggcAppDriver
Imports ggcLRTransaction

Public Class frmPaymentPartners
    Private WithEvents oTrans As APIPayment
    Private WithEvents oPayment As ARPayment
    Private WithEvents oMPPayment As ARPayment_MP

    Private Const p_sDefDatex As String = "1900-01-01"

    Private pnLoadx As Integer
    Private pbRecLoaded As Boolean

    Private poControl As Control

    Private pnIndex As Integer
    Private pbORIssued As Boolean

    Private pbCtrlPressed As Boolean

    Private Sub loadData()
        Dim lnCtr As Integer

        With dgView
            .Rows.Clear()

            progressBar.Value = 0
            progressBar.Maximum = oTrans.ItemCount

            lnCtr = 0

            Do While lnCtr < oTrans.ItemCount
                .Rows.Add()
                .Rows(lnCtr).Cells(0).Value = Format(CDate(oTrans.Master(lnCtr, "dTransact")), "MMM dd, yyyy")
                .Rows(lnCtr).Cells(1).Value = oTrans.Master(lnCtr, "sAcctNmbr")
                .Rows(lnCtr).Cells(2).Value = oTrans.Master(lnCtr, "sClientNm")
                .Rows(lnCtr).Cells(3).Value = oTrans.Master(lnCtr, "sReferNox")
                lnCtr = lnCtr + 1

                progressBar.PerformStep()
            Loop

            progressBar.Value = 0
        End With
    End Sub

    Private Sub frmPaymentPartners_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        If pnLoadx = 1 Then
            Panel2.Enabled = False
            pbRecLoaded = False

            pnLoadx = 2
        End If
    End Sub

    Private Sub frmPaymentPartners_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.ControlKey Then
            pbCtrlPressed = True
        Else
            If pbCtrlPressed Then
                Select Case e.KeyCode
                    Case Keys.Up
                        If pnIndex > 0 Then
                            dgView.ClearSelection()
                            dgView.CurrentCell = dgView.Rows(pnIndex - 1).Cells(0)
                            'dgView.Rows(pnIndex - 1).Selected = True
                        End If
                    Case Keys.Down
                        If pnIndex < oTrans.ItemCount - 1 Then
                            dgView.ClearSelection()
                            dgView.CurrentCell = dgView.Rows(pnIndex + 1).Cells(0)
                            'dgView.Rows(pnIndex + 1).Selected = True
                        End If
                End Select
            End If
        End If
    End Sub

    Private Sub frmPaymentPartners_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyUp
        If e.KeyCode = Keys.ControlKey Then
            pbCtrlPressed = False
        End If
    End Sub

    Private Sub frmPaymentPartners_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If pnLoadx = 0 Then
            oTrans = New APIPayment
            oTrans.AppDriver = p_oAppDriver

            oPayment = New ARPayment(p_oAppDriver, "2")

            Call grpEventHandler(Me, GetType(TextBox), "txtSeeks", "GotFocus", AddressOf txtField_GotFocus)
            Call grpEventHandler(Me, GetType(TextBox), "txtSeeks", "LostFocus", AddressOf txtField_LostFocus)
            Call grpKeyHandler(Me, GetType(TextBox), "txtSeeks", "KeyDown", AddressOf txtField_KeyDown)
            Call grpEventHandler(Me, GetType(Button), "cmdButton", "Click", AddressOf cmdButton_Click)

            Call grpEventHandler(Me, GetType(TextBox), "txtPayxx", "GotFocus", AddressOf txtField_GotFocus)
            Call grpEventHandler(Me, GetType(TextBox), "txtPayxx", "LostFocus", AddressOf txtField_LostFocus)
            Call grpKeyHandler(Me, GetType(TextBox), "txtPayxx", "KeyDown", AddressOf txtField_KeyDown)

            progressBar.Value = 0
            progressBar.Minimum = 0
            progressBar.Maximum = 0
            progressBar.Step = 1

            pnLoadx = 1
        End If
    End Sub

    'Handles GotFocus Events for txtField & txtField
    Private Sub txtField_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim loTxt As TextBox
        loTxt = CType(sender, System.Windows.Forms.TextBox)
        loTxt.BackColor = Color.Azure

        poControl = loTxt

        Dim loIndex As Integer
        loIndex = Val(Mid(loTxt.Name, 9))

        If Mid(loTxt.Name, 1, 8) = "txtSeeks" Then
            If loIndex = 1 Then
                If IsDate(loTxt.Text) Then
                    loTxt.Text = Format(CDate(loTxt.Text), "yyyy-MM-dd")
                Else
                    loTxt.Text = ""
                End If
            End If
        End If

        loTxt.SelectAll()
    End Sub

    'Handles LostFocus Events for txtField & txtField
    Private Sub txtField_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim loTxt As TextBox
        loTxt = CType(sender, System.Windows.Forms.TextBox)
        loTxt.BackColor = SystemColors.Window

        Dim loIndex As Integer
        loIndex = Val(Mid(loTxt.Name, 9))

        If Mid(loTxt.Name, 1, 8) = "txtSeeks" Then
            If loIndex = 1 Then
                If IsDate(loTxt.Text) Then
                    loTxt.Text = Format(CDate(loTxt.Text), "MMMM dd, yyyy")
                Else
                    loTxt.Text = ""
                End If
            End If
        Else
            Select Case loIndex
                Case 3
                    oTrans.Master(pnIndex, "sORNoxxxx") = loTxt.Text
                Case 7
                    oTrans.Master(pnIndex, "sRemarksx") = loTxt.Text
                Case 8
                    If Not IsNumeric(loTxt.Text) Then
                        oTrans.Master(pnIndex, "nAmtPaidx") = 0.0#
                    Else
                        oTrans.Master(pnIndex, "nAmtPaidx") = CDbl(loTxt.Text)
                    End If
                Case 10
                    If Not IsNumeric(loTxt.Text) Then
                        oTrans.Master(pnIndex, "nRebatesx") = 0.0#
                    Else
                        oTrans.Master(pnIndex, "nRebatesx") = CDbl(loTxt.Text)
                    End If
            End Select
        End If
    End Sub

    Private Sub txtField_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        If e.KeyCode = Keys.F3 Or e.KeyCode = Keys.Enter Then
            Dim loTxt As Control
            loTxt = CType(sender, System.Windows.Forms.TextBox)

            Dim loIndex As Integer
            loIndex = Val(Mid(loTxt.Name, 9))

            If Mid(loTxt.Name, 1, 8) = "txtSeeks" Then
                If e.KeyCode = Keys.F3 Then
                    If loIndex = 0 Then
                        loTxt.Text = oTrans.SearchPartner(loTxt.Text)
                    End If

                    Exit Sub
                ElseIf e.KeyCode = Keys.Enter Then
                    Select Case loIndex
                        Case 0
                            oTrans.Partner = loTxt.Text
                        Case 1
                            If Not IsDate(loTxt.Text) Then
                                loTxt.Text = ""
                                oTrans.DatePaid = CDate(p_sDefDatex)
                            Else
                                oTrans.DatePaid = CDate(loTxt.Text)
                            End If
                        Case 2
                            oTrans.AccountNo = loTxt.Text
                        Case 3
                            oTrans.ClientName = loTxt.Text
                    End Select

                    If oTrans.Filter Then
                        loadData()
                    End If
                End If
            ElseIf Mid(loTxt.Name, 1, 8) = "txtPayxx" Then
                If loIndex = 4 Then
                    If e.KeyCode = Keys.Enter Or e.KeyCode = Keys.F3 Then
                        If oTrans.SearchAccount(pnIndex, loTxt.Text, False) Then
                            'use this block of code to get the customer information
                            If Strings.Left(oTrans.Master(pnIndex, "sAcctNmbr"), 1).ToLower = "m" Then
                                oPayment = New ARPayment(p_oAppDriver, "2")
                                oPayment.NewTransaction()
                                oPayment.SearchMaster(4, oTrans.Master(pnIndex, "sAcctNmbr"))
                                oPayment = Nothing
                            Else
                                oMPPayment = New ARPayment_MP(p_oAppDriver, "2")
                                oMPPayment.NewTransaction()
                                oMPPayment.SearchMaster(4, oTrans.Master(pnIndex, "sAcctNmbr"))
                                oMPPayment = Nothing
                            End If
                            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                        End If
                    End If
                End If
            End If

            If TypeOf poControl Is TextBox Then
                If Mid(loTxt.Name, 1, 8) = "txtSeeks" Then
                    SelectNextControl(loTxt, True, True, True, True)
                Else
                    If loIndex <> 7 Then
                        SelectNextControl(loTxt, True, True, True, True)
                    Else
                        txtPayxx03.Focus()
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub cmdButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim loChk As Button
        loChk = CType(sender, System.Windows.Forms.Button)
        Dim lnIndex As Integer
        lnIndex = Val(Mid(loChk.Name, 10))

        Select Case lnIndex
            Case 0 'exit
                Me.Dispose()
            Case 1 'release OR
                If p_oAppDriver.ProductID.ToLower <> "lrtrackr" Then
                    MsgBox("This feature is not supported by this PRODUCT TYPE.", vbInformation, "Notice")
                    Exit Sub
                End If

                If oTrans.ReleaseOR Then
                    If pnLoadx = 3 Then Exit Sub

                    pnLoadx = 3
                    Panel2.Enabled = False

                    If oTrans.LoadTransaction() Then
                        If oTrans.Filter Then
                            progressBar.Value = 0

                            pbRecLoaded = True
                            Panel2.Enabled = True
                            txtSeeks00.Focus()

                            loadData()
                        End If
                    End If

                    pnLoadx = 2
                End If
            Case 2 'load
                If pnLoadx = 3 Then Exit Sub

                pnLoadx = 3
                Panel2.Enabled = False

                Dim loForm As frmDateCriteria
                loForm = New frmDateCriteria
                loForm.AppDriver = p_oAppDriver

                loForm.ShowDialog()

                If loForm.isOkey Then
                    Application.DoEvents()
                    If oTrans.LoadTransaction(loForm.txtDateFrom.Text, loForm.txtDateThru.Text) Then
                        If oTrans.Filter Then
                            progressBar.Value = 0

                            pbRecLoaded = True
                            Panel2.Enabled = True
                            txtSeeks00.Focus()

                            loadData()
                        End If
                    End If
                Else
                    MsgBox("Data generation cancelled.")
                End If

                loForm = Nothing
                pnLoadx = 2
            Case 3 'cancel
                MsgBox("Feature is not yet available.")
            Case 4 'save
                MsgBox("Feature is not yet available.")
            Case 5 'export
                If pbRecLoaded Then
                    If pnLoadx = 3 Then Exit Sub

                    pnLoadx = 3
                    Panel2.Enabled = False
                    Application.DoEvents()
                    If oTrans.Export() Then
                        progressBar.Value = 0

                        MsgBox("API Payments Exported Successfully." & vbCrLf & vbCrLf & "Please check D:\APIPayment.xlsx file.", vbInformation, "Notice")
                    End If
                Else
                    MsgBox("Please load transactions first. (Click load button)", vbInformation, "Notice")
                End If

                Panel2.Enabled = True
                pnLoadx = 2
        End Select
    End Sub

    Private Sub oTrans_FirstRecord() Handles oTrans.FirstRecord
        progressBar.Value = 0
    End Sub

    Private Sub oTrans_MaxRecord(ByVal fnRecord As Integer) Handles oTrans.MaxRecord
        progressBar.Maximum = fnRecord
    End Sub

    Private Sub oTrans_NextRecord() Handles oTrans.NextRecord
        progressBar.PerformStep()
    End Sub

    Private Sub oPayment_MasterRetrieved(ByVal Index As Integer, ByVal Value As Object) Handles oPayment.MasterRetrieved
        Dim lnCtr As Integer
        Dim loTxt As TextBox
        'Find TextBox with specified name
        loTxt = CType(FindTextBox(Me, "txtField" & Format(Index, "00")), TextBox)

        Select Case Index
            Case 80
                For lnCtr = 80 To 94
                    loTxt = CType(FindTextBox(Me, "txtField" & Format(lnCtr, "00")), TextBox)
                    Select Case lnCtr
                        Case 80
                            txtPayxx04.Text = Value
                        Case 81
                        Case 82 To 90
                            loTxt.Text = Format(oPayment.Master(lnCtr), xsDECIMAL)
                        Case Else
                            loTxt.Text = oPayment.Master(lnCtr)
                    End Select
                Next
            Case 8
                loTxt.Text = Format(Value, xsDECIMAL)
            Case 10
                loTxt.Text = Format(Value, xsDECIMAL)
            Case Else
                loTxt.Text = Value
        End Select
    End Sub

    Private Sub oMPPayment_MasterRetrieved(ByVal Index As Integer, ByVal Value As Object) Handles oMPPayment.MasterRetrieved
        Dim lnCtr As Integer
        Dim loTxt As TextBox
        'Find TextBox with specified name
        loTxt = CType(FindTextBox(Me, "txtField" & Format(Index, "00")), TextBox)

        Select Case Index
            Case 80
                For lnCtr = 80 To 94
                    loTxt = CType(FindTextBox(Me, "txtField" & Format(lnCtr, "00")), TextBox)
                    Select Case lnCtr
                        Case 80
                            txtPayxx04.Text = Value
                        Case 81
                        Case 82 To 90
                            loTxt.Text = Format(oMPPayment.Master(lnCtr), xsDECIMAL)
                        Case 92
                            txtField92.Text = ""
                            txtField99.Text = oMPPayment.Master(lnCtr)
                        Case Else
                            loTxt.Text = oMPPayment.Master(lnCtr)
                    End Select
                Next
            Case 8
                loTxt.Text = Format(Value, xsDECIMAL)
            Case 10
                loTxt.Text = Format(Value, xsDECIMAL)
            Case Else
                loTxt.Text = Value
        End Select
    End Sub

    Public Sub setTransTat(ByVal nStat As Integer)
        Select Case nStat
            Case 0
                PictureBox2.Image = My.Resources.STAT_OPEN
            Case 1
                PictureBox2.Image = My.Resources.STAT_CLOSED
            Case 2
                PictureBox2.Image = My.Resources.STAT_POSTED
            Case 3
                PictureBox2.Image = My.Resources.STAT_CANCELLED
            Case 4
                PictureBox2.Image = My.Resources.STAT_VOID
            Case Else
                PictureBox2.Image = My.Resources.STAT_UNKNOWN
        End Select
    End Sub

    Protected Overrides Sub Finalize()
        oTrans = Nothing
        oPayment = Nothing
        oMPPayment = Nothing

        MyBase.Finalize()
    End Sub

    Private Sub oTrans_PaymentRetreive(ByVal fnIndex As Integer) Handles oTrans.PaymentRetreive
        Select Case fnIndex
            Case 3
                txtPayxx03.Text = oTrans.Master(pnIndex, "sORNoxxxx")
            Case 7
                txtPayxx07.Text = oTrans.Master(pnIndex, "sRemarksx")
            Case 8
                txtPayxx08.Text = Format(oTrans.Master(pnIndex, "nAmtPaidx"), "#,##0.00")
            Case 10
                txtPayxx10.Text = Format(oTrans.Master(pnIndex, "nRebatesx"), "#,##0.00")
        End Select
    End Sub

    Private Sub dgView_CellMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles dgView.CellMouseClick
        If Not pbRecLoaded Then Exit Sub

        Dim lnIndex As Integer = e.RowIndex

        If lnIndex < 0 Then Exit Sub

        With oTrans
            txtField00.Text = .Master(lnIndex, "sPartnerx")
            txtField01.Text = .Master(lnIndex, "sAcctNmbr")
            txtField02.Text = .Master(lnIndex, "sClientNm")
            txtField03.Text = IFNull(.Master(lnIndex, "sAddressx"))
            txtField04.Text = .Master(lnIndex, "sReferNox")
            txtField05.Text = Format(CDate(.Master(lnIndex, "dTransact")), "MMM dd, yyyy")
            txtField06.Text = Format(CDbl(.Master(lnIndex, "nAmountxx")), "#,##0.00")

            If .Master(lnIndex, "sORNoxxxx") = "" Then
                txtField07.Text = "NOT RELEASED"
            Else
                txtField07.Text = .Master(lnIndex, "sORNoxxxx")
            End If

            'clear customer loan info
            txtField82.Text = ""
            txtField83.Text = ""
            txtField84.Text = ""
            txtField85.Text = ""
            txtField86.Text = ""
            txtField87.Text = ""
            txtField88.Text = ""
            txtField89.Text = ""
            txtField90.Text = ""
            txtField91.Text = ""
            txtField92.Text = ""
            txtField93.Text = ""
            txtField99.Text = ""

            'use this block of code to get the customer information
            If Strings.Left(.Master(lnIndex, "sAcctNmbr"), 1).ToLower = "m" Then
                oPayment = New ARPayment(p_oAppDriver, "2")
                oPayment.NewTransaction()
                oPayment.SearchMaster(4, oTrans.Master(lnIndex, "sAcctNmbr"))
                oPayment = Nothing
            Else
                oMPPayment = New ARPayment_MP(p_oAppDriver, "2")
                oMPPayment.NewTransaction()
                oMPPayment.SearchMaster(4, oTrans.Master(lnIndex, "sAcctNmbr"))
                oMPPayment = Nothing
            End If
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

            txtPayxx03.ReadOnly = .Master(lnIndex, "cTranStat") <> "0" Or p_oAppDriver.ProductID.ToLower <> "lrtrackr"
            txtPayxx07.ReadOnly = .Master(lnIndex, "cTranStat") <> "0" Or p_oAppDriver.ProductID.ToLower <> "lrtrackr"
            txtPayxx08.ReadOnly = .Master(lnIndex, "cTranStat") <> "0" Or p_oAppDriver.ProductID.ToLower <> "lrtrackr"
            txtPayxx10.ReadOnly = .Master(lnIndex, "cTranStat") <> "0" Or p_oAppDriver.ProductID.ToLower <> "lrtrackr"

            txtPayxx04.Text = .Master(lnIndex, "sAcctNmbr")
            txtPayxx03.Text = .Master(lnIndex, "sORNoxxxx")
            txtPayxx07.Text = .Master(lnIndex, "sRemarksx")
            txtPayxx08.Text = Format(.Master(lnIndex, "nAmtPaidx"), "#,##0.00")
            txtPayxx10.Text = Format(.Master(lnIndex, "nRebatesx"), "#,##0.00")

            If .Master(lnIndex, "cTranStat") = "0" Then
                Label31.Text = "Account Name:"

                If txtPayxx04.Text <> "" Then
                    txtPayxx04.Enabled = False
                    txtPayxx03.Focus()
                Else
                    txtPayxx04.Enabled = True
                    txtPayxx04.Focus()
                End If

                If LCase(Strings.Left(.Master(lnIndex, "sReferNox"), 2)) = "cc" Or _
                    LCase(Strings.Left(.Master(lnIndex, "sReferNox"), 2)) = "bp" Then

                    .Master(lnIndex, "sRemarksx") = .Master(lnIndex, "sReferNox")
                    txtPayxx07.Text = .Master(lnIndex, "sRemarksx")
                    txtPayxx07.Enabled = False
                Else
                    txtPayxx07.Enabled = True
                End If
            Else
                Label31.Text = "Account No.:"
                txtPayxx04.Enabled = False
            End If

            Call setTransTat(CInt(.Master(lnIndex, "cTranStat")))

            pnIndex = lnIndex
        End With
    End Sub
End Class