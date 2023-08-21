Imports MySql.Data.MySqlClient
Imports ggcAppDriver
Imports ggcLRTransaction
Imports System.Globalization

Public Class frmCTBillingMaster
    Private WithEvents p_oTrans As ggcLRTransaction.CTBilling
    Private pnLoadx As Integer
    Private poControl As Control
    Private pnRow As Integer
    Dim selRow As Integer
    Dim selCol As Integer
    Private pb_ChkdOk As Boolean
    Private p_nEditMode As Integer
    Private p_TranStatus As String
    Private p_oDriver As ggcAppDriver.GRider

    Public WriteOnly Property GRider() As ggcAppDriver.GRider
        Set(ByVal foValue As ggcAppDriver.GRider)
            p_oDriver = foValue
        End Set
    End Property

    Public Function isOkey() As Boolean
        Return pb_ChkdOk
    End Function

    Public Property TranStatus() As String
        Get
            Return p_TranStatus
        End Get
        Set(ByVal value As String)
            p_TranStatus = value
        End Set
    End Property

    Private Sub frmCTBillingMaster_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        Debug.Print("frmCTBillingMaster_Activated")
        If p_TranStatus = "0" Then
            Me.Text = "CarTrade Billing Master"
        End If

        If pnLoadx = 1 Then
            p_oTrans.TranStatus = p_TranStatus
            p_oTrans.NewTransaction()
            Call newRecord()
            Call loadDetail()
            txtField02.Focus()
            pnLoadx = 2
        End If

    End Sub

    Private Function newRecord() As Boolean
        Call loadMaster()
        p_nEditMode = xeEditMode.MODE_ADDNEW
        initButton()
        Return True

    End Function

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

    Private Sub frmCTBillingMaster_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Debug.Print("frmCTBillingMaster_Load")
        If pnLoadx = 0 Then
            p_oTrans = New ggcLRTransaction.CTBilling(p_oAppDriver)

            'Set event Handlers for txtField
            Call grpEventHandler(Me, GetType(TextBox), "txtField", "GotFocus", AddressOf txtField_GotFocus)
            Call grpEventHandler(Me, GetType(TextBox), "txtDetail", "GotFocus", AddressOf txtField_GotFocus)
            Call grpEventHandler(Me, GetType(TextBox), "txtField", "LostFocus", AddressOf txtField_LostFocus)
            Call grpEventHandler(Me, GetType(TextBox), "txtDetail", "LostFocus", AddressOf txtField_LostFocus)
            Call grpCancelHandler(Me, GetType(TextBox), "txtField", "Validating", AddressOf txtField_Validating)
            Call grpCancelHandler(Me, GetType(TextBox), "txtDetail", "Validating", AddressOf txtField_Validating)
            Call grpKeyHandler(Me, GetType(TextBox), "txtField", "KeyDown", AddressOf txtField_KeyDown)
            Call grpKeyHandler(Me, GetType(TextBox), "txtDetail", "KeyDown", AddressOf txtField_KeyDown)
            Call grpKeyHandler(Me, GetType(TextBox), "txtField", "KeyDown", AddressOf ArrowKeys_Keydown)
            Call grpKeyHandler(Me, GetType(TextBox), "txtDetail", "KeyDown", AddressOf ArrowKeys_Keydown)
            Call grpEventHandler(Me, GetType(Button), "cmdButton", "Click", AddressOf cmdButton_Click)
            pnLoadx = 1
        End If

    End Sub

    Private Sub loadMaster()
        Call loadEntry(Me.Panel5)
        Call loadEntry(Me.Panel2)
        txtField00.Text = p_oTrans.Master("sTransNox")
    End Sub

    Private Sub loadEntry(ByVal loControl As Control)
        Dim loTxt As Control
        For Each loTxt In loControl.Controls
            If loTxt.HasChildren Then
                Call loadEntry(loTxt)
            ElseIf (TypeOf loTxt Is TextBox) Then
                Dim loIndex As Integer
                Dim ldIndex As Integer
                loIndex = Val(Mid(loTxt.Name, 9))
                ldIndex = Val(Mid(loTxt.Name, 10))
                If LCase(Mid(loTxt.Name, 1, 8)) = "txtField" Then
                    Select Case loIndex
                        Case 1
                            If IsDate(p_oTrans.Master(loIndex)) Then
                                loTxt.Text = Format(p_oTrans.Master(loIndex), "MMMM dd, yyyy")
                            End If
                        Case 4
                            If IsNumeric(p_oTrans.Master(loIndex)) Then
                                loTxt.Text = Format(p_oTrans.Master(loIndex), xsDECIMAL)
                            Else
                                loTxt.Text = "0.00"
                            End If
                        Case 9
                            If p_oTrans.Master("cTranStat") = "0" Then
                                loTxt.Text = "OPEN"
                            ElseIf p_oTrans.Master("cTranStat") = "1" Then
                                loTxt.Text = "CLOSED"
                            ElseIf p_oTrans.Master("cTranStat") = "2" Then
                                loTxt.Text = "RELEASED"
                            ElseIf p_oTrans.Master("cTranStat") = "3" Then
                                loTxt.Text = "CANCELLED"
                            ElseIf p_oTrans.Master("cTranStat") = "4" Then
                                loTxt.Text = "RECEIVED"
                            End If
                        Case Else
                            loTxt.Text = IFNull(p_oTrans.Master(loIndex), "")
                    End Select
                ElseIf LCase(Mid(loTxt.Name, 1, 9)) = "txtDetail" Then
                    Select Case ldIndex
                        Case 1
                            If IsDate(p_oTrans.Detail(0, ldIndex)) Then
                                loTxt.Text = Format(p_oTrans.Detail(0, ldIndex), "MMMM dd, yyyy")
                            End If
                        Case 5, 6, 7, 8, 13, 14
                            If IsNumeric(p_oTrans.Detail(0, ldIndex)) Then
                                loTxt.Text = Format(p_oTrans.Detail(0, ldIndex), xsDECIMAL)
                            Else
                                loTxt.Text = "0.00"
                            End If
                        Case Else
                            loTxt.Text = IFNull(p_oTrans.Detail(0, ldIndex), "")
                    End Select
                    cmbBillType.SelectedIndex = CInt(p_oTrans.Detail(pnRow, "cBillType").ToString)
                End If
            End If

        Next
    End Sub

    Private Sub initButton()
        'UNKNOWN = -1
        'READY = 0
        'ADDNEW = 1
        'UPDATE = 2
        'DELETE = 3

        Dim lbShow As Integer
        lbShow = (p_nEditMode = 1 Or p_nEditMode = 2)

        cmdButton02.Visible = lbShow
        cmdButton03.Visible = lbShow
        cmdButton04.Visible = lbShow
        Panel2.Enabled = lbShow
        Panel5.Enabled = lbShow

        cmdButton00.Visible = Not lbShow
        cmdButton01.Visible = Not lbShow
        cmdButton06.Visible = Not lbShow
        cmdButton10.Visible = Not lbShow
    End Sub


    'Handles GotFocus Events for txtField & txtFieldW
    Private Sub txtField_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim loTxt As TextBox
        loTxt = CType(sender, System.Windows.Forms.TextBox)

        poControl = loTxt

        loTxt.BackColor = Color.Azure
        loTxt.SelectAll()
    End Sub

    'Handles LostFocus Events for txtField & txtField
    Private Sub txtField_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim loTxt As TextBox
        loTxt = CType(sender, System.Windows.Forms.TextBox)
        loTxt.BackColor = SystemColors.Window

    End Sub

    'Handles Validating Events for txtField & txtField
    Private Sub txtField_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs)
        Dim loTxt As TextBox
        loTxt = CType(sender, System.Windows.Forms.TextBox)
        Dim loIndex As Integer
        Dim ldIndex As Integer
        loIndex = Val(Mid(loTxt.Name, 9))
        ldIndex = Val(Mid(loTxt.Name, 10))
        If Mid(loTxt.Name, 1, 8) = "txtField" And loTxt.ReadOnly = False Then
            Select Case loIndex
                Case 1
                    If Not IsDate(loTxt.Text) Then
                        p_oTrans.Master(loIndex) = p_oAppDriver.SysDate
                        loTxt.Text = Format(p_oTrans.Master(loIndex), "MMMM dd, yyyy")
                        p_oTrans.Master(loIndex) = loTxt.Text
                    Else
                        p_oTrans.Master(loIndex) = CDate(loTxt.Text)
                        loTxt.Text = Format(p_oTrans.Master(loIndex), "MMMM dd, yyyy")
                    End If
                Case Else
                    p_oTrans.Master(loIndex) = IFNull(loTxt.Text, "")
            End Select
        ElseIf LCase(Mid(loTxt.Name, 1, 9)) = "txtDetail" And loTxt.ReadOnly = False Then
            Select Case ldIndex
                Case 1
                    If Not IsDate(loTxt.Text) Then
                        loTxt.Text = Format(p_oAppDriver.getSysDate, "MMMM dd, yyyy")
                        p_oTrans.Detail(pnRow, ldIndex) = loTxt.Text
                    Else
                        loTxt.Text = Format(CDate(loTxt.Text), "MMMM dd, yyyy")
                        p_oTrans.Detail(pnRow, ldIndex) = loTxt.Text
                    End If
                Case 5, 6, 7, 8
                    If Not IsNumeric(loTxt.Text) Then
                        loTxt.Text = "0.00"
                        p_oTrans.Detail(pnRow, ldIndex) = loTxt.Text
                    Else
                        loTxt.Text = Format(CDbl(loTxt.Text), xsDECIMAL)
                        p_oTrans.Detail(pnRow, ldIndex) = loTxt.Text
                    End If
                    p_oTrans.Detail(pnRow, ldIndex) = loTxt.Text
                Case 13
                    If Not IsNumeric(loTxt.Text) Then
                        loTxt.Text = "0.00"
                        p_oTrans.Detail(pnRow, ldIndex) = loTxt.Text
                    Else
                        loTxt.Text = Format(CDbl(loTxt.Text), xsDECIMAL)
                        p_oTrans.Detail(pnRow, ldIndex) = loTxt.Text
                    End If
                    txtDetail14.Text = Format(p_oTrans.Detail(pnRow, "nAmountxx"), xsDECIMAL)
                    p_oTrans.Detail(pnRow, "nApproved") = txtDetail14.Text
                Case 10, 11, 12
                    p_oTrans.Detail(pnRow, ldIndex) = loTxt.Text
                Case Else
                    p_oTrans.Detail(pnRow, ldIndex) = IFNull(loTxt.Text, "")
            End Select
            loadDetail()
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
            Case 1 ' New
                If p_oTrans.NewTransaction Then
                    pnRow = 0
                    selRow = 0
                    Call newRecord()
                    Call loadDetail()
                    p_nEditMode = xeEditMode.MODE_ADDNEW
                    txtField02.Focus()
                End If
            Case 2 ' Search
                pnRow = Me.dgvDetail.CurrentRow.Index
                Dim loIndex As Integer
                loIndex = Val(Mid(poControl.Name, 9))
                Dim ldIndex As Integer
                ldIndex = Val(Mid(poControl.Name, 10))

                If InStr(poControl.Name, "txtField") > 0 Then
                    Select Case loIndex
                        Case 2
                            Call p_oTrans.SearchMaster(1, poControl.Text)
                        Case 3
                            Call p_oTrans.SearchMaster(2, poControl.Text)
                        Case 8
                            Call p_oTrans.SearchMaster(8, poControl.Text)
                    End Select
                Else
                    Select Case ldIndex
                        Case 2
                            Call p_oTrans.SearchDetail(pnRow, ldIndex, poControl.Text)
                            Call loadDetail()
                            Me.dgvDetail.Rows(Me.dgvDetail.RowCount - 1).Selected = True
                    End Select
                End If
            Case 3 ' Cancel Update
                If MsgBox("Do you want to discard all changes?", MsgBoxStyle.Information + MsgBoxStyle.OkCancel, "CarTrade Billing") = MsgBoxResult.Ok Then
                    selRow = 0
                    pnRow = 0
                    p_oTrans.NewTransaction()
                    Call newRecord()
                    Call loadMaster()
                    Call loadDetail()
                    p_nEditMode = xeEditMode.MODE_READY
                    initButton()
                End If
            Case 4 'Save confirmation
                If DataComplete() Then
                    If Trim(IFNull(p_oTrans.Detail(p_oTrans.ItemCount - 1, "sAcctNmbr"))) = "" Then
                        MessageBox.Show("Unable to save transaction" + Environment.NewLine + "Empty Data detected", "No Data",
                                            MessageBoxButtons.OK, MessageBoxIcon.Error)
                        txtDetail02.Focus()
                        Exit Sub
                    End If
                    If p_oTrans.SaveTransaction Then
                        MsgBox("Transaction was save successfully!", MsgBoxStyle.Information, "CarTrade Entry")
                        If MsgBox("Do you want to print the transaction", MsgBoxStyle.Information + MsgBoxStyle.OkCancel, "CT Billing Printing") = MsgBoxResult.Ok Then
                            Dim loRpt As clsCTPrint
                            loRpt = New clsCTPrint(p_oAppDriver)
                            loRpt.Transaction = p_oTrans.Master("sTransNox")
                            loRpt.TranDate = p_oTrans.Master("dTransact")
                            Call loRpt.ReportTrans()
                            pb_ChkdOk = True
                        Else
                            pb_ChkdOk = False
                        End If
                        If MsgBox("Do you want to CLOSE the transaction", MsgBoxStyle.Information + MsgBoxStyle.OkCancel, "CarTrade Confirmation") = MsgBoxResult.Ok Then
                            If Not p_oTrans.CloseTransaction Then
                                MsgBox("Unable to CLOSE the transaction!", MsgBoxStyle.Information, "CarTrade Entry")
                            End If
                            MsgBox("Transaction was CLOSED successfully!", MsgBoxStyle.Information, "CarTrade Entry")
                            selRow = 0
                            pnRow = 0
                            Call p_oTrans.NewTransaction()
                            Call loadMaster()
                            Call loadDetail()
                            p_nEditMode = xeEditMode.MODE_UPDATE
                            initButton()
                        End If
                    Else
                        MsgBox("Please check your Entry!", MsgBoxStyle.Information, "CarTrade Entry")
                    End If
                    p_oTrans.OpenTransaction((p_oTrans.Master("sTransNox")))
                    p_nEditMode = xeEditMode.MODE_READY
                    initButton()

                End If

            Case 5 'Browse
                If p_oTrans.SearchTransaction("", False) = True Then
                    selRow = 0
                    pnRow = 0
                    loadMaster()
                    loadDetail()
                    p_nEditMode = xeEditMode.MODE_READY
                    initButton()
                Else
                    selRow = 0
                    pnRow = 0
                    p_oTrans.NewTransaction()
                    Call newRecord()
                    Call loadDetail()
                End If
            Case 6 'Void
                If Trim(IFNull(p_oTrans.Detail(p_oTrans.ItemCount - 1, "sAcctNmbr"))) = "" Then
                    MessageBox.Show("Please select a record to cancel", "No Data",
                                MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End If
                If txtField09.Text = "OPEN" Then
                    If MsgBox("Do you want to cancel the transaction?", MsgBoxStyle.Information + MsgBoxStyle.OkCancel, "CarTrade Entry") = MsgBoxResult.Ok Then
                        If p_oTrans.CancelTransaction() Then
                            MsgBox("Transaction was cancelled successfully!", MsgBoxStyle.Information, "CarTrade Eentry")
                            selRow = 0
                            pnRow = 0
                            Call p_oTrans.NewTransaction()
                            Call newRecord()
                            loadMaster()
                            loadDetail()
                            p_nEditMode = xeEditMode.MODE_ADDNEW
                            initButton()
                        End If
                    End If
                Else
                    MessageBox.Show("Unable to cancel transaction" + Environment.NewLine + "Already " + txtField09.Text + "", "Error!",
                                        MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
            Case 7 ' update
                If Trim(IFNull(p_oTrans.Detail(p_oTrans.ItemCount - 1, "sAcctNmbr"))) = "" Then
                    MessageBox.Show("Please select a record to update", "No Data",
                                MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End If
                If txtField09.Text = "OPEN" Then
                    If p_oTrans.UpdateTransaction() Then
                        p_nEditMode = xeEditMode.MODE_UPDATE
                        initButton()
                        txtField02.Focus()
                        selRow = 0
                        pnRow = 0
                    Else
                        MessageBox.Show("Unable to update data", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End If
                Else
                    MessageBox.Show("Unable to update transaction" + Environment.NewLine + "Already " + txtField09.Text + "", "Error!",
                                       MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If

            Case 8 'delete detail
                If dgvDetail.RowCount - 1 > 0 Then
                    If MsgBox("Do you really want to delete this data?", MsgBoxStyle.Information + MsgBoxStyle.OkCancel, "CarTrade Entry") = MsgBoxResult.Ok Then
                        p_oTrans.DeleteDetail(pnRow)
                        dgvDetail.CurrentCell = dgvDetail(0, 0)
                        dgvDetail_Click(sender, New System.EventArgs())
                        loadMaster()
                        loadDetail()
                    End If
                Else
                    MessageBox.Show("Cannot delete last data" + Environment.NewLine + "Please use void button to cancel " + Environment.NewLine + "this transaction if save!", "Invalid transaction",
                               MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
            Case 9 ' add detail
                If detailComplete() Then
                    If p_oTrans.AddDetail() = True Then
                        p_oTrans.Detail(p_oTrans.ItemCount - 1, "dTransact") = Format(p_oAppDriver.getSysDate, xsDATE_MEDIUM)
                        Call loadMaster()
                        Call loadDetail()
                        dgvDetail.CurrentCell = dgvDetail(0, Me.dgvDetail.RowCount - 1)
                        dgvDetail_Click(sender, New System.EventArgs())
                    Else
                        MessageBox.Show("Cannot add new Row" + Environment.NewLine + "Please populate empty row", "No Data",
                                      MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End If
                End If
            Case 10 ' print
                If Trim(IFNull(p_oTrans.Detail(p_oTrans.ItemCount - 1, "sAcctNmbr"))) = "" Then
                    MessageBox.Show("Please select a record to print", "No Data",
                                MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End If
                 If MsgBox("Do you want to print the transaction", MsgBoxStyle.Information + MsgBoxStyle.OkCancel, "CT Billing Printing") = MsgBoxResult.Ok Then
                    Dim loRpt As clsCTPrint
                    loRpt = New clsCTPrint(p_oAppDriver)
                    loRpt.Transaction = p_oTrans.Master("sTransNox")
                    loRpt.TranDate = p_oTrans.Master("dTransact")
                    Call loRpt.ReportTrans()
                    pb_ChkdOk = True
                Else
                    pb_ChkdOk = False
                End If

        End Select
    End Sub

    Private Sub p_oTrans_DetailRetrieved(ByVal Row As Integer, ByVal Index As Integer, ByVal Value As Object) Handles p_oTrans.DetailRetrieved
        Select Case Index
            Case 1
                txtDetail01.Text = Format(Value, xsDATE_MEDIUM)
            Case 2
                txtDetail02.Text = Value
            Case 3
                txtDetail03.Text = Value
            Case 4
                txtDetail04.Text = Value
            Case 5
                txtDetail05.Text = Format(CDbl(Value), xsDECIMAL)
            Case 6
                txtDetail06.Text = Format(CDbl(Value), xsDECIMAL)
            Case 7
                txtDetail07.Text = Format(CDbl(Value), xsDECIMAL)
            Case 8
                txtDetail08.Text = Format(CDbl(Value), xsDECIMAL)
        End Select
    End Sub

    Private Sub loadDetail()
        Dim lnCtr As Integer
        dgvDetail.Rows.Clear()
        For lnCtr = 0 To p_oTrans.ItemCount - 1
            dgvDetail.Rows.Add()
            dgvDetail.Rows(lnCtr).Cells(0).Value = IFNull(p_oTrans.Detail(lnCtr, "sAcctNmbr"), "")
            dgvDetail.Rows(lnCtr).Cells(1).Value = IFNull(p_oTrans.Detail(lnCtr, "sClientNm"), "")
            dgvDetail.Rows(lnCtr).Cells(2).Value = CInt(p_oTrans.Detail(lnCtr, "cBillType").ToString)
            dgvDetail.Rows(lnCtr).Cells(3).Value = Format(p_oTrans.Detail(lnCtr, "nPrincipl"), xsDECIMAL)
            dgvDetail.Rows(lnCtr).Cells(4).Value = Format(p_oTrans.Detail(lnCtr, "nInterest"), xsDECIMAL)
            dgvDetail.Rows(lnCtr).Cells(5).Value = Format(p_oTrans.Detail(lnCtr, "nSubsidze"), xsDECIMAL)
            dgvDetail.Rows(lnCtr).Cells(6).Value = Format(p_oTrans.Detail(lnCtr, "nInctvAmt"), xsDECIMAL)
            dgvDetail.Rows(lnCtr).Cells(7).Value = Format(p_oTrans.Detail(lnCtr, "nAmountxx"), xsDECIMAL)

            If p_oTrans.Detail(lnCtr, "cBillType") = "0" Then
                dgvDetail.Rows(lnCtr).Cells(2).Value = "Principal/Finance"
            ElseIf p_oTrans.Detail(lnCtr, "cBillType") = "1" Then
                dgvDetail.Rows(lnCtr).Cells(2).Value = "Insurance Amount"
            ElseIf p_oTrans.Detail(lnCtr, "cBillType") = "2" Then
                dgvDetail.Rows(lnCtr).Cells(2).Value = "Dealer's Incentive"
            ElseIf p_oTrans.Detail(lnCtr, "cBillType") = "3" Then
                dgvDetail.Rows(lnCtr).Cells(2).Value = "Subsidized Interest"
            ElseIf p_oTrans.Detail(lnCtr, "cBillType") = "4" Then
                dgvDetail.Rows(lnCtr).Cells(2).Value = "Adjustment"
            End If
        Next

        dgvDetail.CurrentCell = dgvDetail.Rows(selRow).Cells(selCol)

        Dim total As Decimal
        For i As Integer = 0 To dgvDetail.RowCount - 1
            total += dgvDetail.Rows(i).Cells(7).Value
        Next

        p_oTrans.Master(4) = Format(total, xsDECIMAL)
    End Sub

    Private Function DataComplete() As Boolean
        If txtField02.Text = "" Or p_oTrans.Master("sCompnyID") = "" Then
            MessageBox.Show("Please select entry in Company Name", "No entry",
                                MessageBoxButtons.OK, MessageBoxIcon.Error)
            With txtField02
                .Focus()
                .SelectAll()
            End With
            Return False
        ElseIf txtField03.Text = "" Or p_oTrans.Master("sBranchCD") = "" Then
            MessageBox.Show("Please select entry in Branch", "No entry",
                                MessageBoxButtons.OK, MessageBoxIcon.Error)
            With txtField03
                .Focus()
                .SelectAll()
            End With
            Return False
        ElseIf cmbBillType.Text = "" Or p_oTrans.Detail(pnRow, "cBillType") = "" Then
            MessageBox.Show("Please select Bill Type", "No entry",
                                MessageBoxButtons.OK, MessageBoxIcon.Error)
            With cmbBillType
                .Focus()
            End With
            Return False
        ElseIf txtField08.Text = "" Or p_oTrans.Master("sClientID") = "" Then
            MessageBox.Show("Please select entry in Billing Name", "No entry",
                                MessageBoxButtons.OK, MessageBoxIcon.Error)
            With txtField08
                .Focus()
            End With
            Return False
        End If
        Return True
    End Function

    Private Function detailComplete() As Boolean
        If txtDetail02.Text = "" Then
            MessageBox.Show("Please have entry in Account Number", "No entry",
                                MessageBoxButtons.OK, MessageBoxIcon.Error)
            With txtDetail02
                .Focus()
                .SelectAll()
            End With
            Return False
        ElseIf txtDetail03.Text = "" Then
            MessageBox.Show("Please have entry in client name", "No entry",
                                MessageBoxButtons.OK, MessageBoxIcon.Error)
            With txtDetail03
                .Focus()
                .SelectAll()
            End With
            Return False
        ElseIf txtDetail04.Text = "" Then
            MessageBox.Show("Please have entry in Engine No", "No entry",
                                MessageBoxButtons.OK, MessageBoxIcon.Error)
            With txtDetail04
                .Focus()
                .SelectAll()
            End With
            Return False
        End If
        Return True
    End Function

    Private Sub p_oTrans_MasterRetrieved(ByVal Index As Integer, ByVal Value As Object) Handles p_oTrans.MasterRetrieved
        Dim loTxt As TextBox
        'ind TextBox with specified name
        loTxt = CType(FindTextBox(Me, "txtField" & Format(Index, "00")), TextBox)
        Select Case Index
            Case 1
                loTxt.Text = Format(Value, IsDate("MMMM dd, yyyy"))
            Case 4, 5
                loTxt.Text = Format(Value, xsDECIMAL)
            Case Else
                loTxt.Text = IFNull(Value, "")
        End Select
    End Sub

    Private Sub dgvDetail_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgvDetail.Click
        pnRow = Me.dgvDetail.CurrentRow.Index
        selRow = dgvDetail.CurrentCell.RowIndex
        selCol = 0
        txtDetail01.Text = Format(p_oTrans.Detail(pnRow, "dTransact"), "MMMM dd, yyyy")
        txtDetail02.Text = IFNull(p_oTrans.Detail(pnRow, "sAcctNmbr"), "")
        txtDetail03.Text = IFNull(p_oTrans.Detail(pnRow, "sClientNm"), "")
        txtDetail04.Text = IFNull(p_oTrans.Detail(pnRow, "sEngineNo"), "")
        txtDetail05.Text = Format(p_oTrans.Detail(pnRow, "nPrincipl"), xsDECIMAL)
        txtDetail06.Text = Format(p_oTrans.Detail(pnRow, "nInterest"), xsDECIMAL)
        txtDetail07.Text = Format(p_oTrans.Detail(pnRow, "nSubsidze"), xsDECIMAL)
        txtDetail08.Text = Format(p_oTrans.Detail(pnRow, "nInctvAmt"), xsDECIMAL)
        cmbBillType.SelectedIndex = CInt(p_oTrans.Detail(pnRow, "cBillType").ToString)
        txtDetail10.Text = IFNull(p_oTrans.Detail(pnRow, "sDescript"), "")
        txtDetail11.Text = IFNull(p_oTrans.Detail(pnRow, "sRemarks1"), "")
        txtDetail13.Text = Format(p_oTrans.Detail(pnRow, "nAmountxx"), xsDECIMAL)
    End Sub

    Private Sub txtField_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        pnRow = Me.dgvDetail.CurrentRow.Index
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
            Dim ldIndex As Integer
            ldIndex = Val(Mid(loTxt.Name, 10))

            If InStr(loTxt.Name, "txtField") > 0 Then
                Select Case loIndex
                    Case 2
                        If e.KeyCode = Keys.F3 Or e.KeyCode = Keys.Enter Then
                            If p_oTrans.SearchMaster(1, loTxt.Text) = True Then
                            End If
                        End If

                    Case 3
                        If e.KeyCode = Keys.F3 Or e.KeyCode = Keys.Enter Then
                            If p_oTrans.SearchMaster(2, loTxt.Text) = True Then
                            End If
                        End If

                    Case 8
                        If e.KeyCode = Keys.F3 Or e.KeyCode = Keys.Enter Then
                            If p_oTrans.SearchMaster(8, loTxt.Text) = True Then
                            End If
                        End If

                End Select
            Else
                Select Case ldIndex
                    Case 2
                        If e.KeyCode = Keys.F3 Or e.KeyCode = Keys.Enter Then
                            Call p_oTrans.SearchDetail(pnRow, ldIndex, loTxt.Text)
                            Call loadDetail()
                            Me.dgvDetail.Rows(Me.dgvDetail.RowCount - 1).Selected = True
                        End If
                    Case 3
                        If e.KeyCode = Keys.F3 Or e.KeyCode = Keys.Enter Then
                            Call p_oTrans.SearchDetail(pnRow, ldIndex, loTxt.Text)
                            Call loadDetail()
                            Me.dgvDetail.Rows(Me.dgvDetail.RowCount - 1).Selected = True
                        End If
                End Select
            End If
            '###########################
            If TypeOf poControl Is TextBox Then
                SelectNextControl(loTxt, True, True, True, True)
            End If
        End If
    End Sub

    Private Sub cmbBillType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbBillType.SelectedIndexChanged
        p_oTrans.Detail(pnRow, "cBillType") = cmbBillType.SelectedIndex.ToString

        If (p_oTrans.Detail(pnRow, "cBillType") = "0") Then
            txtDetail13.Text = Format(p_oTrans.Detail(pnRow, "nPrincipl"), xsDECIMAL)
            txtDetail13.Enabled = False
        ElseIf (p_oTrans.Detail(pnRow, "cBillType") = "1") Then
            txtDetail13.Text = Format(p_oTrans.Detail(pnRow, "nInterest"), xsDECIMAL)
            txtDetail13.Enabled = False
        ElseIf (p_oTrans.Detail(pnRow, "cBillType") = "2") Then
            txtDetail13.Text = Format(p_oTrans.Detail(pnRow, "nInctvAmt"), xsDECIMAL)
            txtDetail13.Enabled = False
        ElseIf (p_oTrans.Detail(pnRow, "cBillType") = "3") Then
            txtDetail13.Text = Format(p_oTrans.Detail(pnRow, "nSubsidze"), xsDECIMAL)
            txtDetail13.Enabled = False
        ElseIf (p_oTrans.Detail(pnRow, "cBillType") = "4") Then
            txtDetail13.Text = Format(p_oTrans.Detail(pnRow, "nAmountxx"), xsDECIMAL)
            txtDetail13.Enabled = True
        End If
        p_oTrans.Detail(pnRow, 13) = CDbl(txtDetail13.Text)
        txtDetail14.Text = txtDetail13.Text
        p_oTrans.Detail(pnRow, 14) = CDbl(txtDetail14.Text)
        loadDetail()
    End Sub


End Class