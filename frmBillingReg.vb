Imports MySql.Data.MySqlClient
Imports ggcAppDriver
Imports ggcLRTransaction
Imports System.Globalization

Public Class frmBillingReg
    Private WithEvents p_oTrans As ggcLRTransaction.CTBilling
    Private pnLoadx As Integer
    Private poControl As Control
    Private pnRow As Integer
    Dim selRow As Integer
    Dim selCol As Integer
    Private p_nEditMode As Integer
    Private p_TranStatus As String

    Public Property TranStatus() As String
        Get
            Return p_TranStatus
        End Get
        Set(ByVal value As String)
            p_TranStatus = value
        End Set
    End Property

    Private Sub frmBilling_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        Debug.Print("frmBilling_Activated")

        If p_TranStatus = "10234" Then
            Me.Text = "Billing Master History"
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
        p_nEditMode = xeEditMode.MODE_UPDATE
        cmbBillType.Enabled = False
        txtSeeks00.Focus()

        Return True
    End Function

    Public Function setTextSeeks() As Boolean
        txtSeeks00.Text = p_oTrans.Master("sTransNox")
        txtSeeks01.Text = txtField02.Text
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

    Private Sub clearText()
        txtSeeks00.Text = ""
        txtSeeks01.Text = ""
    End Sub

    Private Sub frmBilling_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Debug.Print("frmBilling_Load")
        If pnLoadx = 0 Then
            p_oTrans = New ggcLRTransaction.CTBilling(p_oAppDriver)

            'Set event Handlers for txtField
            Call grpEventHandler(Me, GetType(TextBox), "txtField", "GotFocus", AddressOf txtField_GotFocus)
            Call grpEventHandler(Me, GetType(TextBox), "txtSeeks", "GotFocus", AddressOf txtSeeks_GotFocus)
            Call grpEventHandler(Me, GetType(TextBox), "txtDetail", "GotFocus", AddressOf txtField_GotFocus)
            Call grpEventHandler(Me, GetType(TextBox), "txtField", "LostFocus", AddressOf txtField_LostFocus)
            Call grpEventHandler(Me, GetType(TextBox), "txtDetail", "LostFocus", AddressOf txtField_LostFocus)
            Call grpEventHandler(Me, GetType(TextBox), "txtSeeks", "LostFocus", AddressOf txtSeeks_LostFocus)
            Call grpCancelHandler(Me, GetType(TextBox), "txtField", "Validating", AddressOf txtField_Validating)
            Call grpCancelHandler(Me, GetType(TextBox), "txtDetail", "Validating", AddressOf txtField_Validating)
            Call grpKeyHandler(Me, GetType(TextBox), "txtField", "KeyDown", AddressOf txtField_KeyDown)
            Call grpKeyHandler(Me, GetType(TextBox), "txtDetail", "KeyDown", AddressOf txtField_KeyDown)
            Call grpKeyHandler(Me, GetType(TextBox), "txtField", "KeyDown", AddressOf ArrowKeys_Keydown)
            Call grpKeyHandler(Me, GetType(TextBox), "txtDetail", "KeyDown", AddressOf ArrowKeys_Keydown)
            Call grpKeyHandler(Me, GetType(TextBox), "txtSeeks", "KeyDown", AddressOf txtField_KeyDown)
            Call grpEventHandler(Me, GetType(Button), "cmdButton", "Click", AddressOf cmdButton_Click)
            pnLoadx = 1
        End If

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

            If Mid(loTxt.Name, 1, 8) = "txtSeeks" Then
                Select Case loIndex
                    Case 0, 1
                        If p_oTrans.SearchTransaction(poControl.Text, IIf(loIndex = 0, True, False)) = True Then
                            selRow = 0
                            pnRow = 0
                            loadMaster()
                            loadDetail()
                            setTextSeeks()
                        Else
                            selRow = 0
                            pnRow = 0
                            p_oTrans.NewTransaction()
                            Call newRecord()
                            Call loadMaster()
                            Call loadDetail()
                            clearText()
                            p_nEditMode = xeEditMode.MODE_READY
                        End If
                End Select
            End If
            '*********************
            If TypeOf poControl Is TextBox Then
                SelectNextControl(loTxt, True, True, True, True)
            End If
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
                                loTxt.Text = Format(CDbl(p_oTrans.Detail(0, ldIndex)), xsDECIMAL)
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
                Case 4, 5
                    If Not IsNumeric(loTxt.Text) Then
                        p_oTrans.Master(loIndex) = "0.00"
                        loTxt.Text = Format(p_oTrans.Master(loIndex), xsDECIMAL)
                        p_oTrans.Master(loIndex) = loTxt.Text
                    Else
                        p_oTrans.Master(loIndex) = CDec(loTxt.Text)
                        loTxt.Text = Format(p_oTrans.Master(loIndex), xsDECIMAL)
                    End If
                Case Else
                    p_oTrans.Master(loIndex) = loTxt.Text
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
                Case 5, 6, 7, 8, 13, 14
                    If Not IsNumeric(loTxt.Text) Then
                        loTxt.Text = "0.00"
                        p_oTrans.Detail(pnRow, ldIndex) = loTxt.Text
                    Else
                        loTxt.Text = Format(CDbl(loTxt.Text), xsDECIMAL)
                        p_oTrans.Detail(pnRow, ldIndex) = loTxt.Text
                    End If
                    p_oTrans.Detail(pnRow, ldIndex) = loTxt.Text
                Case 9, 10, 11
                    p_oTrans.Detail(pnRow, ldIndex) = loTxt.Text
                Case Else
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
            Case 3 'Browse
                    If p_oTrans.SearchTransaction("", False) = True Then
                        selRow = 0
                        pnRow = 0
                        loadMaster()
                        loadDetail()
                        setTextSeeks()
                        p_nEditMode = xeEditMode.MODE_READY
                    Else
                        selRow = 0
                        pnRow = 0
                        p_oTrans.NewTransaction()
                        Call newRecord()
                        Call loadMaster()
                        Call loadDetail()
                        clearText()
                        p_nEditMode = xeEditMode.MODE_READY
                    End If
        End Select
    End Sub

    Private Sub p_oTrans_DetailRetrieved(ByVal Row As Integer, ByVal Index As Integer, ByVal Value As Object) Handles p_oTrans.DetailRetrieved
        Select Case Index
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
            Case 13
                txtDetail13.Text = Format(CDbl(Value), xsDECIMAL)
            Case 14
                txtDetail14.Text = Format(CDbl(Value), xsDECIMAL)
        End Select
    End Sub

    Private Sub txtSeeks_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim loTxt As TextBox
        loTxt = CType(sender, System.Windows.Forms.TextBox)
        poControl = loTxt
        loTxt.BackColor = Color.Azure
        loTxt.SelectAll()
    End Sub

    Private Sub txtSeeks_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim loTxt As TextBox
        loTxt = CType(sender, System.Windows.Forms.TextBox)
        loTxt.BackColor = SystemColors.Window
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
            dgvDetail.Rows(lnCtr).Cells(8).Value = Format(p_oTrans.Detail(lnCtr, "nApproved"), xsDECIMAL)

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
            total += dgvDetail.Rows(i).Cells(8).Value
        Next
        txtField05.Text = total
        p_oTrans.Master(5) = Format(CDec(txtField05.Text), xsDECIMAL)
    End Sub

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
        txtDetail01.Text = Format(p_oTrans.Detail(pnRow, "dTransact"), xsDATE_MEDIUM)
        txtDetail02.Text = IFNull(p_oTrans.Detail(pnRow, "sAcctNmbr"), "")
        txtDetail03.Text = IFNull(p_oTrans.Detail(pnRow, "sClientNm"), "")
        txtDetail04.Text = IFNull(p_oTrans.Detail(pnRow, "sEngineNo"), "")
        txtDetail05.Text = Format(p_oTrans.Detail(pnRow, "nPrincipl"), xsDECIMAL)
        txtDetail06.Text = Format(p_oTrans.Detail(pnRow, "nInterest"), xsDECIMAL)
        txtDetail05.Text = Format(p_oTrans.Detail(pnRow, "nSubsidze"), xsDECIMAL)
        txtDetail06.Text = Format(p_oTrans.Detail(pnRow, "nInctvAmt"), xsDECIMAL)
        cmbBillType.SelectedIndex = CInt(p_oTrans.Detail(pnRow, "cBillType").ToString)
        txtDetail10.Text = IFNull(p_oTrans.Detail(pnRow, "sDescript"), "")
        txtDetail11.Text = IFNull(p_oTrans.Detail(pnRow, "sRemarks1"), "")
        txtDetail12.Text = IFNull(p_oTrans.Detail(pnRow, "sRemarks2"), "")
        txtDetail13.Text = Format(p_oTrans.Detail(pnRow, "nAmountxx"), xsDECIMAL)
        txtDetail14.Text = Format(p_oTrans.Detail(pnRow, "nApproved"), xsDECIMAL)
    End Sub

End Class