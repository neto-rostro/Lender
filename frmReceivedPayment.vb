Imports MySql.Data.MySqlClient
Imports ggcAppDriver
Imports ggcLRTransaction
Imports System.Globalization
Public Class frmReceivedPayment
    Private WithEvents p_oTrans As ggcLRTransaction.CTBilling
    Private pnLoadx As Integer
    Private p_nEditMode As Integer
    Private poControl As Control
    Private pnRow As Integer
    Dim selRow As Integer
    Dim selCol As Integer
    Private p_TranStatus As String

    Public Property TranStatus() As String
        Get
            Return p_TranStatus
        End Get
        Set(ByVal value As String)
            p_TranStatus = value
        End Set
    End Property

    Private Sub frmCTBillingMaster_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        Debug.Print("frmCTPosting_Activated")
        If p_TranStatus = "2" Then
            'Me.Text = "Car Trade Billing Master"
        End If
        If pnLoadx = 1 Then
            p_oTrans.TranStatus = p_TranStatus
            p_oTrans.NewTransaction()
            Call newRecord()
            Call loadDetail()
            pnLoadx = 2
        End If
    End Sub

    Private Function newRecord() As Boolean
        Call loadMaster()
        cmbBillType.Enabled = False
        p_nEditMode = xeEditMode.MODE_UPDATE
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

    Private Sub frmCTPosting_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Debug.Print("frmCTPosting_Load")
        If pnLoadx = 0 Then
            p_oTrans = New ggcLRTransaction.CTBilling(p_oAppDriver)

            'Set event Handlers for txtField
            Call grpEventHandler(Me, GetType(TextBox), "txtField", "GotFocus", AddressOf txtField_GotFocus)
            Call grpEventHandler(Me, GetType(TextBox), "txtDetail", "GotFocus", AddressOf txtField_GotFocus)
            Call grpEventHandler(Me, GetType(TextBox), "txtField", "LostFocus", AddressOf txtField_LostFocus)
            Call grpEventHandler(Me, GetType(TextBox), "txtDetail", "LostFocus", AddressOf txtField_LostFocus)
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
                        Case 4, 5
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
                        Case 5, 6, 11, 12
                            If IsNumeric(p_oTrans.Detail(0, ldIndex)) Then
                                loTxt.Text = Format(p_oTrans.Detail(0, ldIndex), xsDECIMAL)
                            Else
                                loTxt.Text = "0.00"
                            End If
                        Case Else
                            loTxt.Text = IFNull(p_oTrans.Detail(0, ldIndex), "")
                    End Select
                    cmbBillType.SelectedIndex = CInt(p_oTrans.Detail(0, "cBillType").ToString)
                End If
            End If
        Next

    End Sub

    Private Sub cmdButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim loChk As Button
        loChk = CType(sender, System.Windows.Forms.Button)

        Dim lnIndex As Integer
        lnIndex = Val(Mid(loChk.Name, 10))

        Select Case lnIndex
            Case 0 ' Exit
                Me.Dispose()
            Case 1 ' Received
                If p_oTrans.Master("sCompnyID") = "" And p_oTrans.Master("sBranchCD") = "" Then
                    MessageBox.Show("Please select a record to received", "No Data",
                                MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End If
                If MsgBox("Do you want to RECEIVED the transaction", MsgBoxStyle.Information + MsgBoxStyle.OkCancel, "NorthPoint Confirmation") = MsgBoxResult.Ok Then
                    If Not p_oTrans.ReceivedPayment() Then
                        MsgBox("Unable to RECEIVED the transaction!", MsgBoxStyle.Information, "NorthPoint Entry")
                    End If
                    selRow = 0
                    pnRow = 0
                    MsgBox("Transaction was RECEIVED successfully!", MsgBoxStyle.Information, "NorthPoint Entry")
                    Call p_oTrans.NewTransaction()
                    p_nEditMode = xeEditMode.MODE_UPDATE
                    loadMaster()
                    loadDetail()
                End If
            Case 5 ' Browse
                If p_oTrans.SearchTransaction("", False) Then
                    loadMaster()
                    loadDetail()
                    selRow = 0
                    pnRow = 0
                    p_nEditMode = xeEditMode.MODE_READY
                Else
                    selRow = 0
                    pnRow = 0
                    p_oTrans.NewTransaction()
                    Call newRecord()
                    Call loadDetail()
                End If
        End Select
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
    Private Sub loadDetail()
        Dim lnCtr As Integer
        dgvDetail.Rows.Clear()
        For lnCtr = 0 To p_oTrans.ItemCount - 1
            dgvDetail.Rows.Add()
            dgvDetail.Rows(lnCtr).Cells(0).Value = Format(p_oTrans.Detail(lnCtr, "dTransact"), xsDATE_MEDIUM)
            dgvDetail.Rows(lnCtr).Cells(1).Value = IFNull(p_oTrans.Detail(lnCtr, "sAcctNmbr"), "")
            dgvDetail.Rows(lnCtr).Cells(2).Value = IFNull(p_oTrans.Detail(lnCtr, "sClientNm"), "")
            dgvDetail.Rows(lnCtr).Cells(3).Value = IFNull(p_oTrans.Detail(lnCtr, "sEngineNo"), "")
            dgvDetail.Rows(lnCtr).Cells(4).Value = Format(p_oTrans.Detail(lnCtr, "nPrincipl"), xsDECIMAL)
            dgvDetail.Rows(lnCtr).Cells(5).Value = Format(p_oTrans.Detail(lnCtr, "nInterest"), xsDECIMAL)
            dgvDetail.Rows(lnCtr).Cells(6).Value = IFNull(p_oTrans.Detail(lnCtr, "cBillType"), "0")
            dgvDetail.Rows(lnCtr).Cells(7).Value = p_oTrans.Detail(lnCtr, "sDescript")
            dgvDetail.Rows(lnCtr).Cells(8).Value = p_oTrans.Detail(lnCtr, "sRemarks1")
            dgvDetail.Rows(lnCtr).Cells(9).Value = p_oTrans.Detail(lnCtr, "sRemarks2")
            dgvDetail.Rows(lnCtr).Cells(10).Value = Format(p_oTrans.Detail(lnCtr, "nAmountxx"), xsDECIMAL)
            If p_oTrans.Detail(lnCtr, "cBillType") = "0" Then
                dgvDetail.Rows(lnCtr).Cells(6).Value = "Principal/Finance"
            ElseIf p_oTrans.Detail(lnCtr, "cBillType") = "1" Then
                dgvDetail.Rows(lnCtr).Cells(6).Value = "Insurance Amount"
            ElseIf p_oTrans.Detail(lnCtr, "cBillType") = "2" Then
                dgvDetail.Rows(lnCtr).Cells(6).Value = "Dealer's Incentive"
            ElseIf p_oTrans.Detail(lnCtr, "cBillType") = "3" Then
                dgvDetail.Rows(lnCtr).Cells(6).Value = "Subsidized Interest"
            ElseIf p_oTrans.Detail(lnCtr, "cBillType") = "4" Then
                dgvDetail.Rows(lnCtr).Cells(6).Value = "Adjustment"
            End If
        Next
        dgvDetail.CurrentCell = dgvDetail.Rows(selRow).Cells(selCol)
    End Sub

    Private Function DataComplete() As Boolean
        If txtField02.Text = "" Then
            MessageBox.Show("Please select entry in Company Name", "No entry",
                                MessageBoxButtons.OK, MessageBoxIcon.Error)
            With txtField02
                .Focus()
                .SelectAll()
            End With
            Return False
        ElseIf txtField03.Text = "" Then
            MessageBox.Show("Please select entry in Branch", "No entry",
                                MessageBoxButtons.OK, MessageBoxIcon.Error)
            With txtField03
                .Focus()
                .SelectAll()
            End With
            Return False
        ElseIf cmbBillType.Text = "" Then
            MessageBox.Show("Please select Bill Type", "No entry",
                                MessageBoxButtons.OK, MessageBoxIcon.Error)
            With cmbBillType
                .Focus()
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
        cmbBillType.SelectedIndex = p_oTrans.Detail(pnRow, "cBillType").ToString
        txtDetail08.Text = IFNull(p_oTrans.Detail(pnRow, "sDescript"), "")
        txtDetail09.Text = IFNull(p_oTrans.Detail(pnRow, "sRemarks1"), "")
        txtDetail10.Text = IFNull(p_oTrans.Detail(pnRow, "sRemarks2"), "")
        txtDetail11.Text = Format(p_oTrans.Detail(pnRow, "nAmountxx"), xsDECIMAL)
    End Sub

    Private Sub cmbBillType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbBillType.SelectedIndexChanged
        p_oTrans.Detail(pnRow, "cBillType") = cmbBillType.SelectedIndex.ToString
        loadDetail()
    End Sub
End Class